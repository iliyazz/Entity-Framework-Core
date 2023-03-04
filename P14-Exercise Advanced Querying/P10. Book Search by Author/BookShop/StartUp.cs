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
        string input = Console.ReadLine();
        Console.WriteLine(GetBooksByAuthor(context, input));
    }

    //Problem 10
    public static string GetBooksByAuthor(BookShopContext context, string input)
    {
        var output = context
            .Authors
            .Where(a => a.LastName.ToLower().StartsWith(input.ToLower()))
            .Select(a => new
            {
                AuthorFirstName = a.FirstName,
                AuthorLastName = a.LastName,
                bookTitle = a.Books
                    .Select(b => new
                    {
                        BookTitle = b.Title
                    })

            })
            .AsNoTracking()
            .ToArray();

        StringBuilder sb = new StringBuilder();
        foreach (var info in output)
        {
            foreach (var b in info.bookTitle)
            {
                sb.AppendLine($"{b.BookTitle} ({info.AuthorFirstName} {info.AuthorLastName})");
            }
        }

        return sb.ToString().TrimEnd();
    }
}