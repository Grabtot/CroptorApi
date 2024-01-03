namespace Croptor.Domain.Common.ValueObjects
{
    public record Size
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public string Name { get; private set; } = null!;
        public Uri? IconUri { get; private set; }

        private Size() { }

        public Size(int width, int height, string? name = null, Uri? iconUri = null)
        {
            Width = width;
            Height = height;
            Name = name ?? $"{width} x {height}";
            IconUri = iconUri;
        }
    }
}
