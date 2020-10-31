using Services.Interfaces;
using System;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Text;
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

        public async Task SendEmail(string email, string displayName, string callbackUrl, string subject)
        {
            string body = $"<h1>Hi {0}.</h1><p>Click on the link to cinfirm you email adress {1}</p>";

            var emailMessage = new MimeMessage();

            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.From.Add(new MailboxAddress("Administration", _configuration["EmailServiceSettings:login"]));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"<h1>Hi {displayName}.</h1><p>Click on the link to cinfirm you email adress {callbackUrl}</p>"
            };
            

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
