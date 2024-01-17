using Croptor.Domain.Presets;
using Croptor.Domain.Users.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Croptor.Domain.Users
{
    public class User : IdentityUser<Guid>
    {
        private User() { }

        public List<Preset> Presets { get; private set; } = [];
        public Plan Plan { get; set; } = Plan.Create(PlanType.Free);

    }
}
