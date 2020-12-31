namespace Services.DTOs.OrderAggregate
{
    public class DeliveryMethodDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Price { get; set; }
    }
}