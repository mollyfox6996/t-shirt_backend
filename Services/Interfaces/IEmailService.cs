using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IEmailService
    {
        Task SendConfirmEmail(string email, string callbackUrl, string subject, string text);

        Task SendOrderEmail(string email, string subject, string text);
    }
}
