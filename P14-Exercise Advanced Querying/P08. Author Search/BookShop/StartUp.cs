namespace BookShop;

using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using BookShop.Models;
using Data;
using Microsoft.EntityFrameworkCore;
using Models.Enums;

public class StartUp
{
    public static void Main()
    {
        using var context = new BookShopContext();
        //DbInitializer.ResetDatabase(db);
        string input = Console.ReadLine();
        Console.WriteLine(GetAuthorNamesEndingIn(context, input));
    }

    //Problem 08
    public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
    {
        var output = context
            .Authors
            .Where(a => a.FirstName.EndsWith(input))
            .OrderBy(a => a.FirstName)
            .ThenBy(a => a.LastName)
            .Select(a => new
            {
                FullName = $"{a.FirstName} {a.LastName}"
            })
            .AsNoTracking()
            .ToArray();
        StringBuilder sb = new StringBuilder();
        foreach (var fn in output)
        {
            sb.AppendLine(fn.FullName);
        }
        return sb.ToString().TrimEnd();
    }
}