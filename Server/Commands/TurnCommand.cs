using System.Numerics;
using Server.Interfaces;

namespace Server.Commands;

public class TurnCommand(IMovable movableObject, IRotatable rotatableObject) : ICommand
{
    public void Execute()
    {
        var currentVelocity = movableObject.GetVelocity();

        bool isNotMoving = CheckNotMoving(currentVelocity);

        if (isNotMoving)
        {
            return;
        }

        var currentAngle = rotatableObject.GetAnglePosition().GetValue() * Math.PI / 180;

        var cos = Math.Cos(currentAngle);
        var sin = Math.Sin(currentAngle);

        var newX = currentVelocity.X * cos + currentVelocity.Y * sin;
        var newY = -currentVelocity.X * sin + currentVelocity.Y * cos;

        var newVelocity = new Vector2(
            (float)Math.Round(newX, 0, MidpointRounding.AwayFromZero),
            (float)Math.Round(newY, 0, MidpointRounding.AwayFromZero));
        movableObject.SetVelocity(newVelocity);
    }

    private static bool CheckNotMoving(Vector2 currentVelocity)
    {
        return (Math.Abs(currentVelocity.X) - Defines.Tolerance < 0) && (Math.Abs(currentVelocity.Y) - Defines.Tolerance < 0);
    }
}
