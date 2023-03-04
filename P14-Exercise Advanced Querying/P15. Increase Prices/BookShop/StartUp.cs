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
        IncreasePrices(context);
    }

    //Problem 15
    public static void IncreasePrices(BookShopContext context)
    {
        var output = context
            .Books
            .Where(b => b.ReleaseDate.Value.Year < 2010);

        foreach (var book in output)
        {
            book.Price += 5;
        }

        context.SaveChanges();
    }
}