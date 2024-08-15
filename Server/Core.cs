using Server.Commands;
using Server.ExceptionHandling;

namespace Server;

public class Core
{
    private readonly CommandsSource _commandsSource = CommandsSource.GetInstance();

    public void AddCommand(ICommand command)
    {
        _commandsSource.Source.Enqueue(command);
    }

    public void Start()
    {
        while (true)
        {
            var hasValue = _commandsSource.Source.TryDequeue(out ICommand? command);

            if (!hasValue || command is null)
            {
                break;
            }

            try
            {
                Console.WriteLine($"{command.GetType()} started");
                command.Execute();
                Console.WriteLine($"{command.GetType()} finished");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ExceptionHandler {command.GetType()} started");
                ExceptionHandlerTwoRetries.Handle(command, ex).Execute();
                Console.WriteLine($"ExceptionHandler {command.GetType()} finished");
            }
        }
    }
}
