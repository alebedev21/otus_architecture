using Server.Interfaces;

namespace Server.Commands;

public class RotateAndTurnCommand(IMovable movableObject, IRotatable rotatableObject) : ICommand
{
    public void Execute()
    {
        ICommand rotateCommand = new RotateCommand(rotatableObject);
        ICommand turnCommand = new TurnCommand(movableObject, rotatableObject);
        ICommand macroCommand = new MacroCommand([rotateCommand, turnCommand]);

        macroCommand.Execute();
    }
}
