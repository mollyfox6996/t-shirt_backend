namespace Services.DTOs.OrderAggregate
{
    public class OrderDTO
    {
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDTO Address { get; set; }
    }
}