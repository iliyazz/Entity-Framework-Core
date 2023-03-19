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

            //09
            //string inputXml = File.ReadAllText("../../../Datasets/suppliers.xml");
            //string output = ImportSuppliers(context, inputXml);

            //10
            string inputXml = File.ReadAllText("../../../Datasets/parts.xml");
            string output = ImportParts(context, inputXml);





            Console.WriteLine(output);
        }
        //10
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            IMapper mapper = InitializeAutoMapper();
            XmlHelper xmlHelper = new XmlHelper();
            ImportPartDto[] partDtos = xmlHelper.Deserialize<ImportPartDto[]>(inputXml, "Parts");
            ICollection<Part> parts = new HashSet<Part>();
            foreach (var partDto in partDtos)
            {
                if (string.IsNullOrEmpty(partDto.Name))
                {
                    continue;
                }

                if (!partDto.SupplierId.HasValue || !context.Suppliers.Any(s => s.Id == partDto.SupplierId))
                {
                    continue;
                }
                Part part = mapper.Map<Part>(partDto);
                parts.Add(part);
            }
            context.Parts.AddRange(parts);
            context.SaveChanges();
            return $"Successfully imported {parts.Count}";
        }

        //09
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