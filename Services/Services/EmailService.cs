using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Services.Interfaces;

namespace Services.Services
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

        public async Task SendOrderEmail(string email, string subject, string text)
        {
            var emailMessage = new MimeMessage();
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.From.Add(new MailboxAddress("Administration", _configuration["EmailServiceSettings:login"]));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart((MimeKit.Text.TextFormat.Html))
            {
                Text = text
            };

            await SendEmail(emailMessage);
        }

        private async Task SendEmail(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync( _configuration["EmailServiceSettings:login"], _configuration["EmailServiceSettings:password"] );
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
            
        }
    }
}
