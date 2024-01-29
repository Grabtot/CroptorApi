using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Domain.Users;
using Croptor.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Croptor.Infrastructure.Persistence.Repositories
{
    public class UserRepository(CroptorDbContext context, ILogger<UserRepository> logger) : IUserRepository
    {
        private readonly DbSet<User> _dbSet = context.Set<User>();
        private readonly ILogger<UserRepository> _logger = logger;

        public async Task<User> GetUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync([userId], cancellationToken)
                   ?? throw new InvalidOperationException($"Could not find User {userId}");
        }

        public void UpdateSubscriptionForUser(User user)
        {
            try
            {
                if (user.Plan.ExpireDate != null && user.Plan.ExpireDate > DateOnly.FromDateTime(DateTime.Now) && user.Plan.Type == PlanType.Pro)
                {
                    user.Plan = Plan.Create(PlanType.Free);

                    _dbSet.Update(user);

                    // await context.SaveChangesAsync();

                    _logger.LogInformation($"User subscription updated for user {user.Id}");
                }
                else
                {
                    _logger.LogInformation($"User subscription not updated for user {user.Id}. Conditions not met.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update subscription for user {user.Id}");
            }
        }
    }
}