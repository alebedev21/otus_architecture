namespace Server.Exceptions;

public class CommandException : Exception
{
    public CommandException() : base() { }

    public CommandException(string message, Exception innerException) : base(message, innerException) { }
}
