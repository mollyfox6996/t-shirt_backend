using Services.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Domain.Entities.OrderAggregate;
using Microsoft.Extensions.Configuration;

namespace Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendConfirmEmail(string email, string callbackUrl, string subject, string text)
        {
            var emailMessage = new MimeMessage();

            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.From.Add(new MailboxAddress("Administration", _configuration["EmailServiceSettings:login"]));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = text
            };

            await SendEmail(emailMessage);
        }

        public async Task SendOrderEmail(Order order)
        {
            var emailMessage = new MimeMessage();
            
            var body =  $"<h1>Dear {order.Address.FirstName} {order.Address.LastName}, your order.</h1>" +
                        $"<p>Delivery address: {order.Address.City}, {order.Address.Street} {order.Address.ZipCode}</p>" +
                        $"<p>Delivery method: {order.DeliveryMethod}</p>" +
                        $"Total bill: ${order.Total}";
            
            
            emailMessage.To.Add(new MailboxAddress("", order.Email));
            emailMessage.From.Add(new MailboxAddress("Administration", _configuration["EmailServiceSettings:login"]));
            emailMessage.Subject = "Order";
            emailMessage.Body = new TextPart((MimeKit.Text.TextFormat.Html))
            {
                Text = body
            };

            await SendEmail(emailMessage);
        }

        private async Task SendEmail(MimeMessage emailMessage)
        {
            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 465, true);
            await client.AuthenticateAsync( _configuration["EmailServiceSettings:login"], _configuration["EmailServiceSettings:password"] );
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
