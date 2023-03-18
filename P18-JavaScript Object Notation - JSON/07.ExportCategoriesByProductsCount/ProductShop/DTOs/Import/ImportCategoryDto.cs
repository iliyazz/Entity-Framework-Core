namespace ProductShop.DTOs.Import
{
    using Microsoft.Extensions.Primitives;
    using Newtonsoft.Json;

    public class ImportCategoryDto
    {
        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}
