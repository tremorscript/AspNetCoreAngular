using System.Threading.Tasks;
using AspNetCoreAngular.Application.Features.Notifications.Models;

namespace AspNetCoreAngular.Application.Abstractions
{
    public interface IEmailService
    {
        Task RegistrationConfirmationEmail(string to, string link);

        Task ForgottentPasswordEmail(string to, string link);

        Task SendCustomerCreatedEmail(EmailMessage emailMessage);
    }
}
