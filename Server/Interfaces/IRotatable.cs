using Server.ValueTypes;

namespace Server.Interfaces;

public interface IRotatable
{
    Angle GetAnglePosition();
    Angle GetAngleVelocity();
    void SetAnglePosition(Angle position);
}
