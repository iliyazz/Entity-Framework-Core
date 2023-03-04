namespace BookShop;

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

        //Console.WriteLine(GetBooksByPrice(context).Length);
        Console.WriteLine(GetBooksByPrice(context));
    }
    //Problem 04
    public static string GetBooksByPrice(BookShopContext context)
    {
        var bookByPrice = context
            .Books
            .Where(b => b.Price > 40)
            .OrderByDescending(b => b.Price)
            .Select(b => $"{b.Title} - ${b.Price:F2}")
            .AsNoTracking()
            .ToArray();
        return string.Join(Environment.NewLine, bookByPrice).Trim();
    }

}