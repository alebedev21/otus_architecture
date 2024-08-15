using Server.Commands;

namespace Server.ExceptionHandling;

public static class ExceptionHandlerOneRetry
{
    private static readonly DefaultValueDictionary<Type, Func<ICommand, Exception, ICommand>> _defaultHandlers = new(Handles.LastAttemptHandle);
    private static readonly DefaultValueDictionary<Type, DefaultValueDictionary<Type, Func<ICommand, Exception, ICommand>>> _handlers = new(_defaultHandlers);

    static ExceptionHandlerOneRetry()
    {
        RegisterDefaultHandler(typeof(LastAttemptCommand), Handles.LogHandle);
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
