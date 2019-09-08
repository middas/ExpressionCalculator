using System.Text.RegularExpressions;

namespace ExpressionLibrary.Expressions
{
    internal class SubExpression : FunctionExpression
    {
        private const string ValidSubStartPattern = "^[Ss][Uu][Bb] *\\(";

        public static bool IsMatch(string expression)
        {
            return Regex.IsMatch(expression, ValidSubStartPattern, RegexOptions.Compiled);
        }

        public override int Evaluate(string expression)
        {
            ExtractExpressionSides(expression);

            return EvaluateLeftExpression() - EvaluateRightExpression();
        }
    }
}