using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rio.SME.Domain.Util.ExtensionMethods
{
    public static class IEnumerableExtension
    {
        public static IEnumerable Append(this IEnumerable first, params object[] second)
        {
            return first.OfType<object>().Concat(second);
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> first, params T[] second)
        {
            return first.Concat(second);
        }

        public static int Count(this IEnumerable source)
        {
            return Enumerable.Count(source.Cast<object>());
        }        
    }
}
