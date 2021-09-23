using FluentAssertions;
using Xunit;

namespace CalculatorTests
{
    public class CalculatorWithParenthesesTest
    {
        [Fact]
        public void GivenExpressionWithInvalidNumberOfParenthesesOpeningAndClosingShouldReturn()
        {
            string expression = "(()";
            var result = Calculator.Calculator.CalcWithParentheses(expression);
            result.Success.Should().BeFalse();
            result.Error.Should().Be("the number of parentheses opening must be the same as parentheses closing");
        }

        [Fact]
        public void GivenExpressionWithParenthesesBackwarsShouldReturnFalse()
        {
            string expression = "))((";
            var result = Calculator.Calculator.CalcWithParentheses(expression);
            result.Success.Should().BeFalse();
            result.Error.Should().Be("for every parentheses opening must be an after parentheses closing");
        }

        [Fact]
        public void GivenExpressionWithMoreParenthesesClosingShouldReturnFalse()
        {
            string expression = "())(";
            var result = Calculator.Calculator.CalcWithParentheses(expression);
            result.Success.Should().BeFalse();
            result.Error.Should().Be("for every parentheses opening must be an after parentheses closing");
        }

        [Fact]
        public void GivenExpressionWithTwoExpressionsShouldReturn()
        {
            string expression = "((1+2)+5)";
            var result = Calculator.Calculator.CalcWithParentheses(expression);
            result.Success.Should().BeTrue();
            result.Result.Should().Be(8);
        }
    }
}
