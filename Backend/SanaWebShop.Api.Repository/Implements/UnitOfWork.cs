using SanaWebShop.Api.Models.Contexts;
using SanaWebShop.Api.Models.Entities;
using SanaWebShop.Api.Repository.Interfaces;

namespace SanaWebShop.Api.Repository.Implements
{
    public class UnitOfWork : IUnitOfWork
    {
        private SanaWebShopContext _context;
        private IRepository<Category>? _categories;
        private IRepository<Product>? _products;
        private IRepository<ProdCategory>? _prodCategory;
        private IRepository<Customer>? _customers;
        private IRepository<Order>? _orders;
        private IRepository<OrderDetail>? _orderDetails;

        public UnitOfWork(SanaWebShopContext context) => this._context = context;

        public IRepository<Category> Categories
        {
            get
            {
                return _categories == null ?
                    _categories = new Repository<Category>(_context) :
                    _categories;
            }
        }

        public IRepository<Product> Products
        {
            get
            {
                return _products == null ?
                    _products = new Repository<Product>(_context) :
                    _products;
            }
        }

        public IRepository<ProdCategory> ProdCategories
        {
            get
            {
                return _prodCategory == null ?
                    _prodCategory = new Repository<ProdCategory>(_context) :
                    _prodCategory;
            }
        }

        public IRepository<Customer> Customers
        {
            get
            {
                return _customers == null ?
                    _customers = new Repository<Customer>(_context) :
                    _customers;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                return _orders == null ?
                    _orders = new Repository<Order>(_context) :
                    _orders;
            }
        }

        public IRepository<OrderDetail> OrderDetails
        {
            get
            {
                return _orderDetails == null ?
                    _orderDetails = new Repository<OrderDetail>(_context) :
                    _orderDetails;
            }
        }
    }
}