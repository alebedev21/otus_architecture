using Server.Commands;

namespace Server.ExceptionHandling;

public static class Handles
{
    public static readonly Func<ICommand, Exception, ICommand> SecondAttemptHandle = (c, _) =>
    {
        SecondAttemptCommand cmd = new(c);
        return new RetryCommand(cmd);
    };

    public static readonly Func<ICommand, Exception, ICommand> LastAttemptHandle = (c, _) =>
    {
        LastAttemptCommand cmd = new(c);
        return new RetryCommand(cmd);
    };

    public static readonly Func<ICommand, Exception, ICommand> LogHandle = (_, e) =>
    {
        LogCommand cmd = new(e);
        return new RetryCommand(cmd);
    };
}
