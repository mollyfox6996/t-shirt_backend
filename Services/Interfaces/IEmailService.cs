using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Domain;

namespace Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string email, string displayName, string callbackUrl, string subject);
    }
}
