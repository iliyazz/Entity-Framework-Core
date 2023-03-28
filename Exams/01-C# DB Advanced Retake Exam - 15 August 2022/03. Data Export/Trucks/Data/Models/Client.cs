namespace Trucks.Data.Models;

using System.ComponentModel.DataAnnotations;
using Common;

public class Client
{
    public Client()
    {
        this.ClientsTrucks = new HashSet<ClientTruck>();
    }

    [Key]
    public int Id { get; set; }


    [Required]
    //[MinLength(ValidationConstants.ClientNameMinLength)]
    [MaxLength(ValidationConstants.ClientNameMaxLength)]
    public string Name { get; set; } = null!;


    [Required]
    //[MinLength(ValidationConstants.ClientNationalityMinLength)]
    [MaxLength(ValidationConstants.ClientNationalityMaxLength)]
    public string Nationality { get; set; } = null!;


    [Required] 
    public string Type { get; set; } = null!;
    

    public virtual ICollection<ClientTruck> ClientsTrucks { get; set; }
}