using Server.Interfaces;
using Server.ValueTypes;

namespace Server.Commands;

public class RotateCommand(IRotatable rotatableObject) : ICommand
{
    public void Execute()
    {
        Angle newPosition = rotatableObject.GetPosition() + rotatableObject.GetVelocity();
        rotatableObject.SetPosition(newPosition);
        Console.WriteLine("Rotated");
    }
}
