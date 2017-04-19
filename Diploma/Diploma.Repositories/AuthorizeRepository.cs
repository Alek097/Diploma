﻿using Diploma.Core.ConfigureModels;
using Diploma.Data;
using Diploma.Data.Models;
using Diploma.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diploma.Core.ViewModels;
using Diploma.Core.OAuthResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Diploma.Core;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Diploma.Repositories
{
    public class AuthorizeRepository : IAuthorizeRepository
    {
        private static bool isRoleCreated = false;

        private readonly IContext context;
        private readonly List<OAuth> oauth;
        private readonly App app;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        private const string url = "api/Authorize/SetCode";

        public AuthorizeRepository(
            IContext context,
            IOptions<List<OAuth>> oauth,
            IOptions<App> app,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            this.context = context;
            this.oauth = oauth.Value;
            this.app = app.Value;
            this.userManager = userManager;
            this.signInManager = signInManager;

            if (!(isRoleCreated))
            {
                this.CreateRoles(roleManager);
                isRoleCreated = true;
            }
        }

        private void CreateRoles(RoleManager<IdentityRole<Guid>> roleManager)
        {
            Task waiter = this.CreateRolesAsync(roleManager);

            waiter.Wait();
        }

        private async Task CreateRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
        {
            if (!(await roleManager.RoleExistsAsync("User")))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("User"));
            }

            if (!(await roleManager.RoleExistsAsync("Moderator")))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("Moderator"));
            }

            if (!(await roleManager.RoleExistsAsync("Administrator")))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("Administrator"));
            }
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public async Task<List<OAuthViewModel>> GetOAuthProviders()
        {
            return await Task.Run(() =>
            {
                return this.oauth.Select((oa) => new OAuthViewModel()
                {
                    Name = oa.Name,
                    Description = oa.Description,
                    LogoUrl = oa.LogoUrl
                })
                .ToList();
            });
        }

        public async Task<string> GetRedirectUrl(string provider)
        {
            OAuth OAuth = this.oauth.First((oa) => oa.Name.ToUpper() == provider.ToUpper());

            string state = Guid.NewGuid().ToString();

            string redirectUrl = $"{this.app.Domain}{url}";

            this.context.Create<OAuthState, Guid>(new OAuthState()
            {
                State = state,
                Provider = provider
            });

            await this.context.SaveChangesAsync();

            return $"{string.Format(OAuth.GetCodeUrl, OAuth.ClientId, redirectUrl)}&{OAuth.GetCodeParameters}&state={state}";
        }

        public async Task<string> SetAccessCode(string code, string state)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("Code is null or empty", "code");
            }
            else if (string.IsNullOrWhiteSpace(state))
            {
                throw new ArgumentException("State is null or empty", "state");
            }

            OAuthState stateEntity;

            try
            {
                stateEntity = this.context.OAuthStates.First((s) => s.State == state);
            }
            catch (InvalidOperationException ex)
            {
                throw new ArgumentException("Invalid state", "state", ex);
            }

            OAuth provider = this.oauth.FirstOrDefault((p) => p.Name.ToUpper() == stateEntity.Provider.ToUpper());

            this.context.Delete(stateEntity);

            await this.context.SaveChangesAsync();

            string redirectUrl = $"{this.app.Domain}{url}";

            HttpClient client = new HttpClient();

            Dictionary<string, string> parametrs = new Dictionary<string, string>()
            {
                { "client_id" , provider.ClientId },
                { "client_secret" , provider.ClientSecret },
                { "redirect_uri" , redirectUrl },
                { "code" , code }
            };

            if (provider.GetAccessTokenParameters != null)
            {
                foreach (AccessTokenParameter param in provider.GetAccessTokenParameters)
                {
                    parametrs[param.Name] = param.Value;
                }
            }

            string json = await client.PostAsync(provider.GetAccessTokenUrl,
                new FormUrlEncodedContent(parametrs))
                    .Result.Content.ReadAsStringAsync();

            Type parser = typeof(IOAuthResult).GetTypeInfo().Assembly.GetType(provider.Parser);

            IOAuthResult resultMaker = Activator.CreateInstance(parser, json) as IOAuthResult;

            OAuthResult oResult = await resultMaker.ToOAuthResultAsync();

            User user = this.context.Users.FirstOrDefault(u => u.Email == oResult.Email);

            if (user == null)
            {
                user = new User()
                {
                    Email = oResult.Email,
                    ActiveEmail = oResult.Email,
                    UserName = oResult.UserId,
                    CreateDate = DateTime.Now,
                    LastModifyDate = DateTime.Now
                };

                user.Tokens.Add(new Token()
                {
                    Provider = oResult.Provider,
                    AccessToken = oResult.AccessToken,
                    UserProviderId = oResult.UserId,
                    Code = code
                });

                await this.userManager.CreateAsync(user);

                await this.userManager.AddToRoleAsync(user, "User");
            }

            await this.signInManager.SignInAsync(user, false);

            return $"/";
        }

        public async Task<ControllerResult<UserViewModel>> GetUser(string name)
        {
            return await Task.Run<ControllerResult<UserViewModel>>(() =>
            {

                User user = this.context.Users
                    .Include(u => u.Addresses)
                    .Include(u => u.Orders)
                    .Include(u => u.Roles)
                    .FirstOrDefault((u) => u.UserName == name);

                if (user == null)
                {
                    return new ControllerResult<UserViewModel>()
                    {
                        IsSuccess = false,
                        Message = $"User not a found",
                        Status = 404,
                        Value = new UserViewModel() { IsAuthorize = false }
                    };
                }


                Guid roleId = user.Roles.FirstOrDefault().RoleId;

                IdentityRole<Guid> role = context.Roles.First(r => r.Id == roleId);

                UserViewModel result = new UserViewModel()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    ActiveEmail = user.ActiveEmail,
                    IsAuthorize = true,
                    IsBanned = user.IsBanned,
                    Role = role.Name,

                    Addresses = user.Addresses.Select(a => new AddressViewModel()
                    {
                        City = a.City,
                        Country = a.Country,
                        FirstName = a.FirstName,
                        FullName = a.FullName,
                        LastName = a.LastName,
                        MiddleName = a.MiddleName,
                        PhoneNumber = a.PhoneNumber,
                        PostCode = a.PostCode,
                        Region = a.Region
                    }).ToList(),

                    Orders = user.Orders.Select(order => new OrderViewModel()
                    {
                        TotalPrice = order.TotalPrice,

                        Products = order.Products.Select(product => new ProductViewModel()
                        {
                            Name = product.Name,
                            Description = product.Description,
                            PhotoPath = product.PhotoPath,
                            Price = product.Price,

                            Characteristics = product.Characteristics.Select(characteristics => new CharacteristicViewModel()
                            {
                                Name = characteristics.Name,
                                Value = characteristics.Value
                            }).ToList(),

                            CharacteristicsGroups = product.CharacteristicsGroups.Select(chGroup => new CharacteristicsGroupViewModel()
                            {
                                Name = chGroup.Name,

                                Characteristics = chGroup.Characteristics.Select(characteristics => new CharacteristicViewModel()
                                {
                                    Name = characteristics.Name,
                                    Value = characteristics.Value
                                }).ToList()

                            }).ToList()

                        }).ToList()

                    }).ToList()
                };

                return new ControllerResult<UserViewModel>()
                {
                    IsSuccess = true,
                    Status = 200,
                    Value = result
                };
            });
        }

        public async Task SignOut()
        {
            await this.signInManager.SignOutAsync();
        }
    }
}
