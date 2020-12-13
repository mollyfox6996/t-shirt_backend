using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class OrderDTO
    {
        public string BasketId { get; set; }
        public string DeliveryMethodId { get; set; }
        public AddressDTO Address { get; set; }
    }
}
