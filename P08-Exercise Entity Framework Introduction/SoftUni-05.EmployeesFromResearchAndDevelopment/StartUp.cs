namespace SoftUni;

using System.Text;

using Data;
using Microsoft.EntityFrameworkCore;

public class StartUp
{
    static void Main(string[] args)
    {
        SoftUniContext dbContext = new SoftUniContext();
        string result = GetEmployeesFromResearchAndDevelopment(dbContext);
        Console.WriteLine(result);
    }

    //Problem 05

    public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
    {
        StringBuilder sb = new StringBuilder();

        var employeesRnD = context.Employees
            .Where(e => e.Department.Name == "Research and Development")
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                DepartmentName = e.Department.Name,
                e.Salary
            })
            .OrderBy(e => e.Salary)
            .ThenByDescending(e => e.FirstName)
            .ToArray();
        foreach (var e in employeesRnD)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} from {e.DepartmentName} - ${e.Salary:f2}");
        }
        return sb.ToString().Trim();
    }
}




