namespace Footballers.DataProcessor
{
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using ExportDto;
    using Footballers.Data.Models.Enums;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportCoachesWithTheirFootballers(FootballersContext context)
        {
            XmlRootAttribute xmlRoot = new XmlRootAttribute("Coaches");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportCoachDto[]), xmlRoot);

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            StringBuilder sb = new StringBuilder();
            using StringWriter writer = new StringWriter(sb);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FootballersProfile>();
            });

            ExportCoachDto[] coaches = context
                .Coaches
                //.ToArray()
                .Where(c => c.Footballers.Any())
                .ProjectTo<ExportCoachDto>(config)
                .OrderByDescending(c => c.FootballersCount)
                .ThenBy(c => c.Name)
                .ToArray();
            xmlSerializer.Serialize(writer, coaches, namespaces);
            return sb.ToString().TrimEnd();

        }

        public static string ExportTeamsWithMostFootballers(FootballersContext context, DateTime date)
        {
            var teams = context.Teams
                .Where(t => t.TeamsFootballers.Any(f => f.Footballer.ContractStartDate >= date))
                .ToArray()
                .Select(t => new
                {
                    Name = t.Name,
                    Footballers = t.TeamsFootballers
                        .Where(tf => tf.Footballer.ContractStartDate >= date)
                        .ToArray()
                        .OrderByDescending(tf => tf.Footballer.ContractEndDate)
                        .ThenBy(tf => tf.Footballer.Name)
                        .Select(tf => new
                        {
                            FootballerName = tf.Footballer.Name,
                            ContractStartDate = tf.Footballer.ContractStartDate.ToString("d", CultureInfo.InvariantCulture),
                            ContractEndDate = tf.Footballer.ContractEndDate.ToString("d", CultureInfo.InvariantCulture),
                            BestSkillType = tf.Footballer.BestSkillType.ToString(),
                            PositionType = tf.Footballer.PositionType.ToString(),
                        })

                        .ToArray()
                })
                .OrderByDescending(t => t.Footballers.Length)
                .ThenBy(t => t.Name)
                .Take(5)
                .ToArray();
            string output = JsonConvert.SerializeObject(teams, Formatting.Indented);
            return output;
        }
    }
}
