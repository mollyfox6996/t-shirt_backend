using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Domain.Entities.OrderAggregate;


namespace Services.Interfaces
{
    public interface IEmailService
    {
        Task SendConfirmEmail(string email, string callbackUrl, string subject, string text);

        Task SendOrderEmail(Order order);
    }
}
