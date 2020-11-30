using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class OrderToReturnDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime OrderDate { get; set; }
        public AddressDTO Adress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal ShippingPrice { get; set; }
        public IReadOnlyList<OrderItemDTO> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
    }
}
