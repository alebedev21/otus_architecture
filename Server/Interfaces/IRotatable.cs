using Server.ValueTypes;

namespace Server.Interfaces;

public interface IRotatable
{
    Angle GetPosition();
    Angle GetVelocity();
    void SetPosition(Angle position);
}
