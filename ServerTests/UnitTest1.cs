using Server.Adapters;
using Server.Commands;
using Server.Entities;
using Server.Interfaces;
using ServerTests.Entities;

namespace ServerTests;

public class Tests
{
    [Fact(DisplayName = "Starship moves")]
    public void Test1()
    {
        // arrange
        Starship starship = new()
        {
            Position = new(12,5),
            Velocity = new(-7,3)
        };

        IMovable adapter = new MovableAdapter(starship);
        MoveCommand moveCommand = new(adapter);

        // act
        moveCommand.Execute();

        // assert
        Assert.Equal(new(5,8), starship.Position);
    }

    [Fact(DisplayName = "Unknown position object throws an exception")]
    public void Test2()
    {
        // arrange
        UnknownPositionObject obj = new();
        obj.SetPosition(new(12,5));
        obj.SetVelocity(new(-7,3));

        IMovable adapter = new MovableAdapter(obj);
        MoveCommand moveCommand = new(adapter);

        // act & assert
        Assert.Throws<ArgumentException>(() => moveCommand.Execute());
    }

    [Fact(DisplayName = "Unknown velocity object throws an exception")]
    public void Test3()
    {
        // arrange
        UnknownVelocityObject obj = new();
        obj.SetPosition(new(12,5));
        obj.SetVelocity(new(-7,3));

        IMovable adapter = new MovableAdapter(obj);
        MoveCommand moveCommand = new(adapter);

        // act & assert
        Assert.Throws<ArgumentException>(() => moveCommand.Execute());
    }

    [Fact(DisplayName = "Unchangeable position object throws an exception")]
    public void Test4()
    {
        // arrange
        UnchangeablePositionObject obj = new();
        obj.SetVelocity(new(-7,3));

        IMovable adapter = new MovableAdapter(obj);
        MoveCommand moveCommand = new(adapter);

        // act & assert
        Assert.Throws<ArgumentException>(() => moveCommand.Execute());
    }

    [Fact(DisplayName = "Starship rotates")]
    public void Test5()
    {
        // arrange
        Starship starship = new()
        {
            AnglePosition = new(15),
            AngleVelocity = new(45),
        };

        IRotatable adapter = new RotatableAdapter(starship);
        RotateCommand rotateCommand = new(adapter);

        // act
        rotateCommand.Execute();

        // assert
        Assert.Equal(new(60), starship.AnglePosition);
    }

    [Fact(DisplayName = "Starship full circle rotation")]
    public void Test6()
    {
        // arrange
        Starship starship = new()
        {
            AnglePosition = new(15),
            AngleVelocity = new(360_000_000),
        };

        IRotatable adapter = new RotatableAdapter(starship);
        RotateCommand rotateCommand = new(adapter);

        // act
        rotateCommand.Execute();

        // assert
        Assert.Equal(new(15), starship.AnglePosition);
    }

    [Fact(DisplayName = "Not rotatable object throws exception")]
    public void Test7()
    {
        // arrange
        UnknownVelocityObject obj = new();

        IRotatable adapter = new RotatableAdapter(obj);
        RotateCommand rotateCommand = new(adapter);

        // act & assert
        Assert.Throws<ArgumentException>(() => rotateCommand.Execute());
    }
}
