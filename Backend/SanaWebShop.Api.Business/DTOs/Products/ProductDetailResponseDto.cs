using System.Text.Json.Serialization;

#nullable disable

namespace SanaWebShop.Api.Business.DTOs.Products
{
    public partial class ProductDetailResponseDto : InfoTableDto
    {
        [JsonPropertyName("totalPages")]
        public int? TotalPages { get; set; }

        [JsonPropertyName("totalRecords")]
        public int? TotalRecords { get; set; }

        [JsonPropertyName("products")]
        public virtual ICollection<ProductDto> Products { get; set; }
    }
}