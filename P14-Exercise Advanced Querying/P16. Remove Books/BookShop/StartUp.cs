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
        Console.WriteLine(RemoveBooks(context));
    }

    //Problem 16
    public static int RemoveBooks(BookShopContext context)
    {
        var output = context
            .Books
            .Where(b => b.Copies < 4200);
        context.Books.RemoveRange(output);
        int countOfRemovedBooks = output.Count();
        context.SaveChanges();
        return countOfRemovedBooks;
    }
}