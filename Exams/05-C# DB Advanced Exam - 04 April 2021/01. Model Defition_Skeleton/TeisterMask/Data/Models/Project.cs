namespace TeisterMask.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Common;
    using TeisterMask.Data.Models.Enums;

    public class Project
    {
        public Project()
        {
            this.Tasks = new HashSet<Task>();
        }

        [Key] public int Id { get; set; }


        [Required]
        [MinLength(ValidationConstants.TaskNameMinLength)]
        [MaxLength(ValidationConstants.TaskNameMaxLength)]
        public string Name { get; set; }


        [Required]
        public DateTime OpenDate { get; set; }


        public DateTime? DueDate { get; set; }



        public virtual ICollection<Task> Tasks { get; set; }



    }
}
