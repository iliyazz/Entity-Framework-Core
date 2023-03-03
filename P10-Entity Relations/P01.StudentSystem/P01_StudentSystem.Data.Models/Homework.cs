﻿namespace P01_StudentSystem.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common;
using Enums;
using Microsoft.EntityFrameworkCore;

public class Homework
{
 
    [Key]
    public int HomeworkId { get; set; }

    [Required]
    [MaxLength(ValidationConstants.HomeworkContentMaxLength)]
    [Unicode(false)]
    public string Content { get; set; }

    public ContentType ContentType { get; set; }

    public DateTime SubmissionTime { get; set; }


    [ForeignKey(nameof(Student))]
    public int StudentId { get; set; }
    public virtual Student Student { get; set; }



    [ForeignKey(nameof(Course))]
    public int CourseId { get; set; }
    public virtual Course Course { get; set; }
}