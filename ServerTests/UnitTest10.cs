using Server.Adapters;
using Server.Commands;
using Server.Entities;
using Server.Exceptions;
using Server.Interfaces;

namespace ServerTests;

public class UnitTest10
{
    [Fact(DisplayName = "CheckFuelCommand throw CommandException if there is not enough fuel")]
    public void Test_1_1()
    {
        // arrange
        FuelTank tank = new()
        {
            FuelAmount = 1
        };

        WarpEngine engine = new()
        {
            FuelConsumption = 10,
        };

        IFuelProvider fuelProviderAdapter = new FuelProviderAdapter(tank);
        IFuelConsumer fuelConsumerAdapter = new FuelConsumerAdapter(engine);

        CheckFuelCommand command = new(fuelProviderAdapter, fuelConsumerAdapter);

        //act & assert
        Assert.Throws<CommandException>(() => command.Execute());
    }

    [Fact(DisplayName = "CheckFuelCommand doesn't throw CommandException if there is enough fuel")]
    public void Test_1_2()
    {
        // arrange
        FuelTank tank = new()
        {
            FuelAmount = 100
        };

        WarpEngine engine = new()
        {
            FuelConsumption = 10,
        };

        IFuelProvider fuelProviderAdapter = new FuelProviderAdapter(tank);
        IFuelConsumer fuelConsumerAdapter = new FuelConsumerAdapter(engine);

        CheckFuelCommand command = new(fuelProviderAdapter, fuelConsumerAdapter);

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
            FuelAmount = 100
        };

        WarpEngine engine = new()
        {
            FuelConsumption = 10,
        };

        IFuelProvider fuelProviderAdapter = new FuelProviderAdapter(tank);
        IFuelConsumer fuelConsumerAdapter = new FuelConsumerAdapter(engine);

        BurnFuelCommand command = new(fuelProviderAdapter, fuelConsumerAdapter);

        // act
        var ex = Record.Exception(() => command.Execute());

        // assert
        Assert.Equal(90, tank.FuelAmount);
    }

    [Fact(DisplayName = "MacroCommand throw CommandException if there is not enough fuel")]
    public void Test_3_1()
    {
        // arrange
        FuelTank tank = new()
        {
            FuelAmount = 1
        };

        WarpEngine engine = new()
        {
            FuelConsumption = 10,
        };

        IFuelProvider fuelProviderAdapter = new FuelProviderAdapter(tank);
        IFuelConsumer fuelConsumerAdapter = new FuelConsumerAdapter(engine);

        CheckFuelCommand checkFuelCommand = new(fuelProviderAdapter, fuelConsumerAdapter);
        BurnFuelCommand burnFuelCommand = new(fuelProviderAdapter, fuelConsumerAdapter);
        MacroCommand macroCommand = new([checkFuelCommand, burnFuelCommand]);

        //act & assert
        Assert.Throws<CommandException>(() => macroCommand.Execute());
        Assert.Equal(1, tank.FuelAmount);
    }

    [Fact(DisplayName = "MacroCommand doesn't throw CommandException if there is enough fuel")]
    public void Test_3_2()
    {
        // arrange
        FuelTank tank = new()
        {
            FuelAmount = 100
        };

        WarpEngine engine = new()
        {
            FuelConsumption = 10,
        };

        IFuelProvider fuelProviderAdapter = new FuelProviderAdapter(tank);
        IFuelConsumer fuelConsumerAdapter = new FuelConsumerAdapter(engine);

        CheckFuelCommand checkFuelCommand = new(fuelProviderAdapter, fuelConsumerAdapter);
        BurnFuelCommand burnFuelCommand = new(fuelProviderAdapter, fuelConsumerAdapter);
        MacroCommand macroCommand = new([checkFuelCommand, burnFuelCommand]);

        // act
        var ex = Record.Exception(() => macroCommand.Execute());

        // assert
        Assert.Null(ex);
        Assert.Equal(90, tank.FuelAmount);
    }

    [Fact(DisplayName = "Starship moves consuming fuel")]
    public void Test_4_1()
    {
        // arrange
        Starship starship = new()
        {
            Position = new(12,5),
            Velocity = new(-7,3)
        };

        IMovable adapter = new MovableAdapter(starship);
        MoveCommand moveCommand = new(adapter);

        FuelTank tank = new()
        {
            FuelAmount = 100
        };

        WarpEngine engine = new()
        {
            FuelConsumption = 10,
        };

        IFuelProvider fuelProviderAdapter = new FuelProviderAdapter(tank);
        IFuelConsumer fuelConsumerAdapter = new FuelConsumerAdapter(engine);

        CheckFuelCommand checkFuelCommand = new(fuelProviderAdapter, fuelConsumerAdapter);
        BurnFuelCommand burnFuelCommand = new(fuelProviderAdapter, fuelConsumerAdapter);
        MacroCommand macroCommand = new([checkFuelCommand, burnFuelCommand, moveCommand]);

        // act
        var ex = Record.Exception(() => macroCommand.Execute());

        // assert
        Assert.Null(ex);
        Assert.Equal(90, tank.FuelAmount);
        Assert.Equal(new(5,8), starship.Position);
    }
}
