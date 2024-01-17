using Croptor.Domain.Presets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Croptor.Infrastructure.Persistence.Configurations
{
    public class PresetsConfigurations : IEntityTypeConfiguration<Preset>
    {
        public void Configure(EntityTypeBuilder<Preset> builder)
        {
            builder.OwnsMany(preset => preset.Sizes);

            builder.HasIndex(preset => new { preset.UserId, preset.Name });
        }
    }
}
