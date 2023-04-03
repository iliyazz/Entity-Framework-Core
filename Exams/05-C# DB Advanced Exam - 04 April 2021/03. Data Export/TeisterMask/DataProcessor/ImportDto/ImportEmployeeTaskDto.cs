namespace TeisterMask.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Newtonsoft.Json;
    using TeisterMask.Common;

    public class ImportEmployeeTaskDto
    {


        [Required]
        [RegularExpression(ValidationConstants.EmployeeUsernameRegex)]
        [JsonProperty(nameof(Username))]
        public string Username { get; set; }



        [Required]
        [EmailAddress]
        [JsonProperty(nameof(Email))]
        public string Email { get; set; }



        [Required]
        [RegularExpression(ValidationConstants.EmployeePhoneRegex)]
        [JsonProperty(nameof(Phone))]
        public string Phone { get; set; }

        public int[] Tasks { get; set; }
    }
}
