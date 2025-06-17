using System;

namespace SimHash
{
    public class FileStat : IComparable<FileStat>
    {
        public FileStat(string name, long size, DateTime written)
        {
            Name = name;
            Size = size;
            Written = written;
        }

        public string Name { get; init; }
        public long Size { get; init; }
        public DateTime Written { get; init; }

        public int CompareTo(FileStat other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (other is null) return 1;
            var nameComparison = string.Compare(Name, other.Name, StringComparison.Ordinal);
            if (nameComparison != 0) return nameComparison;
            var sizeComparison = Size.CompareTo(other.Size);
            if (sizeComparison != 0) return sizeComparison;
            return Written.CompareTo(other.Written);
        }
    }
}