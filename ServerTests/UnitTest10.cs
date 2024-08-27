using Server.Commands;
using Server.Entities;
using Server.Exceptions;

namespace ServerTests;

public class UnitTest10
{
    [Fact(DisplayName = "CheckFuelCommand throw Command exception if there is not enough fuel")]
    public void Test10()
    {
        // arrange
        FuelTank tank = new()
        {
            Amount = 1
        };
        WarpEngine engine = new()
        {
            FuelConsumption = 10,
        };

        CheckFuelCommand command = new(tank, engine);

        //act & assert
        Assert.Throws<CommandException>(() => command.Execute());
    }

    [Fact(DisplayName = "CheckFuelCommand doesn't throw Command exception if there is enough fuel")]
    public void Test11()
    {
        // arrange
        FuelTank tank = new()
        {
            Amount = 100
        };
        WarpEngine engine = new()
        {
            FuelConsumption = 10,
        };

        CheckFuelCommand command = new(tank, engine);

        // act
        var ex = Record.Exception(() => command.Execute());

        // assert
        Assert.Null(ex);
    }
}
