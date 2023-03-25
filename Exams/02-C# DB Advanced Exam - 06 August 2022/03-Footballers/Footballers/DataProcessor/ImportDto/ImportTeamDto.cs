namespace Footballers.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    public class ImportTeamDto
    {

        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        [RegularExpression(@"^[A-Za-z0-9\s\.\-]{3,}$")]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Nationality { get; set; }
        [Required]
        public int Trophies { get; set; }
        public int[] Footballers { get; set; }


    }
}
