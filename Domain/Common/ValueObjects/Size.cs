namespace Croptor.Domain.Common.ValueObjects
{
    public record Size
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public string Name { get; set; } = null!;
        public Uri? IconUri { get; set; }

        private Size() { }

        public Size(int width, int height, string? name = null, Uri? iconUri = null)
        {
            Width = width;
            Height = height;
            Name = name ?? $"{width} x {height}";
            IconUri = iconUri;
        }
        public Size(System.Drawing.Size size, string? name = null, Uri? iconUri = null)
        {
            Width = size.Width;
            Height = size.Height;
            Name = name ?? $"{Width} x {Height}";
            IconUri = iconUri;
        }
    }
}
