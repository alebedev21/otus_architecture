using System.Numerics;

namespace ServerTests.Entities;

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
public class UnchangeablePositionObject
{
    private Vector2 _position;
    private Vector2 _velocity;

    public Vector2 GetPosition()
    {
        return _position;
    }

    public Vector2 GetVelocity()
    {
        return _velocity;
    }

    public void SetVelocity(Vector2 velocity)
    {
        _velocity = velocity;
    }
}
