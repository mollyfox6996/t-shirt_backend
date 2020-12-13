using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }

        public int ShirtId { get; set; }
        public TShirt Shirt { get; set; }
        public string Text { get; set; }
    }
}
