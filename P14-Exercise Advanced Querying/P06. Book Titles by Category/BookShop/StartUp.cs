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
        Console.WriteLine(GetBooksByCategory(context, input));
    }
    //Problem 06
    public static string GetBooksByCategory(BookShopContext context, string input)
    {
        var inputArr = input.ToLower().Split(new [] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        var output = context.Books
            .Where(b => b.BookCategories.Any(c => inputArr.Contains(c.Category.Name.ToLower())))
            .Select(b => b.Title)
            .OrderBy(b => b)
            .AsNoTracking()
            .ToArray();
        return string.Join(Environment.NewLine, output);
    }

}