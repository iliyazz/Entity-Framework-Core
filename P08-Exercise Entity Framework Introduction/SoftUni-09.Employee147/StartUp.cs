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
        string result = GetEmployee147(dbContext);
        Console.WriteLine(result);
    }

    //Problem 09
    public static string GetEmployee147(SoftUniContext context)
    {
        var employeeInfo = context.Employees
            .Where(x => x.EmployeeId == 147)
            .Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.JobTitle,
                Projects = x.EmployeesProjects
                    .Select(ep => new
                    {
                        ep.Project.Name,
                    })
                    .OrderBy(ep => ep.Name)
                    .ToArray()
            })
            .ToArray();
        StringBuilder sb = new StringBuilder();
        var employee147 = employeeInfo.First();
        sb.AppendLine($"{employee147.FirstName} {employee147.LastName} - {employee147.JobTitle}");
        foreach (var project in employee147.Projects)
        {
            sb.AppendLine(project.Name);
        }
        return sb.ToString().Trim();
    }
}




