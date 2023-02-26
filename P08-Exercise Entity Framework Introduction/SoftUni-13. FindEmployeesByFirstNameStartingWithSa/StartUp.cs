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
        string result = GetEmployeesByFirstNameStartingWithSa(dbContext);
        Console.WriteLine(result);
    }

    //Problem 13
    public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
    {
        var employeesNameStartWithSa = context.Employees
            .Where(x => x.FirstName.StartsWith("Sa"))
            .OrderBy(x => x.FirstName)
            .ThenBy(x => x.LastName)
            .ToArray();
        StringBuilder sb = new StringBuilder();
        foreach (var e in employeesNameStartWithSa)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})");
        }
        return sb.ToString().TrimEnd();
    }
}




