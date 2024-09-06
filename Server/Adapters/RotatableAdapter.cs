using Server.Interfaces;
using Server.ValueTypes;

namespace Server.Adapters;

public class RotatableAdapter(object rotatableObject) : IRotatable
{
    public Angle GetAnglePosition()
    {
        return Invoke<Angle>("AnglePosition");
    }

    public Angle GetAngleVelocity()
    {
        return Invoke<Angle>("AngleVelocity");
    }

    public void SetAnglePosition(Angle position)
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
