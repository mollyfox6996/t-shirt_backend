using System.ComponentModel.DataAnnotations;

namespace Services.DTOs.TshirtDTOs
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
