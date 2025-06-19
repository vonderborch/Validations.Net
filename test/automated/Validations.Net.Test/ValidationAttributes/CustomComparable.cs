using System;

namespace Validations.Net.Test.ValidationAttributes;

public class CustomComparable : IComparable<CustomComparable>
{
    public int X { get; }
    public CustomComparable(int x) => X = x;
    public int CompareTo(CustomComparable? other) => X.CompareTo(other?.X ?? 0);
    public override bool Equals(object? obj) => obj is CustomComparable c && c.X == X;
    public override int GetHashCode() => X.GetHashCode();
} 