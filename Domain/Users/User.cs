using Croptor.Domain.Presets;
using Croptor.Domain.SizeCollections;
using Croptor.Domain.Users.ValueObjects;

namespace Croptor.Domain.Users
{
    public class User
    {
        private User(Guid id, string name, List<Preset> presets, SizeCollection sizes, Plan plan)
        {
            Id = id;
            Name = name;
            Presets = presets;
            CustomSizes = sizes;
            Plan = plan;
        }

        private User() { }

        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public List<Preset> Presets { get; private set; } = [];
        public SizeCollection CustomSizes { get; private set; }
        public Plan Plan { get; private set; } = Plan.Create(PlanType.Free);


    }
}
