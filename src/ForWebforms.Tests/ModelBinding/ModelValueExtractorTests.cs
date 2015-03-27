using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using ExitStrategy.ForWebforms.ModelBinding;
using ExitStrategy.ForWebforms.Tests.Mocks;
using Moq;
using Shouldly;
using ExitStrategy.ForWebforms.Bridge;

namespace ExitStrategy.ForWebforms.Tests.ModelBinding
{
    public class ModelValueExtractorTests
    {
        public void GetValuesFromForm()
        {
            var control = new MockControl {ID = "ControlId", ClientIDMode = ClientIDMode.Static};
            var extractor = new ModelValueExtractor(control);

            var values = extractor.ExtractValues(new NameValueCollection()
            {
                {"ControlId.Value", "In collection"},
                {"SomeOtherControlID.Value", "Not in collection"}
            });

            values.ShouldBe(new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Value", "In collection")
            });
        }

        public void GetValuesWithSpecialMvcBooleanLogic()
        {
            var control = new MockControl { ID = "ControlId", ClientIDMode = ClientIDMode.Static };
            var extractor = new ModelValueExtractor(control);

            var values = extractor.ExtractValues(new NameValueCollection()
            {
                {"ControlId.MvcTrue", "true,false"},
                {"ControlId.MvcFalse", "false,true"},
                {"ControlId.RegularTrue", "true"},
                {"ControlId.RegularFalse", "false"},
            }).ToList();

            values.ShouldBe(new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("MvcTrue", "true"),
                new KeyValuePair<string, string>("MvcFalse", "false"),
                new KeyValuePair<string, string>("RegularTrue", "true"),
                new KeyValuePair<string, string>("RegularFalse", "false"),
            }, true);
        }

        public void IntegrationTest()
        {
            WebformsScaffold.Create().Test((p, w) =>
            {
                var control = new MockControl { ID = "ControlId", ClientIDMode = ClientIDMode.Static };
                var context = new Mock<HttpContextBase>();
                var request = new Mock<HttpRequestBase>();
                request.Setup(r => r.Form).Returns(new NameValueCollection {{"ControlId.TestValue", "Value"}});
                context.Setup(c => c.Request).Returns(request.Object);
                HttpContextProvider.SetHttpContext(context.Object);

                var dictionary = new OrderedDictionary();
                control.ExtractValues(dictionary);

                dictionary.Count.ShouldBe(1);
                dictionary["TestValue"].ShouldBe("Value");

                var valueProvider = control.GetValueProvider();
                valueProvider.GetValue("TestValue").AttemptedValue.ShouldBe("Value");
            });
        }
    }
}
