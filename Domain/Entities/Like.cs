using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Like
    {
        public int Id { get; set; }
        
        public string AuthorName { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public int ShirtId { get; set; }
        public TShirt Shirt { get; set; }

    }
}
