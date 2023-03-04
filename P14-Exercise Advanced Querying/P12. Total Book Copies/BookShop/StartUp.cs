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
        //int input = int.Parse(Console.ReadLine());
        Console.WriteLine(CountCopiesByAuthor(context));
    }

    //Problem 12
    public static string CountCopiesByAuthor(BookShopContext context)
    {
        var output = context
            .Authors
            .Select(a => new
            {
                AuthorName = $"{a.FirstName} {a.LastName}",
                BooksCopies = a.Books
                    .Sum(b => b.Copies)
            })
            .OrderByDescending(b => b.BooksCopies)
            .AsNoTracking()
            .ToArray();
         
        StringBuilder sb = new StringBuilder();
        foreach (var item in output)

            sb.AppendLine($"{item.AuthorName} - {item.BooksCopies}");
        return sb.ToString().TrimEnd();
    }

}