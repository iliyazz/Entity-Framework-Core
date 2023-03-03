namespace P01_StudentSystem.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common;
using Enums;
using Microsoft.EntityFrameworkCore;

public class Resource
{
    [Key]
    public int ResourceId { get; set; }

    [Required]
    [MaxLength(ValidationConstants.ResourceNameMaxLength)]
    public string Name { get; set; }

    [Required]
    [MaxLength(ValidationConstants.ResourceUrlMaxLength)]
    [Unicode(false)]
    public string Url { get; set; }
    
    public ResourceType ResourceType { get; set; }



    [ForeignKey(nameof(Course))]
    public int CourseId { get; set; }
    public virtual Course Course { get; set; }




}