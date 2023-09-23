using System.Text.Json.Serialization;

#nullable disable

namespace SanaWebShop.Api.Business.DTOs.Categories
{
    public class CategoryDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}