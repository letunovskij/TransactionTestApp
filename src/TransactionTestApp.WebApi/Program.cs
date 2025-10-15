using Mapster;
using NSwag;
using Serilog;
using System.Reflection;
using TransactionTestApp.WebApi.Middleware;

namespace TransactionTestApp.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration)
            => loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration));

        // Add services to the container.
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        builder.Services.RegisterApplicationServices(builder.Configuration);

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddOpenApiDocument(config =>
        {
            config.SchemaSettings.GenerateEnumMappingDescription = true;
            config.DocumentName = "v1";
            config.Title = "Transaction";
            config.Version = "v1";
        });

        builder.Services.AddExceptionHandler<ExceptionHandler>();
        builder.Services.AddProblemDetails();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.UseOpenApi();
        app.UseSwaggerUi();
        app.UseExceptionHandler();

        app.Run();
    }
}
