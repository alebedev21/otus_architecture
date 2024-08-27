using Server.Entities;
using Server.Exceptions;

namespace Server.Commands;

public class CheckFuelCommand(FuelTank tank, WarpEngine engine) : ICommand
{
    public void Execute()
    {
        if (tank.Amount < engine.FuelConsumption)
        {
            throw new CommandException();
        }
    }
}
