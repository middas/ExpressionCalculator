using ExpressionLibrary.Expressions;

namespace ExpressionLibrary
{
    public static class ExpressionFactory
    {
        public static IExpression CreateFromExpression(string expression)
        {
            IExpression result = null;

            if (ConstantExpression.IsMatch(expression))
            {
                result = new ConstantExpression();
            }
            else if (AddExpression.IsMatch(expression))
            {
                result = new AddExpression();
            }
            else if (SubExpression.IsMatch(expression))
            {
                result = new SubExpression();
            }
            else if (MulExpression.IsMatch(expression))
            {
                result = new MulExpression();
            }
            else if (DivExpression.IsMatch(expression))
            {
                result = new DivExpression();
            }
            else if (LetExpression.IsMatch(expression))
            {
                result = new LetExpression();
            }

            return result;
        }
    }
}