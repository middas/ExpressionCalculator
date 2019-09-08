using ExpressionLibrary.Expressions;
using NUnit.Framework;
using System;

namespace ExpressionLibrary.Tests
{
    [TestFixture(Category = "SubExpressions")]
    public class SubExpressionTests
    {
        [TestCase("sub(,1)")]
        [TestCase("sub(1,)")]
        [TestCase("sub(1,1")]
        [TestCase("sub(sub(1,1),)")]
        [TestCase("sub(sub(,1),1)")]
        [TestCase("sub(sub(1,),1)")]
        [TestCase("sub(sub(1,1,1)")]
        [TestCase("sub(sub1,1),1)")]
        [TestCase("sub(1,sub(1,))")]
        [TestCase("sub(1, sub(,1))")]
        [TestCase("sub(1, sub(,))")]
        [TestCase("sub(, sub(1,1))")]
        [TestCase("sub(1, sub(1,1)")]
        [TestCase("sub(1, sub1,1))")]
        public void ErrorEvaluateTests(string expression)
        {
            var subExpression = new SubExpression();

            Assert.Throws(typeof(ArithmeticException), () => subExpression.Evaluate(expression));
        }

        [TestCase("sub(1,1)", 0)]
        [TestCase("sub(1, 1)", 0)]
        [TestCase("sub (1,1)", 0)]
        [TestCase("sub (1 , 1)", 0)]
        [TestCase("sub(sub(1,1),1)", -1)]
        [TestCase("sub(sub(1,1), sub(1 , 1))", 0)]
        [TestCase("sub(sub(sub(1,1),1),1)", -2)]
        public void EvaluateTests(string expression, int expectedResult)
        {
            var subExpression = new SubExpression();

            int result = subExpression.Evaluate(expression);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("sub(1,1)", true)]
        [TestCase("sub(1, 1)", true)]
        [TestCase("sub (1,1)", true)]
        [TestCase("sub (1 , 1)", true)]
        [TestCase("sub(sub(1,1),1)", true)]
        [TestCase("sub(mul(1,1),sub(1,1))", true)]
        [TestCase("1", false)]
        [TestCase("s", false)]
        [TestCase("add(1,1)", false)]
        [TestCase("su", false)]
        [TestCase("su(", false)]
        [TestCase("s-u-b(", false)]
        public void IsMatchTests(string expression, bool expectedResult)
        {
            bool result = SubExpression.IsMatch(expression);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("sub(1,1)", true, null)]
        [TestCase("sub(1, 1)", true, null)]
        [TestCase("sub (1,1)", true, null)]
        [TestCase("sub (1 , 1)", true, null)]
        [TestCase("sub(sub(1,1),1)", true, null)]
        [TestCase("sub(sub(1,1), sub(1 , 1))", true, null)]
        [TestCase("sub(,1)", false, "A function requires 2 valid expressions")]
        [TestCase("sub(1,)", false, "A function requires 2 valid expressions")]
        [TestCase("sub(1,1", false, "The end of the expression was reached before all parans closed")]
        [TestCase("sub(sub(1,1),)", false, "A function requires 2 valid expressions")]
        [TestCase("sub(sub(,1),1)", false, "The left expression is invalid: A function requires 2 valid expressions")]
        [TestCase("sub(sub(1,),1)", false, "The left expression is invalid: A function requires 2 valid expressions")]
        [TestCase("sub(sub(1,1,1)", false, "The end of the expression was reached before all parans closed")]
        [TestCase("sub(sub1,1),1)", false, "The parans open and close count did not match up")]
        [TestCase("sub(1,sub(1,))", false, "The right expression is invalid: A function requires 2 valid expressions")]
        [TestCase("sub(1, sub(,1))", false, "The right expression is invalid: A function requires 2 valid expressions")]
        [TestCase("sub(1, sub(,))", false, "The right expression is invalid: A function requires 2 valid expressions")]
        [TestCase("sub(, sub(1,1))", false, "A function requires 2 valid expressions")]
        [TestCase("sub(1, sub(1,1)", false, "The end of the expression was reached before all parans closed")]
        [TestCase("sub(1, sub1,1))", false, "The parans open and close count did not match up")]
        public void ValidateExpressionTests(string expression, bool expectedResult, string expectedError)
        {
            var subExpression = new SubExpression();

            bool result = subExpression.ValidateExpression(expression, out string error);

            Assert.That(result, Is.EqualTo(expectedResult));
            Assert.That(error, Is.EqualTo(expectedError));
        }
    }
}