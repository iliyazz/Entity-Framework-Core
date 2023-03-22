namespace SoftJail.DataProcessor
{
    using System;
    using System.Globalization;
    using Data;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ExportDto;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            XmlRootAttribute xmlRoot = new XmlRootAttribute("Prisoners");
            string[] prisonerNameArray = prisonersNames.Split(',').ToArray();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportPrisonerDto[]), xmlRoot);
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            StringBuilder sb = new StringBuilder();
            using StringWriter writer = new StringWriter(sb);


            //ExportPrisonerDto[] prisoners = context
            //    .Prisoners

            //    .Where(p => prisonerNameArray.Contains(p.FullName))
            //    .ProjectTo<ExportPrisonerDto>(Mapper.Configuration)
            //    .OrderBy(p => p.FullName)
            //    .ThenBy(p => p.Id)
            //    .ToArray();

            ExportPrisonerDto[] prisoners = context
                .Prisoners
                .ToArray()
                .Where(p => prisonerNameArray.Contains(p.FullName))
                .Select(p => new ExportPrisonerDto()
                {
                    Id = p.Id,
                    FullName = p.FullName,
                    IncarcerationDate = p.IncarcerationDate.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture),
                    Mails = p.Mails
                        .Select(m => new ExportPrisonerMailsDto()
                        {
                            Description = String.Join("", m.Description.Reverse())
                        })
                        .ToArray()
                })
                .OrderBy(p => p.FullName)
                .ThenBy(p => p.Id)
                .ToArray();



            xmlSerializer.Serialize(writer, prisoners, namespaces);
            return sb.ToString().TrimEnd();
        }

        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisoners = context
                .Prisoners
                .Where(p => ids.Contains(p.Id))
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.FullName,
                    CellNumber = p.Cell.CellNumber,
                    Officers = p.PrisonerOfficers
                        .Select(po => new
                        {
                            OfficerName = po.Officer.FullName,
                            Department = po.Officer.Department.Name,
                        })
                        .OrderBy(o => o.OfficerName)
                        .ToArray(),
                    TotalOfficerSalary = decimal.Parse(p.PrisonerOfficers.Sum(po => po.Officer.Salary).ToString("f2"))
                })
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id)
                .ToArray();
            string output = JsonConvert.SerializeObject(prisoners, Formatting.Indented);
            return output;
        }
    }
}