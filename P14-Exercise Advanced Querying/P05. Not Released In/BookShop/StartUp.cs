namespace BookShop;

using System.Globalization;
using System.Text;
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
        int input = int.Parse(Console.ReadLine());
        Console.WriteLine(GetBooksNotReleasedIn(context, input));
    }

    //Problem 05
    public static string GetBooksNotReleasedIn(BookShopContext context, int year)
    {
        var bookNotReleasedInGivenYear = context
            .Books
            .Where(b => b.ReleaseDate.Value.Year != year)
            .OrderBy(b => b.BookId)
            .Select(b => b.Title)
            .AsNoTracking()
            .ToArray();
        return string.Join(Environment.NewLine, bookNotReleasedInGivenYear).TrimEnd();
    }

}