namespace Server.Commands;

public class RetryCommand (ICommand command) : ICommand
{
    private readonly CommandsSource _commandsSource = CommandsSource.GetInstance();

    public void Execute()
    {
        _commandsSource.Source.Enqueue(command);
    }
}
