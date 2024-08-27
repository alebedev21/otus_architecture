using System.Numerics;
using Server.ValueTypes;

namespace Server.Entities;

public class Starship
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }

    public Angle? AnglePosition { get; set; }
    public Angle? AngleVelocity { get; set; }
}
