namespace U2U.Games.Core.ValueObjects;

public class Address
{
  public string Street { get; private set; }

  public string City { get; private set; }

  public Address(string street, string city)
  {
    Street = street;
    City = city;
  }

  public override bool Equals(object? obj)
    => ValueObjectComparer<Address>.Instance.Equals(this, obj);

  public bool Equals(Address? other)
    => ValueObjectComparer<Address>.Instance.Equals(this, other);

  public static bool operator ==(Address left, Address right)
  => ValueObjectComparer<Address>.Instance.Equals(left, right);

  public static bool operator !=(Address left, Address right)
    => !(left == right);

  public override int GetHashCode()
  => ValueObjectComparer<Address>.Instance.GetHashCode(this);
}
