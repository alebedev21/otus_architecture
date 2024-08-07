using Xunit;

namespace SolverTests;

public class Tests
{
    [Fact(DisplayName = $"x^2+1 = 0 equation has no roots")]
    public void Test1()
    {
        // arrange
        Solver.Solver solver = new();
        double a = 1;
        double b = 0;
        double c = 1;

        // act
        var roots = Solver.Solver.Solve(a, b, c);

        // assert
        Assert.Empty(roots);
    }

    [Fact(DisplayName = $"x^2-1 = 0 equation has two roots")]
    public void Test2()
    {
        // arrange
        double a = 1;
        double b = 0;
        double c = -1;

        // act
        var roots = Solver.Solver.Solve(a, b, c);

        // assert
        Assert.Equal(2, roots.Length);
        Assert.Contains(roots, x => Math.Abs(x - 1) < Solver.Solver.Tolerance);
        Assert.Contains(roots, x => Math.Abs(x - (-1)) < Solver.Solver.Tolerance);
    }

    [Fact(DisplayName = $"x^2+2x+1 = 0 equation has only one root")]
    public void Test3()
    {
        // arrange
        double a = 0.99999999999;
        double b = 2;
        double c = 1;

        // act
        var roots = Solver.Solver.Solve(a, b, c);

        // assert
        Assert.Single(roots);
        Assert.Contains(roots, x => Math.Abs(x - (-1)) < Solver.Solver.Tolerance);
    }

    [Fact(DisplayName = $"'a' must be greater than zero")]
    public void Test4()
    {
        // arrange
        double a = 0;
        double b = 2;
        double c = 1;

        // act & assert
        Assert.Throws<ArgumentException>(() => Solver.Solver.Solve(a, b, c));
    }

    [Fact(DisplayName = $"'a' must not be NaN")]
    public void Test5()
    {
        // arrange
        double a = Double.NaN;
        double b = 2;
        double c = 1;

        // act & assert
        Assert.Throws<ArgumentException>(() => Solver.Solver.Solve(a, b, c));
    }

    [Fact(DisplayName = $"'b' must not be NaN")]
    public void Test6()
    {
        // arrange
        double a = 1;
        double b = Double.NaN;
        double c = 1;

        // act & assert
        Assert.Throws<ArgumentException>(() => Solver.Solver.Solve(a, b, c));
    }

    [Fact(DisplayName = $"'c' must not be NaN")]
    public void Test7()
    {
        // arrange
        double a = 1;
        double b = 2;
        double c = Double.NaN;

        // act & assert
        Assert.Throws<ArgumentException>(() => Solver.Solver.Solve(a, b, c));
    }

    [Fact(DisplayName = $"'a' must not be PositiveInfinity")]
    public void Test8()
    {
        // arrange
        double a = Double.PositiveInfinity;
        double b = 2;
        double c = 1;

        // act & assert
        Assert.Throws<ArgumentException>(() => Solver.Solver.Solve(a, b, c));
    }

    [Fact(DisplayName = $"'b' must not be PositiveInfinity")]
    public void Test9()
    {
        // arrange
        double a = 1;
        double b = Double.PositiveInfinity;
        double c = 1;

        // act & assert
        Assert.Throws<ArgumentException>(() => Solver.Solver.Solve(a, b, c));
    }

    [Fact(DisplayName = $"'c' must not be PositiveInfinity")]
    public void Test10()
    {
        // arrange
        double a = 1;
        double b = 2;
        double c = Double.PositiveInfinity;

        // act & assert
        Assert.Throws<ArgumentException>(() => Solver.Solver.Solve(a, b, c));
    }

    [Fact(DisplayName = $"'a' must not be NegativeInfinity")]
    public void Test11()
    {
        // arrange
        double a = Double.NegativeInfinity;
        double b = 2;
        double c = 1;

        // act & assert
        Assert.Throws<ArgumentException>(() => Solver.Solver.Solve(a, b, c));
    }

    [Fact(DisplayName = $"'b' must not be NegativeInfinity")]
    public void Test12()
    {
        // arrange
        double a = 1;
        double b = Double.NegativeInfinity;
        double c = 1;

        // act & assert
        Assert.Throws<ArgumentException>(() => Solver.Solver.Solve(a, b, c));
    }

    [Fact(DisplayName = $"'c' must not be NegativeInfinity")]
    public void Test13()
    {
        // arrange
        double a = 1;
        double b = 2;
        double c = Double.NegativeInfinity;

        // act & assert
        Assert.Throws<ArgumentException>(() => Solver.Solver.Solve(a, b, c));
    }
}
