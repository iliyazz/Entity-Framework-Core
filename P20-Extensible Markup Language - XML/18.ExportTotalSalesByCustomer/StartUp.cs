namespace CarDealer
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using DTOs.Export;
    using DTOs.Import;
    using DTOs.Import.ImportCar;
    using Models;
    using System.Xml.Linq;
    using DTOs.Export.CarsWithTheirListOfParts;
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
            //string inputXml = File.ReadAllText("../../../Datasets/cars.xml");
            //string output = ImportCars(context, inputXml);

            //12
            //string inputXml = File.ReadAllText("../../../Datasets/customers.xml");
            //string output = ImportCustomers(context, inputXml);

            //13
            //string inputXml = File.ReadAllText("../../../Datasets/sales.xml");
            //string output = ImportSales(context, inputXml);
            //Console.WriteLine(output);

            //14
            //string output = GetCarsWithDistance( context);
            //File.WriteAllText(@"../../../Results/CarsWithDistance.xml", output);

            //15
            //string output = GetCarsFromMakeBmw(context);
            //File.WriteAllText(@"../../../Results/CarsFromMakeBmw.xml", output);

            //16
            //string output = GetLocalSuppliers(context);
            //File.WriteAllText(@"../../../Results/LocalSuppliers.xml", output);

            //17
            //string output = GetCarsWithTheirListOfParts(context);
            //File.WriteAllText(@"../../../Results/CarsWithTheirListOfParts.xml", output);

            //18
            string output = GetTotalSalesByCustomer(context);
            File.WriteAllText(@"../../../Results/TotalSalesByCustomer.xml", output);
        }

        //18
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            IMapper mapper = InitializeAutoMapper();
            XmlHelper xmlHelper = new XmlHelper();
   
            var customers = context.Customers
                .Where(c => c.Sales.Any())
                .Select(c => new
                {
                    Name = c.Name,
                    BoughtCars = c.Sales.Count(),
                    SpentMoney1 = c.Sales.Select(s => new
                        {
                            Prices = c.IsYoungDriver
                                ? s.Car.PartsCars.Sum(pc => Math.Round((double)pc.Part.Price * 0.95, 2))
                                : s.Car.PartsCars.Sum(pc => (double)pc.Part.Price)
                        })
                        .ToArray()
                })
                .ToArray();
            ExportTotalSalesByCustomerDto[] total = customers.OrderByDescending(x => x.SpentMoney1.Sum(y => y.Prices))
                .Select(x => new ExportTotalSalesByCustomerDto()
                {
                    Name = x.Name,
                    BoughtCars = x.BoughtCars,
                    SpentMoney = Math.Round((decimal)x.SpentMoney1.Sum(y => y.Prices), 2),
                })
                .ToArray();
            return xmlHelper.Serialize(total, "customers");
        }

        //17
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            IMapper mapper = InitializeAutoMapper();
            XmlHelper xmlHelper = new XmlHelper();
            ExportCarsWithTheirListOfPartsDto[] carsWithTheirListOfParts = context.Cars
                .OrderByDescending(c => c.TraveledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .ProjectTo<ExportCarsWithTheirListOfPartsDto>(mapper.ConfigurationProvider)
                .ToArray();
            return xmlHelper.Serialize(carsWithTheirListOfParts, "cars");
        }

        //16
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            IMapper mapper = InitializeAutoMapper();
            XmlHelper xmlHelper = new XmlHelper();
            ExportLocalSuppliersDto[] localSuppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .ProjectTo<ExportLocalSuppliersDto>(mapper.ConfigurationProvider)
                .ToArray();
            return xmlHelper.Serialize<ExportLocalSuppliersDto>(localSuppliers, "suppliers");
        }

        //15
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            IMapper mapper = InitializeAutoMapper();
            XmlHelper xmlHelper = new XmlHelper();
            ExportCarsFromMakeBmwDto[] carsFromMakeBmw = context.Cars
                .Where(c => c.Make == "BMW")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TraveledDistance)
                .ProjectTo<ExportCarsFromMakeBmwDto>(mapper.ConfigurationProvider)
                .ToArray();
            return xmlHelper.Serialize<ExportCarsFromMakeBmwDto>(carsFromMakeBmw, "cars");
        }
        
        //14
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            IMapper mapper = InitializeAutoMapper();
            XmlHelper xmlHelper = new XmlHelper();
            ExportCarsWithDistanceDto[] carsWithDistance = context.Cars
                .Where(c => c.TraveledDistance > 2000000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .ProjectTo<ExportCarsWithDistanceDto>(mapper.ConfigurationProvider)
                .ToArray();
            return xmlHelper.Serialize<ExportCarsWithDistanceDto>(carsWithDistance, "cars");
        }

        //13
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            IMapper mapper = InitializeAutoMapper();
            XmlHelper xmlHelper = new XmlHelper();

            XDocument xmlDocument = XDocument.Parse(inputXml);
            string root = xmlDocument.Root.Name.ToString();

            ImportSaleDto[] saleDtos = xmlHelper.Deserialize<ImportSaleDto[]>(inputXml,root);
            ICollection<int> existingCarIdInDb = context.Cars.Select(c => c.Id).ToArray();
            ICollection<Sale> sales = new HashSet<Sale>();
            foreach (var saleDto in saleDtos)
            {
                if (!saleDto.CarId.HasValue || existingCarIdInDb.All(id => id != saleDto.CarId))
                {
                    continue;
                }

                Sale sale = mapper.Map<Sale>(saleDto);
                sales.Add(sale);
            }
            context.Sales.AddRange(sales);
            context.SaveChanges();
            return $"Successfully imported {sales.Count}";
        }

        //12
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            IMapper mapper = InitializeAutoMapper();
            XmlHelper xmlHelper = new XmlHelper();

            XDocument xmlDocument = XDocument.Parse(inputXml);
            string root = xmlDocument.Root.Name.ToString();

            ImportCustomerDto[] customerDtos = xmlHelper.Deserialize<ImportCustomerDto[]>(inputXml, root);
            ICollection<Customer> customers = new HashSet<Customer>();
            foreach (var customerDto in customerDtos)
            {
                if (string.IsNullOrEmpty(customerDto.Name) || string.IsNullOrEmpty((customerDto.BirthDate)))
                {
                    continue;
                }
                Customer customer = mapper.Map<Customer>(customerDto);
                customers.Add(customer);
            }
            context.Customers.AddRange(customers);
            context.SaveChanges();
            return $"Successfully imported {customers.Count}";
        }

        //11
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            IMapper mapper = InitializeAutoMapper();
            XmlHelper xmlHelper = new XmlHelper();

            XDocument xmlDocument = XDocument.Parse(inputXml);
            string root = xmlDocument.Root.Name.ToString();

            ImportCarDto[] carDtos = xmlHelper.Deserialize<ImportCarDto[]>(inputXml, root);
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

            XDocument xmlDocument = XDocument.Parse(inputXml);
            string root = xmlDocument.Root.Name.ToString();

            ImportPartDto[] partDtos = xmlHelper.Deserialize<ImportPartDto[]>(inputXml, root);
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

            XDocument xmlDocument = XDocument.Parse(inputXml);
            string root = xmlDocument.Root.Name.ToString();

            ImportSupplierDto[] supplierDtos = xmlHelper.Deserialize<ImportSupplierDto[]>(inputXml, root);
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