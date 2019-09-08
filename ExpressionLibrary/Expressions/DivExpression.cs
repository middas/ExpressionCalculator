using System;
using System.Text.RegularExpressions;

namespace ExpressionLibrary.Expressions
{
    internal class DivExpression : FunctionExpression
    {
        private const string ValidMulStartPattern = "^[Dd][Ii][Vv] *\\(";

        public static bool IsMatch(string expression)
        {
            return Regex.IsMatch(expression, ValidMulStartPattern, RegexOptions.Compiled);
        }

        public override int Evaluate(string expression)
        {
            ExtractExpressionSides(expression);

            return (int)Math.Round((decimal)(EvaluateLeftExpression() / EvaluateRightExpression()), MidpointRounding.AwayFromZero);
        }
    }
}