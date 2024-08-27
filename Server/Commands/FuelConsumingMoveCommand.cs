using Server.Entities;
using Server.Interfaces;

namespace Server.Commands;

public class FuelConsumingMoveCommand(IMovable movableObject, FuelTank tank, WarpEngine engine) : ICommand
{
    public void Execute()
    {
        CheckFuelCommand checkFuelCommand = new(tank, engine);
        BurnFuelCommand burnFuelCommand = new(tank, engine);
        MoveCommand moveCommand = new(movableObject);
        MacroCommand macroCommand = new([checkFuelCommand, burnFuelCommand, moveCommand]);

        macroCommand.Execute();
    }
}
