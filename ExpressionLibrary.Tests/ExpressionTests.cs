using NUnit.Framework;

namespace ExpressionLibrary.Tests
{
    [TestFixture(Category = "Expressions")]
    public class ExpressionTests
    {
        [TestCase("1", 1)]
        [TestCase("add(1,1)", 2)]
        [TestCase("sub(1,1)", 0)]
        [TestCase("mul(2,1)", 2)]
        [TestCase("div(2,2)", 1)]
        [TestCase("let(a,1,a)", 1)]
        [TestCase("add(1, 2)", 3)]
        [TestCase("sub(111, -30000)", 30111)]
        [TestCase("div(5, 2)", 2)]
        [TestCase("add(1, mul(2, 3))", 7)]
        [TestCase("mul(add(2, 2), div(9, 3))", 12)]
        [TestCase("let(a, 5, add(a, a))", 10)]
        [TestCase("let(a, 5, let(b, mul(a, 10), add(b, a)))", 55)]
        [TestCase("let(a, let(b, 10, add(b, b)), let(b, 20, add(a, b)))", 40)]
        public void EvaluateTest(string expression, int expectedResult)
        {
            int result = Expression.Evaluate(expression);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("1", true, null)]
        [TestCase("", false, "The expression is null or empty")]
        [TestCase("   ", false, "The expression is null or empty")]
        [TestCase(null, false, "The expression is null or empty")]
        [TestCase("   1   ", true, null)]
        [TestCase("-1", true, null)]
        [TestCase("1a", false, "The expression is invalid")]
        [TestCase("mul", false, "The expression is invalid")]
        [TestCase("add", false, "The expression is invalid")]
        [TestCase("sub", false, "The expression is invalid")]
        [TestCase("div", false, "The expression is invalid")]
        [TestCase("let", false, "The expression is invalid")]
        [TestCase("add(1,1)", true, null)]
        [TestCase("sub(1,1)", true, null)]
        [TestCase("mul(1,1)", true, null)]
        [TestCase("div(1,1)", true, null)]
        [TestCase("let(a,1,a)", true, null)]
        public void ValidateExpressionTest(string testExpression, bool expectedResult, string expectedError)
        {
            bool result = Expression.ValidateExpression(testExpression, out string error);

            Assert.That(result, Is.EqualTo(expectedResult));
            Assert.That(error, Is.EqualTo(expectedError));
        }
    }
}