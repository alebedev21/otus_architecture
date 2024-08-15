namespace Server.Commands;

public class LastAttemptCommand(ICommand command) : ICommand
{
    public void Execute()
    {
        command.Execute();
    }
}
