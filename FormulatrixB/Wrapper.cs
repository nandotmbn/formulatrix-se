using FormulatrixB.Database;
using FormulatrixB.Interfaces;
using FormulatrixB.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FormulatrixB;

public static class FormulatrixB
{
  public static IServiceCollection FormulatrixBServices(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDbContext<AppDBContext>
    (
      options =>
      {
        options.ConfigureWarnings(op => op.Ignore(RelationalEventId.PendingModelChangesWarning)).UseNpgsql(configuration.GetConnectionString("Default"),
        b => b.MigrationsAssembly("WebAPI"));
      },
      ServiceLifetime.Scoped
    );

    services.AddScoped<IItem, ItemRepository>();
    return services;
  }
}

