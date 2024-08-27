using Server;
using Server.Adapters;
using Server.Commands;
using Server.Entities;
using Server.ExceptionHandling;
using Server.Interfaces;
using ServerTests.Entities;

namespace ServerTests;

public class UnitTest9
{
    [Fact(DisplayName = "LogCommand works")]
    public void Test4()
    {
        // arrange
        LogCommand logCommand = new(new Exception(""));

        // act
        var ex = Record.Exception(() => logCommand.Execute());

        // assert
        Assert.Null(ex);
    }

    [Fact(DisplayName = "LogHandle works")]
    public void Test5()
    {
        // arrange
        UnknownPositionObject obj = new();
        IMovable adapter = new MovableAdapter(obj);
        MoveCommand moveCommand = new(adapter);

        // act
        Handles.LogHandle(moveCommand, new Exception()).Execute();

        // assert
        CommandsSource.GetInstance().Source.TryDequeue(out ICommand? command);
        Assert.IsType<LogCommand>(command);
    }

    [Fact(DisplayName = "SecondAttemptCommand works")]
    public void Test6()
    {
        // arrange
        UnknownPositionObject obj = new();
        IMovable adapter = new MovableAdapter(obj);
        MoveCommand moveCommand = new(adapter);
        SecondAttemptCommand secondAttemptCommand = new(moveCommand);

        // act & assert
        Assert.Throws<ArgumentException>(() => secondAttemptCommand.Execute());
    }

    [Fact(DisplayName = "SecondAttemptHandle works")]
    public void Test7()
    {
        // arrange
        UnknownPositionObject obj = new();
        IMovable adapter = new MovableAdapter(obj);
        MoveCommand moveCommand = new(adapter);

        // act
        Handles.SecondAttemptHandle(moveCommand, new Exception()).Execute();

        // assert
        CommandsSource.GetInstance().Source.TryDequeue(out ICommand? command);
        Assert.IsType<SecondAttemptCommand>(command);
    }

    [Fact(DisplayName = "One retry strategy")]
    public void Test8()
    {
        // arrange
        UnknownPositionObject obj = new();
        IMovable adapter = new MovableAdapter(obj);
        MoveCommand moveCommand = new(adapter);

        // act & assert
        try
        {
            moveCommand.Execute();
        }
        catch (Exception ex)
        {
            ExceptionHandlerOneRetry.Handle(moveCommand, ex).Execute();
            CommandsSource.GetInstance().Source.TryDequeue(out ICommand? lastAttemptCommand);
            Assert.IsType<LastAttemptCommand>(lastAttemptCommand);

            try
            {
                lastAttemptCommand.Execute();
            }
            catch (Exception ex2)
            {
                ExceptionHandlerOneRetry.Handle(lastAttemptCommand, ex2).Execute();
                CommandsSource.GetInstance().Source.TryDequeue(out ICommand? logCommand);
                Assert.IsType<LogCommand>(logCommand);
            }
        }
    }

    [Fact(DisplayName = "Two retries strategy")]
    public void Test9()
    {
        // arrange
        UnknownPositionObject obj = new();
        IMovable adapter = new MovableAdapter(obj);
        MoveCommand moveCommand = new(adapter);

        // act & assert
        try
        {
            moveCommand.Execute();
        }
        catch (Exception ex)
        {
            ExceptionHandlerTwoRetries.Handle(moveCommand, ex).Execute();
            CommandsSource.GetInstance().Source.TryDequeue(out ICommand? secondAttemptCommand);
            Assert.IsType<SecondAttemptCommand>(secondAttemptCommand);

            try
            {
                secondAttemptCommand.Execute();
            }
            catch (Exception ex2)
            {
                ExceptionHandlerTwoRetries.Handle(secondAttemptCommand, ex2).Execute();
                CommandsSource.GetInstance().Source.TryDequeue(out ICommand? lastAttemptCommand);
                Assert.IsType<LastAttemptCommand>(lastAttemptCommand);

                try
                {
                    lastAttemptCommand.Execute();
                }
                catch (Exception ex3)
                {
                    ExceptionHandlerTwoRetries.Handle(lastAttemptCommand, ex3).Execute();
                    CommandsSource.GetInstance().Source.TryDequeue(out ICommand? logCommand);
                    Assert.IsType<LogCommand>(logCommand);
                }
            }
        }
    }
}
