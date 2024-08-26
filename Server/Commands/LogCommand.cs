namespace Server.Commands;

public class LogCommand(Exception ex) : ICommand
{
    public void Execute()
    {
        Console.WriteLine(ex.Message);
    }
}
