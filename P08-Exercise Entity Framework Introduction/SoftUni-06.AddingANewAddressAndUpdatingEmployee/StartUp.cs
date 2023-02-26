namespace SoftUni;

using System.Text;

using Data;
using Microsoft.EntityFrameworkCore;
using Models;

public class StartUp
{
    static void Main(string[] args)
    {
        SoftUniContext dbContext = new SoftUniContext();
        string result = AddNewAddressToEmployee(dbContext);
        Console.WriteLine(result);
    }

    //Problem 06
    public static string AddNewAddressToEmployee(SoftUniContext context)
    {
        Address newAddress = new Address()
        {
            AddressText = "Vitoshka 15",
            TownId = 4
        };
        //context.Addresses.Add(newAddress);
        Employee? employee = context.Employees
            .FirstOrDefault(e => e.LastName == "Nakov");
        employee.Address = newAddress;
        context.SaveChanges();
        var employeeAddresses = context.Employees
            .OrderByDescending(e => e.AddressId)
            .Take(10)
            .Select(e => e.Address!.AddressText)
            .ToArray();
        return string.Join(Environment.NewLine, employeeAddresses);
    }
}




