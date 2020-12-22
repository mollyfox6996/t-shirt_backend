using System;

namespace Services.DTOs.TshirtDTOs
{
    public class TShirtToReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Gender { get; set; }
        public string PictureUrl { get; set; }
        public string AuthorName { get; set; }
    }
}
