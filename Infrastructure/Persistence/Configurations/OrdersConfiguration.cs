using Croptor.Domain.Users;
using Croptor.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Croptor.Infrastructure.Persistence.Configurations
{
    public class OrdersConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne<User>().WithMany()
                .HasForeignKey(order => order.UserId);
        }
    }
}
