using System.Security.Claims;
using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Users;
using Croptor.Domain.Users.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Croptor.Application.Orders.Commands.ApproveOrder;

public class ApproveOrderCommandHandler(
    IUserRepository userRepository,
    // UserManager<User> userManager,
    IOrderRepository orderRepository
) : IRequestHandler<ApproveOrderCommand>
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
        // await userManager.ReplaceClaimAsync(user, new Claim("plan", "Free"), new Claim("plan", "Pro"));
        await orderRepository.DeleteOrderAsync(request.Order, cancellationToken);
    }
}