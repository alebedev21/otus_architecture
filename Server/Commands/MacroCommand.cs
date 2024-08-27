using Server.Exceptions;

namespace Server.Commands;

public class MacroCommand(IReadOnlyList<ICommand> commands) : ICommand
{
    public void Execute()
    {
        try
        {
            foreach (var command in commands)
            {
                command.Execute();
            }
        }
        catch (Exception ex)
        {
            throw new CommandException("Something went wrong", ex);
        }
    }
}
