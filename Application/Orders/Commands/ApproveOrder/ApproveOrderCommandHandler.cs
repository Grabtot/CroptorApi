using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Users;
using Croptor.Domain.Users.ValueObjects;
using MediatR;

namespace Croptor.Application.Orders.Commands.ApproveOrder;

public class ApproveOrderCommandHandler(IUserRepository userRepository) : IRequestHandler<ApproveOrderCommand>
{
    public async Task Handle(ApproveOrderCommand request, CancellationToken cancellationToken)
    {
        User user = await userRepository.GetUserAsync(request.Order.UserId, cancellationToken);
        DateOnly expireDate;
        if (user.Plan.ExpireDate.HasValue)
            expireDate = user.Plan.ExpireDate.Value;
        else
            expireDate = DateOnly.FromDateTime(DateTime.Now);
        expireDate = expireDate.AddMonths(request.Order.Amount);
        user.Plan = Plan.Create(PlanType.Pro, expireDate);
    }
}