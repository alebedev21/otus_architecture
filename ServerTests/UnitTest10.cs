using Server.Commands;
using Server.Entities;
using Server.Exceptions;

namespace ServerTests;

public class UnitTest10
{
    [Fact(DisplayName = "CheckFuelCommand throw CommandException if there is not enough fuel")]
    public void Test_1_1()
    {
        // arrange
        FuelTank tank = new()
        {
            Amount = 1
        };
        WarpEngine engine = new()
        {
            FuelConsumption = 10,
        };

        CheckFuelCommand command = new(tank, engine);

        //act & assert
        Assert.Throws<CommandException>(() => command.Execute());
    }

    [Fact(DisplayName = "CheckFuelCommand doesn't throw CommandException if there is enough fuel")]
    public void Test_1_2()
    {
        // arrange
        FuelTank tank = new()
        {
            Amount = 100
        };
        WarpEngine engine = new()
        {
            FuelConsumption = 10,
        };

        CheckFuelCommand command = new(tank, engine);

        // act
        var ex = Record.Exception(() => command.Execute());

        // assert
        Assert.Null(ex);
    }

    [Fact(DisplayName = "BurnFuelCommand works")]
    public void Test_2()
    {
        // arrange
        FuelTank tank = new()
        {
            Amount = 100
        };
        WarpEngine engine = new()
        {
            FuelConsumption = 10,
        };

        BurnFuelCommand command = new(tank, engine);

        // act
        var ex = Record.Exception(() => command.Execute());

        // assert
        Assert.Equal(90, tank.Amount);
    }

    [Fact(DisplayName = "MacroCommand throw CommandException if there is not enough fuel")]
    public void Test_3_1()
    {
        // arrange
        FuelTank tank = new()
        {
            Amount = 1
        };
        WarpEngine engine = new()
        {
            FuelConsumption = 10,
        };

        CheckFuelCommand checkFuelCommand = new(tank, engine);
        BurnFuelCommand burnFuelCommand = new(tank, engine);
        MacroCommand macroCommand = new([checkFuelCommand, burnFuelCommand]);

        //act & assert
        Assert.Throws<CommandException>(() => macroCommand.Execute());
        Assert.Equal(1, tank.Amount);
    }

    [Fact(DisplayName = "MacroCommand doesn't throw CommandException if there is enough fuel")]
    public void Test_3_2()
    {
        // arrange
        FuelTank tank = new()
        {
            Amount = 100
        };
        WarpEngine engine = new()
        {
            FuelConsumption = 10,
        };

        CheckFuelCommand checkFuelCommand = new(tank, engine);
        BurnFuelCommand burnFuelCommand = new(tank, engine);
        MacroCommand macroCommand = new([checkFuelCommand, burnFuelCommand]);

        // act
        var ex = Record.Exception(() => macroCommand.Execute());

        // assert
        Assert.Null(ex);
        Assert.Equal(90, tank.Amount);
    }
}
