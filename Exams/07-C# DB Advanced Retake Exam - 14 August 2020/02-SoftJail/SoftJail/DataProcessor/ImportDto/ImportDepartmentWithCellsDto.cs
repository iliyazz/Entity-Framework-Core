namespace SoftJail.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Newtonsoft.Json;

    
    public class ImportDepartmentWithCellsDto
    {
        [Required]
        [MinLength(ValidationConstatnts.DepartmentNameMinLength)]
        [MaxLength(ValidationConstatnts.DepartmentNameMaxLength)]
        [JsonProperty(nameof(Name))]
        public string Name { get; set; }



        [JsonProperty(nameof(Cells))]
        public ImportDepartmentCellDto[] Cells { get; set; }
    }
}
