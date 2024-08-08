using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMDA.Core.Extentions
{
    public static class StringExtentions
    {
        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
