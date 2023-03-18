namespace CarDealer
{
    using System.Collections;
    using System.Globalization;
    using AutoMapper;
    using Castle.Core.Resource;
    using Data;
    using DTOs.Import;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System.IO;

    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext context = new CarDealerContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
            ////09
            //string inputJson = File.ReadAllText(@"../../../Datasets/suppliers.json");
            //ImportSuppliers(context, inputJson);

            ////10
            //inputJson = File.ReadAllText(@"../../../Datasets/parts.json");
            //ImportParts(context, inputJson);

            ////11
            //inputJson = File.ReadAllText(@"../../../Datasets/cars.json");
            //ImportCars(context, inputJson);

            ////12
            //inputJson = File.ReadAllText(@"../../../Datasets/customers.json");
            //ImportCustomers(context, inputJson);

            ////13
            //inputJson = File.ReadAllText(@"../../../Datasets/sales.json");
            //string output = ImportSales(context, inputJson);
            //Console.WriteLine(output);

            //14
            //string output = GetOrderedCustomers(context);
            //File.WriteAllText(@"../../../Results/orderedCustomers14.json", output);

            //15
            //string output = GetCarsFromMakeToyota(context);
            //File.WriteAllText(@"../../../Results/CarsFromMakeToyota15.json", output);

            //16
            string output = GetLocalSuppliers(context);
            File.WriteAllText(@"../../../Results/LocalSuppliers16.json", output);
        }



        //16
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var orderedSuppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count,
                })
                .ToArray();
            return JsonConvert.SerializeObject(orderedSuppliers, Formatting.Indented);
        }

        //15
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var orderedCarsToyota = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TraveledDistance)
                .Select(c => new
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    TraveledDistance = c.TraveledDistance 
                })
                .ToArray();
            return JsonConvert.SerializeObject(orderedCarsToyota, Formatting.Indented);
        }

        //14
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var orderedCustomers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(c => new
                {
                    Name = c.Name,
                    BirthDate = c.BirthDate.ToString(@"dd/MM/yyyy", CultureInfo.InvariantCulture),
                    IsYoungDriver = c.IsYoungDriver,
                })
                .ToArray();
                return JsonConvert.SerializeObject(orderedCustomers, Formatting.Indented);
        }

        //13
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var mapper = CreateMapper();
            ImportSaleDto[] importSaleDtos = JsonConvert.DeserializeObject<ImportSaleDto[]>(inputJson);
            ICollection<Sale> sales = new HashSet<Sale>();
            foreach (var importSaleDto in importSaleDtos)
            {
                Sale sale = mapper.Map<Sale>(importSaleDto);
                sales.Add(sale);
            }
            context.AddRange(sales);
            context.SaveChanges();
            return $"Successfully imported {sales.Count}.";
        }

        //12
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var mapper = CreateMapper();
            ImportCustomerDto[] importCustomerDtos = JsonConvert.DeserializeObject<ImportCustomerDto[]>(inputJson);
            ICollection<Customer> customers = new HashSet<Customer>();
            foreach (ImportCustomerDto importCustomerDto in importCustomerDtos)
            {
                Customer customer = mapper.Map<Customer>(importCustomerDto);
                customers.Add(customer);
            }
            context.AddRange(customers);
            context.SaveChanges();
            return $"Successfully imported {customers.Count}.";
        }


        //11
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var mapper = CreateMapper();
            ImportCarDto[] importCarDtos = JsonConvert.DeserializeObject<ImportCarDto[]>(inputJson);
            ICollection<Car> validCars = new HashSet<Car>();
            ICollection<PartCar> validParts = new HashSet<PartCar>();
            foreach (var importCarDto in importCarDtos)
            {
                Car car = mapper.Map<Car>(importCarDto);
                validCars.Add(car);

                foreach (var partId in importCarDto.PartsId.Distinct())
                {
                    PartCar partCar = new PartCar()
                    {
                        Car = car,
                        PartId = partId,
                    };
                    validParts.Add(partCar);
                }
            }
            context.Cars.AddRange(validCars);
            context.PartsCars.AddRange(validParts);
            context.SaveChanges();
            return $"Successfully imported {validCars.Count}.";
        }

        //10
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var mapper = CreateMapper();
            ImportPartDto[] importPartDtos = JsonConvert.DeserializeObject<ImportPartDto[]>(inputJson);
            ICollection<Part> validParts = new HashSet<Part>();
            foreach (var importPartDto in importPartDtos)
            {
                if (!context.Suppliers.Any(s => s.Id == importPartDto.SupplierId))
                {
                    continue;
                }

                Part part = mapper.Map<Part>(importPartDto);
                validParts.Add(part);
            }
            context.Parts.AddRange(validParts);
            context.SaveChanges();
            return $"Successfully imported {validParts.Count}.";
        }

        //09
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var mapper = CreateMapper();
            ImportSupplierDto[] importSupplierDtos = JsonConvert.DeserializeObject<ImportSupplierDto[]>(inputJson);
            ICollection<Supplier> validSuppliers = new HashSet<Supplier>();
            foreach (var supplierDto in importSupplierDtos)
            {
                Supplier supplier = mapper.Map<Supplier>(supplierDto);
                validSuppliers.Add(supplier);
            }
            context.Suppliers.AddRange(validSuppliers);
            context.SaveChanges();
            return $"Successfully imported {validSuppliers.Count}.";
        }

        private static IMapper CreateMapper()
        {
            return new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            }));
        }

        //private static IContractResolver ConfigureCamelCasingNaming()
        //{
        //    return new DefaultContractResolver()
        //    {
        //        NamingStrategy = new CamelCaseNamingStrategy(false, true)
        //    };
        //}


    }
}