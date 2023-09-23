using SanaWebShop.Api.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanaWebShop.Api.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        public IRepository<Category> Categories { get; }
        public IRepository<Product> Products { get; }
        public IRepository<ProdCategory> ProdCategories { get; }
        public IRepository<Customer> Customers { get; }
        public IRepository<Order> Orders { get; }
        public IRepository<OrderDetail> OrderDetails { get; }
    }
}