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
        Console.WriteLine(GetBooksReleasedBefore(context, input));
    }
    //Problem 07
    public static string GetBooksReleasedBefore(BookShopContext context, string date)
    {
        bool isParsed = DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,out DateTime inputDate);
        if (!isParsed)
        {
            return "";
        }

        var output = context.Books
            .Where(b => b.ReleaseDate < inputDate)
            .OrderByDescending(b => b.ReleaseDate)
            .Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:f2}")
            .AsNoTracking()
            .ToArray();

        return string.Join(Environment.NewLine, output);
    }
}