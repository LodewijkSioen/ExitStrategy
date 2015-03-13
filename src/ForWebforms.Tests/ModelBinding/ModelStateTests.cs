using System;
using System.Globalization;
using System.Web.ModelBinding;
using ExitStrategy.ForWebforms.ModelBinding;
using Shouldly;
using MvcModelError = System.Web.Mvc.ModelError;
using MvcModelState = System.Web.Mvc.ModelState;
using MvcModelStateDictionary = System.Web.Mvc.ModelStateDictionary;
using MvcValueProviderResult = System.Web.Mvc.ValueProviderResult;
using WebformsModelState = System.Web.ModelBinding.ModelState;
using WebformsModelStateDictionary = System.Web.ModelBinding.ModelStateDictionary;

namespace ExitStrategy.ForWebforms.Tests.ModelBinding
{
    public class ModelStateTests
    {
        public void AddWebformsStatesToMvcStates()
        {
            var mvcStateDictionary = new MvcModelStateDictionary();
            var webformsStateDictionary = new WebformsModelStateDictionary()
            {
                {"Test", new WebformsModelState(){Value = new ValueProviderResult("raw", "attempted", CultureInfo.InvariantCulture)}}
            };

            mvcStateDictionary.AdaptModelState(webformsStateDictionary, "");

            mvcStateDictionary.Count.ShouldBe(1);
            mvcStateDictionary["Test"].Value.AttemptedValue.ShouldBe("attempted");
            mvcStateDictionary["Test"].Value.RawValue.ShouldBe("raw");
            mvcStateDictionary["Test"].Value.Culture.ShouldBe(CultureInfo.InvariantCulture);
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