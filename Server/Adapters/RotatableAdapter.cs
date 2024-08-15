using Server.Interfaces;
using Server.ValueTypes;

namespace Server.Adapters;

public class RotatableAdapter(object rotatableObject) : IRotatable
{
    public Angle GetPosition()
    {
        return Invoke<Angle>("GetAnglePosition");
    }

    public Angle GetVelocity()
    {
        return Invoke<Angle>("GetAngleVelocity");
    }

    public void SetPosition(Angle position)
    {
        Invoke("SetAnglePosition", position);
    }

    private T Invoke<T>(string methodName)
    {
        try
        {
            return (T)rotatableObject.GetType().GetMethod(methodName)!.Invoke(rotatableObject, null)!;
        }
        catch (Exception e)
        {
            throw new ArgumentException("Argument is invalid", e);
        }
    }

    private void Invoke<T>(string methodName, T value)
    {
        try
        {
            rotatableObject.GetType().GetMethod(methodName)!.Invoke(rotatableObject, [value]);
        }
        catch (Exception e)
        {
            throw new ArgumentException("Argument is invalid", e);
        }
    }
}
