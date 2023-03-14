namespace ProductShop
{
    using System.Runtime.CompilerServices;
    using AutoMapper;
    using Data;
    using DTOs.Import;
    using Models;
    using Newtonsoft.Json;

    public class StartUp
    {

        public static void Main()
        {


            ProductShopContext context = new ProductShopContext();
            string inputJson = File.ReadAllText(@"../../../Datasets/users.json");

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            string output = ImportUsers(context, inputJson);
            Console.WriteLine(output);
        }


        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            IMapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            }));
            ImportUserDto[] userDtos = JsonConvert.DeserializeObject<ImportUserDto[]>(inputJson);
            ICollection<User> validUsers = new HashSet<User>();

            foreach (ImportUserDto userDto in userDtos)
            {
                User user = mapper.Map<User>(userDto);
                validUsers.Add(user);
            }

            context.Users.AddRange(validUsers);
            context.SaveChanges();
            return $"Successfully imported {validUsers.Count}";
        }
    }
}