using System.CodeDom;
using System.Web.Compilation;
using System.Web.UI;

namespace ExitStrategy.ForWebforms
{
    public class NewObjectExpressionBuilder : ExpressionBuilder
    {
        private const string New = "new ";

        public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
        {
            return new CodeSnippetExpression(string.Concat(New, entry.Expression));
        }
    }
}