using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}