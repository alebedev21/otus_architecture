using Server;
using Server.Adapters;
using Server.Commands;
using Server.Entities;
using Server.Interfaces;

namespace ServerTests;

public class UnitTest9
{
    [Fact(DisplayName = "LogCommand works")]
    public void Test1()
    {
        // arrange
        LogCommand command = new(new Exception(""));

        // act
        var ex = Record.Exception(() => command.Execute());

        //assert
        Assert.Null(ex);
    }

    [Fact(DisplayName = "LogHandle works")]
    public void Test2()
    {
        // arrange
        UnknownPositionObject obj = new();
        obj.SetPosition(new(12,5));
        obj.SetVelocity(new(-7,3));

        IMovable adapter = new MovableAdapter(obj);
        MoveCommand moveCommand = new(adapter);
        ThirdAttemptCommand thirdAttemptCommand = new(moveCommand);

        CommandsSource _commandsSource = CommandsSource.GetInstance();

        // act
        ExceptionHandler.Handle(thirdAttemptCommand, new Exception()).Execute();

        //assert
        _commandsSource.Source.TryDequeue(out ICommand? command);
        Assert.IsType<LogCommand>(command);
    }
}
