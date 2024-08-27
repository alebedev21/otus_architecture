using Server.Interfaces;

namespace Server.Commands;

public class FuelConsumingMoveCommand(IMovable movableObject, IFuelProvider fuelProviderObject, IFuelConsumer fuelConsumerObject) : ICommand
{
    public void Execute()
    {
        CheckFuelCommand checkFuelCommand = new(fuelProviderObject, fuelConsumerObject);
        BurnFuelCommand burnFuelCommand = new(fuelProviderObject, fuelConsumerObject);
        MoveCommand moveCommand = new(movableObject);
        MacroCommand macroCommand = new([checkFuelCommand, burnFuelCommand, moveCommand]);

        macroCommand.Execute();
    }
}
