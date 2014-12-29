using Shouldly;

namespace ExitStrategy.ForWebforms.Tests
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