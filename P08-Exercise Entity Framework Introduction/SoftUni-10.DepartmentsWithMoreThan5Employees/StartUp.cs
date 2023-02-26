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
        string result = GetDepartmentsWithMoreThan5Employees(dbContext);
        Console.WriteLine(result);
    }

    //Problem 10
    public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
    {
        var departments = context.Departments
            .Where(d => d.Employees.Count > 5)
            .OrderBy(d => d.Employees.Count)
            .ThenBy(d => d.Name)
            .Select(d => new
            {
                d.Name,
                d.Manager.FirstName,
                d.Manager.LastName,
                EmployeesInfo = d.Employees
                    .Select(ei => new
                    {
                        ei.FirstName,
                        ei.LastName,
                        ei.JobTitle,
                    })
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .ToArray()
            })
            .ToArray();
        StringBuilder sb = new StringBuilder();
        foreach (var department in departments)
        {
            sb.AppendLine($"{department.Name} - {department.FirstName} {department.LastName}");
            foreach (var empl in department.EmployeesInfo)
            {
                sb.AppendLine($"{empl.FirstName} {empl.LastName} - {empl.JobTitle}");
            }
        }
        return sb.ToString().Trim();
    }

}




