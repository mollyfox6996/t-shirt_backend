using System.Collections.Generic;

namespace Services.DTOs.BasketDTOs
{
    public class BasketDTO
    {
        public string Id { get; set; }
        public List<BasketItemDTO> Items { get; set; }
    }
}
