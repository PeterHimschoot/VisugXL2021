using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using U2U.Currencies.Core.Entities;
using U2U.EntityFrameworkCore.Abstractions;
using U2U.EntityFrameworkCore.Testing;

namespace U2U.Currencies.Core.Tests
{
  public class HardCodedCurrenciesFixture
  {
    public IQueryable<Currency> None
      => new List<Currency>().AsQueryable();

    public IQueryable<Currency> Doubles
    {
      get
      {
        IEnumerable<Currency> Gen()
        {
          foreach (CurrencyName currencyName in Enum.GetValues(typeof(CurrencyName)))
          {
            if (currencyName == CurrencyName.EUR)
            {
              yield return new Currency(0, currencyName, 1.0M);
            }
            else
            {
              yield return new Currency(0, currencyName, 2.0M);
            }
          }
        }
        return Gen().AsQueryable();
      }
    }

    public IReadonlyRepository<Currency> FakeRepo()
      => new Mock<IReadonlyRepository<Currency>>().Object;

    public IRepository<Currency> FakeRepoWithData(IQueryable<Currency> data)
    {
      FakeRepositoryFactory<Currency> factory = new FakeRepositoryFactory<Currency>();
      return factory.FakeRepoWithData(data);
    } // Don't Repeat Yourself
  }
}
