namespace SoftJail.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Newtonsoft.Json;

    public class ImportDepartmentCellDto
    {
        [Range(ValidationConstatnts.CellNumberMinLength, ValidationConstatnts.CellNumberMaxLength)]
        [JsonProperty(nameof(CellNumber))]
        public int CellNumber { get; set; }


        [JsonProperty(nameof(HasWindow))]
        public bool HasWindow { get; set; }
    }
}
