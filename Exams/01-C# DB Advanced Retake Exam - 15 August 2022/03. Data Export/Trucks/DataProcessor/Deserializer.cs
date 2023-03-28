namespace Trucks.DataProcessor;

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using Castle.Core.Internal;
using Data;
using Data.Models;
using Data.Models.Enums;
using ImportDto;
using Newtonsoft.Json;

public class Deserializer
{
    private const string ErrorMessage = "Invalid data!";

    private const string SuccessfullyImportedDespatcher
        = "Successfully imported despatcher - {0} with {1} trucks.";

    private const string SuccessfullyImportedClient
        = "Successfully imported client - {0} with {1} trucks.";

    public static string ImportDespatcher(TrucksContext context, string xmlString)
    {
        StringBuilder sb = new StringBuilder();
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportDespatcherDto[]), new XmlRootAttribute("Despatchers"));
        using StringReader reader = new StringReader(xmlString);
        ImportDespatcherDto[] oDtosD = (ImportDespatcherDto[])xmlSerializer.Deserialize(reader);
        List<Despatcher> despatchers = new List<Despatcher>();

        foreach (var oDtoD in oDtosD)
        {
            if (!IsValid(oDtoD))
            {
                sb.AppendLine(ErrorMessage);
                continue;
            }

            if (string.IsNullOrEmpty(oDtoD.Position))
            {
                sb.AppendLine(ErrorMessage);
                continue;
            }
            Despatcher despatcher = new Despatcher()
            {
                Name = oDtoD.Name,
                Position = oDtoD.Position,
            };

            foreach (var truckDto in oDtoD.Trucks)
            {
                if (!IsValid(truckDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Truck truck = new Truck()
                {
                    RegistrationNumber = truckDto.RegistrationNumber,
                    VinNumber = truckDto.VinNumber,
                    TankCapacity = truckDto.TankCapacity,
                    CargoCapacity = truckDto.CargoCapacity,
                    CategoryType = (CategoryType)truckDto.CategoryType,
                    MakeType = (MakeType)truckDto.MakeType
                };
                despatcher.Trucks.Add(truck);
            }
            despatchers.Add(despatcher);
            sb.AppendLine(string.Format(SuccessfullyImportedDespatcher, oDtoD.Name, despatcher.Trucks.Count));
        }
        context.Despatchers.AddRange(despatchers);
        context.SaveChanges();
        return sb.ToString().TrimEnd();
    }
    public static string ImportClient(TrucksContext context, string jsonString)
    {
        var oDtos = JsonConvert.DeserializeObject<ImportClientDto[]>(jsonString);
        StringBuilder sb = new StringBuilder();
        var clients = new List<Client>();
        var existingTrucks = context.Trucks.Select(t => t.Id).ToArray();
        foreach (var oDto in oDtos)
        {
            if (!IsValid(oDto))
            {
                sb.AppendLine(ErrorMessage);
                continue;
            }

            if (oDto.Type == "usual")
            {
                sb.AppendLine(ErrorMessage);
                continue;
            }

            Client client = new Client()
            {
                Name = oDto.Name,
                Nationality = oDto.Nationality,
                Type = oDto.Type,
            };

            foreach (var id in oDto.Trucks.Distinct())
            {
                if (!existingTrucks.Contains(id))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                ClientTruck clientTruck = new ClientTruck()
                {
                    TruckId = id
                };
                client.ClientsTrucks.Add(clientTruck);
            }
            clients.Add(client);
            sb.AppendLine(String.Format(SuccessfullyImportedClient, client.Name, client.ClientsTrucks.Count));
        }
        context.Clients.AddRange(clients);
        context.SaveChanges();
        return  sb.ToString().TrimEnd();
    }

    private static bool IsValid(object dto)
    {
        var validationContext = new ValidationContext(dto);
        var validationResult = new List<ValidationResult>();

        return Validator.TryValidateObject(dto, validationContext, validationResult, true);
    }
}