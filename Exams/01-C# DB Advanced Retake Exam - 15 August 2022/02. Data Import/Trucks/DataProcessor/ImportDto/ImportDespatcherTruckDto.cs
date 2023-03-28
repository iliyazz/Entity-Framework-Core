namespace Trucks.DataProcessor.ImportDto
{

    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using Data.Models;
    using Trucks.Common;

    [XmlType("Truck")]
    public class ImportDespatcherTruckDto
    {
        [Required]
        [XmlElement("RegistrationNumber")]
        [RegularExpression(ValidationConstants.TruckRegistrationNumberRegex)]
        public string RegistrationNumber { get; set; }

        [Required]
        [RegularExpression(ValidationConstants.TruckVinNumberRegex)]
        [XmlElement("VinNumber")]
        public string VinNumber { get; set; } 

        [XmlElement("TankCapacity")]
        [Range(ValidationConstants.TruckTankCapacityMinValue, ValidationConstants.TruckTankCapacityMaxValue)]
        public int TankCapacity { get; set; }

        [XmlElement("CargoCapacity")]
        [Range(ValidationConstants.TruckCargoCapacityMinValue, ValidationConstants.TruckCargoCapacityMaxValue)]
        public int CargoCapacity { get; set; }

        [XmlElement("CategoryType")]
        [Required]
        [Range(ValidationConstants.TruckCategoryTypeMinValue, ValidationConstants.TruckCategoryTypeMaxValue)]
        public int CategoryType { get; set; }

        [XmlElement("MakeType")]
        [Required]
        [Range(ValidationConstants.TruckMakeTypeMinValue, ValidationConstants.TruckMakeTypeMaxValue)]
        public int MakeType { get; set; }
    }
}
