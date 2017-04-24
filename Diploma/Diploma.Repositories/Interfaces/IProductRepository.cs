using Diploma.Data.Models;
using System;

namespace Diploma.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
    }
}
