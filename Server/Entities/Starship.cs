using System.Numerics;
using Server.ValueTypes;

namespace Server.Entities;

public class Starship
{
    private Vector2 _position;
    private Vector2 _velocity;

    private Angle? _anglePosition;
    private Angle? _angleVelocity;

    public Vector2 GetPosition()
    {
        return _position;
    }

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

    public Angle GetAnglePosition()
    {
        return _anglePosition;
    }

    public Angle GetAngleVelocity()
    {
        return _angleVelocity;
    }

    public void SetAnglePosition(Angle anglePosition)
    {
        _anglePosition = anglePosition;
    }

    public void SetAngleVelocity(Angle angleVelocity)
    {
        _angleVelocity = angleVelocity;
    }
}
