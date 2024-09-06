﻿namespace Server.IoC;

public static class IoC
{
    internal static Func<string, object[], object> _strategy =
    (string dependency, object[] args) =>
    {
        if ("Update Ioc Resolve Dependency Strategy" == dependency)
        {
            return new UpdateIocResolveDependencyStrategyCommand(
              (Func<Func<string, object[], object>, Func<string, object[], object>>)args[0]
            );
        }

        throw new ArgumentException(@"Dependency {dependency} is not found.");

    };

    public static T Resolve<T>(string dependency, params object[] args)
    {
        return (T)_strategy(dependency, args);
    }
}
