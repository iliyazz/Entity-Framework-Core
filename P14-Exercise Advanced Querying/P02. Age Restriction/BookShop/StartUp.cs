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
        using var db = new BookShopContext();
        //DbInitializer.ResetDatabase(db);
        string input = Console.ReadLine();

        Console.WriteLine(GetBooksByAgeRestriction(db, input));
    }

    //Problem 02
    public static string GetBooksByAgeRestriction(BookShopContext context, string command)
    {
        //StringBuilder sb = new StringBuilder();
        AgeRestriction ageRestriction;
            bool isParsed= Enum.TryParse<AgeRestriction>(command, true, out ageRestriction);
            if (!isParsed)
            {
                return String.Empty;
            }
        string[] bookTitles = context
            .Books
            .Where(b => b.AgeRestriction == ageRestriction)
            .Select(b => b.Title)
            .OrderBy(t => t)
            .AsNoTracking()
            .ToArray();
        return string.Join(Environment.NewLine, bookTitles);
    }
}