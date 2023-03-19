namespace ProductShop
{
    using System.IO;
    using System.Text;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using DTOs.Export;
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
            //string inputXml = File.ReadAllText("../../../Datasets/categories.xml");
            //string output = ImportCategories(context, inputXml);

            //04
            //string inputXml = File.ReadAllText("../../../Datasets/categories-products.xml");
            //string output = ImportCategoryProducts(context, inputXml);

            //05
            //string output = GetProductsInRange(context);
            //File.WriteAllText(@"../../../Results/ProductsInRange.xml", output);

            //06
            //string output = GetSoldProducts(context);
            //File.WriteAllText(@"../../../Results/SoldProducts.xml", output);

            //07
            string output = GetCategoriesByProductsCount(context);
            File.WriteAllText(@"../../../Results/CategoriesByProductsCount.xml", output);
        }

        //07
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            XmlHelper xmlHelper = new XmlHelper();
            IMapper mapper = InitializeAutoMapper();
            ExportCategoriesByProductsCountDto[] productsByCountDtos = context.Categories
                .OrderByDescending(c => c.CategoryProducts.Count)
                .ThenBy(c => c.CategoryProducts.Sum(s => s.Product.Price))
                .ProjectTo<ExportCategoriesByProductsCountDto>(mapper.ConfigurationProvider)
                .ToArray();
            return xmlHelper.Serialize<ExportCategoriesByProductsCountDto[]>(productsByCountDtos, "Categories");
        }

        //06
        public static string GetSoldProducts(ProductShopContext context)
        {
            XmlHelper xmlHelper = new XmlHelper();
            IMapper mapper = InitializeAutoMapper();
            ExportSoldProductDto[] soldProductDto = context.Users
                .Where(s => s.ProductsSold.Count >= 1)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Take(5)
                .ProjectTo<ExportSoldProductDto>(mapper.ConfigurationProvider)
                .ToArray();
            return xmlHelper.Serialize<ExportSoldProductDto[]>(soldProductDto, "Users");
        }

        //05
        public static string GetProductsInRange(ProductShopContext context)
        {
            XmlHelper xmlHelper = new XmlHelper();
            IMapper mapper = InitializeAutoMapper();

            ExportProductsInRangeDto[] productsInRange = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Take(10)
                .ProjectTo<ExportProductsInRangeDto>(mapper.ConfigurationProvider)
                .ToArray();

            return xmlHelper.Serialize<ExportProductsInRangeDto[]>(productsInRange, "Products");
        }

        //04
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            IMapper mapper = InitializeAutoMapper();
            XmlHelper xmlHelper = new XmlHelper();

            XDocument xmlDocument = XDocument.Parse(inputXml);
            string root = xmlDocument.Root.Name.ToString();

            ImportCategoryProductDto[] categoryProductDtos = xmlHelper.Deserialize<ImportCategoryProductDto[]>(inputXml, root);
            ICollection<CategoryProduct> categoryProducts = new HashSet<CategoryProduct>();
            foreach (var categoryProductDto in categoryProductDtos)
            {
                CategoryProduct categoryProduct = mapper.Map<CategoryProduct>(categoryProductDto);
                categoryProducts.Add(categoryProduct);
            }
            context.CategoryProducts.AddRange( categoryProducts);
            context.SaveChanges();
            return $"Successfully imported {categoryProducts.Count}";
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