using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string DisplayName { get; set; }
    }
}
