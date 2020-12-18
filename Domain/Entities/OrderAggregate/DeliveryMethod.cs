using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.OrderAggregate
{
    public class DeliveryMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DeliveryTime { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
