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
        int input = int.Parse(Console.ReadLine());
        Console.WriteLine(CountBooks(context, input));
    }

    //Problem 11
    public static int CountBooks(BookShopContext context, int lengthCheck)
    {
        var output = context
            .Books
            .Count(b => b.Title.Length > lengthCheck);
        return output;
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

    //Problem 08
    public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
    {
        var output = context
            .Authors
            .Where(a => a.FirstName.EndsWith(input))
            .OrderBy(a => a.FirstName)
            .ThenBy(a => a.LastName)
            .Select(a => new
            {
                FullName = $"{a.FirstName} {a.LastName}"
            })
            .AsNoTracking()
            .ToArray();
        StringBuilder sb = new StringBuilder();
        foreach (var fn in output)
        {
            sb.AppendLine(fn.FullName);
        }
        return sb.ToString().TrimEnd();
    }

    //Problem 07
    public static string GetBooksReleasedBefore(BookShopContext context, string date)
    {
        bool isParsed = DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,out DateTime inputDate);
        if (!isParsed)
        {
            return "";
        }

        var output = context.Books
            .Where(b => b.ReleaseDate < inputDate)
            .OrderByDescending(b => b.ReleaseDate)
            .Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:f2}")
            .AsNoTracking()
            .ToArray();

        return string.Join(Environment.NewLine, output);
    }


    //Problem 06
    public static string GetBooksByCategory(BookShopContext context, string input)
    {
        //Regex does not work in Judge
        //Regex regex = new Regex(@"\s+");
        //string inputWithoutSpaces = regex.Replace(input, " ");
        //string[] inputArr = inputWithoutSpaces.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var inputArr = input.ToLower().Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        var output = context.Books
            .Where(b => b.BookCategories.Any(c => inputArr.Contains(c.Category.Name.ToLower())))
            .Select(b => b.Title)
            .OrderBy(b => b)
            .AsNoTracking()
            .ToArray();
        return string.Join(Environment.NewLine, output);
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
    //Problem 02
    public static string GetBooksByAgeRestriction(BookShopContext context, string command)
    {
        //StringBuilder sb = new StringBuilder();
        AgeRestriction ageRestriction;
        bool isParsed = Enum.TryParse<AgeRestriction>(command, true, out ageRestriction);
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