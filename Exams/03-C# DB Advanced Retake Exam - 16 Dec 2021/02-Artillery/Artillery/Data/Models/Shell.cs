namespace Artillery.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common;


    public class Shell
    {
        public Shell()
        {
            this.Guns = new HashSet<Gun>();
        }

        [Key]
        public int Id { get; set; }


        [Required]
        [Range(ValidationConstants.ShellShellWeightMinValue, ValidationConstants.ShellShellWeightMaxValue)]
        public double ShellWeight { get; set; }


        [Required]
        [MinLength(ValidationConstants.ShellCaliberMinLength)]
        [MaxLength(ValidationConstants.ShellCaliberMaxLength)]
        public string Caliber { get; set; }



        public virtual ICollection<Gun> Guns { get; set; }
    }
}
