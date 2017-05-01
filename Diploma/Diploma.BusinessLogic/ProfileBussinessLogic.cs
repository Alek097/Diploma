using Diploma.BusinessLogic.Interfaces;
using System;
using Diploma.Core;
using System.Threading.Tasks;
using Diploma.Repositories.Interfaces;
using Diploma.Data.Models;
using MimeKit;
using System.Linq;
using Microsoft.Extensions.Logging;
using MailKit.Net.Smtp;
using MimeKit.Text;
using Microsoft.Extensions.Options;
using Diploma.Core.ConfigureModels;
using Diploma.Core.ViewModels;

namespace Diploma.BusinessLogic
{
    public class ProfileBussinessLogic : IProfileBussinessLogic
    {
        private readonly IUserRepository userRepository;
        private readonly IEditEmailConfirmMessageRepository editEmailConfirmMessageRepository;
        private readonly ILogger<ProfileBussinessLogic> logger;
        private readonly Email email;
        private readonly IAddressRepository addressRepository;

        public ProfileBussinessLogic(
            IUserRepository userRepository,
            IEditEmailConfirmMessageRepository editEmailConfirmMessageRepository,
            ILogger<ProfileBussinessLogic> logger,
            IOptions<Email> email,
            IAddressRepository addressRepository)
        {
            this.userRepository = userRepository;
            this.editEmailConfirmMessageRepository = editEmailConfirmMessageRepository;
            this.logger = logger;
            this.email = email.Value;
            this.addressRepository = addressRepository;
        }

        public async Task<ControllerResult<AddressViewModel>> AddAddress(AddressViewModel address, string userName)
        {
            User currentUser = this.userRepository.Get()
                .FirstOrDefault(u => u.UserName == userName);

            if (currentUser == null)
            {
                this.logger.LogError(new EventId(), $"User {userName} not a found");

                return new ControllerResult<AddressViewModel>()
                {
                    IsSuccess = false,
                    Message = "Пользователь не найден. Попробуйте обновить страницу.",
                    Status = 404,
                    Value = null
                };
            }
            else
            {
                Address newAddress = new Address()
                {
                    Id = Guid.Parse(address.Id),
                    City = address.City,
                    Country = address.Country,
                    FirstName = address.FirstName,
                    LastName = address.LastName,
                    LocalAddress = address.Address,
                    MiddleName = address.MiddleName,
                    PhoneNumber = address.PhoneNumber,
                    PostCode = address.PostCode,
                    Region = address.Region
                };

                currentUser.Addresses.Add(newAddress);

                this.userRepository.Modify(currentUser, currentUser.Id);

                await this.userRepository.SaveChangesAsync();

                return new ControllerResult<AddressViewModel>()
                {
                    IsSuccess = true,
                    Status = 200,
                    Value = address
                };
            }
        }

        public async Task<ControllerResult> DeleteAddress(string id, string name)
        {
            Guid guidId = Guid.Parse(id);

            User current = this.userRepository.Get()
                .FirstOrDefault(u => u.UserName == name);

            if (current == null)
            {
                return new ControllerResult()
                {
                    IsSuccess = false,
                    Status = 404,
                    Message = "Пользователь не найден. Попробуйте обновить страницу."
                };
            }
            else
            {
                Address deleteAddress = current.Addresses.FirstOrDefault(address => address.Id == guidId);

                if (deleteAddress != null)
                {
                    this.addressRepository.Delete(deleteAddress, current.Id);
                    await this.addressRepository.SaveChangesAsync();

                    return new ControllerResult()
                    {
                        IsSuccess = true,
                        Status = 200
                    };
                }
                else
                {
                    return new ControllerResult()
                    {
                        IsSuccess = true,
                        Message = "Адрес не найден. Попробуйте перезагрузить страницу.",
                        Status = 404
                    };
                }
            }
        }

        public async Task<ControllerResult<AddressViewModel>> EditAddress(string name, AddressViewModel address)
        {
            User current = this.userRepository.Get()
                .FirstOrDefault(u => u.UserName == name);

            Guid guidId = Guid.Parse(address.Id);

            if (current == null)
            {
                return new ControllerResult<AddressViewModel>()
                {
                    IsSuccess = false,
                    Status = 404,
                    Message = "Пользователь не найден. Попробуйте обновить страницу."
                };
            }
            else
            {
                Address modifyAddress = current.Addresses.FirstOrDefault(a => a.Id == guidId);

                if (modifyAddress == null)
                {
                    return new ControllerResult<AddressViewModel>()
                    {
                        IsSuccess = true,
                        Message = "Адрес не найден. Попробуйте перезагрузить страницу.",
                        Status = 404
                    };
                }
                else
                {
                    modifyAddress.City = address.City;
                    modifyAddress.Country = address.Country;
                    modifyAddress.FirstName = address.FirstName;
                    modifyAddress.LastName = address.LastName;
                    modifyAddress.MiddleName = address.MiddleName;
                    modifyAddress.PhoneNumber = address.PhoneNumber;
                    modifyAddress.PostCode = address.PostCode;
                    modifyAddress.Region = address.Region;
                    modifyAddress.LocalAddress = address.Address;

                    this.addressRepository.Modify(modifyAddress, current.Id);

                    await this.addressRepository.SaveChangesAsync();

                    return new ControllerResult<AddressViewModel>()
                    {
                        IsSuccess = true,
                        Status = 200,
                        Value = address
                    };
                }
            }
        }

        public async Task<ControllerResult<string>> EditEmail(string code, string newEmail, string userName)
        {
            EditEmailConfirmMessage codeFromEmail = this.editEmailConfirmMessageRepository.Get()
                .FirstOrDefault(e => e.SecretCode == code);

            if (codeFromEmail == null)
            {
                return new ControllerResult<string>()
                {
                    IsSuccess = false,
                    Message = "Код не найден.",
                    Status = 404
                };
            }
            else
            {

                User currentUser = this.userRepository.Get()
                    .FirstOrDefault(u => u.UserName == userName);

                if (currentUser == null)
                {
                    return new ControllerResult<string>()
                    {
                        IsSuccess = false,
                        Message = "Пользователь не найден. Попробуйте перезайти в приложение.",
                        Status = 404
                    };
                }

                this.editEmailConfirmMessageRepository.Delete(codeFromEmail, currentUser.Id);
                await this.editEmailConfirmMessageRepository.SaveChangesAsync();

                currentUser.Email = newEmail;
                this.userRepository.Modify(currentUser);
                await this.userRepository.SaveChangesAsync();

                return new ControllerResult<string>()
                {
                    IsSuccess = true,
                    Status = 200,
                    Value = newEmail
                };
            }
        }

        public async Task<ControllerResult> SendConfirmEditEmail(string userName, string newEmail)
        {
            string code = $"{Guid.NewGuid()}-{Guid.NewGuid()}";

            User user = this.userRepository.Get()
                .FirstOrDefault(u => u.UserName == userName);

            if (userName == null)
            {
                logger.LogWarning("user is null");

                return new ControllerResult()
                {
                    IsSuccess = false,
                    Status = 500,
                    Message = "Ошибка сервера, попробуйте скинуть кэш вашего браузера, а так же перезайти в приложение."
                };
            }

            MimeMessage emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(this.email.Name, this.email.Address));
            emailMessage.To.Add(new MailboxAddress(string.Empty, newEmail));
            emailMessage.Subject = "Смена электронной почты";
            emailMessage.Body = new TextPart(TextFormat.Text) { Text = $"Ваш код: {code}" };

            using (SmtpClient client = new SmtpClient())
            {
                await client.ConnectAsync(this.email.Host, this.email.Port, false);
                await client.AuthenticateAsync(this.email.Address, this.email.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }

            await this.editEmailConfirmMessageRepository.AddAsync(new EditEmailConfirmMessage()
            {
                SecretCode = code
            });

            await this.editEmailConfirmMessageRepository.SaveChangesAsync();

            return new ControllerResult()
            {
                IsSuccess = true,
                Message = string.Empty,
                Status = 200
            };
        }
    }
}
