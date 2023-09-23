using Microsoft.Extensions.Configuration;
using SanaWebShop.Api.Business.DTOs;
using SanaWebShop.Api.Business.DTOs.Categories;
using SanaWebShop.Api.Business.DTOs.Products;
using SanaWebShop.Api.Business.Interfaces;
using SanaWebShop.Api.Models.Entities;
using SanaWebShop.Api.Repository.Interfaces;
using System.Runtime.ExceptionServices;

namespace SanaWebShop.Api.Business.Implements
{
    public class ActionsProductsBusiness : IActionsProductsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public ActionsProductsBusiness(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<string> AddProduct(ProductInfoDto product)
        {
            string result = "ok";
            try
            {
                Product prod = new()
                {
                    Code = product.Code,
                    Description = product.Description,
                    Title = product.Title,
                    Price = product.Price,
                    Stock = product.Stock
                };
                _unitOfWork.Products.Add(prod);
                await _unitOfWork.Products.Save();

                if (product.Categories.Any())
                {
                    foreach (var category in product.Categories)
                    {
                        ProdCategory prodCat = new()
                        {
                            CodeProduct = product.Code,
                            IdCategory = category
                        };
                        _unitOfWork.ProdCategories.Add(prodCat);
                    }
                    await _unitOfWork.ProdCategories.Save();
                }
            }
            catch (Exception ex)
            {
                result = "Error found: " + ex.InnerException?.Message;
            }
            return result;
        }

        public async Task<string> AddCategory(CategoryDto category)
        {
            string result = "ok";
            try
            {
                Category cat = new()
                {
                    Name = category.Name,
                    Description = category.Description
                };
                _unitOfWork.Categories.Add(cat);
                await _unitOfWork.Categories.Save();
            }
            catch (Exception ex)
            {
                result = "Error found: " + ex.InnerException?.Message;
            }
            return result;
        }

        /// <summary>
        /// This is a method that receives the current page and number of items per page for returm Products.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>List of Products information.</returns>
        public async Task<ProductDetailResponseDto> GetProductsDetail(InfoTableDto req)
        {
            ProductDetailResponseDto productsResponse = new();
            try
            {
                int pageNumber = req.PageNumber ?? Convert.ToInt32(_config.GetSection("DefaultParams").GetSection("PageNumber").Value);
                int pageSize = req.PageSize ?? Convert.ToInt32(_config.GetSection("DefaultParams").GetSection("PageSize").Value);

                List<ProductDto> products;

                var productsQuery = await _unitOfWork.Products.GetWithPagesAsync(pageNumber, pageSize);

                products = (from p in productsQuery
                            select new ProductDto
                            {
                                Code = p.Code,
                                Title = p.Title,
                                Description = p.Description,
                                Price = p.Price,
                                Stock = p.Stock
                            }).ToList();

                productsResponse.TotalRecords = await _unitOfWork.Products.CountAsync();

                var totalPages = (double)productsResponse.TotalRecords / pageSize;
                productsResponse.TotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
                productsResponse.PageNumber = pageNumber;
                productsResponse.PageSize = pageSize;

                productsResponse.Products = products;
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            return productsResponse;
        }
    }
}