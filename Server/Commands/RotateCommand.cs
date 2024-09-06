using Server.Interfaces;
using Server.ValueTypes;

namespace Server.Commands;

public class RotateCommand(IRotatable rotatableObject) : ICommand
{
    public void Execute()
    {
        Angle newPosition = rotatableObject.GetAnglePosition() + rotatableObject.GetAngleVelocity();
        rotatableObject.SetAnglePosition(newPosition);
        Console.WriteLine("Rotated");
    }
}
