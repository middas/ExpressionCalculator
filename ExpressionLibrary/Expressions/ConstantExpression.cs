using System;
using System.Linq;

namespace ExpressionLibrary.Expressions
{
    internal class ConstantExpression : IExpression
    {
        public static bool IsMatch(string expression)
        {
            // remove a leading hyphen, it is a valid character, but slows down the number check
            if (expression[0] == '-')
            {
                expression = expression.Substring(1);
            }

            return expression.All(c => c >= '0' && c <= '9');
        }

        public int Evaluate(string expression)
        {
            if (expression.Equals("-0"))
            {
                throw new ArithmeticException("-0 is not a valid number");
            }

            if (short.TryParse(expression, out short result))
            {
                return result;
            }

            throw new ArithmeticException($"Invalid constant value, must be a number between {short.MinValue} and {short.MaxValue}");
        }

        public bool ValidateExpression(string expression, out string error)
        {
            bool result = false;
            error = "Not a valid number";

            // evaluate using int64 instead of int16 to give better errors
            if (!expression.Equals("-0") && long.TryParse(expression, out long i))
            {
                if (i > short.MaxValue || i < short.MinValue)
                {
                    error = $"The number is outside the valid range ({short.MinValue} - {short.MaxValue})";
                }
                else
                {
                    result = true;
                    error = null;
                }
            }

            return result;
        }
    }
}