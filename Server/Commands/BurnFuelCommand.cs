using Server.Entities;

namespace Server.Commands;

public class BurnFuelCommand(FuelTank tank, WarpEngine engine) : ICommand
{
    public void Execute()
    {
        tank.Amount -= engine.FuelConsumption;
    }
}
