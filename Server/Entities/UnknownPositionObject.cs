using System.Numerics;

namespace Server.Entities;

public class UnknownPositionObject
{
    private Vector2 _position;
    private Vector2 _velocity;

    public Vector2 GetVelocity()
    {
        return _velocity;
    }

    public void SetPosition(Vector2 position)
    {
        _position = position;
    }

    public void SetVelocity(Vector2 velocity)
    {
        _velocity = velocity;
    }
}
