namespace ProductShop
{
    using System.IO;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using AutoMapper;
    using DTOs.Import;
    using Models;
    using ProductShop.Data;
    using Utilities;

    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext context = new ProductShopContext();
            string inputXml = File.ReadAllText("../../../Datasets/users.xml");

            //01
            string output = ImportUsers(context, inputXml);
            Console.WriteLine(output);
        }

        //01
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            IMapper mapper = InitializeAutoMapper();

            XmlHelper xmlHelper = new XmlHelper();
            ImportUserDto[] userDtos = xmlHelper.Deserialize<ImportUserDto[]>(inputXml, "Users");
            ICollection<User> users = new HashSet<User>();
            foreach (var userDto in userDtos)
            {
                if (string.IsNullOrEmpty(userDto.FirstName) || string.IsNullOrEmpty(userDto.LastName))
                {
                    continue;
                }
                ////Manual mapping
                //User user = new User()
                //{
                //    FirstName = userDto.FirstName,
                //    LastName = userDto.LastName,
                //    Age = userDto.Age,
                //};
                //users.Add(user);

                //Auto mapping
                User user = mapper.Map<User>(userDto);
                users.Add(user);

            }
            context.Users.AddRange(users);
            context.SaveChanges();
            return $"Successfully imported {users.Count}";
        }

        private static IMapper InitializeAutoMapper() => new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ProductShopProfile>();
        }));
    }
}