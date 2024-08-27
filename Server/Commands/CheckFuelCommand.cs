using Server.Exceptions;
using Server.Interfaces;

namespace Server.Commands;

public class CheckFuelCommand(IFuelProvider fuelProviderObject, IFuelConsumer fuelConsumerObject) : ICommand
{
    public void Execute()
    {
        var currentFuelAmount = fuelProviderObject.GetFuelAmount();
        var consumption = fuelConsumerObject.GetFuelConsumption();

        if (currentFuelAmount < consumption)
        {
            throw new CommandException();
        }
    }
}
