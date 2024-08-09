using System.Numerics;
using Server.Interfaces;

namespace Server.Adapters;

public class MovableAdapter(object movableObject) : IMovable
{
    public Vector2 GetPosition()
    {
        return Invoke<Vector2>("GetPosition");
    }

    public Vector2 GetVelocity()
    {
        return Invoke<Vector2>("GetVelocity");
    }

    public void SetPosition(Vector2 position)
    {
        Invoke("SetPosition", position);
    }

    private T Invoke<T>(string methodName)
    {
        try
        {
            return (T)movableObject.GetType().GetMethod(methodName)!.Invoke(movableObject, null)!;
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
            movableObject.GetType().GetMethod(methodName)!.Invoke(movableObject, [value]);
        }
        catch (Exception e)
        {
            throw new ArgumentException("Argument is invalid", e);
        }
    }
}
