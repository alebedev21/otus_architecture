using System.Numerics;
using Server.Interfaces;

namespace Server.Commands;

public class MoveCommand(IMovable movableObject) : ICommand
{
    public void Execute()
    {
        Vector2 newPosition = movableObject.GetPosition() + movableObject.GetVelocity();
        movableObject.SetPosition(newPosition);
        Console.WriteLine("Moved");
    }
}
