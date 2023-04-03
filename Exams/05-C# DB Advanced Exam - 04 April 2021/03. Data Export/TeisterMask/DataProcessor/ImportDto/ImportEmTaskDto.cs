namespace TeisterMask.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    using TeisterMask.Common;

    public class ImportEmTaskDto
    {
        [Required]
        [JsonProperty(nameof(Id))]
        public int Id { get; set; }
    }
}
