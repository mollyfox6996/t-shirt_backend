using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.OrderAggregate
{
    public class DeliveryMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Price { get; set; }
    }
}
