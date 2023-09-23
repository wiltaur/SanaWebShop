using SanaWebShop.Api.Business.DTOs;

namespace SanaWebShop.Api.Business.Implements
{
    public interface IActionsOrdersBusiness
    {
        Task<string> CreateShoppingCart(PreOrderDto preOrder);
        Task<List<ShoppingCartDtoResponse>> GetShoppingCart(OrderUserDto data);
        Task<string> DeleteFromShoppingCart(int id);
        Task<string> EditQuantity(ShoppingCartDto data);
        Task<string> ProcessOrder(int idOrder);
    }
}