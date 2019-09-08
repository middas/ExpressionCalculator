using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExpressionLibrary.Expressions
{
    internal class VariableExpression : IExpression
    {
        private const string ValidVariablePattern = "^[a-zA-Z][a-zA-Z0-9-]{0,}$";
        private static readonly string[] InvalidVariables = { "add", "sub", "mul", "div", "let" };

        private static readonly Dictionary<string, int> variables = new Dictionary<string, int>();

        public static void AddVariable(string variableName, int value)
        {
            if (!InvalidVariables.Contains(variableName) && Regex.IsMatch(variableName, ValidVariablePattern, RegexOptions.Compiled))
            {
                variables[variableName] = value;
            }
        }

        public static bool IsMatch(string expression)
        {
            return Regex.IsMatch(expression, ValidVariablePattern, RegexOptions.Compiled);
        }

        public int Evaluate(string expression)
        {
            bool hasMatch = false;

            foreach (KeyValuePair<string, int> keyValuePair in variables)
            {
                string pattern = $"\\b{keyValuePair.Key}\\b";

                if (Regex.IsMatch(expression, pattern))
                {
                    expression = Regex.Replace(expression, pattern, keyValuePair.Value.ToString());
                    hasMatch = true;
                }
            }

            if (!hasMatch)
            {
                throw new ArithmeticException("Variable has not yet been initialized");
            }

            return Expression.Evaluate(expression);
        }

        public bool ValidateExpression(string expression, out string error)
        {
            bool result = true;
            error = null;

            if (InvalidVariables.Contains(expression) || !Regex.IsMatch(expression, ValidVariablePattern))
            {
                result = false;
                error = "The expression is invalid";
            }
            else if (!variables.ContainsKey(expression))
            {
                result = false;
                error = "Variable has not yet been initialized";
            }

            return result;
        }

        internal static void ClearVariables()
        {
            variables.Clear();
        }
    }
}