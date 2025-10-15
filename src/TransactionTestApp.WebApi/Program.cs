using Mapster;
using NSwag;
using System.Reflection;

namespace TransactionTestApp.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

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

        app.Run();
    }
}
