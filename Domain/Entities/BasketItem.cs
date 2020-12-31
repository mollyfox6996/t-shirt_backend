using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class BasketItem
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string TShirtName { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
