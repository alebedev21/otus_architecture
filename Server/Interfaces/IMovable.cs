using System.Numerics;

namespace Server.Interfaces;

public interface IMovable
{
    Vector2 GetPosition();
    Vector2 GetVelocity();
    void SetPosition(Vector2 position);
}
