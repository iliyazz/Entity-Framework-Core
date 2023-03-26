namespace Artillery.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Artillery.Data.Models.Enums;
    using Common;


    public class Gun
    {
        public Gun()
        {
            this.CountriesGuns = new HashSet<CountryGun>();
        }

        [Key]
        public int Id { get; set; }


        [Required]
        [ForeignKey(nameof(Manufacturer))]
        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }



        [Required]
        [Range(ValidationConstants.GunGunWeightMinValue, ValidationConstants.GunGunWeightMaxValue)]
        public int GunWeight { get; set; }


        [Required]
        [Range(ValidationConstants.GunBarrelLengthMinValue, ValidationConstants.GunBarrelLengthMaxValue)]
        public double BarrelLength { get; set; }



        public int? NumberBuild { get; set; }


        [Required]
        [Range(ValidationConstants.GunRangeMinValue, ValidationConstants.GunRangeMaxValue)]
        public int Range { get; set; }


        [Required]
        public GunType GunType { get; set; }


        [Required]
        [ForeignKey(nameof(Shell))]
        public int ShellId { get; set; }
        public virtual Shell Shell { get; set; }



        public virtual ICollection<CountryGun> CountriesGuns { get; set; }

    }
}
