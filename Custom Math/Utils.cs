using System;

[System.Serializable]
public struct Utils
{
    // Radians to degrees constant

    public const float Rad2Deg = 57.29578f;

    internal static readonly float Epsilon = 1e-6f;

    public const float gravity = 9.81f;

    private static readonly System.Random random = new System.Random();
    public static double Radians(double ang) => (ang * (Math.PI / 180));

    //Min max between 2 values
    public static int Min(int a, int b) => (a < b) ? a : b;
    public static int Max(int a, int b) => (a > b) ? a : b;
    public static float Min(float a, float b) => (a < b) ? a : b;
    public static float Max(float a, float b) => (a > b) ? a : b;


    public static int Clamp(int value, int min, int max)
    {
        if (value < min) { value = min; }
        else if (value > max) { value = max; }
        return value;
    }

    public static float Clamp(float value, float min, float max)
    {
        if (value < min) { value = min; }
        else if (value > max) { value = max; }
        return value;
    }

    public static float Clamp01(float value)
    {
        if(value < 0.0f)
        {
            return 0.0f;
        }
        else if (value > 1.0f)
        {
            return 1.0f;
        }
        return value;
    }

    public static float Lerp(float a, float b, float t) => a + (b - a) * Clamp01(t);
    public static float LerpUnclamped(float a, float b, float t) => a + (b - a) * t;

    public static Vector3C Lerp(Vector3C a, Vector3C b, float t)
    {
        return new Vector3C(
            Lerp(a.x, b.x, t),
            Lerp(a.y, b.y, t),
            Lerp(a.z, b.z, t)
        );
    }

    public static float MoveTowards(float current, float target, float maxDelta)
    {
        if (Math.Abs(target - current) <= maxDelta)
        {
            return target;
        }

        return current + Math.Sign(target - current) * maxDelta;
    }
    // -- Random funcs -- //

    //Rand number between 0-1
    public static float GetRandomNumber01() => (float)random.NextDouble();
    public static float GetRandomNumberMinMax(float min, float max) => GetRandomNumber01() * (max - min) + min;
    public static int RandomRange(int min, int max) => random.Next(min, max + 1);
    public static float RandomRange(float min, float max) => (float)(random.NextDouble() * (max - min) + min);

    public static Vector3C PointInLine(Vector3C pA, Vector3C pB)
    {
        LineC line = LineC.LineFromPointAPointB(pA, pB);

        return line.origin + GetRandomNumber01() * line.direction;

    }
}
