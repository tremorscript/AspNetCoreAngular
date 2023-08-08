using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Abstractions;
using AspNetCoreAngular.Application.Features.Notifications.Models;
using MediatR;

namespace AspNetCoreAngular.Application.Features.Customers.Commands.CreateCustomer
{
    public class CustomerCreated : INotification
    {
        public string CustomerId { get; set; }

        public class CustomerCreatedHandler : INotificationHandler<CustomerCreated>
        {
            private readonly IEmailService email;

            public CustomerCreatedHandler(IEmailService email)
            {
                this.email = email;
            }

            public async Task Handle(
                CustomerCreated notification,
                CancellationToken cancellationToken)
            {
                await email.SendCustomerCreatedEmail(new EmailMessage());
            }
        }
    }
}
