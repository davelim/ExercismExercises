using System;

public static class SimpleCalculator
{
    public static string Calculate(int operand1, int operand2, string operation)
    {
        int result = 0;
        try
        {
            switch (operation)
            {
                case "+":
                    result = operand1 + operand2;
                    break;
                case "*":
                    result = operand1 * operand2;
                    break;
                case "/":
                    result = operand1 / operand2;
                    break;
                case "":
                    throw new ArgumentException("'operation' cannot be empty string",
                        "operation");
                    break;
                case null:
                    throw new ArgumentNullException("'operation' cannot be null",
                        "opration");
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"'{operation}' is not supported",
                        "operation");
                    break;
            }
        }
        catch (DivideByZeroException)
        {
            return "Division by zero is not allowed.";
        }
        return $"{operand1} {operation} {operand2} = {result}";
    }
}
