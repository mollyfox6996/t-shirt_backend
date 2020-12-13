using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.DTOs
{
    public class CreateTshirtDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Range(1, 10000)]
        public decimal Price { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string PictureUrl { get; set; }
        
    }
}
