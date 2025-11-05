namespace Domain.Structs;

public struct Email : IComparable<Email>, IEquatable<Email>
{
    private readonly string _value;

    public Email(string value)
    {
        _value = value.Trim().ToLower();
    }
    
    public int CompareTo(Email other)
    {
        return string.Compare(_value, other._value, StringComparison.Ordinal);
    }

    public bool Equals(Email other)
    {
        return _value == other._value;
    }

    public override bool Equals(object? obj)
    {
        return obj is Email other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    public override string ToString()
    {
        return _value;
    }
    
    public static bool operator ==(Email left, Email right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Email left, Email right)
    {
        return !(left == right);
    }
    
    public static implicit operator Email(string s) => new (s);
    public static implicit operator string(Email e) => e.ToString();
}