using Fixie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExitStrategy.ForWebforms.Tests
{
    public abstract class BasicConvention : Convention
    {
        public BasicConvention()
        {
            Classes.NameEndsWith("Tests").InTheSameNamespaceAs(this.GetType());;

            Methods.Where(m => m.IsVoid());

            Parameters.Add<FromInputAttributes>();

            HideExceptionDetails.For(typeof(Shouldly.Should));
        }
    }

    class FromInputAttributes : ParameterSource
    {
        public IEnumerable<object[]> GetParameters(MethodInfo method)
        {
            return method.GetCustomAttributes<InputAttribute>(true).Select(input => input.Parameters);
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class InputAttribute : Attribute
    {
        public InputAttribute(params object[] parameters)
        {
            Parameters = parameters;
        }

        public object[] Parameters { get; private set; }
    }
}
