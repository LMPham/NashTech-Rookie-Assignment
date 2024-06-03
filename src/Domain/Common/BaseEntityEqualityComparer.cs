using System.Diagnostics.CodeAnalysis;

namespace Domain.Common
{
    /// <summary>
    /// Equality comparer for <see cref="BaseEntity{T}"/>
    /// </summary>
    public class BaseEntityEqualityComparer<T> : IEqualityComparer<T> where T : BaseEntity<int>
    {
        public bool Equals(T? x, T? y)
        {
            return x?.Equals(y) ?? false;
        }

        public int GetHashCode([DisallowNull] T obj)
        {
            return obj.GetHashCode();
        }
    }
}
