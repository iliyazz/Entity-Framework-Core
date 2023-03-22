namespace SoftJail.DataProcessor
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;

    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using ImportDto;

    using Newtonsoft.Json;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            //StringBuilder sb = new StringBuilder();

            //ImportDepartmentWithCellsDto[] departmentWithCellsDtos = JsonConvert.DeserializeObject<ImportDepartmentWithCellsDto[]>(jsonString);

            //ICollection<Department> validDepartments = new List<Department>();

            //foreach (ImportDepartmentWithCellsDto depDto in departmentWithCellsDtos)
            //{
            //    if (!IsValid(depDto))
            //    {
            //        sb.AppendLine("Invalid Data");
            //        continue;
            //    }

            //    Department department = new Department()
            //    {
            //        Name = depDto.Name
            //    };

            //    bool isDepValid = true;
            //    foreach (ImportDepartmentCellDto cellDto in depDto.Cells)
            //    {
            //        if (!IsValid(cellDto))
            //        {
            //            isDepValid = false;
            //            break;
            //        }

            //        department.Cells.Add(new Cell()
            //        {
            //            CellNumber = cellDto.CellNumber,
            //            HasWindow = cellDto.HasWindow
            //        });
            //    }

            //    if (!isDepValid)
            //    {
            //        sb.AppendLine("Invalid Data");
            //        continue;
            //    }

            //    if (department.Cells.Count == 0)
            //    {
            //        sb.AppendLine("Invalid Data");
            //        continue;
            //    }

            //    validDepartments.Add(department);
            //    sb.AppendLine(String.Format($"Imported {department.Name} with {department.Cells.Count} cells"));
            //}

            //context.Departments.AddRange(validDepartments);
            //context.SaveChanges();

            //return sb.ToString().TrimEnd();







            ImportDepartmentWithCellsDto[] departmentWithCellsDtos =
                JsonConvert.DeserializeObject<ImportDepartmentWithCellsDto[]>(jsonString);
            ICollection<Department> validDepartments = new List<Department>();
            StringBuilder sb = new StringBuilder();
            foreach (var depDto in departmentWithCellsDtos)
            {





                if (!IsValid(depDto))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }


                if (!depDto.Cells.Any())
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                Department department = new Department()
                {
                    Name = depDto.Name,
                };

                if (depDto.Cells.Any(c => !IsValid(c)))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                foreach (var cellDto in depDto.Cells)
                {
                    //Cell cell = Mapper.Map<Cell>(cellDto);
                    //department.Cells.Add(cell);

                    department.Cells.Add(new Cell()
                    {
                        CellNumber = cellDto.CellNumber,
                        HasWindow = cellDto.HasWindow
                    });
                }
                validDepartments.Add(department);
                sb.AppendLine($"Imported {department.Name} with {department.Cells.Count} cells");
            }
            context.Departments.AddRange(validDepartments);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            ImportPrisonersWithMailsDto[] prisonerDtos =
                JsonConvert.DeserializeObject<ImportPrisonersWithMailsDto[]>(jsonString);

            ICollection<Prisoner> validPrisoners = new List<Prisoner>();
            foreach (var pDto in prisonerDtos)
            {
                if (!IsValid(pDto))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                if (pDto.Mails.Any(mDto => !IsValid(mDto)))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                bool isIncarcerationDateValid = DateTime.TryParseExact(pDto.IncarcerationDate, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime incarcerationDate);

                if (!isIncarcerationDateValid)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                DateTime? releaseDate = null;
                if (!string.IsNullOrEmpty(pDto.ReleaseDate))
                {
                    bool isReleaseDateValid = DateTime.TryParseExact(pDto.ReleaseDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime releaseDateValue);
                    if (!isReleaseDateValid)
                    {
                        sb.AppendLine("Invalid Data");
                        continue;
                    }
                    releaseDate = releaseDateValue;
                }

                Prisoner prisoner = new Prisoner()
                {
                    FullName = pDto.FullName,
                    Nickname = pDto.Nickname,
                    Age = pDto.Age,
                    IncarcerationDate = incarcerationDate,
                    ReleaseDate = releaseDate,
                    Bail = pDto.Bail,
                    CellId = pDto.CellId,
                };
                foreach (var mDto in pDto.Mails)
                {
                    Mail mail = new Mail()
                    {
                        Description = mDto.Description,
                        Sender = mDto.Sender,
                        Address = mDto.Address,
                    };


                    //Mail mail = Mapper.Map<Mail>(mDto);
                    prisoner.Mails.Add(mail);
                }
                validPrisoners.Add(prisoner);
                sb.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
            }
            context.Prisoners.AddRange(validPrisoners);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlRootAttribute xmlRoot = new XmlRootAttribute("Officers");

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportOfficerWithPrisonersDto[]), xmlRoot);

            using StringReader reader = new StringReader(xmlString);

            ImportOfficerWithPrisonersDto[] oDtos = (ImportOfficerWithPrisonersDto[])xmlSerializer.Deserialize(reader);

            ICollection<Officer> validOfficers = new List<Officer>();

            foreach (var oDto in oDtos)
            {
                if (!IsValid(oDto))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                bool isPositionEnumValid = Enum.TryParse(typeof(Position), oDto.Position, out object positionObjResult);
                if (!isPositionEnumValid)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                bool isWeaponEnumValid = Enum.TryParse(typeof(Weapon), oDto.Weapon, out object weaponObjResult);
                if (!isWeaponEnumValid)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                //if (!context.Departments.Any(d => d.Id == oDto.DepartmentId))
                //{
                //    sb.AppendLine("Invalid Data");
                //    continue;
                //}


                Officer officer = new Officer()
                {
                    FullName = oDto.FullName,
                    Salary = oDto.Salary,
                    Position = (Position)positionObjResult,
                    Weapon = (Weapon)weaponObjResult,
                    DepartmentId = oDto.DepartmentId,
                };
                foreach (var pDto in oDto.Prisoners)
                {
                    OfficerPrisoner op = new OfficerPrisoner()
                    {
                        Officer = officer,
                        PrisonerId = pDto.PrisonerId
                    };
                    officer.OfficerPrisoners.Add(op);
                }
                validOfficers.Add(officer);
                sb.AppendLine($"Imported {officer.FullName} ({officer.OfficerPrisoners.Count} prisoners)");
            }
            context.Officers.AddRange(validOfficers);
            context.SaveChanges();
            return sb.ToString().TrimEnd();

            //--------------------------------------------------------------
            //StringBuilder sb = new StringBuilder();

            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportOfficerWithPrisonersDto[]), new XmlRootAttribute("Officers"));

            //List<Officer> officers = new List<Officer>();

            //using (StringReader stringReader = new StringReader(xmlString))
            //{
            //    ImportOfficerWithPrisonersDto[] officerDtos = (ImportOfficerWithPrisonersDto[])xmlSerializer.Deserialize(stringReader);

            //    foreach (ImportOfficerWithPrisonersDto officerDto in officerDtos)
            //    {
            //        if (!IsValid(officerDto))
            //        {
            //            sb.AppendLine("Invalid Data");
            //            continue;
            //        }

            //        object positionObj;
            //        bool isPositionValid = Enum.TryParse(typeof(Position), officerDto.Position, out positionObj);

            //        if (!isPositionValid)
            //        {
            //            sb.AppendLine("Invalid Data");
            //            continue;
            //        }

            //        object weaponObj;
            //        bool isWeaponValid = Enum.TryParse(typeof(Weapon), officerDto.Weapon, out weaponObj);

            //        if (!isWeaponValid)
            //        {
            //            sb.AppendLine("Invalid Data");
            //            continue;
            //        }

            //        Officer o = new Officer()
            //        {
            //            FullName = officerDto.FullName,
            //            Salary = officerDto.Salary,
            //            Position = (Position)positionObj,
            //            Weapon = (Weapon)weaponObj,
            //            DepartmentId = officerDto.DepartmentId
            //        };

            //        foreach (ImportOfficerPrisonerDto prisonerDto in officerDto.Prisoners)
            //        {
            //            o.OfficerPrisoners.Add(new OfficerPrisoner()
            //            {
            //                Officer = o,
            //                PrisonerId = prisonerDto.Id
            //            });
            //        }

            //        officers.Add(o);
            //        sb.AppendLine($"Imported {o.FullName} ({o.OfficerPrisoners.Count} prisoners)");
            //    }

            //    context.Officers.AddRange(officers);
            //    context.SaveChanges();
            //}

            //return sb.ToString().TrimEnd();
        }

        //Helper method for attribute validations
        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}