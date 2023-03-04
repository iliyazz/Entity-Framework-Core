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
        Console.WriteLine(GetBookTitlesContaining(context, input));
    }

    //Problem 09
    public static string GetBookTitlesContaining(BookShopContext context, string input)
    {
        var output = context
            .Books
            .Where(b => b.Title.ToLower().Contains(input.ToLower()))
            .Select(b => b.Title)
            .OrderBy(b => b)
            .AsNoTracking()
            .ToArray();
        return string.Join(Environment.NewLine, output);
    }

}