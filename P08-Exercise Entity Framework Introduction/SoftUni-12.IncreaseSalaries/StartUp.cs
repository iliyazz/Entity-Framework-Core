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
        string result = IncreaseSalaries(dbContext);
        Console.WriteLine(result);
    }

    //Problem 12
    public static string IncreaseSalaries(SoftUniContext context)
    {
        var emToEncSalary = context.Employees
            .Where(e => e.Department.Name == "Engineering" ||
                        e.Department.Name == "Tool Design" ||
                        e.Department.Name == "Marketing" ||
                        e.Department.Name == "Information Services")
            .OrderBy(e => e.FirstName)
            .ThenBy(e => e.LastName)
            .ToList();
        foreach (var e in emToEncSalary)
        {
            e.Salary *= 1.12m;
        }

        context.SaveChanges();
        StringBuilder sb = new StringBuilder();
        foreach (var e in emToEncSalary)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} (${e.Salary:f2})");
        }

        return sb.ToString().TrimEnd();
    }
}




