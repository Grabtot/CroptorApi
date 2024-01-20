using System.Text.Json.Serialization;

namespace Croptor.Domain.Common.ValueObjects
{
    public record Size
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public string Name { get; set; } = null!;
        public Uri? IconUri { get; set; }

        private Size() { }

        [JsonConstructor]
        public Size(int width, int height, string? name = null, Uri? iconUri = null)
        {
            Width = width;
            Height = height;
            Name = name ?? $"{width} x {height}";
            IconUri = iconUri;
        }
    }
}
