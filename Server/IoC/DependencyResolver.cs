namespace Server.IoC;

public class DependencyResolver(object scope) : IDependencyResolver
{
    IDictionary<string, Func<object[], object>> _dependencies = (IDictionary<string, Func<object[], object>>)scope;

    public object Resolve(string dependency, object[] args)
    {
        var dependencies = _dependencies;

        while (true)
        {
            Func<object[], object>? dependencyResolverStrategy = null;
            if (dependencies.TryGetValue(dependency, out dependencyResolverStrategy))
            {
                return dependencyResolverStrategy(args);
            }

            dependencies = (IDictionary<string, Func<object[], object>>)dependencies["IoC.Scope.Parent"](args);
        }
    }
}
