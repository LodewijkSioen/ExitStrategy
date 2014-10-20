using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace ExitStrategy.ForWebforms
{
    public static class PropertyHelper
    {
        public static ModelMetadata GetMetadataForExpression(this ModelMetadataProvider provider,
            Expression<Func<Object>> expresion)
        {
            var propetryInfo = GetProperty(expresion);
            if (propetryInfo == null)
            {
                return null;
            }

            return provider.GetMetadataForProperty((Func<object>)null, propetryInfo.DeclaringType, propetryInfo.Name);
        }


        private static PropertyInfo GetProperty(
            Expression<Func<object>> selector)
        {
            Expression body = selector;
            if (body is LambdaExpression)
            {
                body = ((LambdaExpression)body).Body;
            }
            switch (body.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return (PropertyInfo)((MemberExpression)body).Member;
                case ExpressionType.Convert:
                    return (PropertyInfo)((MemberExpression)((UnaryExpression)body).Operand).Member;
                case ExpressionType.Call:
                    return null;//Don't support this for methods/indexers
                default:
                    throw new InvalidOperationException("Cannot handle ExpressionType " + body.NodeType);
            }
        }
    }
}
