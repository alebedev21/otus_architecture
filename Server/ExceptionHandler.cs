using Server.Commands;

namespace Server;

public static class ExceptionHandler
{
    private static readonly Func<ICommand, Exception, ICommand> SecondAttemptHandle = (c, _) =>
    {
        SecondAttemptCommand cmd = new(c);
        return new RetryCommand(cmd);
    };
    private static readonly Func<ICommand, Exception, ICommand> ThirdAttemptHandle = (c, _) =>
    {
        ThirdAttemptCommand cmd = new(c);
        return new RetryCommand(cmd);
    };

    private static readonly Func<ICommand, Exception, ICommand> LogHandle = (_, e) =>
    {
        LogCommand cmd = new(e);
        return new RetryCommand(cmd);
    };

    private static readonly DefaultValueDictionary<Type, Func<ICommand, Exception, ICommand>> _defaultHandlers = new(SecondAttemptHandle);
    private static readonly DefaultValueDictionary<Type, DefaultValueDictionary<Type, Func<ICommand, Exception, ICommand>>> _handlers = new(_defaultHandlers);

    static ExceptionHandler()
    {
        RegisterDefaultHandler(typeof(SecondAttemptCommand), ThirdAttemptHandle);
        RegisterDefaultHandler(typeof(ThirdAttemptCommand), LogHandle);
    }

    public static ICommand Handle(ICommand command, Exception exception)
    {
        Type commandType = command.GetType();
        Type exceptionType = exception.GetType();

        var handle = _handlers[commandType][exceptionType];
        return handle(command, exception);
    }

    private static void RegisterDefaultHandler(Type commandType, Func<ICommand, Exception, ICommand> handler)
    {
        _handlers[commandType] = new DefaultValueDictionary<Type, Func<ICommand, Exception, ICommand>>(handler);
    }

    private static void RegisterExceptionHandler(Type commandType, Type exceptionType, Func<ICommand, Exception, ICommand> handler)
    {
        _handlers[commandType][exceptionType] = handler;
    }
}
