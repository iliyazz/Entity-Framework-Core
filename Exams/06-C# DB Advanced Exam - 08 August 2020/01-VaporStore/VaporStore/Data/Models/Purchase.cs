﻿namespace VaporStore.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Common;
    using Enums;

    public class Purchase
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public PurchaseType Type { get; set; }



        [Required]
        [RegularExpression(ValidationConstants.PurchaseProductKeyValidationRegex)]
        public string ProductKey { get; set; }


        [Required]
        public DateTime Date { get; set; }


        [Required]
        [ForeignKey(nameof(Card))]
        public int CardId { get; set; }
        public Card Card { get; set; }



        [Required]
        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
