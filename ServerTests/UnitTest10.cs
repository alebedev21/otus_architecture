﻿using System.Numerics;
using Server;
using Server.Adapters;
using Server.Commands;
using Server.Entities;
using Server.Exceptions;
using Server.Interfaces;
using Server.ValueTypes;

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

        FuelTank tank = new()
        {
            FuelAmount = 100
        };

        WarpEngine engine = new()
        {
            FuelConsumption = 10,
        };

        IMovable movableAdapter = new MovableAdapter(starship);
        IFuelProvider fuelProviderAdapter = new FuelProviderAdapter(tank);
        IFuelConsumer fuelConsumerAdapter = new FuelConsumerAdapter(engine);

        ICommand command = new FuelConsumingMoveCommand(movableAdapter, fuelProviderAdapter, fuelConsumerAdapter);

        // act
        var ex = Record.Exception(() => command.Execute());

        // assert
        Assert.Null(ex);
        Assert.Equal(90, tank.FuelAmount);
        Assert.Equal(new(5,8), starship.Position);
    }

    [Fact(DisplayName = "Starship turns")]
    public void Test_5_1()
    {
        // arrange
        Starship starship = new()
        {
            Position = new(12,5),
            Velocity = new(-7,3),
            AnglePosition = new Angle(180)
        };

        IMovable movableAdapter = new MovableAdapter(starship);
        IRotatable rotatableAdapter = new RotatableAdapter(starship);
        TurnCommand turnCommand = new(movableAdapter, rotatableAdapter);

        // act
        var ex = Record.Exception(() => turnCommand.Execute());

        // assert
        Assert.Null(ex);
        Assert.Equal(new(7,-3), starship.Velocity);
    }

    [Fact(DisplayName = "Starship doesn't turn")]
    public void Test_5_2()
    {
        // arrange
        Starship starship = new()
        {
            Position = new(12,5),
            Velocity = new(0,0),
            AnglePosition = new Angle(180)
        };

        IMovable movableAdapter = new MovableAdapter(starship);
        IRotatable rotatableAdapter = new RotatableAdapter(starship);
        TurnCommand turnCommand = new(movableAdapter, rotatableAdapter);

        // act
        var ex = Record.Exception(() => turnCommand.Execute());

        // assert
        Assert.Null(ex);
        Assert.Equal(new(0,0), starship.Velocity);
    }

    [Fact(DisplayName = "Starship rotates and turns")]
    public void Test_6_1()
    {
        // arrange
        Starship starship = new()
        {
            Position = new(12,5),
            Velocity = new(2,0),
            AnglePosition = new Angle(0),
            AngleVelocity = new Angle(180)
        };

        IMovable movableAdapter = new MovableAdapter(starship);
        IRotatable rotatableAdapter = new RotatableAdapter(starship);
        RotateAndTurnCommand command = new(movableAdapter, rotatableAdapter);

        // act
        var ex = Record.Exception(() => command.Execute());

        // assert
        Assert.Null(ex);
        Assert.Equal(new(180), starship.AnglePosition);
        Assert.Equal(new(-2, 0), starship.Velocity);
    }
}
