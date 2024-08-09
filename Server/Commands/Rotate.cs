using Server.Interfaces;
using Server.ValueTypes;

namespace Server.Commands;

public class Rotate(IRotatable rotatableObject)
{
    public void Execute()
    {
        Angle newPosition = rotatableObject.GetPosition() + rotatableObject.GetVelocity();
        rotatableObject.SetPosition(newPosition);
    }
}
