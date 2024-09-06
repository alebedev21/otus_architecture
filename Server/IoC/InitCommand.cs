using System.Collections.Concurrent;
using Server.Commands;

namespace Server.IoC;

public class InitCommand : ICommand
{
        static readonly AsyncLocal<object> currentScopes = new ();
        static readonly ConcurrentDictionary<string, Func<object[], object>> rootScope = new ();

        static bool _alreadyExecutesSuccessfully = false;
        public void Execute()
        {
            if (_alreadyExecutesSuccessfully)
                return;

            lock (rootScope)
            {
                rootScope.TryAdd(
                    "IoC.Scope.Current.Set",
                    (object[] args) => new SetCurrentScopeCommand(args[0])
                );

                rootScope.TryAdd(
                    "IoC.Scope.Current.Clear",
                    (object[] args) => new ClearCurrentScopeCommand()
                );

                rootScope.TryAdd(
                    "IoC.Scope.Current",
                    (object[] args) => currentScopes.Value != null ? currentScopes.Value! : rootScope
                );

                rootScope.TryAdd(
                    "IoC.Scope.Parent",
                    (object[] args) => throw new Exception("The root scope has no a parent scope.")
                );

                rootScope.TryAdd(
                    "IoC.Scope.Create.Empty",
                    (object[] args) => new Dictionary<string, Func<object[], object>>()
                );

                rootScope.TryAdd(
                    "IoC.Scope.Create",
                    (object[] args) =>
                    {
                        var creatingScope = IoC.Resolve<IDictionary<string, Func<object[], object>>>("IoC.Scope.Create.Empty");

                        if (args.Length > 0)
                        {
                            var parentScope = args[0];
                            creatingScope.Add("IoC.Scope.Parent", (object[] args) => parentScope);
                        }
                        else
                        {
                            var parentScope = IoC.Resolve<object>("IoC.Scope.Current");
                            creatingScope.Add("IoC.Scope.Parent", (object[] args) => parentScope);
                        }
                        return creatingScope;
                    }
                );

                rootScope.TryAdd(
                    "IoC.Register",
                    (object[] args) => new RegisterDependencyCommand((string)args[0], (Func<object[], object>)args[1])
                );

                IoC.Resolve<ICommand>(
                    "Update Ioc Resolve Dependency Strategy",
                    (Func<string, object[], object> oldStrategy) =>
                    (string dependency, object[] args) =>
                    {
                        var scope = currentScopes.Value != null ? currentScopes.Value! : rootScope;
                        var dependencyResolver = new DependencyResolver(scope);

                        return dependencyResolver.Resolve(dependency, args);
                    }
                ).Execute();

                _alreadyExecutesSuccessfully = true;
            }
        }
}
