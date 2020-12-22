using System.ComponentModel.DataAnnotations;

namespace Services.DTOs.UserDTOs
{
    public class UserForRegisterDTO
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        
        
    }
}
