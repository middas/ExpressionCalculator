using ExpressionLibrary.Expressions;
using NUnit.Framework;
using System;

namespace ExpressionLibrary.Tests
{
    [TestFixture(Category = "DivExpressions")]
    public class DivExpressionTests
    {
        [TestCase("div(,1)")]
        [TestCase("div(1,)")]
        [TestCase("div(1,1")]
        [TestCase("div(div(1,1),)")]
        [TestCase("div(div(,1),1)")]
        [TestCase("div(div(1,),1)")]
        [TestCase("div(div(1,1,1)")]
        [TestCase("div(div1,1),1)")]
        [TestCase("div(1,div(1,))")]
        [TestCase("div(1, div(,1))")]
        [TestCase("div(1, div(,))")]
        [TestCase("div(, div(1,1))")]
        [TestCase("div(1, div(1,1)")]
        [TestCase("div(1, div1,1))")]
        public void ErrorEvaluateTests(string expression)
        {
            var divExpression = new DivExpression();

            Assert.Throws(typeof(ArithmeticException), () => divExpression.Evaluate(expression));
        }

        [Test]
        public void EvaluateDivideByZero()
        {
            var divExpression = new DivExpression();

            Assert.Throws(typeof(DivideByZeroException), () => divExpression.Evaluate("div(1,0)"));
        }

        [TestCase("div(2,1)", 2)]
        [TestCase("div(2, 1)", 2)]
        [TestCase("div (2,1)", 2)]
        [TestCase("div (2 , 1)", 2)]
        [TestCase("div(div(2,1),2)", 1)]
        [TestCase("div(div(2,1), div(2 , 2))", 2)]
        [TestCase("div(div(div(8,1),2),2)", 2)]
        public void EvaluateTests(string expression, int expectedResult)
        {
            var divExpression = new DivExpression();

            int result = divExpression.Evaluate(expression);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("div(1,1)", true)]
        [TestCase("div(1, 1)", true)]
        [TestCase("div (1,1)", true)]
        [TestCase("div (1 , 1)", true)]
        [TestCase("div(div(1,1),1)", true)]
        [TestCase("div(div(1,1),div(1,1))", true)]
        [TestCase("1", false)]
        [TestCase("s", false)]
        [TestCase("add(1,1)", false)]
        [TestCase("di", false)]
        [TestCase("di(", false)]
        [TestCase("d-i-v(", false)]
        public void IsMatchTests(string expression, bool expectedResult)
        {
            bool result = DivExpression.IsMatch(expression);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("div(1,1)", true, null)]
        [TestCase("div(1, 1)", true, null)]
        [TestCase("div (1,1)", true, null)]
        [TestCase("div (1 , 1)", true, null)]
        [TestCase("div(div(1,1),1)", true, null)]
        [TestCase("div(div(1,1), div(1 , 1))", true, null)]
        [TestCase("div(,1)", false, "A function requires 2 valid expressions")]
        [TestCase("div(1,)", false, "A function requires 2 valid expressions")]
        [TestCase("div(1,1", false, "The end of the expression was reached before all parans closed")]
        [TestCase("div(div(1,1),)", false, "A function requires 2 valid expressions")]
        [TestCase("div(div(,1),1)", false, "The left expression is invalid: A function requires 2 valid expressions")]
        [TestCase("div(div(1,),1)", false, "The left expression is invalid: A function requires 2 valid expressions")]
        [TestCase("div(div(1,1,1)", false, "The end of the expression was reached before all parans closed")]
        [TestCase("div(div1,1),1)", false, "The parans open and close count did not match up")]
        [TestCase("div(1,div(1,))", false, "The right expression is invalid: A function requires 2 valid expressions")]
        [TestCase("div(1, div(,1))", false, "The right expression is invalid: A function requires 2 valid expressions")]
        [TestCase("div(1, div(,))", false, "The right expression is invalid: A function requires 2 valid expressions")]
        [TestCase("div(, div(1,1))", false, "A function requires 2 valid expressions")]
        [TestCase("div(1, div(1,1)", false, "The end of the expression was reached before all parans closed")]
        [TestCase("div(1, div1,1))", false, "The parans open and close count did not match up")]
        public void ValidateExpressionTests(string expression, bool expectedResult, string expectedError)
        {
            var divExpression = new DivExpression();

            bool result = divExpression.ValidateExpression(expression, out string error);

            Assert.That(result, Is.EqualTo(expectedResult));
            Assert.That(error, Is.EqualTo(expectedError));
        }
    }
}