using System.Collections.Concurrent;
using Server.Commands;

namespace Server;

public class CommandsSource
{
    public ConcurrentQueue<ICommand> Source { get; private set; }

    private static readonly Lazy<CommandsSource> lazy = new (() => new CommandsSource());

    private CommandsSource()
    {
        Source = new();
    }

    public static CommandsSource GetInstance()
    {
        return lazy.Value;
    }
}
