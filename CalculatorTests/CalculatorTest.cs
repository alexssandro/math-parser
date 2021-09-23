using FluentAssertions;
using Xunit;

namespace CalculatorTests
{
    public class CalculatorTest
    {
        [Fact]
        public void GivenExpression1Plus3_ShouldReturn4()
        {
            string expression = "1+3";
            var result = Calculator.Calculator.Calc(expression);
            result.Success.Should().BeTrue();
            result.Result.Should().Be(4);
        }

        [Fact]
        public void GivenExpression5Minus2_ShouldReturn3()
        {
            string expression = "5-2";
            var result = Calculator.Calculator.Calc(expression);
            result.Success.Should().BeTrue();
            result.Result.Should().Be(3);
        }

        [Fact]
        public void GivenExpression5Plus2_ShouldReturn10()
        {
            string expression = "5*2";
            var result = Calculator.Calculator.Calc(expression);
            result.Success.Should().BeTrue();
            result.Result.Should().Be(10);
        }

        [Fact]
        public void GivenExpression10DivideBy2_ShouldReturn5()
        {
            string expression = "10/2";
            var result = Calculator.Calculator.Calc(expression);
            result.Success.Should().BeTrue();
            result.Result.Should().Be(5);
        }

        [Fact]
        public void GivenExpression10DivideBy0_ShouldReturnError()
        {
            string expression = "10/0";
            var result = Calculator.Calculator.Calc(expression);
            result.Success.Should().BeFalse();
            result.Error.Should().Be("Syntax error, division by zero is not possible");
        }

        [Fact]
        public void GivenExpression1Plus1Plus2ShouldBe4()
        {
            string expression = "1+1+2";
            var result = Calculator.Calculator.Calc(expression);
            result.Success.Should().BeTrue();
            result.Result.Should().Be(4);
        }

        [Fact]
        public void GivenExpression1Plus1Minus2ShouldBe0()
        {
            string expression = "1+1-2";
            var result = Calculator.Calculator.Calc(expression);
            result.Success.Should().BeTrue();
            result.Result.Should().Be(0);
        }

        [Fact]
        public void GivenExpression1Minus1Plus2ShouldBe2()
        {
            string expression = "1-1+2";
            var result = Calculator.Calculator.Calc(expression);
            result.Success.Should().BeTrue();
            result.Result.Should().Be(2);
        }

        [Fact]
        public void GivenExpression1Minus1Times2ShouldBeMinus1()
        {
            string expression = "1-1*2";
            var result = Calculator.Calculator.Calc(expression);
            result.Success.Should().BeTrue();
            result.Result.Should().Be(-1);
        }

        [Fact]
        public void GivenExpression1Plus1Times4DivideBy2ShouldBeMinus3()
        {
            string expression = "1+1*4/2";
            var result = Calculator.Calculator.Calc(expression);
            result.Success.Should().BeTrue();
            result.Result.Should().Be(3);
        }

        [Fact]
        public void GivenExpressionWithAllOperatorsShouldBe54()
        {
            string expression = "104-52+1*1*4/2";
            var result = Calculator.Calculator.Calc(expression);
            result.Success.Should().BeTrue();
            result.Result.Should().Be(54);
        }

        [Fact]
        public void GivenExpressionWithNoSignalsShouldReturnError()
        {
            string expression = "13";
            var result = Calculator.Calculator.Calc(expression);
            result.Success.Should().BeFalse();
            result.Error.Should().Be("There are no signals in expression");
        }
    }
}
