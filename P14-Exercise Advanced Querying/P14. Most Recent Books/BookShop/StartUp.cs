namespace BookShop;

using System.Globalization;
using System.Text;
using Data;
using Microsoft.EntityFrameworkCore;
using Models.Enums;

public class StartUp
{
    public static void Main()
    {
        using var context = new BookShopContext();
        //DbInitializer.ResetDatabase(db);
        Console.WriteLine(GetMostRecentBooks(context));
    }

    //Problem 14
    public static string GetMostRecentBooks(BookShopContext context)
    {
        var output = context
            .Categories
            .Select(c => new
            {
                Category = c.Name,
                Books = c.CategoryBooks
                    .Select(b => b.Book)
                    .OrderByDescending(b => b.ReleaseDate)
                    .Take(3)
            })
            .OrderBy(c => c.Category)
            .ToList();

        StringBuilder sb = new StringBuilder();
        foreach (var info in output)
        {
            sb.AppendLine($"--{info.Category}");
            foreach (var book in info.Books)
            {
                sb.AppendLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
            }
        }
        return sb.ToString().TrimEnd();
    }
}