using System;
using System.Collections.Generic;

namespace ExpressionLibrary.Expressions
{
    internal abstract class FunctionExpression : IExpression
    {
        private string leftExpression;
        private string rightExpression;

        public abstract int Evaluate(string expression);

        public virtual bool ValidateExpression(string expression, out string error)
        {
            bool result = true;
            error = null;

            try
            {
                ExtractExpressionSides(expression);
            }
            catch (ArithmeticException ex)
            {
                result = false;
                error = ex.Message;
            }
            catch (Exception ex)
            {
                result = false;
                error = $"An unexpected error occurred\r\n{ex.Message}";
            }

            return result;
        }

        protected int EvaluateLeftExpression()
        {
            return Expression.Evaluate(leftExpression);
        }

        protected int EvaluateRightExpression()
        {
            return Expression.Evaluate(rightExpression);
        }

        protected void ExtractExpressionSides(string expression)
        {
            leftExpression = null;
            rightExpression = null;
            int commaIndex = -1;
            Stack<int> paranIndexStack = new Stack<int>();

            try
            {
                for (int i = 0; i < expression.Length; i++)
                {
                    if (expression[i] == '(')
                    {
                        paranIndexStack.Push(i);
                    }
                    else if (expression[i] == ')')
                    {
                        paranIndexStack.Pop();

                        if (paranIndexStack.Count == 0)
                        {
                            rightExpression = expression.Substring(commaIndex + 1, i - 1 - commaIndex).Trim();
                        }
                    }
                    else if (expression[i] == ',')
                    {
                        if (paranIndexStack.Count == 1)
                        {
                            commaIndex = i;
                            int startIndex = paranIndexStack.Peek() + 1;
                            leftExpression = expression.Substring(startIndex, i - startIndex).Trim();
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new ArithmeticException("The parans open and close count did not match up", ex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArithmeticException("The end of the expression was reached before all parans closed", ex);
            }

            if (paranIndexStack.Count > 0)
            {
                throw new ArithmeticException("The end of the expression was reached before all parans closed");
            }

            if (string.IsNullOrWhiteSpace(leftExpression) || string.IsNullOrWhiteSpace(rightExpression))
            {
                throw new ArithmeticException("A function requires 2 valid expressions");
            }

            // encapsulate these so the "error" variable can be used twice
            {
                if (!Expression.ValidateExpression(leftExpression, out string error))
                {
                    throw new ArithmeticException($"The left expression is invalid: {error}");
                }
            }

            {
                if (!Expression.ValidateExpression(rightExpression, out string error))
                {
                    throw new ArithmeticException($"The right expression is invalid: {error}");
                }
            }
        }
    }
}