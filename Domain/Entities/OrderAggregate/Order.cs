using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.OrderAggregate 
{
    public class Order
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime OrderDate { get; set; }
        public Address Address { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }        
    }
}
