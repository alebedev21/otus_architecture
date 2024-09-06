using System.Numerics;

namespace ServerTests.Entities;

public class UnknownVelocityObject
{
    private Vector2 _position;
    private Vector2 _velocity;

    public Vector2 GetPosition()
    {
        return _position;
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
