using Server.Adapters;
using Server.Commands;
using Server.Entities;
using Server.Interfaces;

namespace ServerTests;

public class Tests
{
    [Fact(DisplayName = "Starship moves")]
    public void Test1()
    {
        // arrange
        Starship starship = new();
        starship.SetPosition(new(12,5));
        starship.SetVelocity(new(-7,3));

        IMovable adapter = new MovableAdapter(starship);
        Move move = new(adapter);

        // act
        move.Execute();

        // assert
        Assert.Equal(new(5,8), starship.GetPosition());
    }

    [Fact(DisplayName = "Unknown position object throws an exception")]
    public void Test2()
    {
        // arrange
        UnknownPositionObject obj = new();
        obj.SetPosition(new(12,5));
        obj.SetVelocity(new(-7,3));

        IMovable adapter = new MovableAdapter(obj);
        Move move = new(adapter);

        // act & assert
        Assert.Throws<ArgumentException>(() => move.Execute());
    }

    [Fact(DisplayName = "Unknown velocity object throws an exception")]
    public void Test3()
    {
        // arrange
        UnknownVelocityObject obj = new();
        obj.SetPosition(new(12,5));
        obj.SetVelocity(new(-7,3));

        IMovable adapter = new MovableAdapter(obj);
        Move move = new(adapter);

        // act & assert
        Assert.Throws<ArgumentException>(() => move.Execute());
    }

    [Fact(DisplayName = "Unchangeable position object throws an exception")]
    public void Test4()
    {
        // arrange
        UnchangeablePositionObject obj = new();
        obj.SetVelocity(new(-7,3));

        IMovable adapter = new MovableAdapter(obj);
        Move move = new(adapter);

        // act & assert
        Assert.Throws<ArgumentException>(() => move.Execute());
    }

    [Fact(DisplayName = "Starship rotates")]
    public void Test5()
    {
        // arrange
        Starship starship = new();
        starship.SetAnglePosition(new(15));
        starship.SetAngleVelocity(new(45));

        IRotatable adapter = new RotatableAdapter(starship);
        Rotate rotate = new(adapter);

        // act
        rotate.Execute();

        // assert
        Assert.Equal(new(60), starship.GetAnglePosition());
    }

    [Fact(DisplayName = "Starship full circle rotation")]
    public void Test6()
    {
        // arrange
        Starship starship = new();
        starship.SetAnglePosition(new(15));
        starship.SetAngleVelocity(new(360_000_000));

        IRotatable adapter = new RotatableAdapter(starship);
        Rotate rotate = new(adapter);

        // act
        rotate.Execute();

        // assert
        Assert.Equal(new(15), starship.GetAnglePosition());
    }

    [Fact(DisplayName = "Not rotatable object throws exception")]
    public void Test7()
    {
        // arrange
        UnknownVelocityObject obj = new();

        IRotatable adapter = new RotatableAdapter(obj);
        Rotate rotate = new(adapter);

        // act & assert
        Assert.Throws<ArgumentException>(() => rotate.Execute());
    }
}
