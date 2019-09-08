using ExpressionLibrary.Expressions;
using NUnit.Framework;
using System;

namespace ExpressionLibrary.Tests
{
    [TestFixture(Category = "AddExpressions")]
    public class AddExpressionTests
    {
        [TestCase("add(,1)")]
        [TestCase("add(1,)")]
        [TestCase("add(1,1")]
        [TestCase("add(add(1,1),)")]
        [TestCase("add(add(,1),1)")]
        [TestCase("add(add(1,),1)")]
        [TestCase("add(add(1,1,1)")]
        [TestCase("add(add1,1),1)")]
        [TestCase("add(1,add(1,))")]
        [TestCase("add(1, add(,1))")]
        [TestCase("add(1, add(,))")]
        [TestCase("add(, add(1,1))")]
        [TestCase("add(1, add(1,1)")]
        [TestCase("add(1, add1,1))")]
        public void ErrorEvaluateTest(string expression)
        {
            var addExpression = new AddExpression();

            Assert.Throws(typeof(ArithmeticException), () => addExpression.Evaluate(expression));
        }

        [TestCase("add(1,1)", 2)]
        [TestCase("add(1, 1)", 2)]
        [TestCase("add (1,1)", 2)]
        [TestCase("add (1 , 1)", 2)]
        [TestCase("add(add(1,1),1)", 3)]
        [TestCase("add(add(1,1), add(1 , 1))", 4)]
        [TestCase("add(add(add(1,1),1),1)", 4)]
        public void EvaluateTest(string expression, int expectedResult)
        {
            var addExpression = new AddExpression();

            int result = addExpression.Evaluate(expression);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("add(1,1)", true)]
        [TestCase("add(1, 1)", true)]
        [TestCase("add (1,1)", true)]
        [TestCase("add (1 , 1)", true)]
        [TestCase("add(add(1,1),1)", true)]
        [TestCase("add(mul(1,1),sub(1,1))", true)]
        [TestCase("1", false)]
        [TestCase("a", false)]
        [TestCase("sub(1,1)", false)]
        [TestCase("ad", false)]
        [TestCase("ad(", false)]
        [TestCase("a-d-d(", false)]
        public void IsMatchTest(string expression, bool expectedResult)
        {
            bool result = AddExpression.IsMatch(expression);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("add(1,1)", true, null)]
        [TestCase("add(1, 1)", true, null)]
        [TestCase("add (1,1)", true, null)]
        [TestCase("add (1 , 1)", true, null)]
        [TestCase("add(add(1,1),1)", true, null)]
        [TestCase("add(add(1,1), add(1 , 1))", true, null)]
        [TestCase("add(,1)", false, "A function requires 2 valid expressions")]
        [TestCase("add(1,)", false, "A function requires 2 valid expressions")]
        [TestCase("add(1,1", false, "The end of the expression was reached before all parans closed")]
        [TestCase("add(add(1,1),)", false, "A function requires 2 valid expressions")]
        [TestCase("add(add(,1),1)", false, "The left expression is invalid: A function requires 2 valid expressions")]
        [TestCase("add(add(1,),1)", false, "The left expression is invalid: A function requires 2 valid expressions")]
        [TestCase("add(add(1,1,1)", false, "The end of the expression was reached before all parans closed")]
        [TestCase("add(add1,1),1)", false, "The parans open and close count did not match up")]
        [TestCase("add(1,add(1,))", false, "The right expression is invalid: A function requires 2 valid expressions")]
        [TestCase("add(1, add(,1))", false, "The right expression is invalid: A function requires 2 valid expressions")]
        [TestCase("add(1, add(,))", false, "The right expression is invalid: A function requires 2 valid expressions")]
        [TestCase("add(, add(1,1))", false, "A function requires 2 valid expressions")]
        [TestCase("add(1, add(1,1)", false, "The end of the expression was reached before all parans closed")]
        [TestCase("add(1, add1,1))", false, "The parans open and close count did not match up")]
        public void ValidateExpressionTest(string expression, bool expectedResult, string expectedError)
        {
            var addExpression = new AddExpression();

            bool result = addExpression.ValidateExpression(expression, out string error);

            Assert.That(result, Is.EqualTo(expectedResult));
            Assert.That(error, Is.EqualTo(expectedError));
        }
    }
}