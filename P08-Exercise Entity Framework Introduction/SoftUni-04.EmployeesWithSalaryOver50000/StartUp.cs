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
        string result = GetEmployeesWithSalaryOver50000(dbContext);
        Console.WriteLine(result);
    }

    //Problem 04
    public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
    {
        StringBuilder sb = new StringBuilder();
        var employeeWithSalaryOver50000 = context.Employees
            .Where(s => s.Salary > 50000)
            .Select(e => new
            {
                e.FirstName,
                e.Salary
            })
            .OrderBy(e => e.FirstName)
            .AsNoTracking()
            .ToArray();
        foreach (var e in employeeWithSalaryOver50000)
        {
            sb.AppendLine($"{e.FirstName} - {e.Salary:f2}");
        }
        return sb.ToString().TrimEnd();
    }

}




