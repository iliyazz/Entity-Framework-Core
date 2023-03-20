namespace SoftJail.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class Department
    {
        public Department()
        {
            this.Cells = new HashSet<Cell>();
        }

        [Key]
        public int Id { get; set; }


        [Required]
        [MaxLength(ValidationConstatnts.DepartmentNameMaxLength)]
        public string Name { get; set; }


        public ICollection<Cell> Cells { get; set; }
        
    }
}
