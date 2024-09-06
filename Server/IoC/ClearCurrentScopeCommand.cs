using Server.Commands;

namespace Server.IoC;

public class ClearCurrentScopeCommand : ICommand
{
    public void Execute()
    {
        // InitCommand.currentScopes.Value = null;
    }
}
