using System;
using System.Collections.Specialized;
using System.Linq;

namespace ExitStrategy.TestWebsite.Helpers
{
    public static class CollectionHelper
    {
        public static String GetValueOrEmptyString(this NameValueCollection collection, string key)
        {
            var actualKey = collection.AllKeys.FirstOrDefault(k => k.Equals(key, StringComparison.InvariantCultureIgnoreCase));

            return actualKey == null ? string.Empty : collection[actualKey];
        }
    }
}