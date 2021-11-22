using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Hosting;

namespace U2U.Currencies.Infra;

public class CurrencyDb : DbContext
{
  public CurrencyDb(DbContextOptions<CurrencyDb> options)
    : base(options) { }

  public DbSet<Currency>? Currencies { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfiguration(new CurrencyConfiguration());

    // TODO : Figure out if we can use this to get the current environment
    //IServiceProvider services = (this as IInfrastructure<IServiceProvider>).Instance;
    //IHostEnvironment? hostEnvironment = services.GetService<IHostEnvironment>();

    //if (hostEnvironment is not null && hostEnvironment.IsDevelopment())
    //{
      modelBuilder.HasData(new CurrencyData());
    //}
  }
}

