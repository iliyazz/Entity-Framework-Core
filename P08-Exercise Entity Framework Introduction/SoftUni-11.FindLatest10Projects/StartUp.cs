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
        string result = GetLatestProjects(dbContext);
        Console.WriteLine(result);
    }

    //Problem 11
    public static string GetLatestProjects(SoftUniContext context)
    {
        var latestTenProjects = context.Projects
            .OrderByDescending(x => x.StartDate)
            .Take(10)
            .Select(x => new
            {
                x.Name,
                x.Description,
                x.StartDate
            })
            .OrderBy(x => x.Name)
            .ToArray();
        StringBuilder sb = new StringBuilder();
        foreach (var l in latestTenProjects)
        {
            sb.AppendLine(l.Name)
                .AppendLine(l.Description)
                .AppendLine(l.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));
        }

        return sb.ToString().TrimEnd();

    }

}




