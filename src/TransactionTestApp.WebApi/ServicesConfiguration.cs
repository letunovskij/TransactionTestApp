using Microsoft.EntityFrameworkCore;
using TransactionTestApp.Abstractions.Transactions;
using TransactionTestApp.Data;
using TransactionTestApp.Services.Transactions;

namespace TransactionTestApp.WebApi;

public static class ServicesConfiguration
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<TransactionDbContext>(x =>
        {
            x.UseSqlite("Filename=TransactionDb.sqlite");
        });

        services.AddScoped<ITransactionService, TransactionService>();

        return services;
    }
}
