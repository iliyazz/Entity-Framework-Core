namespace Artillery.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Microsoft.EntityFrameworkCore;

    public class Manufacturer
    {
        public Manufacturer()
        {
            this.Guns = new HashSet<Gun>();
        }

        [Key]
        public int Id { get; set; }

        
        [Required]
        [MinLength(ValidationConstants.ManufacturerManufacturerNameMinLength)]
        [MaxLength(ValidationConstants.ManufacturerManufacturerNameMaxLength)]
        public string ManufacturerName { get; set; }


        [Required]
        [MinLength(ValidationConstants.ManufacturerFoundedNameMinLength)]
        [MaxLength(ValidationConstants.ManufacturerFoundedNameMaxLength)]
        public string Founded { get; set; }



        public virtual ICollection<Gun> Guns { get; set; }
    }
}
