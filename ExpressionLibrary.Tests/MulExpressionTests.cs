using ExpressionLibrary.Expressions;
using NUnit.Framework;
using System;

namespace ExpressionLibrary.Tests
{
    [TestFixture(Category = "MulExpressions")]
    public class MulExpressionTests
    {
        [TestCase("mul(,1)")]
        [TestCase("mul(1,)")]
        [TestCase("mul(1,1")]
        [TestCase("mul(mul(1,1),)")]
        [TestCase("mul(mul(,1),1)")]
        [TestCase("mul(mul(1,),1)")]
        [TestCase("mul(mul(1,1,1)")]
        [TestCase("mul(mul1,1),1)")]
        [TestCase("mul(1,mul(1,))")]
        [TestCase("mul(1, mul(,1))")]
        [TestCase("mul(1, mul(,))")]
        [TestCase("mul(, mul(1,1))")]
        [TestCase("mul(1, mul(1,1)")]
        [TestCase("mul(1, mul1,1))")]
        public void ErrorEvaluateTests(string expression)
        {
            var mulExpression = new MulExpression();

            Assert.Throws(typeof(ArithmeticException), () => mulExpression.Evaluate(expression));
        }

        [TestCase("mul(1,2)", 2)]
        [TestCase("mul(1, 2)", 2)]
        [TestCase("mul (1,2)", 2)]
        [TestCase("mul (1 , 2)", 2)]
        [TestCase("mul(mul(1,2),2)", 4)]
        [TestCase("mul(mul(1,2), mul(1 , 2))", 4)]
        [TestCase("mul(mul(mul(1,2),2),2)", 8)]
        public void EvaluateTests(string expression, int expectedResult)
        {
            var mulExpression = new MulExpression();

            int result = mulExpression.Evaluate(expression);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("mul(1,1)", true)]
        [TestCase("mul(1, 1)", true)]
        [TestCase("mul (1,1)", true)]
        [TestCase("mul (1 , 1)", true)]
        [TestCase("mul(mul(1,1),1)", true)]
        [TestCase("mul(mul(1,1),mul(1,1))", true)]
        [TestCase("1", false)]
        [TestCase("s", false)]
        [TestCase("add(1,1)", false)]
        [TestCase("mu", false)]
        [TestCase("mu(", false)]
        [TestCase("m-u-l(", false)]
        public void IsMatchTests(string expression, bool expectedResult)
        {
            bool result = MulExpression.IsMatch(expression);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("mul(1,1)", true, null)]
        [TestCase("mul(1, 1)", true, null)]
        [TestCase("mul (1,1)", true, null)]
        [TestCase("mul (1 , 1)", true, null)]
        [TestCase("mul(mul(1,1),1)", true, null)]
        [TestCase("mul(mul(1,1), mul(1 , 1))", true, null)]
        [TestCase("mul(,1)", false, "A function requires 2 valid expressions")]
        [TestCase("mul(1,)", false, "A function requires 2 valid expressions")]
        [TestCase("mul(1,1", false, "The end of the expression was reached before all parans closed")]
        [TestCase("mul(mul(1,1),)", false, "A function requires 2 valid expressions")]
        [TestCase("mul(mul(,1),1)", false, "The left expression is invalid: A function requires 2 valid expressions")]
        [TestCase("mul(mul(1,),1)", false, "The left expression is invalid: A function requires 2 valid expressions")]
        [TestCase("mul(mul(1,1,1)", false, "The end of the expression was reached before all parans closed")]
        [TestCase("mul(mul1,1),1)", false, "The parans open and close count did not match up")]
        [TestCase("mul(1,mul(1,))", false, "The right expression is invalid: A function requires 2 valid expressions")]
        [TestCase("mul(1, mul(,1))", false, "The right expression is invalid: A function requires 2 valid expressions")]
        [TestCase("mul(1, mul(,))", false, "The right expression is invalid: A function requires 2 valid expressions")]
        [TestCase("mul(, mul(1,1))", false, "A function requires 2 valid expressions")]
        [TestCase("mul(1, mul(1,1)", false, "The end of the expression was reached before all parans closed")]
        [TestCase("mul(1, mul1,1))", false, "The parans open and close count did not match up")]
        public void ValidateExpressionTests(string expression, bool expectedResult, string expectedError)
        {
            var mulExpression = new MulExpression();

            bool result = mulExpression.ValidateExpression(expression, out string error);

            Assert.That(result, Is.EqualTo(expectedResult));
            Assert.That(error, Is.EqualTo(expectedError));
        }
    }
}