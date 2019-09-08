using System.Text.RegularExpressions;

namespace ExpressionLibrary.Expressions
{
    internal class MulExpression : FunctionExpression
    {
        private const string ValidMulStartPattern = "^[Mm][Uu][Ll] *\\(";

        public static bool IsMatch(string expression)
        {
            return Regex.IsMatch(expression, ValidMulStartPattern, RegexOptions.Compiled);
        }

        public override int Evaluate(string expression)
        {
            ExtractExpressionSides(expression);

            return EvaluateLeftExpression() * EvaluateRightExpression();
        }
    }
}