namespace TeisterMask.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class Employee
    {
        [Key]
        public int Id { get; set; }



        [Required]
        [RegularExpression(ValidationConstants.EmployeeUsernameRegex)]
        public string Username { get; set; }



        [Required]
        [EmailAddress]
        public string Email { get; set; }



        [Required]
        [RegularExpression(ValidationConstants.EmployeePhoneRegex)]
        public string Phone { get; set; }

        
        public ICollection<EmployeeTask> EmployeesTasks { get; set; }
    }
}
