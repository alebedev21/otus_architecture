using Server.Commands;

namespace Server.IoC;

public class SetCurrentScopeCommand(object scope) : ICommand
{
    public void Execute()
    {
        // InitCommand.currentScopes.Value = scope;
    }
}
