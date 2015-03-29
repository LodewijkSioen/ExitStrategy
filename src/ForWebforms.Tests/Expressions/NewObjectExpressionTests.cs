using Shouldly;

namespace ExitStrategy.ForWebforms.Tests.Expressions
{
    public class NewObjectExpressionTests
    {
        public void CanCreateAnObjectFromAString()
        {   
            var expressionBuilder = new NewObjectExpressionBuilder();
            var x = expressionBuilder.GetCodeExpressionImpl("Dummy()");
            
            x.ShouldNotBe(null);
            x.Value.ShouldBe("new Dummy()");
        }
    }
}