using System.Text.Json.Serialization;
#nullable disable

namespace SanaWebShop.Api.Business.DTOs.Products
{
    public partial class ProductsToOrderDto
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}