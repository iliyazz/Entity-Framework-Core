namespace P01_StudentSystem.Data;

using Common;
using Microsoft.EntityFrameworkCore;
using Models;

public class StudentSystemContext : DbContext
{

    //Use it when developing the application
    //When we tast th application locally on our PC
    public StudentSystemContext()
    {

    }

    //Loading of DbContext with database injection
    public StudentSystemContext(DbContextOptions options)
        : base(options)
    {

    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Homework> Homeworks { get; set; }
    public DbSet<StudentCourse> StudentsCourses { get; set; }




    //Connection configuration
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //Set default connection string
            //Someone used empty constructor of our DbContext
            optionsBuilder.UseSqlServer(DbConfig.ConnectionString);
        }
        base.OnConfiguring(optionsBuilder);
    }

    //Fluent API and Entities configuration
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<StudentCourse>(entity =>
        {
            entity.HasKey(sc => new { sc.StudentId, sc.CourseId });
        });
    }
}