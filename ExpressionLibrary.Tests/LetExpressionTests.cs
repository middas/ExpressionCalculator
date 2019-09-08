using ExpressionLibrary.Expressions;
using NUnit.Framework;
using System;

namespace ExpressionLibrary.Tests
{
    [TestFixture(Category = "LetExpressions")]
    public class LetExpressionTests
    {
        [TestCase("let(,1,a)")]
        [TestCase("let(a,,a)")]
        [TestCase("let(a,1,)")]
        [TestCase("let(a,add(1,),a)")]
        [TestCase("let(a,1,add(a,))")]
        [TestCase("let(a,add(a,1),a)")]
        public void ErrorEvaluateTests(string expression)
        {
            var letExpression = new LetExpression();

            Assert.Throws(typeof(ArithmeticException), () => letExpression.Evaluate(expression));
        }

        [TestCase("let(a,1,a)", 1)]
        [TestCase("let(a, 1, a)", 1)]
        [TestCase("let (a,1,a)", 1)]
        [TestCase("let (a , 1,a)", 1)]
        [TestCase("let(a-1,1,a-1)", 1)]
        [TestCase("let(a-b,1,a-b)", 1)]
        [TestCase("let(abc,1,abc)", 1)]
        [TestCase("let(a, add(1,1),a)", 2)]
        [TestCase("let(a, mul(1,1),sub(a,1))", 0)]
        [TestCase("let(a, 1, let(b, 2, add(a,b)))", 3)]
        [TestCase("let(a, 1, let(abc, 2, add(a,abc)))", 3)]
        [TestCase("let(abc, 1, let(b, 2, add(abc,b)))", 3)]
        public void EvaluateTests(string expression, int expectedResult)
        {
            var letExpression = new LetExpression();

            int result = letExpression.Evaluate(expression);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("let(a,1,a)", true)]
        [TestCase("let(a, 1, a)", true)]
        [TestCase("let (a,1,a)", true)]
        [TestCase("let (a , 1,a)", true)]
        [TestCase("let(a, add(1,1),a)", true)]
        [TestCase("let(a, mul(1,1),sub(a,1))", true)]
        [TestCase("1", false)]
        [TestCase("a", false)]
        [TestCase("sub(1,1)", false)]
        [TestCase("le", false)]
        [TestCase("le(", false)]
        [TestCase("l-e-t(", false)]
        public void IsMatchTests(string expression, bool expectedResult)
        {
            bool result = LetExpression.IsMatch(expression);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("let(a,1,a)", true, null)]
        [TestCase("let(a, 1, a)", true, null)]
        [TestCase("let (a,1,a)", true, null)]
        [TestCase("let (a , 1,a)", true, null)]
        [TestCase("let(a, add(1,1),a)", true, null)]
        [TestCase("let(a, mul(1,1),sub(a,1))", true, null)]
        [TestCase("let(a, 1, let(b, 2, add(a,b)))", true, null)]
        [TestCase("let(a, 1, let(abc, 2, add(a,abc)))", true, null)]
        [TestCase("let(abc,1,abc)", true, null)]
        [TestCase("let(a-b,1,a-b)", true, null)]
        [TestCase("let(a-1,1,a-1)", true, null)]
        [TestCase("let(,1,a)", false, "No variable name was provided")]
        [TestCase("let(a,,a)", false, "No value expression was provided")]
        [TestCase("let(a,1,)", false, "No result expression was provided")]
        [TestCase("let(a,add(1,),a)", false, "The value expression is invalid: A function requires 2 valid expressions")]
        [TestCase("let(a,1,add(a,))", false, "The result expression is invalid: A function requires 2 valid expressions")]
        [TestCase("let(a,add(a,1),a)", false, "The value expression cannot have the variable in it")]
        [TestCase("let(mul,1,mul)", false, "Invalid variable name provided")]
        [TestCase("let(div,1,div)", false, "Invalid variable name provided")]
        [TestCase("let(sub,1,sub)", false, "Invalid variable name provided")]
        [TestCase("let(add,1,add)", false, "Invalid variable name provided")]
        [TestCase("let(let,1,let)", false, "Invalid variable name provided")]
        [TestCase("let(1a,1,1a)", false, "Invalid variable name provided")]
        public void ValidateExpressionTests(string expression, bool expectedResult, string expectedError)
        {
            var letExpression = new LetExpression();

            bool result = letExpression.ValidateExpression(expression, out string error);

            Assert.That(result, Is.EqualTo(expectedResult));
            Assert.That(error, Is.EqualTo(expectedError));
        }
    }
}