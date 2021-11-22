namespace U2U.Currencies.Core.Tests;

public class MoneyShould
{
  [Theory]
  [InlineData(9.93, 9.99)]
  [InlineData(5.63, 5.99)]
  [InlineData(50.00, 49.99)]
  public void RoundDownCorrectly(decimal amount, decimal expected)
  {
    Money? sut = new Money(amount);
    Assert.Equal(new Money(expected), sut.Rounded);
  }

  [Theory]
  [InlineData(9.13, 9.49)]
  [InlineData(5.73, 5.99)]
  public void RoundUpCorrectly(decimal amount, decimal expected)
  {
    Money? sut = new Money(amount);
    Assert.Equal(new Money(expected), sut.Rounded);
  }

  [Fact]
  public void AddCorrectly()
  {
    Money? a = new Money(10, CurrencyName.EUR);
    Money? b = new Money(20, CurrencyName.EUR);

    Money? c = a + b;
    c.Amount.Should().Be(30);
    c.Currency.Should().Be(CurrencyName.EUR);
  }

  [Fact]
  public void OverrideEqualsCorrectlyToo()
  {
    Money? a = new Money(10, CurrencyName.EUR);
    Money? b = new Money(20, CurrencyName.EUR);
    Money? c = new Money(10, CurrencyName.EUR);

    Assert.True(a.Equals(a));
    Assert.True(a.Equals(c));
    Assert.True(b.Equals(b));

    Assert.False(a.Equals(b));
    Assert.False(b.Equals(a));
  }

  [Fact]
  public void CompareCorrectly()
  {
    Money? a = new Money(10, CurrencyName.EUR);
    Money? b = new Money(20, CurrencyName.EUR);

    a.Should().Be(a);
    a.Should().NotBe(b);
  }

  [Fact]
  public void OverrideEqualsCorrectly()
  {
    Money? a = new Money(10, CurrencyName.EUR);
    Money? b = new Money(10, CurrencyName.EUR);

    a.Should().Be(b);
    b.Should().Be(a);
    (a == b).Should().BeTrue();
    (a != b).Should().BeFalse();
  }

  [Fact]
  public void BehaveLikeValueObject()
  {
    Money? a = new Money(10, CurrencyName.EUR);
    Money? b = new Money(20, CurrencyName.EUR);
    Money? c = a + b;
    c.Should().NotBeSameAs(a).And.NotBeSameAs(b);
    a.Amount.Should().Be(10);
    b.Amount.Should().Be(20);
  }
}

