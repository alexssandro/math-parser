using FluentAssertions;
using System;
using Xunit;

namespace Calculator
{
    public class CalculatorTest
    {
        [Fact]
        public void GivenExpression1Plus2_ShouldReturn3()
        {
            string expression = "1+3";
            var result = Calculator.Calc(expression);
            result.Success.Should().BeTrue();
            result.Result.Should().Be(3);
        }

        [Fact]
        public void GivenExpressionWithNoSignalsShouldReturnError()
        {
            string expression = "13";
            var result = Calculator.Calc(expression);
            result.Success.Should().BeFalse();
            result.Error.Should().Be("There are no signals in expression");
        }
    }
}
