namespace Boardgames.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class Seller
    {
        public Seller()
        {
            this.BoardgamesSellers = new HashSet<BoardgameSeller>();
        }

        [Key]
        public int Id { get; set; }


        [Required]
        //[MinLength(ValidationConstants.SellerNameMinLength)]
        [MaxLength(ValidationConstants.SellerNameMaxLength)]
        public string Name { get; set; }


        [Required]
        //[MinLength(ValidationConstants.SellerAddressMinLength)]
        [MaxLength(ValidationConstants.SellerAddressMaxLength)]
        public string Address { get; set; }


        [Required]
        public string Country { get; set; }


        [Required]
        [RegularExpression(ValidationConstants.SellerWebsiteRegex)]
        public string Website { get; set; }



        public ICollection<BoardgameSeller> BoardgamesSellers { get; set; }
    }
}
