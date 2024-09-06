using Server.Interfaces;

namespace Server.Adapters;

public class FuelConsumerAdapter(object consumerObject) : IFuelConsumer
{
    public int GetFuelConsumption()
    {
        return Invoke<int>("FuelConsumption");
    }

    private T Invoke<T>(string propertyName)
    {
        try
        {
            return (T)consumerObject.GetType().GetProperty(propertyName)!.GetValue(consumerObject)!;
        }
        catch (Exception e)
        {
            throw new ArgumentException("Argument is invalid", e);
        }
    }
}
