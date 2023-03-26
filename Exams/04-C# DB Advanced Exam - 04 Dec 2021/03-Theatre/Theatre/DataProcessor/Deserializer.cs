namespace Theatre.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using Common;
    using Data.Models;
    using Data.Models.Enums;
    using ImportDto;
    using Newtonsoft.Json;
    using Theatre.Data;
    using Utilities;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportPlay
            = "Successfully imported {0} with genre {1} and a rating of {2}!";

        private const string SuccessfulImportActor
            = "Successfully imported actor {0} as a {1} character!";

        private const string SuccessfulImportTheatre
            = "Successfully imported theatre {0} with #{1} tickets!";



        public static string ImportPlays(TheatreContext context, string xmlString)
        {
            XmlRootAttribute rootAttribute = new XmlRootAttribute("Plays");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportPlayDto[]), rootAttribute);
            using StringReader reader = new StringReader(xmlString);
            ImportPlayDto[] oDtos = (ImportPlayDto[])xmlSerializer.Deserialize(reader);

            var validGenres = new string[] { "Drama", "Comedy", "Romance", "Musical" };
            //IMapper mapper = InitializeAutoMapper();
            //XmlHelper xmlHelper = new XmlHelper();
            //ImportPlayDto[] oDtos = xmlHelper.Deserialize<ImportPlayDto[]>(xmlString, "Plays");

            StringBuilder sb = new StringBuilder();
            ICollection<Play> plays = new List<Play>();

            foreach (var oDto in oDtos)
            {
                var currentTime = TimeSpan.ParseExact(oDto.Duration, "c", CultureInfo.InvariantCulture);
                var playDurationMinValue = TimeSpan.ParseExact(ValidationConstants.PlayDurationMinValue, "c",
                    CultureInfo.InvariantCulture);

                if (!IsValid(oDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if (currentTime < playDurationMinValue)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if (!validGenres.Contains(oDto.Genre))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var genreResult = (Genre)Enum.Parse(typeof(Genre), oDto.Genre);
                //bool isGenreEnumValid = Enum.TryParse(typeof(Genre), oDto.Genre, out object genreResult);
                //if (!isGenreEnumValid)
                //{
                //    sb.AppendLine(ErrorMessage);
                //    continue;
                //}
                //if (Enum.IsDefined(Genre, oDto.Genre))
                //{

                //}

                //Play play = mapper.Map<Play>(oDto);
                Play play = new Play()
                {
                    Title = oDto.Title,
                    Duration = TimeSpan.ParseExact(oDto.Duration, "c", CultureInfo.InvariantCulture),
                    Rating = oDto.Rating,
                    Genre = (Genre)genreResult,
                    Description = oDto.Description,
                    Screenwriter = oDto.Screenwriter,

                };

                plays.Add(play);
                sb.AppendLine(string.Format(SuccessfulImportPlay, play.Title, play.Genre, play.Rating));
            }
            context.Plays.AddRange(plays);
            context.SaveChanges();
            return sb.ToString().TrimEnd();


        }

        public static string ImportCasts(TheatreContext context, string xmlString)
        {
            XmlRootAttribute rootAttribute = new XmlRootAttribute("Casts");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCastDto[]), rootAttribute);
            using StringReader reader = new StringReader(xmlString);
            ImportCastDto[] oDtos = (ImportCastDto[])xmlSerializer.Deserialize(reader);
            StringBuilder sb = new StringBuilder();
            ICollection<Cast> casts = new List<Cast>();

            foreach (var oDto in oDtos)
            {
                if (!IsValid(oDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Cast cast = new Cast()
                {
                    FullName = oDto.FullName,
                    IsMainCharacter = oDto.IsMainCharacter,
                    PhoneNumber = oDto.PhoneNumber,
                    PlayId = oDto.PlayId
                };
                casts.Add(cast);
                sb.AppendLine(string.Format(SuccessfulImportActor, oDto.FullName,
                    cast.IsMainCharacter ? "main" : "lesser"));
            }
            context.Casts.AddRange(casts);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportTtheatersTickets(TheatreContext context, string jsonString)
        {
            ImportTheatreTicketDto[] oDtos = JsonConvert.DeserializeObject<ImportTheatreTicketDto[]>(jsonString);
            ICollection<Theatre> theatres = new List<Theatre>();
            StringBuilder sb = new StringBuilder();

            foreach (var oDto in oDtos)
            {
                if (!IsValid(oDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Theatre theatre = new Theatre()
                {
                    Name = oDto.Name,
                    NumberOfHalls = oDto.NumberOfHalls,
                    Director = oDto.Director,
                };
                foreach (var oDtoTicket in oDto.Tickets)
                {
                    if (!IsValid(oDtoTicket))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    Ticket ticket = new Ticket()
                    {
                        Price = oDtoTicket.Price,
                        RowNumber = oDtoTicket.RowNumber,
                        PlayId = oDtoTicket.PlayId,
                    };
                    theatre.Tickets.Add(ticket);
                }
                theatres.Add(theatre);
                sb.AppendLine(string.Format(SuccessfulImportTheatre, theatre.Name, theatre.Tickets.Count));
            }
            context.Theatres.AddRange(theatres);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }


        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
        private static IMapper InitializeAutoMapper() => new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<TheatreProfile>();
        }));
    }
}
