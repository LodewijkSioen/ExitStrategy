using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using ExitStrategy.ForWebforms;
using Shouldly;

namespace ForWebforms.Tests
{
    ////http://stackoverflow.com/questions/3702526/is-there-a-way-to-process-an-mvc-view-aspx-file-from-a-non-web-application
    public class FakeAppHost<T> : MarshalByRefObject
        where T : MvcControl, new()
    {
        public string Test(Action<T, MockPage> renderAction)
        {
            var builder = new StringBuilder();
            using (var writer = new HtmlTextWriter(new StringWriter(builder)))
            {
                var httpContext = new HttpContext(new HttpRequest("", "http://example.com", ""), new HttpResponse(writer));
                if (HttpContext.Current != null) throw new NotSupportedException("httpcontext was already set");
                HttpContext.Current = httpContext;

                var page = new MockPage();
                var control = new T();
                page.Controls.Add(control);

                renderAction(control, page);

                control.RenderControl(writer);

                HttpContext.Current = null;
                return builder.ToString();
            }
        }

        public TEx Throws<TEx>(Action<T, MockPage> renderAction)
            where TEx : Exception
        {
            try
            {
                Test(renderAction);
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

        public static FakeAppHost<T> Create()
        {
            var websiteLocation = Directory.GetParent(typeof(FakeAppHost<T>).Assembly.Location).Parent;
            return (FakeAppHost<T>)ApplicationHost.CreateApplicationHost(typeof(FakeAppHost<T>), "/", websiteLocation.FullName);
        }
    }
}