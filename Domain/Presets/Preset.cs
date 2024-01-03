using Croptor.Domain.Common.ValueObjects;

namespace Croptor.Domain.Presets
{
    public class Preset
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid UserId { get; private set; }
        public List<Size> Sizes { get; private set; }
    }
}
