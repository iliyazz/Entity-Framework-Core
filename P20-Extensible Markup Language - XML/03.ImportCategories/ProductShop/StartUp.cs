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


            //01
            //string inputXml = File.ReadAllText("../../../Datasets/users.xml");
            //string output = ImportUsers(context, inputXml);

            //02
            //string inputXml = File.ReadAllText("../../../Datasets/products.xml");
            //string output = ImportProducts(context, inputXml);

            //03
            string inputXml = File.ReadAllText("../../../Datasets/categories.xml");
            string output = ImportCategories(context, inputXml);
            Console.WriteLine(output);



        }

        //03
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            IMapper mapper = InitializeAutoMapper();
            XmlHelper xmlHelper = new XmlHelper();

            XDocument xmlDocument = XDocument.Parse(inputXml);
            string root = xmlDocument.Root.Name.ToString();

            ImportCategoryDto[] categoryDtos = xmlHelper.Deserialize<ImportCategoryDto[]>(inputXml, root);
            ICollection<Category> categories = new HashSet<Category>();
            foreach (var categoryDto in categoryDtos)
            {
                if (string.IsNullOrEmpty(categoryDto.Name))
                {
                    continue;
                }
                Category category = mapper.Map<Category>(categoryDto);
                categories.Add(category);
            }
            context.Categories.AddRange(categories);
            context.SaveChanges();
            return $"Successfully imported {categories.Count}";
        }

        //02
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            IMapper mapper = InitializeAutoMapper();
            XmlHelper xmlHelper = new XmlHelper();
            ImportProductDto[] productDtos = xmlHelper.Deserialize<ImportProductDto[]>(inputXml, "Products");
            ICollection<Product> products = new HashSet<Product>();
            foreach (var productDto in productDtos)
            {
                if (string.IsNullOrEmpty(productDto.Name))
                {
                    continue;
                }

                Product product = mapper.Map<Product>(productDto);
                products.Add(product);
            }
            context.Products.AddRange(products);
            context.SaveChanges();
            return $"Successfully imported {products.Count}";
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