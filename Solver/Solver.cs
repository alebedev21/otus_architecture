namespace Solver;

public class Solver
{
    public static double Tolerance { get; } = 0.000001;
    public static double[] Solve(double a, double b, double c)
    {
        bool isAValid = !double.IsNaN(a) && !double.IsNegativeInfinity(a) && !double.IsPositiveInfinity(a);
        bool isBValid = !double.IsNaN(b) && !double.IsNegativeInfinity(b) && !double.IsPositiveInfinity(b);
        bool isCValid = !double.IsNaN(c) && !double.IsNegativeInfinity(c) && !double.IsPositiveInfinity(c);

        if (!isAValid || !isBValid || !isCValid)
        {
            throw new ArgumentException("Argument is invalid");
        }

        if (Math.Abs(a) - Tolerance < 0)
        {
            throw new ArgumentException("'a' must be greater than zero");
        }

        double d = b * b - 4 * a * c;

        if (d < 0)
        {
            return [];
        }

        if (d - Tolerance < 0)
        {
            return [-b/(2*a)];
        }

        return [(-b + Math.Sqrt(d))/(2*a), (-b - Math.Sqrt(d))/(2*a)];
    }
}
