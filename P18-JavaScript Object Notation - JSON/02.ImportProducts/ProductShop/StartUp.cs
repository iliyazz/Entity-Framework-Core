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

            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //P01
            //string inputJson = File.ReadAllText(@"../../../Datasets/users.json");
            //string output = ImportUsers(context, inputJson);
            //Console.WriteLine(output);

            //02
            string inputJson = File.ReadAllText(@"../../../Datasets/products.json");
            string output = ImportProducts(context, inputJson);
            Console.WriteLine(output);


        }

        //P02
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            IMapper mapper = createmMapper();
            ImportProductDto[] productDtos = JsonConvert.DeserializeObject<ImportProductDto[]>(inputJson);
            Product[] products = mapper.Map<Product[]>(productDtos);
            context.Products.AddRange(products);
            context.SaveChanges();
            return $"Successfully imported {products.Length}";
        }



        //P01
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            IMapper mapper = createmMapper();
            ImportUserDto[] userDtos = JsonConvert.DeserializeObject<ImportUserDto[]>(inputJson);
            ICollection<User> validUsers = new HashSet<User>();

            //in
            //User[] users =mapper.Map<User>(userDtos)

            foreach (ImportUserDto userDto in userDtos)
            {
                User user = mapper.Map<User>(userDto);
                validUsers.Add(user);
            }

            context.Users.AddRange(validUsers);
            context.SaveChanges();
            return $"Successfully imported {validUsers.Count}";
        }

        private static IMapper createmMapper()
        {
            return new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            }));
        }

    }
}