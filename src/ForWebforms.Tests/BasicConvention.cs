using Fixie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExitStrategy.ForWebforms.Tests
{
    public abstract class BasicConvention : Convention
    {
        protected BasicConvention()
        {
            Classes.NameEndsWith("Tests").InTheSameNamespaceAs(this.GetType());;

            Methods.Where(m => m.IsVoid());

            Parameters
                .Add<FromInputAttributes>()
                .Add<FromDataSourceAttributes>();

            HideExceptionDetails.For(typeof(Shouldly.Should));
        }
    }

    public class FromDataSourceAttributes : ParameterSource
    {
        public IEnumerable<object[]> GetParameters(MethodInfo method)
        {
            var attribute = method.GetCustomAttribute<InputSourceAttribute>(true);
            if (attribute == null)
            {
                return Enumerable.Empty<object[]>();
            }

            var testClass = method.DeclaringType;
            if (testClass == null)
            {
                throw new NullReferenceException(string.Format("Cannot determine the Class for Method '{0}' decorated with the DataSourceAttribute", method.Name));
            }

            var sourceMethod = testClass.GetMethod(attribute.MethodName);

            if (!sourceMethod.IsStatic)
            {
                throw new InvalidOperationException(string.Format("Cannot find a static method called '{0}' in class '{1}' to use as a ParameterSource for test '{2}'",
                    attribute.MethodName, testClass.Name, method.Name));
            }

            if (sourceMethod.ReturnType != typeof (IEnumerable<object[]>))
            {
                throw new InvalidOperationException(string.Format("Method '{0}' in class '{1}' used as a ParameterSource for test '{2}' does not return an 'IEnumerable<object[]>'.",
                    sourceMethod.Name, testClass.Name, method.Name));
            }

            return (IEnumerable<object[]>)sourceMethod.Invoke(null, null);
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class InputSourceAttribute :Attribute
    {
        public string MethodName { get; private set; }

        public InputSourceAttribute(string methodName)
        {
            MethodName = methodName;
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
