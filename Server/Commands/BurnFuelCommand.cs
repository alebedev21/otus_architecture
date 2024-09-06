using Server.Interfaces;

namespace Server.Commands;

public class BurnFuelCommand(IFuelProvider fuelProviderObject, IFuelConsumer fuelConsumerObject) : ICommand
{
    public void Execute()
    {
        var currentFuelAmount = fuelProviderObject.GetFuelAmount();
        var consumption = fuelConsumerObject.GetFuelConsumption();

        fuelProviderObject.SetFuelAmount(currentFuelAmount - consumption);
    }
}
