using System.Text.Json.Serialization;
#nullable disable

namespace SanaWebShop.Api.Business.DTOs
{
    public partial class OrderUserDto
    {
        [JsonPropertyName("customer")]
        public int Customer { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }
    }
}