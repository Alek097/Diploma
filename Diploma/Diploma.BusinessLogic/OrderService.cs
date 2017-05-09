using Diploma.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Diploma.Core;
using Diploma.Core.ViewModels;
using System.Threading.Tasks;
using Diploma.Repositories.Interfaces;
using Diploma.Data.Models;
using System.Linq;

namespace Diploma.BusinessLogic
{
    public class OrderService : IOrderService
    {
        private readonly IUserRepository userRepository;
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;

        public OrderService(
            IUserRepository userRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository)
        {
            this.userRepository = userRepository;
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
        }

        public async Task<ControllerResult<string>> AddProduct(IEnumerable<ProductViewModel> products, string name)
        {
            User current = this.userRepository.Get()
                .FirstOrDefault((u) => u.UserName == name);

            if (current == null)
            {
                return new ControllerResult<string>()
                {
                    IsSuccess = false,
                    Status = 404,
                    Message = "Пользователь не найден. Попробуйте перезагрузить страницу или войти сново."
                };
            }
            else
            {
                decimal totalPrice = 0;

                IEnumerable<Guid> ids = products.Select(product => Guid.Parse(product.Id));

                List<Product> productsEntity = new List<Product>();

                foreach (Guid id in ids)
                {
                    productsEntity.Add(this.productRepository.Get(id));
                }

                foreach (Product item in productsEntity)
                {
                    totalPrice += item.Price;
                }

                Order order = new Order()
                {
                    TotalPrice = totalPrice,
                };

                this.orderRepository.Add(order, current.Id);

                await this.orderRepository.SaveChangesAsync();

                foreach (Product item in productsEntity)
                {
                    item.OrderId = order.Id;

                    this.productRepository.Modify(item, current.Id);
                }

                await this.productRepository.SaveChangesAsync();

                return new ControllerResult<string>()
                {
                    IsSuccess = true,
                    Status = 20,
                    Value = order.Id.ToString()
                };
            }
        }

        public void Dispose()
        {
            this.orderRepository.Dispose();
            this.productRepository.Dispose();
            this.userRepository.Dispose();
        }

        public async Task<ControllerResult<IEnumerable<OrderViewModel>>> GetAll()
        {
            IEnumerable<Order> orders = await this.orderRepository.GetAsync();

            return new ControllerResult<IEnumerable<OrderViewModel>>()
            {
                IsSuccess = true,
                Status = 200,
                Value = orders
                .Where(order => !(order.IsDeleted))
                .Select(order => new OrderViewModel()
                {
                    TotalPrice = order.TotalPrice,

                    CreateDate = order.CreateDate.ToString(),

                    Products = order.Products
                    .Select(product => new ProductViewModel()
                    {
                        CoverUrl = product.CoverUrl,
                        Description = product.Description,
                        Id = product.Id.ToString(),
                        Name = product.Name,
                        Price = product.Price
                    })
                })
            };
        }
    }
}
