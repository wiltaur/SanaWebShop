using SanaWebShop.Api.Business.DTOs.Products;
using System.Text.Json.Serialization;
#nullable disable

namespace SanaWebShop.Api.Business.DTOs
{
    public partial class ShoppingCartDtoResponse: ProductDto
    {
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("idOrderDetail")]
        public int IdOrderDetail { get; set; }
    }
}