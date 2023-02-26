namespace SoftUni;

using System.Globalization;
using System.Text;

using Data;
using Microsoft.EntityFrameworkCore;
using Models;

public class StartUp
{
    static void Main(string[] args)
    {
        SoftUniContext dbContext = new SoftUniContext();
        string result = DeleteProjectById(dbContext);
        Console.WriteLine(result);
    }

    //Problem 14
    public static string DeleteProjectById(SoftUniContext context)
    {
        var employeeProjectToDelete = context.EmployeesProjects
            .Where(x => x.ProjectId == 2);
        context.EmployeesProjects.RemoveRange(employeeProjectToDelete);

        var projectToDelete = context.Projects
            .Find(2);
        if (projectToDelete != null)
        {
            context.Projects.Remove(projectToDelete);
        }

        context.SaveChanges();

        var tenProjects = context.Projects
            .Select(x => x.Name)
            .Take(10)
            .ToArray();
        return string.Join(Environment.NewLine, tenProjects);
    }
}




