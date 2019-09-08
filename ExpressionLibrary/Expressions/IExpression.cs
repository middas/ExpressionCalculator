namespace ExpressionLibrary.Expressions
{
    public interface IExpression
    {
        int Evaluate(string expression);

        bool ValidateExpression(string expression, out string error);
    }
}