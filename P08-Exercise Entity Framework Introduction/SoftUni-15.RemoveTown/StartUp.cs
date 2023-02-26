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
        string result = RemoveTown(dbContext);
        Console.WriteLine(result);
    }

    //Problem 15
    public static string RemoveTown(SoftUniContext context)
    {
        Town townToDelete = context.Towns
            .FirstOrDefault(t => t.Name == "Seattle");

        Address[] addressesToDelete = context.Addresses
            .Where(t => t.TownId == townToDelete.TownId)
            .ToArray();
        foreach (var e in context.Employees)
        {
            if (addressesToDelete.Any(a => a.AddressId == e.AddressId))
            {
                e.AddressId = null;
            }
        }
        int countOfAddressesToDelete = addressesToDelete.Length;
        context.Addresses.RemoveRange(addressesToDelete);
        context.Towns.Remove(townToDelete);
        context.SaveChanges();
        return $"{countOfAddressesToDelete} addresses in Seattle were deleted";
    }
}




