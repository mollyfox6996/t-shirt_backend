using Services.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
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

        public async Task SendEmail(string email, string callbackUrl, string subject, string text)
        {
            MimeMessage emailMessage = new MimeMessage();

            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.From.Add(new MailboxAddress("Administration", _configuration["EmailServiceSettings:login"]));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = text
            };
            
            using (SmtpClient client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync( _configuration["EmailServiceSettings:login"], _configuration["EmailServiceSettings:password"] );
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
