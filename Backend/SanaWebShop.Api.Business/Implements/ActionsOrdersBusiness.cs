using Microsoft.Extensions.Configuration;
using SanaWebShop.Api.Business.DTOs;
using SanaWebShop.Api.Business.DTOs.Products;
using SanaWebShop.Api.Models.Entities;
using SanaWebShop.Api.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.ExceptionServices;

namespace SanaWebShop.Api.Business.Implements
{
    public class ActionsOrdersBusiness : IActionsOrdersBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActionsOrdersBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateShoppingCart(PreOrderDto preOrder)
        {
            int result = 0;
            try
            {
                if (preOrder.Products.Any())
                {
                    Order order = new()
                    {
                        IdCustomer = preOrder.User
                    };
                    _unitOfWork.Orders.Add(order);
                    await _unitOfWork.Orders.Save();
                    await AddPreOrderDetail(preOrder.Products, order.Id);
                    result = order.Id;
                }
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            return result;
        }

        public async Task<List<ShoppingCartDtoResponse>> GetShoppingCart(OrderUserDto data)
        {
            List<ShoppingCartDtoResponse> shoppingCarts = new();
            try
            {
                var order = await _unitOfWork.Orders.GetByIdAsync(data.Order);
                if (order != null && order.IdCustomer == data.Customer)
                {
                    var orderDetails = await _unitOfWork.OrderDetails.GetFilteredData(OrderFilterExpression(order.Id));

                    foreach (var product in orderDetails)
                    {
                        ShoppingCartDtoResponse shoppingCartResponseDto = new ShoppingCartDtoResponse();
                        shoppingCartResponseDto.IdOrderDetail = product.Id;
                        shoppingCartResponseDto.Quantity = product.Quantity;
                        shoppingCartResponseDto.Code = product.CodeProduct;
                        GetProductDetail(ref shoppingCartResponseDto);
                        shoppingCarts.Add(shoppingCartResponseDto);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            return shoppingCarts;
        }

        public async Task<string> DeleteFromShoppingCart(int id)
        {
            string result = string.Empty;
            try
            {
                _unitOfWork.OrderDetails.DeleteById(id);
                await _unitOfWork.OrderDetails.Save();
                result = "ok";
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            return result;
        }

        public async Task<string> EditQuantity(ShoppingCartDto data)
        {
            string result = string.Empty;
            try
            {
                var orderD = await _unitOfWork.OrderDetails.GetByIdAsync(data.Id);
                if (orderD != null)
                {
                    orderD.Quantity = data.Quantity;
                    _unitOfWork.OrderDetails.Update(orderD);
                    await _unitOfWork.OrderDetails.Save();
                    result = "ok";
                }
                else
                {
                    throw new Exception("Product of order not found.");
                }
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            return result;
        }

        public async Task<string> ProcessOrder(int idOrder)
        {
            string result = string.Empty;
            try
            {
                var orderDetails = await _unitOfWork.OrderDetails.GetFilteredData(OrderFilterExpression(idOrder));
                List<Product> prodLst = new();
                foreach (var product in orderDetails)
                {
                    var productQuery = await _unitOfWork.Products.GetByCodeAsync(product.CodeProduct);
                    if (productQuery != null && productQuery.Stock >= product.Quantity)
                    {
                        productQuery.Stock -= product.Quantity;
                        prodLst.Add(productQuery);
                    }
                    else
                    {
                        throw new Exception("There is no stock available for the product (" + product.CodeProduct + ").");
                    }
                }
                _unitOfWork.Products.UpdateRange(prodLst);
                var order = await _unitOfWork.Orders.GetByIdAsync(idOrder);
                order.Status = true;
                _unitOfWork.Orders.Update(order);
                await _unitOfWork.Products.Save();
                await _unitOfWork.Orders.Save();
                result = "ok";
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            return result;
        }

        private async Task AddPreOrderDetail(ICollection<ProductsToOrderDto> products, int orderId)
        {
            foreach (var product in products)
            {
                var productQuery = _unitOfWork.Products.GetByCode(product.Code);
                if (productQuery != null && productQuery.Stock >= product.Quantity)
                {
                    OrderDetail orderDetail = new()
                    {
                        CodeProduct = product.Code,
                        Quantity = product.Quantity,
                        IdOrder = orderId
                    };
                    _unitOfWork.OrderDetails.Add(orderDetail);
                }
                else
                {
                    _unitOfWork.Orders.DeleteById(orderId);
                    await _unitOfWork.Orders.Save();
                    throw new Exception("There is no stock available for the product (" + product.Code + ").");
                }
            }
            await _unitOfWork.OrderDetails.Save();
        }

        private static Expression<Func<OrderDetail, bool>> OrderFilterExpression(int idOrder)
        {
            Expression<Func<OrderDetail, bool>> filterExpression = order =>
            order.IdOrder.Equals(idOrder);
            return filterExpression;
        }

        private void GetProductDetail(ref ShoppingCartDtoResponse sCart)
        {
            var product = _unitOfWork.Products.GetByCodeAsync(sCart.Code).Result;

            sCart.Description = product.Description;
            sCart.Price = product.Price;
            sCart.Stock = product.Stock;
            sCart.Title = product.Title;
        }
    }
}