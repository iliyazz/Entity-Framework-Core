namespace Theatre.DataProcessor
{

    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Numerics;
    using System.Text;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using ExportDto;
    using Newtonsoft.Json;
    using Theatre.Data;
    using Theatre.Data.Models;
    using Theatre.Data.Models.Enums;

    public class Serializer
    {
        public static string ExportTheatres(TheatreContext context, int numbersOfHalls)
        {
            var theatres = context
                .Theatres
                .Where(t => t.NumberOfHalls >= numbersOfHalls & t.Tickets.Count >= 20)
                .Select(t => new
                {
                    Name = t.Name,
                    Halls = t.NumberOfHalls,
                    TotalIncome = t.Tickets.Where(tr => tr.RowNumber >= 1 & tr.RowNumber <= 5).Sum(tp => tp.Price),
                    Tickets = t.Tickets
                        .Where(tr => tr.RowNumber >= 1 & tr.RowNumber <= 5)
                        .Select(p => new
                        {
                            Price = p.Price,
                            RowNumber = p.RowNumber,
                        })
                        .OrderByDescending(p => p.Price)
                        .ToArray()
                })
                .OrderByDescending(t => t.Halls)
                .ThenBy(p => p.Name)
                .ToArray();
            string output = JsonConvert.SerializeObject(theatres, Formatting.Indented);
            return output;
        }

        public static string ExportPlays(TheatreContext context, double raiting)
        {
            XmlRootAttribute xmlRoot = new XmlRootAttribute("Plays");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportPlayCastDto[]), xmlRoot);
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            StringBuilder sb = new StringBuilder();
            using StringWriter writer = new StringWriter(sb);

            ExportPlayCastDto[] plays = context
                .Plays
                .Where(p => p.Rating <= raiting)
                .OrderBy(p => p.Title)
                .ThenByDescending(p => p.Genre)
                .Select(p => new ExportPlayCastDto()
                {
                    Title = p.Title,
                    Duration = p.Duration.ToString("c", CultureInfo.InvariantCulture),
                    Rating = p.Rating == 0 ? "Premier" : p.Rating.ToString(),
                    Genre = p.Genre.ToString(),
                    Actors = p.Casts
                        .Where(a => a.IsMainCharacter)
                        .Select(a => new ExportCastDto()
                        {
                            FullName = a.FullName,
                            MainCharacter = $"Plays main character in '{p.Title}'."
                        })
                        .OrderByDescending(a => a.FullName)
                        .ToArray()
                })

                .ToArray();
            xmlSerializer.Serialize(writer,plays, namespaces);
            return sb.ToString().TrimEnd();
        }
    }
}
