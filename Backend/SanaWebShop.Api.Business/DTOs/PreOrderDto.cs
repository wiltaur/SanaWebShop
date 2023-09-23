using System.Text.Json.Serialization;
using SanaWebShop.Api.Business.DTOs.Products;
#nullable disable

namespace SanaWebShop.Api.Business.DTOs
{
    public partial class PreOrderDto
    {
        [JsonPropertyName("user")]
        public int User { get; set; }

        [JsonPropertyName("products")]
        public virtual ICollection<ProductsToOrderDto> Products { get; set; }

    }
}