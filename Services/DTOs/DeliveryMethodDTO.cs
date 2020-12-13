using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class DeliveryMethodDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Price { get; set; }
    }
}
