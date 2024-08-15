using System.Numerics;
using Server.Interfaces;

namespace Server.Commands;

public class Move(IMovable movableObject)
{
    public void Execute()
    {
        Vector2 newPosition = movableObject.GetPosition() + movableObject.GetVelocity();
        movableObject.SetPosition(newPosition);
    }
}
