namespace Theatre.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Common;

    public class Cast
    {

        [Key]
        public int Id { get; set; }


        [Required]
        [MinLength(ValidationConstants.CastFullNameMinLength)]
        [MaxLength(ValidationConstants.CastFullNameMaxLength)]
        public string FullName { get; set; }



        [Required]
        public bool IsMainCharacter { get; set; }


        [Required]
        [RegularExpression(ValidationConstants.CastPhoneNumberRegex)]
        public string PhoneNumber { get; set; }


        [Required]
        [ForeignKey(nameof(Play))]
        public int PlayId { get; set; }
        public Play Play { get; set; }


    }
}
