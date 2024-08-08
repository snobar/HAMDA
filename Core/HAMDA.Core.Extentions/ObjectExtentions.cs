using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HAMDA.Core.Extentions
{
    public static class ObjectExtentions
    {
        public static bool IsNotNullOrEmpty(this object obj)
        {
            if (obj is null)
                return false;

            return obj.GetType().GetRuntimeProperties().Any(property =>
            {
                object value = property.GetValue(obj);
                return value != null;
            });
        }


        public static T ToAnyType<T>(this object obj) where T : struct
        {

            try
            {
                if (obj is not null)
                {
                    var UnderLyingType = typeof(T).UnderlyingSystemType;
                    if (typeof(T) == typeof(Guid) && obj is string text)
                    {

                        return (T)(object)new Guid(text);
                    }
                    else if (UnderLyingType.IsNotNullOrEmpty())
                    {
                        return (T)Convert.ChangeType(obj, UnderLyingType);
                    }
                    else
                    {
                        return (T)Convert.ChangeType(obj, typeof(T));
                    }
                }
                return default;
            }
            catch
            {
                return default;
            }
        }
    }
}
