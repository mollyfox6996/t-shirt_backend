using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
