using System;

namespace ExpressionLibrary
{
    public class Expression
    {
        /// <summary>
        /// Evaluates the given expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="ArithmeticException"></exception>
        public static int Evaluate(string expression)
        {
            var evaluator = ExpressionFactory.CreateFromExpression(expression);

            if (evaluator != null)
            {
                return evaluator.Evaluate(expression);
            }

            throw new ArithmeticException("The expression is invalid");
        }

        public static bool ValidateExpression(string expression, out string error)
        {
            bool success = false;
            error = "The expression is invalid";

            // check for invalid input
            if (string.IsNullOrWhiteSpace(expression))
            {
                success = false;
                error = "The expression is null or empty";
            }
            else
            {
                // whitespace is ignored
                expression = expression.Trim();

                var evaluator = ExpressionFactory.CreateFromExpression(expression);

                if (evaluator == null)
                {
                    success = false;
                    error = "The expression is invalid";
                }
                else
                {
                    success = evaluator.ValidateExpression(expression, out error);
                }
            }

            return success;
        }
    }
}