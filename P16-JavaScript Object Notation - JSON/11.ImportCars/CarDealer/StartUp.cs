namespace CarDealer
{
    using AutoMapper;
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
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            //09
            string inputJson = File.ReadAllText(@"../../../Datasets/suppliers.json");
            ImportSuppliers(context, inputJson);
            //string output = ImportSuppliers(context, inputJson);
            //Console.WriteLine(output);

            //10
            inputJson = File.ReadAllText(@"../../../Datasets/parts.json");
            ImportParts(context, inputJson);
            //string output = ImportParts(context, inputJson);
            //Console.WriteLine(output);

            //11
            inputJson = File.ReadAllText(@"../../../Datasets/cars.json");


            string output = ImportCars(context, inputJson);
            Console.WriteLine(output);

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


        //public static string ImportCars(CarDealerContext context, string inputJson)
        //{

        //    var carsParts = JsonConvert.DeserializeObject<List<ImportCarDto>>(inputJson);
        //    ICollection<Car> validCars = new HashSet<Car>();

        //    ICollection<PartCar> validParts = new HashSet<PartCar>();

        //    foreach (var dto in carsParts)
        //    {
        //        Car car = new Car()
        //        {
        //            Make = dto.Make,
        //            Model = dto.Model,
        //            TravelledDistance = dto.TravelledDistance
        //        };
        //        validCars.Add(car);

        //        foreach (var partId in dto.PartsId.Distinct())
        //        {
        //            PartCar partCar = new PartCar()
        //            {
        //                Car = car,
        //                PartId = partId
        //            };
        //            validParts.Add(partCar);
        //        }

        //    }

        //    context.Cars.AddRange(validCars);
        //    context.PartsCars.AddRange(validParts);
        //    context.SaveChanges();
        //    return $"Successfully imported {validCars.Count}.";
        //}


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