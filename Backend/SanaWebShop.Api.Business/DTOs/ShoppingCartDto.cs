using SanaWebShop.Api.Business.DTOs.Products;
using System.Text.Json.Serialization;
#nullable disable

namespace SanaWebShop.Api.Business.DTOs
{
    public partial class ShoppingCartDto
    {
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}