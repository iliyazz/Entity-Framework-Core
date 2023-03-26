namespace TeisterMask.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Common;
    using TeisterMask.Data.Models.Enums;

    public class Task
    {
        public Task()
        {
            this.EmployeesTasks = new HashSet<EmployeeTask>();
        }

        [Key]
        public int Id { get; set; }


        [Required]
        [MinLength(ValidationConstants.TaskNameMinLength)]
        [MaxLength(ValidationConstants.TaskNameMaxLength)]
        public string Name { get; set; }


        [Required]
        public DateTime OpenDate { get; set; }


        [Required]
        public DateTime DueDate { get; set; }


        [Required]
        public ExecutionType ExecutionType { get; set; }


        [Required]
        public LabelType LabelType { get; set; }


        [Required]
        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }
        public Project Project { get; set; }


        public virtual ICollection<EmployeeTask> EmployeesTasks { get; set; }
    }
}
