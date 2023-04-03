namespace Boardgames.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Xml.Serialization;
    using Boardgames.Data;
    using Boardgames.Data.Models;
    using Data.Models.Enums;
    using ImportDto;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCreator
            = "Successfully imported creator – {0} {1} with {2} boardgames.";

        private const string SuccessfullyImportedSeller
            = "Successfully imported seller - {0} with {1} boardgames.";

        public static string ImportCreators(BoardgamesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCreatorDto[]), new XmlRootAttribute("Creators"));
            using StringReader reader = new StringReader(xmlString);
            ImportCreatorDto[] oDtos = (ImportCreatorDto[])xmlSerializer.Deserialize(reader);
            List<Creator> creators = new List<Creator>();

            foreach (var oDto in oDtos)
            {
                if (!IsValid(oDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Creator creator = new Creator()
                {
                    FirstName = oDto.FirstName,
                    LastName = oDto.LastName,
                };

                foreach (var boardDto in oDto.Boardgames)
                {
                    if (!IsValid(boardDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    if (string.IsNullOrEmpty(boardDto.Name))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Boardgame boardgame = new Boardgame()
                    {
                        Name = boardDto.Name,
                        Rating = boardDto.Rating,
                        YearPublished = boardDto.YearPublished,
                        CategoryType = (CategoryType)boardDto.CategoryType,
                        Mechanics = boardDto.Mechanics,
                    };
                    creator.Boardgames.Add(boardgame);
                }
                creators.Add(creator);
                sb.AppendLine(String.Format(SuccessfullyImportedCreator, creator.FirstName, creator.LastName, creator.Boardgames.Count));
            }
            context.Creators.AddRange(creators);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportSellers(BoardgamesContext context, string jsonString)
        {
            var oDtos = JsonConvert.DeserializeObject<ImportSellerDto[]>(jsonString);
            StringBuilder sb = new StringBuilder();
            var sellers = new List<Seller>();
            var existingSellers = context.Boardgames.Select(t => t.Id).ToArray();

            foreach (var oDto in oDtos)
            {
                if (!IsValid(oDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Seller seller = new Seller()
                {
                    Name = oDto.Name,
                    Address = oDto.Address,
                    Country = oDto.Country,
                    Website = oDto.Website,
                };

                foreach (var boardIdDto in oDto.Boardgames.Distinct())
                {
                    if (!existingSellers.Contains(boardIdDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    BoardgameSeller boardgameSeller = new BoardgameSeller()
                    {
                        BoardgameId = boardIdDto
                    };
                    seller.BoardgamesSellers.Add(boardgameSeller);
                }
                sellers.Add(seller);
                sb.AppendLine(String.Format(SuccessfullyImportedSeller, seller.Name, seller.BoardgamesSellers.Count));
            }
            context.Sellers.AddRange(sellers);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
