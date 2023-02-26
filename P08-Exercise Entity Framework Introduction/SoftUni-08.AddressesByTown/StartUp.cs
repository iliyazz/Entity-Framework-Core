namespace SoftUni;

using System.Globalization;
using System.Text;

using Data;
using Microsoft.EntityFrameworkCore;
using Models;

public class StartUp
{
    static void Main(string[] args)
    {
        SoftUniContext dbContext = new SoftUniContext();
        string result = GetAddressesByTown(dbContext);
        Console.WriteLine(result);
    }

    //Problem 08
    public static string GetAddressesByTown(SoftUniContext context)
    {
        StringBuilder sb = new StringBuilder();
        var addressesByTown = context.Addresses
            .OrderByDescending(a => a.Employees.Count)
            .ThenBy(a => a.Town.Name)
            .ThenBy(a => a.AddressText)
            .Take(10)
            .Select(a => $"{a.AddressText}, {a.Town.Name} - {a.Employees.Count} employees")
            .ToArray();
        return string.Join(Environment.NewLine, addressesByTown);

}




