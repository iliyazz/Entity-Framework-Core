namespace CarDealer
{
    using AutoMapper;
    using Data;
    using DTOs.Import;
    using DTOs.Import.NewFolder;
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
            //string inputXml = File.ReadAllText("../../../Datasets/parts.xml");
            //string output = ImportParts(context, inputXml);

            //11
            string inputXml = File.ReadAllText("../../../Datasets/cars.xml");
            string output = ImportCars(context, inputXml);



            Console.WriteLine(output);
        }

        //11
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            IMapper mapper = InitializeAutoMapper();
            XmlHelper xmlHelper = new XmlHelper();
            ImportCarDto[] carDtos = xmlHelper.Deserialize<ImportCarDto[]>(inputXml, "Cars");
            ICollection<Car> cars = new HashSet<Car>();
            foreach (var carDto in carDtos)
            {
                if (string.IsNullOrEmpty(carDto.Make) || string.IsNullOrEmpty(carDto.Model))
                {
                    continue;
                }
                Car car = mapper.Map<Car>(carDto);
                foreach (var partDto in carDto.Parts.DistinctBy(p => p.PartId))
                {
                    if (!context.Parts.Any(p => p.Id == partDto.PartId))
                    {
                        continue;
                    }

                    PartCar partCar = new PartCar()
                    {
                        PartId = partDto.PartId,
                    };
                    car.PartsCars.Add(partCar);
                }
                cars.Add(car);
            }
            context.Cars.AddRange(cars);
            context.SaveChanges();
            return $"Successfully imported {cars.Count}";
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