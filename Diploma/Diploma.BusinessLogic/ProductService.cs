using System;
using System.Threading.Tasks;
using Diploma.BusinessLogic.Interfaces;
using Diploma.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Collections.Generic;
using Diploma.Core.ViewModels;
using Diploma.Data.Models;
using Diploma.Repositories.Interfaces;
using System.Linq;

namespace Diploma.BusinessLogic
{
    public class ProductService : IProductService
    {
        private readonly IHostingEnvironment environment;
        private readonly ICategoryRepository categoryRepository;
        private readonly IUserRepository userRepository;
        private readonly IProductRepository productRepository;
        private readonly ICharacteristicsGroupRepository characteristicsGroupRepository;

        public ProductService(
            IHostingEnvironment environment,
            ICategoryRepository categoryRepository,
            IUserRepository userRepository,
            IProductRepository productRepository,
            ICharacteristicsGroupRepository characteristicsGroupRepository)
        {
            this.environment = environment;
            this.categoryRepository = categoryRepository;
            this.userRepository = userRepository;
            this.productRepository = productRepository;
            this.characteristicsGroupRepository = characteristicsGroupRepository;
        }

        public async Task<ControllerResult> AddProduct(ProductViewModel product, string categoryId, string name)
        {
            User current = this.userRepository.Get().FirstOrDefault(user => user.UserName == name);

            if (current == null)
            {
                return new ControllerResult()
                {
                    IsSuccess = false,
                    Status = 404,
                    Message = "Пользователь не найден. Попробуйте перезагрузить страницу."
                };
            }

            Category category = this.categoryRepository.Get(Guid.Parse(categoryId));

            if (category == null)
            {
                return new ControllerResult()
                {
                    IsSuccess = false,
                    Status = 404,
                    Message = "Категория не найдена. Возможно она уже удалена."
                };
            }

            Product newProduct = new Product()
            {
                Characteristics = product.Characteristics.Select(characteristic => new Characteristic()
                {
                    Name = characteristic.Name,
                    Value = characteristic.Value,
                    ModifyBy = current.Id,
                    CreateBy = current.Id,
                    CreateDate = DateTime.Now,
                    LastModifyDate = DateTime.Now
                }).ToList(),

                CharacteristicsGroups = product.CharacteristicsGroups.Select(chrg => new CharacteristicsGroup()
                {
                    Name = chrg.Name,
                    ModifyBy = current.Id,
                    CreateBy = current.Id,
                    CreateDate = DateTime.Now,
                    LastModifyDate = DateTime.Now,
                    Characteristics = chrg.Characteristics.Select(characteristic => new Characteristic()
                    {
                        Name = characteristic.Name,
                        Value = characteristic.Value,
                        ModifyBy = current.Id,
                        CreateBy = current.Id,
                        CreateDate = DateTime.Now,
                        LastModifyDate = DateTime.Now
                    }).ToList(),
                }).ToList(),

                Description = product.Name,
                Name = product.Name,
                Price = product.Price,
                CoverUrl = product.CoverUrl,

                Images = product.ImagesUrl.Select(imageUrl => new Image()
                {
                    Url = imageUrl,
                    ModifyBy = current.Id,
                    CreateBy = current.Id,
                    CreateDate = DateTime.Now,
                    LastModifyDate = DateTime.Now
                }).ToList(),

                ModifyBy = current.Id,
                CreateBy = current.Id,
                CreateDate = DateTime.Now,
                LastModifyDate = DateTime.Now
            };

            category.Products.Add(newProduct);

            this.categoryRepository.Modify(category);

            await this.categoryRepository.SaveChangesAsync();

            return new ControllerResult()
            {
                IsSuccess = true,
                Status = 200
            };
        }

        public async Task<ControllerResult<string>> DeleteProduct(string productId, string name)
        {
            User current = this.userRepository.Get().FirstOrDefault(user => user.UserName == name);

            if (current == null)
            {
                return new ControllerResult<string>()
                {
                    IsSuccess = false,
                    Status = 404,
                    Message = "Пользователь не найден. Попробуйте перезагрузить страницу."
                };
            }
            else
            {
                Product deleteProduct = this.productRepository.Get(Guid.Parse(productId));

                if (deleteProduct == null)
                {
                    return new ControllerResult<string>()
                    {
                        IsSuccess = true,
                        Status = 200,
                        Value = productId
                    };
                }
                else
                {
                    this.productRepository.Delete(deleteProduct, current.Id);

                    await this.productRepository.SaveChangesAsync();

                    return new ControllerResult<string>()
                    {
                        IsSuccess = true,
                        Status = 200,
                        Value = productId
                    };
                }
            }
        }

        public async Task<ControllerResult> EditProduct(ProductViewModel product, string categoryId, string name)
        {
            User current = this.userRepository.Get().FirstOrDefault(user => user.UserName == name);

            if (current == null)
            {
                return new ControllerResult()
                {
                    IsSuccess = false,
                    Status = 404,
                    Message = "Пользователь не найден. Попробуйте перезагрузить страницу."
                };
            }

            Product editProduct = this.productRepository.Get(Guid.Parse(product.Id));

            editProduct.Characteristics = product.Characteristics.Select(characteristic => new Characteristic()
            {
                Name = characteristic.Name,
                Value = characteristic.Value,
                ModifyBy = current.Id,
                CreateBy = current.Id,
                CreateDate = DateTime.Now,
                LastModifyDate = DateTime.Now
            }).ToList();

            editProduct.CharacteristicsGroups = product.CharacteristicsGroups.Select(chrg => new CharacteristicsGroup()
            {
                Name = chrg.Name,
                ModifyBy = current.Id,
                CreateBy = current.Id,
                CreateDate = DateTime.Now,
                LastModifyDate = DateTime.Now,
                Characteristics = chrg.Characteristics.Select(characteristic => new Characteristic()
                {
                    Name = characteristic.Name,
                    Value = characteristic.Value,
                    ModifyBy = current.Id,
                    CreateBy = current.Id,
                    CreateDate = DateTime.Now,
                    LastModifyDate = DateTime.Now
                }).ToList(),
            }).ToList();

            editProduct.Description = product.Name;
            editProduct.Name = product.Name;
            editProduct.Price = product.Price;
            editProduct.CoverUrl = product.CoverUrl;

            editProduct.Images = product.ImagesUrl.Select(imageUrl => new Image()
            {
                Url = imageUrl,
                ModifyBy = current.Id,
                CreateBy = current.Id,
                CreateDate = DateTime.Now,
                LastModifyDate = DateTime.Now
            }).ToList();

            this.productRepository.Modify(editProduct, current.Id);

            await this.productRepository.SaveChangesAsync();

            return new ControllerResult()
            {
                IsSuccess = true,
                Status = 200
            };
        }

        public async Task<ControllerResult<ProductViewModel>> GetProductById(string productId)
        {
            IEnumerable<Product> products = await this.productRepository.GetAsync();

            Product result = products
                .Where(prod => !(prod.IsDeleted))
                .FirstOrDefault(prod => prod.Id == Guid.Parse(productId));


            if (result == null)
            {
                return new ControllerResult<ProductViewModel>()
                {
                    IsSuccess = false,
                    Status = 404,
                    Message = $"Продукт в данной категории не найден. Возможно уже удалён."
                };
            }
            else
            {
                IEnumerable<Guid> chgIds = result.CharacteristicsGroups
                    .Where(charc => !(charc.IsDeleted))
                    .Select(chg => chg.Id);

                return new ControllerResult<ProductViewModel>()
                {
                    IsSuccess = true,
                    Status = 200,
                    Value = new ProductViewModel()
                    {
                        Id = result.Id.ToString(),
                        CoverUrl = result.CoverUrl,
                        Description = result.Description,
                        Name = result.Name,
                        Price = result.Price,

                        ImagesUrl = result.Images.Select(img => img.Url),

                        Characteristics = result.Characteristics.Select(characteristic => new CharacteristicViewModel()
                        {
                            Name = characteristic.Name,
                            Value = characteristic.Value
                        }),

                        CharacteristicsGroups = chgIds.Select(id =>
                        {
                            CharacteristicsGroup characteristicsGroup = this.characteristicsGroupRepository.Get(id);

                            return new CharacteristicsGroupViewModel()
                            {
                                Name = characteristicsGroup.Name,

                                Characteristics = characteristicsGroup.Characteristics
                                .Where(charac => !(charac.IsDeleted))
                                .Select(characteristic => new CharacteristicViewModel()
                                {
                                    Name = characteristic.Name,
                                    Value = characteristic.Value
                                })
                            };
                        })
                    }
                };
            }
        }

        public async Task<ControllerResult<string>> SaveCover(IFormFile cover)
        {
            return new ControllerResult<string>()
            {
                Value = await this.saveFileAsync(cover),
                IsSuccess = true,
                Status = 200
            };
        }

        public async Task<ControllerResult<IEnumerable<string>>> SaveImages(ICollection<IFormFile> images)
        {
            List<string> urls = new List<string>();

            foreach (IFormFile image in images)
            {
                urls.Add(await this.saveFileAsync(image));
            }

            return new ControllerResult<IEnumerable<string>>()
            {
                IsSuccess = true,
                Status = 200,
                Value = urls
            };
        }

        private async Task<string> saveFileAsync(IFormFile file)
        {
            string path = Path.Combine(this.environment.ContentRootPath, "Bundles", "images");
            string filePath = Path.Combine(path, $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}");
            string url = $"Bundles/images/{Path.GetFileName(filePath)}";

            if (!(Directory.Exists(path)))
            {
                Directory.CreateDirectory(path);
            }

            using (FileStream fs = File.Create(filePath))
            {
                await file.CopyToAsync(fs);
                fs.Flush();
            }

            return url;
        }
    }
}
