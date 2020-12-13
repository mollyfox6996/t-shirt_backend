using System.ComponentModel.DataAnnotations;

namespace Services.DTOs
{
    public class BasketItemDTO
    {
        public int Id { get; set; }
        public string TShirtName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        public string Category { get; set; }
    }
}