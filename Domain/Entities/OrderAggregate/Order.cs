using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.OrderAggregate
{
    public class Order
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public Address Address { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Order(string email, Address address, DeliveryMethod deliveryMethod, IReadOnlyList<OrderItem> orderItems, decimal subtotal)
        {
            Email = email;
            Address = address;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;
        }

        public decimal GetTotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }
    }
}
