namespace Server.Commands;

public class ThirdAttemptCommand(ICommand command) : ICommand
{
    public void Execute()
    {
        command.Execute();
    }
}
