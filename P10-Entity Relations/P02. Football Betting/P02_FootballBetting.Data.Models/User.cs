namespace P02_FootballBetting.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common;


    public class User
    {
        public User()
        {
            this.Bets = new HashSet<Bet>();
        }



        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(ValidationConstants.UserUsernameMaxLength)]
        public string Username { get; set; } = null!;

        //Hashed string in DB
        [Required]
        [MaxLength(ValidationConstants.UserPasswordMaxLength)]
        public string Password { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.UserEmailMaxLength)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.UseNameMaxLength)]
        public string Name { get; set; } = null!;


        public decimal Balance { get; set; }


        public virtual ICollection<Bet> Bets { get; set; }

    }
}
