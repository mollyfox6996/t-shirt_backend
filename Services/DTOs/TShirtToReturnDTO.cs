using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class TShirtToReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreateDate { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string PictureUrl { get; set; }
        public string AuthorName { get; set; }
    }
}
