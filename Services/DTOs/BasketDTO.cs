using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.DTOs
{
    public class BasketDTO
    {
        public string Id { get; set; }
        public List<BasketItemDTO> Items { get; set; }
    }
}
