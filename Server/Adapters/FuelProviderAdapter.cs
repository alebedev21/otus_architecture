using Server.Interfaces;

namespace Server.Adapters;

public class FuelProviderAdapter(object providerObject) : IFuelProvider
{
    public int GetFuelAmount()
    {
        return Invoke<int>("FuelAmount");
    }

    public void SetFuelAmount(int fuelAmount)
    {
        Invoke("FuelAmount", fuelAmount);
    }

    private T Invoke<T>(string propertyName)
    {
        try
        {
            return (T)providerObject.GetType().GetProperty(propertyName)!.GetValue(providerObject)!;
        }
        catch (Exception e)
        {
            throw new ArgumentException("Argument is invalid", e);
        }
    }

    private void Invoke<T>(string propertyName, T value)
    {
        try
        {
            providerObject.GetType().GetProperty(propertyName)!.SetValue(providerObject, value);
        }
        catch (Exception e)
        {
            throw new ArgumentException("Argument is invalid", e);
        }
    }
}
