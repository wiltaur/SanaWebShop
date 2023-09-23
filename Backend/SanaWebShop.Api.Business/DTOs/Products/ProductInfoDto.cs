using System.Text.Json.Serialization;

#nullable disable

namespace SanaWebShop.Api.Business.DTOs.Products
{
    public partial class ProductInfoDto : ProductDto
    {
        [JsonPropertyName("categories")]
        public List<int> Categories { get; set; }
    }
}