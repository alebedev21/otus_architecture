namespace Server.Commands;

public class SecondAttemptCommand (ICommand command) : ICommand
{
    public void Execute()
    {
        command.Execute();
    }
}
