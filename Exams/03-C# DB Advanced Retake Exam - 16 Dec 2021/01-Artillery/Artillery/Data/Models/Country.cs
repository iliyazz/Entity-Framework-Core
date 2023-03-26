namespace Artillery.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class Country
    {
        public Country()
        {
            this.CountriesGuns = new HashSet<CountryGun>();
        }

        [Key]
        public int Id { get; set; }


        [Required]
        [MinLength(ValidationConstants.CountryCountryNameMinLength)]
        [MaxLength(ValidationConstants.CountryCountryNameMaxLength)]
        public string CountryName { get; set; }


        [Required]
        [Range(ValidationConstants.CountryCountryNameMinLength, ValidationConstants.CountryCountryNameMinLength)]
        public int ArmySize { get; set; }



        public virtual ICollection<CountryGun> CountriesGuns { get; set; }

    }
}
