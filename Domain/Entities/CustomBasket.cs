using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class CustomBasket
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

        public CustomBasket(string id)
        {
            Id = id;
        }
    }
}
