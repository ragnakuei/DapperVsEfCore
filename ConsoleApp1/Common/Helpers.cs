using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Common
{
    public static class Helpers
    {
        public static T2 GetValue<T1, T2>(this Dictionary<T1, T2> dict, T1 key)
        {
            T2 result = default(T2);
            dict?.TryGetValue(key, out result);
            return result;
        }
    }
}
