using Croptor.Domain.Common.ValueObjects;
using System.Collections;

namespace Croptor.Domain.SizeCollections
{
    public class SizeCollection : IEnumerable<Size>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Size> Sizes { get; set; } = [];
        public Uri IconUrI { get; set; }

        public IEnumerator<Size> GetEnumerator()
        {
            return ((IEnumerable<Size>)Sizes).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Sizes).GetEnumerator();
        }

        private SizeCollection() { }

        private SizeCollection(Guid id, string name, List<Size> sizes, Uri iconUri)
        {
            Id = id;
            Name = name;
            Sizes = sizes;
            IconUrI = iconUri;
        }

        public static SizeCollection Create(string name)
        {
            //TODO real uri
            return new(Guid.NewGuid(), name, [], new Uri(""));
        }

        public void Add(Size size)
        {
            size = new(size.Width, size.Height, size.Name, IconUrI);
            Sizes.Add(size);
        }
    }
}
