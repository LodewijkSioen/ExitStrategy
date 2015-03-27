using System.Web;

namespace ExitStrategy.ForWebforms.Bridge
{
    public static class HttpContextProvider
    {
        private static HttpContextBase _current;

        public static HttpContextBase Current
        {
            get
            {
                return _current ?? HttpContext.Current.Request.RequestContext.HttpContext;
            }
        }

        public static void SetHttpContext(HttpContextBase context)
        {
            _current = context;
        }
    }
}
