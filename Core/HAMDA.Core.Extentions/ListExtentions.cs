using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMDA.Core.Extentions
{
    public static class ListExtentions
    {
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return !(list is null) && list.Any();
        }
        public static bool IsNotNullOrEmpty<T>(this ICollection<T> list)
        {
            return !(list is null) && list.Any();
        }
    }
}
