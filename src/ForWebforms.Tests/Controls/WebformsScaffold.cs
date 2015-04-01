using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using ExitStrategy.ForWebforms.Tests.Mocks;

namespace ExitStrategy.ForWebforms.Tests.Controls
{
    ////http://stackoverflow.com/questions/3702526/is-there-a-way-to-process-an-mvc-view-aspx-file-from-a-non-web-application
    public class WebformsScaffold : MarshalByRefObject
    {
        private readonly string[] _unserializableExceptions = new[] {"Shouldly", "Moq"};

        public string Test(Action<MockPage, HtmlTextWriter> arrangeAct)
        {
            var builder = new StringBuilder();
            using (var writer = new HtmlTextWriter(new StringWriter(builder)))
            {
                try
                {
                    var httpContext = new HttpContext(new HttpRequest("", "http://example.com", ""), new HttpResponse(writer));
                    if (HttpContext.Current != null) throw new NotSupportedException("httpcontext was already set");
                    HttpContext.Current = httpContext;

                    var page = new MockPage();

                    arrangeAct(page, writer);

                    return builder.ToString();
                }
                catch (Exception ex)
                {
                    if (_unserializableExceptions.Contains(ex.Source))
                    {
                        throw new Exception(ex.Message);//Schouldly's exceptions are not serializable
                    }
                    throw;
                }
                finally
                {
                    HttpContext.Current = null;
                }
            }
        }

        public TEx Throws<TEx>(Action<MockPage, HtmlTextWriter> arrangeActAssert)
            where TEx : Exception
        {
            try
            {
                Test(arrangeActAssert);
                throw new Exception("Expected exception was " + typeof(TEx).Name + " but no exception was thrown.");
            }
            catch (TEx exception)
            {
                return exception;
            }
            catch (Exception ex)
            {
                throw new Exception("Expected exception was " + typeof(TEx).Name + " but got " + ex.GetType().Name, ex);
            }
        }

        private static WebformsScaffold Create()
        {
            var websiteLocation = Directory.GetParent(typeof(WebformsScaffold).Assembly.Location).Parent;
            return (WebformsScaffold)ApplicationHost.CreateApplicationHost(typeof(WebformsScaffold), "/", websiteLocation.FullName);
        }

        private static WebformsScaffold _default;
        public static WebformsScaffold Default
        {
            get { return _default ?? (_default = Create()); }
        }
    }
}