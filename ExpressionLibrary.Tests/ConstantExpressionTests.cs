using ExpressionLibrary.Expressions;
using NUnit.Framework;
using System;

namespace ExpressionLibrary.Tests
{
    [TestFixture(Category = "Constants")]
    public class ConstantExpressionTests
    {
        [TestCase("-0")]
        [TestCase("a")]
        [TestCase("a1")]
        [TestCase("1a")]
        [TestCase("32768")]
        [TestCase("-32769")]
        public void ErrorEvaluateTest(string expression)
        {
            var constantExpression = new ConstantExpression();

            Assert.Throws(typeof(ArithmeticException), () => constantExpression.Evaluate(expression));
        }

        [TestCase("0", 0)]
        [TestCase("1", 1)]
        [TestCase("-1", -1)]
        [TestCase("32767", 32767)]
        [TestCase("-32767", -32767)]
        public void EvaluateTest(string expression, short expectedResult)
        {
            var constantExpression = new ConstantExpression();

            int result = constantExpression.Evaluate(expression);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("1", true)]
        [TestCase("-1", true)]
        [TestCase("0", true)]
        [TestCase("32767", true)]
        [TestCase("-32768", true)]
        [TestCase("-0", true)]
        [TestCase("a", false)]
        [TestCase("1a", false)]
        [TestCase("a1", false)]
        [TestCase("32768", true)]
        [TestCase("-32769", true)]
        public void IsMatchTests(string expression, bool expectedResult)
        {
            bool result = ConstantExpression.IsMatch(expression);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("1", true, null)]
        [TestCase("-1", true, null)]
        [TestCase("0", true, null)]
        [TestCase("32767", true, null)]
        [TestCase("-32768", true, null)]
        [TestCase("-0", false, "Not a valid number")]
        [TestCase("a", false, "Not a valid number")]
        [TestCase("1a", false, "Not a valid number")]
        [TestCase("a1", false, "Not a valid number")]
        [TestCase("32768", false, "The number is outside the valid range (-32768 - 32767)")]
        [TestCase("-32769", false, "The number is outside the valid range (-32768 - 32767)")]
        public void ValidateExpressionTest(string expression, bool expectedResult, string expectedError)
        {
            var constantExpression = new ConstantExpression();

            bool result = constantExpression.ValidateExpression(expression, out string error);

            Assert.That(result, Is.EqualTo(expectedResult));
            Assert.That(error, Is.EqualTo(expectedError));
        }
    }
}