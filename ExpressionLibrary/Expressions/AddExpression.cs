using System.Text.RegularExpressions;

namespace ExpressionLibrary.Expressions
{
    internal class AddExpression : FunctionExpression
    {
        private const string ValidAddStartPattern = "^[Aa][Dd]{2} *\\(";

        public static bool IsMatch(string expression)
        {
            return Regex.IsMatch(expression, ValidAddStartPattern, RegexOptions.Compiled);
        }

        public override int Evaluate(string expression)
        {
            ExtractExpressionSides(expression);

            return EvaluateLeftExpression() + EvaluateRightExpression();
        }
    }
}