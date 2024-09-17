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
                result = operand1 / operand2;
                break;
            default:
                throw new ArgumentException($"'{operation}' is not supported",
                    "operation");
                break;
        }
        return $"{operand1} {operation} {operand2} = {result}";
    }
}
