namespace VaporStore.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common;


    public class User
    {
        public User()
        {
            this.Cards = new HashSet<Card>();
        }

        [Key]
        public int Id { get; set; }


        [Required]
        [MinLength(ValidationConstants.UserUsernameMinLength)]
        [MaxLength(ValidationConstants.UserUsernameMaxLength)]
        public string Username { get; set; }


        [Required]
        [RegularExpression(ValidationConstants.UserFullNameValidationRegex)]
        public string FullName { get; set; }


        [Required]
        public string Email { get; set; }


        [Required]
        [Range(ValidationConstants.UserAgeMinValue, ValidationConstants.UserAgeMaxValue)]
        public int Age { get; set; }

        
        public ICollection<Card> Cards { get; set; }
    }
}
