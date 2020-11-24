﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;


namespace Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string email, string displayName, string callbackUrl, string subject);
    }
}