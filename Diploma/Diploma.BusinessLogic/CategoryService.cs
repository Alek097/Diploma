using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Diploma.BusinessLogic.Interfaces;
using Diploma.Core;
using Diploma.Core.ViewModels;
using Diploma.Repositories.Interfaces;
using System.Linq;
using Diploma.Data.Models;

namespace Diploma.BusinessLogic
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IUserRepository userRepository;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IUserRepository userRepository)
        {
            this.categoryRepository = categoryRepository;
            this.userRepository = userRepository;
        }

        public async Task<ControllerResult<CategoryViewModel>> AddCategory(string name, CategoryViewModel category)
        {
            User current = this.userRepository.Get()
                .FirstOrDefault((u) => u.UserName == name);

            if (current == null)
            {
                return new ControllerResult<CategoryViewModel>()
                {
                    IsSuccess = false,
                    Status = 404,
                    Message = "Пользователь не найден. Попробуйте перезагрузить страницу или войти сново."
                };
            }
            else
            {
                this.categoryRepository.Add(new Category()
                {
                    Id = Guid.Parse(category.Id),
                    Name = category.Name,
                    Description = category.Description
                },
                current.Id);

                await this.categoryRepository.SaveChangesAsync();

                return new ControllerResult<CategoryViewModel>()
                {
                    IsSuccess = true,
                    Status = 200,
                    Value = category
                };
            }
        }

        public async Task<ControllerResult> DeleteCategory(string name, string id)
        {
            User current = this.userRepository.Get()
                .FirstOrDefault((u) => u.UserName == name);

            if (current == null)
            {
                return new ControllerResult()
                {
                    IsSuccess = false,
                    Status = 404,
                    Message = "Пользователь не найден. Попробуйте перезагрузить страницу или войти сново."
                };
            }
            else
            {
                Category deleteCategory = this.categoryRepository.Get(Guid.Parse(id));

                if(deleteCategory == null)
                {
                    return new ControllerResult()
                    {
                        IsSuccess = false,
                        Status = 404,
                        Message = "Категория не найдена. Перезагрузите страницу"
                    };
                }
                else
                {
                    this.categoryRepository.Delete(deleteCategory, current.Id);
                    await this.categoryRepository.SaveChangesAsync();

                    return new ControllerResult()
                    {
                        IsSuccess = true,
                        Status = 200
                    };
                }
            }
        }

        public async Task<ControllerResult<IEnumerable<CategoryViewModel>>> GetCategoryNames()
        {
            IEnumerable<Category> categories = await categoryRepository.GetAsync();

            return new ControllerResult<IEnumerable<CategoryViewModel>>()
            {
                IsSuccess = true,
                Status = 200,
                Value = categories
                .Where(category => !(category.IsDeleted))
                .Select(category => new CategoryViewModel()
                {
                    Id = category.Id.ToString(),
                    Name = category.Name,
                    Description = category.Description
                })
            };
        }
    }
}
