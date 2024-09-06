namespace Server.Interfaces;

public interface IFuelProvider
{
    int GetFuelAmount();
    void SetFuelAmount(int fuelAmount);
}
