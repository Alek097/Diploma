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
                    Products = productsEntity
                };

                this.orderRepository.Add(order, current.Id);

                await this.orderRepository.SaveChangesAsync();

                return new ControllerResult<string>()
                {
                    IsSuccess = true,
                    Status = 20,
                    Value = order.Id.ToString()
            };
        }
    }
}
}
