using System.Text.Json.Serialization;

#nullable disable

namespace SanaWebShop.Api.Business.DTOs
{
    public partial class InfoTableDto
    {
        [JsonPropertyName("pageNumber")]
        public int? PageNumber { get; set; }

        [JsonPropertyName("pageSize")]
        public int? PageSize { get; set; }
    }
}