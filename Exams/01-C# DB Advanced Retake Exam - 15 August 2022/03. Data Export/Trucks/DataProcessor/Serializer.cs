namespace Trucks.DataProcessor;

using Data;
using System.Xml.Linq;
using Newtonsoft.Json;
using Trucks.Data.Models.Enums;
using System.Text;
using System.Xml.Serialization;
using ExportDto;

public class Serializer
{
    public static string ExportDespatchersWithTheirTrucks(TrucksContext context)
    {
        XmlRootAttribute xmlRoot = new XmlRootAttribute("Despatchers");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportDespatcherDto[]), xmlRoot);

        XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
        namespaces.Add(string.Empty, string.Empty);

        StringBuilder sb = new StringBuilder();
        using StringWriter writer = new StringWriter(sb);
        var despatchers = context
            .Despatchers
            .Where(d => d.Trucks.Any())
            .Select(d => new ExportDespatcherDto()
            {
                DespatcherName = d.Name,
                TrucksCount = d.Trucks.Count,
                Trucks = d.Trucks
                    .Select(t => new ExportDespatcherTruckDto()
                    {
                        RegistrationNumber = t.RegistrationNumber,
                        Make = t.MakeType.ToString()
                    })
                    .OrderBy(t => t.RegistrationNumber)
                    .ToArray()
            })
            .OrderByDescending(d => d.TrucksCount)
            .ThenBy(d => d.DespatcherName)
            .ToArray();
        xmlSerializer.Serialize(writer, despatchers, namespaces);
        return sb.ToString().TrimEnd();
    }

    public static string ExportClientsWithMostTrucks(TrucksContext context, int capacity)
    {
        var clientsWithMostTrucks = context
            .Clients
            .Where(c => c.ClientsTrucks.Any(ct => ct.Truck.TankCapacity >= capacity))
            .ToArray()
            .Select(c => new
            {
                Name = c.Name,
                Trucks = c.ClientsTrucks
                    .Where(ct => ct.Truck.TankCapacity >= capacity)
                    .Select(ct => new
                    {
                        TruckRegistrationNumber = ct.Truck.RegistrationNumber,
                        VinNumber = ct.Truck.VinNumber,
                        TankCapacity = ct.Truck.TankCapacity,
                        CargoCapacity = ct.Truck.CargoCapacity,
                        CategoryType = ct.Truck.CategoryType.ToString(),
                        MakeType = ct.Truck.MakeType.ToString(),
                    })
                    .OrderBy(ct => ct.MakeType)
                    .ThenByDescending(ct => ct.CargoCapacity)
                    .ToArray()
            })
            .OrderByDescending(c => c.Trucks.Length)
            .ThenBy(c => c.Name)
            .Take(10)
            .ToArray();
        string output = JsonConvert.SerializeObject(clientsWithMostTrucks, Formatting.Indented);
        return output;
    }
}