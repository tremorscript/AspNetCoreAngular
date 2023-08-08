using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAngular.Application.Abstractions;
using AspNetCoreAngular.Application.Exceptions;
using AspNetCoreAngular.Domain.Entities;
using MediatR;
using IApplicationDbContext = AspNetCoreAngular.Application.Abstractions.IApplicationDbContext;

namespace AspNetCoreAngular.Application.Features.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
        {
            private readonly IApplicationDbContext context;
            private readonly IUserManager userManager;
            private readonly ICurrentUserService currentUser;

            public DeleteEmployeeCommandHandler(
                IApplicationDbContext context,
                IUserManager userManager,
                ICurrentUserService currentUser)
            {
                this.context = context;
                this.userManager = userManager;
                this.currentUser = currentUser;
            }

            public async Task<Unit> Handle(
                DeleteEmployeeCommand request,
                CancellationToken cancellationToken)
            {
                var entity = await context.Employees.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken)
                                                ?? throw new NotFoundException(nameof(Employee), request.Id);
                if (entity.UserId == currentUser.UserId)
                {
                    throw new BadRequestException("Employees cannot delete their own account.");
                }

                if (entity.UserId != default)
                {
                    await userManager.DeleteUserAsync(entity.UserId);
                }

                // TODO: Update this logic, this will only work if the employee has no associated territories or orders.Emp
                context.Employees.Remove(entity);

                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
