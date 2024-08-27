using Server.Interfaces;
using Server.ValueTypes;

namespace Server.Adapters;

public class RotatableAdapter(object rotatableObject) : IRotatable
{
    public Angle GetPosition()
    {
        return Invoke<Angle>("AnglePosition");
    }

    public Angle GetVelocity()
    {
        return Invoke<Angle>("AngleVelocity");
    }

    public void SetPosition(Angle position)
    {
        Invoke("AnglePosition", position);
    }

    private T Invoke<T>(string property)
    {
        try
        {
            return (T)rotatableObject.GetType().GetProperty(property)!.GetValue(rotatableObject)!;
        }
        catch (Exception e)
        {
            throw new ArgumentException("Argument is invalid", e);
        }
    }

    private void Invoke<T>(string property, T value)
    {
        try
        {
            rotatableObject.GetType().GetProperty(property)!.SetValue(rotatableObject, value);
        }
        catch (Exception e)
        {
            throw new ArgumentException("Argument is invalid", e);
        }
    }
}
