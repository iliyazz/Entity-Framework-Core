namespace BookShop;

using System.Text;
using Data;
using Initializer;
using Microsoft.EntityFrameworkCore;
using Models.Enums;

public class StartUp
{
    public static void Main()
    {
        using var context = new BookShopContext();
        //DbInitializer.ResetDatabase(db);

        Console.WriteLine(GetGoldenBooks(context));
    }
    //Problem 03
    public static string GetGoldenBooks(BookShopContext context)
    {
        string[] bookTitles = context
            .Books
            .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
            .OrderBy(b => b.BookId)
            .Select(b => b.Title)
            .AsNoTracking()
            .ToArray();
        return string.Join(Environment.NewLine, bookTitles);
    }
}