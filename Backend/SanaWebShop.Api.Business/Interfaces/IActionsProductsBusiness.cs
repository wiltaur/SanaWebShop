using SanaWebShop.Api.Business.DTOs;
using SanaWebShop.Api.Business.DTOs.Categories;
using SanaWebShop.Api.Business.DTOs.Products;

namespace SanaWebShop.Api.Business.Interfaces
{
    public interface IActionsProductsBusiness
    {
        Task<string> AddProduct(ProductInfoDto product);
        Task<string> AddCategory(CategoryDto category);
        Task<ProductDetailResponseDto> GetProductsDetail(InfoTableDto req);
    }
}