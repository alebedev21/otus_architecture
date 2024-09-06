namespace Server.IoC;

public class RegisterDependencyCommand(string dependency, Func<object[], object> dependencyResolverStrategy)
{
    public void Execute()
    {
        var currentScope = IoC.Resolve<IDictionary<string, Func<object[], object>>>("IoC.Scope.Current");
        currentScope.Add(dependency, dependencyResolverStrategy);
    }
}
