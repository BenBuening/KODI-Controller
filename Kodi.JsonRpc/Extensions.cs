using System.Collections;
using System.Collections.Generic;

namespace Kodi.JsonRpc
{
    public static class Extensions
    {
        public static void ConditionalAdd(this IDictionary<string, object> dict, string key, object value)
        {
            if (dict != null && key != null && value != null)
            {
                ICollection values = value as ICollection;
                if (values == null || values.Count > 0)
                    dict.Add(key, value);
            }
        }
    }
}
