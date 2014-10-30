using System;
using System.Collections.Generic;

namespace TestWebsite.Helpers
{
    public static class TypeHelpers
    {
        public static Type GetItemType<T>(this IEnumerable<T> enumerable)
        {
            var type = enumerable.GetType();
            var ienum = type.GetInterface(typeof(IEnumerable<>).Name);
            return ienum != null
              ? ienum.GetGenericArguments()[0]
              : null;
        }
    }
}