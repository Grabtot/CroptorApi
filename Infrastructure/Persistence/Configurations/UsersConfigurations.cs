using Croptor.Domain.Users;
using Croptor.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Croptor.Infrastructure.Persistence.Configurations
{
    public class UsersConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.OwnsOne("plan", user => user.Plan, planBuilder =>
            {
                planBuilder.Property(plan => plan.Type)
                .HasDefaultValue(PlanType.Free);
            });
        }
    }
}
