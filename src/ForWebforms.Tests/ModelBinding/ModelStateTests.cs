using ExitStrategy.ForWebforms.ModelBinding;
using Shouldly;
using System;
using System.Globalization;
using System.Linq;
using System.Web.ModelBinding;
using WebformsModelState = System.Web.ModelBinding.ModelState;
using WebformsModelStateDictionary = System.Web.ModelBinding.ModelStateDictionary;

namespace ExitStrategy.ForWebforms.Tests.ModelBinding
{
    public class ModelStateTests
    {
        [Input("")]
        [Input("Client_Id")]
        public void AddWebformsStatesToMvcStates(string prefix)
        {
            var viewBag = new System.Web.Mvc.ViewDataDictionary();
            viewBag.TemplateInfo.HtmlFieldPrefix = prefix;
            var webformsStateDictionary = new WebformsModelStateDictionary()
            {
                {"Test", new WebformsModelState(){Value = new ValueProviderResult("raw", "attempted", CultureInfo.InvariantCulture)}}
            };

            viewBag.AdaptModelState(webformsStateDictionary);

            viewBag.ModelState.Count.ShouldBe(1);

            var expectedKey = string.IsNullOrEmpty(prefix) ? "Test" : prefix + ".Test";
            viewBag.ModelState.Keys.ElementAt(0).ShouldBe(expectedKey);
            var state = viewBag.ModelState[expectedKey];
            state.Value.AttemptedValue.ShouldBe("attempted");
            state.Value.RawValue.ShouldBe("raw");
            state.Value.Culture.ShouldBe(CultureInfo.InvariantCulture);
        }

        public void ConvertModelStateToMvc()
        {
            var ex1 = new Exception("ExceptionMessage1");
            var ex2 = new Exception("ExceptionMessage2");
            var webFormsState = new WebformsModelState
            {
                Value = new ValueProviderResult("raw", "attempted", CultureInfo.InvariantCulture),
                Errors =
                {
                    new ModelError(ex1, "errorMessage1"),
                    new ModelError(ex2, "errorMessage2")
                }
            };

            var mvcState = webFormsState.ToMvc();

            mvcState.Value.RawValue.ShouldBe("raw");
            mvcState.Value.AttemptedValue.ShouldBe("attempted");
            mvcState.Value.Culture.ShouldBe(CultureInfo.InvariantCulture);
            mvcState.Errors.Count.ShouldBe(2);
            mvcState.Errors[0].ErrorMessage.ShouldBe("errorMessage1");
            mvcState.Errors[0].Exception.ShouldBe(ex1);
            mvcState.Errors[1].ErrorMessage.ShouldBe("errorMessage2");
            mvcState.Errors[1].Exception.ShouldBe(ex2);
        }

        public void ConvertModelStateWithNullValueToMvc()
        {
            var ex1 = new Exception("ExceptionMessage1");
            var ex2 = new Exception("ExceptionMessage2");
            var webFormsState = new WebformsModelState
            {
                Value = null,
                Errors =
                {
                    new ModelError(ex1, "errorMessage1"),
                    new ModelError(ex2, "errorMessage2")
                }
            };

            var mvcState = webFormsState.ToMvc();

            mvcState.Value.ShouldBe(null);
            mvcState.Errors.Count.ShouldBe(2);
            mvcState.Errors[0].ErrorMessage.ShouldBe("errorMessage1");
            mvcState.Errors[0].Exception.ShouldBe(ex1);
            mvcState.Errors[1].ErrorMessage.ShouldBe("errorMessage2");
            mvcState.Errors[1].Exception.ShouldBe(ex2);
        }
    }
}