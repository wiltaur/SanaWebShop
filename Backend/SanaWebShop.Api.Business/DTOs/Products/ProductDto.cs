using System.Text.Json.Serialization;
#nullable disable

namespace SanaWebShop.Api.Business.DTOs.Products
{
    public partial class ProductDto
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("stock")]
        public int Stock { get; set; }
    }
}