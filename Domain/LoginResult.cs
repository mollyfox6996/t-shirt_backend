using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
