using Croptor.Domain.Common.Constants;
using Croptor.Domain.Common.ValueObjects;

namespace Croptor.Domain.Presets
{
    public class Preset
    {
        public Guid Id { get; private set; }
        public string Name { get; set; } = Constants.Presets.DefaultName;
        public Guid? UserId { get; set; }
        public List<Size> Sizes { get; set; } = [];
        public Uri? IconUri { get; set; }

        private Preset() { }

        private Preset(Guid id, string name, Guid userId, List<Size> sizes, Uri? iconUri)
        {
            Id = id;
            Name = name;
            UserId = userId;
            Sizes = sizes;
            IconUri = iconUri;
        }

        public static Preset Create(string name, Guid userId, List<Size> sizes, Uri? iconUri = null)
        {
            return new(Guid.NewGuid(), name, userId, sizes, iconUri);
        }

        public static Preset Create(string name, Guid userId, Uri? iconUri = null)
        {
            return Create(name, userId, [], iconUri);
        }

        public void AddSize(Size size) => Sizes.Add(size);
        public void RemoveSize(Size size) => Sizes.Remove(size);
    }
}
