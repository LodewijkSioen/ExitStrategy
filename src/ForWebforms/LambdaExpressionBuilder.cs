using System.CodeDom;
using System.Web.Compilation;
using System.Web.UI;

namespace ExitStrategy.ForWebforms
{
    public class ObjectExpressionBuilder : ExpressionBuilder
    {
        public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
        {
            return new CodeSnippetExpression(entry.Expression);
        }
    }
}