using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs
{
    public class LoginResultDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
