using System.Numerics;
using Server.Interfaces;

namespace Server.Adapters;

public class MovableAdapter(object movableObject) : IMovable
{
    public Vector2 GetPosition()
    {
        return Invoke<Vector2>("Position");
    }

    public Vector2 GetVelocity()
    {
        return Invoke<Vector2>("Velocity");
    }

    public void SetPosition(Vector2 position)
    {
        Invoke("Position", position);
    }

    public void SetVelocity(Vector2 velocity)
    {
        Invoke("Velocity", velocity);
    }

    private T Invoke<T>(string propertyName)
    {
        try
        {
            return (T)movableObject.GetType().GetProperty(propertyName)!.GetValue(movableObject)!;
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
            movableObject.GetType().GetProperty(propertyName)!.SetValue(movableObject, value);
        }
        catch (Exception e)
        {
            throw new ArgumentException("Argument is invalid", e);
        }
    }
}
