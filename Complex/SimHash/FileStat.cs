using System;

namespace SimHash
{
    public record FileStat(
        string Name,
        long Size,
        DateTime Written
    ) : IComparable<FileStat>
    {
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