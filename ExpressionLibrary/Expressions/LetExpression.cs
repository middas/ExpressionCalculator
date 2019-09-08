using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExpressionLibrary.Expressions
{
    internal class LetExpression : IExpression
    {
        private const string ValidLetPattern = "^[Ll][Ee][Tt] *\\(";
        private const string ValidVariablePattern = "^[a-zA-Z][a-zA-Z0-9-]{0,}$";
        private static readonly string[] InvalidVariables = { "add", "sub", "mul", "div", "let" };

        private string resultExpression = null;
        private string valueExpression = null;
        private string variableName = null;

        public static bool IsMatch(string expression)
        {
            return Regex.IsMatch(expression, ValidLetPattern, RegexOptions.Compiled);
        }

        public int Evaluate(string expression)
        {
            ExtractExpressionData(expression);

            int value = Expression.Evaluate(valueExpression);
            resultExpression = Regex.Replace(resultExpression, $"\\b{variableName}\\b", value.ToString());

            VariableExpression.AddVariable(variableName, value);

            return Expression.Evaluate(resultExpression);
        }

        public bool ValidateExpression(string expression, out string error)
        {
            bool result = true;
            error = null;

            try
            {
                ExtractExpressionData(expression);
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

        private void ExtractExpressionData(string expression)
        {
            valueExpression = null;
            resultExpression = null;
            variableName = null;
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
                            resultExpression = expression.Substring(commaIndex + 1, i - 1 - commaIndex).Trim();
                        }
                    }
                    else if (expression[i] == ',')
                    {
                        if (paranIndexStack.Count == 1)
                        {
                            if (commaIndex == -1)
                            {
                                commaIndex = i;
                            }

                            int startIndex = paranIndexStack.Peek() + 1;

                            if (variableName == null)
                            {
                                variableName = expression.Substring(startIndex, i - startIndex).Trim();
                            }
                            else
                            {
                                valueExpression = expression.Substring(commaIndex + 1, i - commaIndex - 1).Trim();
                                commaIndex = i;
                            }
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

            string variablePattern = $"\\b{variableName}\\b";

            if (paranIndexStack.Count > 0)
            {
                throw new ArithmeticException("The end of the expression was reached before all parans closed");
            }

            if (string.IsNullOrWhiteSpace(variableName))
            {
                throw new ArithmeticException("No variable name was provided");
            }

            if (InvalidVariables.Contains(variableName) || !Regex.IsMatch(variableName, ValidVariablePattern, RegexOptions.Compiled))
            {
                throw new ArithmeticException("Invalid variable name provided");
            }

            if (string.IsNullOrWhiteSpace(valueExpression))
            {
                throw new ArithmeticException("No value expression was provided");
            }

            if (string.IsNullOrWhiteSpace(resultExpression))
            {
                throw new ArithmeticException("No result expression was provided");
            }

            if (Regex.IsMatch(valueExpression, variablePattern))
            {
                throw new ArithmeticException("The value expression cannot have the variable in it");
            }

            // encapsulate these so the "error" variable can be used twice
            {
                if (!Expression.ValidateExpression(valueExpression, out string error))
                {
                    throw new ArithmeticException($"The value expression is invalid: {error}");
                }
            }

            {
                string tempResultExpression = Regex.Replace(resultExpression, variablePattern, "1");
                if (!Expression.ValidateExpression(tempResultExpression, out string error))
                {
                    throw new ArithmeticException($"The result expression is invalid: {error}");
                }
            }
        }
    }
}