namespace Server.ValueTypes;

public class Angle : IEquatable<Angle>
{
    private const int _sectorCount = 24;
    private int _currentSector;

    private int SectorSize => 360 / _sectorCount;

    public Angle() { }

    public Angle(int coreAngle)
    {
        SetValue(coreAngle);
    }

    public void SetValue(int coreAngle)
    {
        try
        {
            _currentSector = coreAngle % 360 / SectorSize;
        }
        catch (Exception e)
        {
            throw new ArgumentException("Argument is invalid", e);
        }
    }

    public int GetValue()
    {
        return _currentSector * SectorSize;
    }

    public static Angle operator +(Angle a, Angle b)
    {
        Angle result = new();

        result.SetValue(a.GetValue() + b.GetValue());

        return result;
    }

    public static bool operator ==(Angle a, Angle b) => a.GetValue() == b.GetValue();
    public static bool operator !=(Angle a, Angle b) => a.GetValue() != b.GetValue();

    public bool Equals(Angle? other)
    {
        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (other is null)
        {
            return false;
        }

        return GetValue() == other.GetValue();
    }

    public override bool Equals(object? obj) => obj is Angle other && Equals(other);
    public override int GetHashCode() => GetValue();
}
