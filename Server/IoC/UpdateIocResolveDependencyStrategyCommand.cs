using Server.Commands;

namespace Server.IoC;

internal class UpdateIocResolveDependencyStrategyCommand(
    Func<Func<string, object[], object>, Func<string, object[], object>> updater)
    : ICommand
{
    public void Execute()
    {
        IoC._strategy = updater(IoC._strategy);
    }
}
