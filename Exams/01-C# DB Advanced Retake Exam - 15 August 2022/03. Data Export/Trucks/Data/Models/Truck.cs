namespace Trucks.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common;
using Enums;

public class Truck
{
    public Truck()
    {
        this.ClientsTrucks = new HashSet<ClientTruck>();
    }

    [Key]
    public int Id { get; set; }

    //[RegularExpression(ValidationConstants.TruckRegistrationNumberRegex)]
    public string? RegistrationNumber { get; set; }

    [Required]
    //[RegularExpression(ValidationConstants.TruckVinNumberRegex)]
    //[MinLength(ValidationConstants.TruckVinNumberLength)]
    [MaxLength(ValidationConstants.TruckVinNumberLength)]
    public string VinNumber { get; set; } = null!;

    //[Range(ValidationConstants.TruckTankCapacityMinValue, ValidationConstants.TruckTankCapacityMaxValue)]
    public int TankCapacity { get; set; }

    //[Range(ValidationConstants.TruckCargoCapacityMinValue, ValidationConstants.TruckCargoCapacityMaxValue)]
    public int CargoCapacity { get; set; }

    [Required]
    public CategoryType CategoryType { get; set; }

    [Required]
    public MakeType MakeType { get; set; }


    [Required]
    [ForeignKey(nameof(Despatcher))]
    public int DespatcherId { get; set; }
    public virtual Despatcher Despatcher { get; set; }


    public virtual ICollection<ClientTruck> ClientsTrucks { get; set; }

}