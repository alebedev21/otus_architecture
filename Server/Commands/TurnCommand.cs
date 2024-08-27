using System.Numerics;
using Server.Interfaces;

namespace Server.Commands;

public class TurnCommand(IMovable movableObject, IRotatable rotatableObject) : ICommand
{
    public void Execute()
    {
        var currentVelocity = movableObject.GetVelocity();
        var currentAngle = rotatableObject.GetAnglePosition().GetValue() * Math.PI / 180;

        var cos = (float)Math.Cos(currentAngle);
        var sin = (float)Math.Sin(currentAngle);

        var newX = currentVelocity.X * cos + currentVelocity.Y * sin;
        var newY = -currentVelocity.X * sin + currentVelocity.Y * cos;

        var newVelocity = new Vector2(newX, newY);
        movableObject.SetVelocity(newVelocity);
    }
}
