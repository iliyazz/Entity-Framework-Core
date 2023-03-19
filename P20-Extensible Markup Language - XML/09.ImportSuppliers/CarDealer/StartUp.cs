namespace CarDealer
{
    using AutoMapper;
    using Data;
    using DTOs.Import;
    using Models;
    using Utilities;

    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext context = new CarDealerContext();

            //01
            string inputXml = File.ReadAllText("../../../Datasets/suppliers.xml");
            string output = ImportSuppliers(context, inputXml);
            Console.WriteLine(output);
        }


        //01
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            IMapper mapper = InitializeAutoMapper();
            XmlHelper xmlHelper = new XmlHelper();
            ImportSupplierDto[] supplierDtos = xmlHelper.Deserialize<ImportSupplierDto[]>(inputXml, "Suppliers");
            ICollection<Supplier> suppliers = new HashSet<Supplier>();
            foreach (var supplierDto in supplierDtos)
            {
                if (string.IsNullOrEmpty(supplierDto.Name))
                {
                    continue;
                }

                Supplier supplier = mapper.Map<Supplier>(supplierDto);
                suppliers.Add(supplier);
            }
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
            return $"Successfully imported {suppliers.Count}";
        }


        private static IMapper InitializeAutoMapper() => new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CarDealerProfile>();
        }));
    }
}