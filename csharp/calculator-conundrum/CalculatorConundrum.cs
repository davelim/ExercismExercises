using System;

public static class SimpleCalculator
{
    public static string Calculate(int operand1, int operand2, string operation)
    {
        int result = 0;
        switch (operation)
        {
            case "+":
                result = operand1 + operand2;
                break;
            case "*":
                result = operand1 * operand2;
                break;
            case "/":
                try
                {
                    result = operand1 / operand2;
                }
                catch (DivideByZeroException)
                {
                    return "Division by zero is not allowed.";
                }
                break;
            default:
                if (operation == "")
                    throw new ArgumentException("'operation' cannot be empty string",
                        "operation");
                if (operation == null)
                    throw new ArgumentNullException("'operation' cannot be null",
                        "opration");
                throw new ArgumentOutOfRangeException($"'{operation}' is not supported",
                    "operation");
                break;
        }
        return $"{operand1} {operation} {operand2} = {result}";
    }
}
