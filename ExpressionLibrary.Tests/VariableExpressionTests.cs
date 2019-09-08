using ExpressionLibrary.Expressions;
using NUnit.Framework;
using System;

namespace ExpressionLibrary.Tests
{
    [TestFixture(Category = "VariableExpressions")]
    public class VariableExpressionTests
    {
        [OneTimeTearDown]
        public void CleanUp()
        {
            VariableExpression.ClearVariables();
        }

        [TestCase("1a", "1a", 1)]
        [TestCase("1", "1", 1)]
        [TestCase("a_", "a_", 1)]
        [TestCase("a", null, 1)]
        public void ErrorEvaluateTest(string expression, string variable, int value)
        {
            var variableExpression = new VariableExpression();

            VariableExpression.ClearVariables();

            if (variable != null)
            {
                VariableExpression.AddVariable(variable, value);
            }

            Assert.Throws(typeof(ArithmeticException), () => variableExpression.Evaluate(expression));
        }

        [TestCase("a", 1, "a", 1)]
        [TestCase("abc", 1, "abc", 1)]
        [TestCase("a-b", 1, "a-b", 1)]
        [TestCase("a1", 1, "a1", 1)]
        [TestCase("a-1", 1, "a-1", 1)]
        [TestCase("a-1-b", 1, "a-1-b", 1)]
        public void EvaluateTest(string expression, int expectedResult, string variable, int value)
        {
            var variableExpression = new VariableExpression();

            VariableExpression.ClearVariables();

            if (variable != null)
            {
                VariableExpression.AddVariable(variable, value);
            }

            int result = variableExpression.Evaluate(expression);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("a", true)]
        [TestCase("abc", true)]
        [TestCase("a-b", true)]
        [TestCase("a1", true)]
        [TestCase("a-1", true)]
        [TestCase("a-1-b", true)]
        [TestCase("1a", false)]
        [TestCase("1", false)]
        [TestCase("a_", false)]
        public void IsMatchTest(string expression, bool expectedResult)
        {
            bool result = VariableExpression.IsMatch(expression);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [OneTimeSetUp]
        public void Setup()
        {
            VariableExpression.ClearVariables();
        }

        [TestCase("a", true, null, "a", 1)]
        [TestCase("abc", true, null, "abc", 1)]
        [TestCase("a-b", true, null, "a-b", 1)]
        [TestCase("a1", true, null, "a1", 1)]
        [TestCase("a-1", true, null, "a-1", 1)]
        [TestCase("a-1-b", true, null, "a-1-b", 1)]
        [TestCase("1a", false, "The expression is invalid", "1a", 1)]
        [TestCase("1", false, "The expression is invalid", "1", 1)]
        [TestCase("a_", false, "The expression is invalid", "a_", 1)]
        [TestCase("a", false, "Variable has not yet been initialized", null, 1)]
        public void ValidateExpressionTest(string expression, bool expectedResult, string expectedError, string variable, int value)
        {
            var variableExpression = new VariableExpression();

            VariableExpression.ClearVariables();

            if (variable != null)
            {
                VariableExpression.AddVariable(variable, value);
            }

            bool result = variableExpression.ValidateExpression(expression, out string error);

            Assert.That(result, Is.EqualTo(expectedResult));
            Assert.That(error, Is.EqualTo(expectedError));
        }
    }
}