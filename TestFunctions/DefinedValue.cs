public static class DefinedValue {
    public const int Max = 99999;
    public const int Min = -99999;
    public const int UndefinedDeterminedValue = 99999;

    public static int FindMax(int a, int b)
    {
        return (a >= b) ? a : b;
    }

    public static int FindMin(int a, int b)
    {
        return (a <= b) ? a : b;
    }
}