namespace Server.IoC;

public interface IDependencyResolver
{
    object Resolve(string dependency, object[] args);
}
