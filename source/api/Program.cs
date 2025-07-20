using System.Text.Json;
using Azure.Identity;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication;
using NJsonSchema.Generation.TypeMappers;
using BowlingMegabucks.TournamentManager;
using BowlingMegabucks.TournamentManager.Api;
using BowlingMegabucks.TournamentManager.Api.Authentication;
using BowlingMegabucks.TournamentManager.Database;
using BowlingMegabucks.TournamentManager.Models;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Scalar.AspNetCore;
using BowlingMegabucks.TournamentManager.Api.Middleware;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

#pragma warning disable CA1861 // Avoid constant arrays as arguments

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.Configure<RateLimitingOptions>(builder.Configuration.GetSection("RateLimiting"));

builder.Services.AddProblemDetails();

builder.Services.AddHealthChecks()
    .AddMySql(
        builder.Configuration.GetConnectionString("Default")
            ?? throw new InvalidOperationException("Default connection string is not configured."),
        name: "database",
        tags: new[] { "db", "sql", "mysql" }
    );

builder.Services.AddFastEndpoints()
    .AddAuthorization()
    .AddAuthentication(ApiKeyAuthentication.SchemeName)
    .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthentication>(ApiKeyAuthentication.SchemeName, null);

builder.Services.AddBusinessLogic(builder.Configuration);

builder.Services.SwaggerDocument(o =>
{
    o.ReleaseVersion = 1;
    o.DocumentSettings = s =>
    {
        s.DocumentName = "v1";
        s.Title = "Northeast Megabuck Tournament API";
        s.Version = "v1";
        s.AddAuth(ApiKeyAuthentication.SchemeName, new()
        {
            Name = ApiKeyAuthentication.HeaderName,
            In = NSwag.OpenApiSecurityApiKeyLocation.Header,
            Type = NSwag.OpenApiSecuritySchemeType.ApiKey
        });

        s.SchemaSettings.TypeMappers.Add(new PrimitiveTypeMapper(typeof(TournamentId), schema =>
        {
            schema.Type = NJsonSchema.JsonObjectType.String;
            schema.Format = "uuid";
            schema.Example = TournamentId.New();
        }));

        s.SchemaSettings.TypeMappers.Add(new PrimitiveTypeMapper(typeof(SquadId), schema =>
        {
            schema.Type = NJsonSchema.JsonObjectType.String;
            schema.Format = "uuid";
            schema.Example = SquadId.New();
        }));

        s.SchemaSettings.TypeMappers.Add(new PrimitiveTypeMapper(typeof(RegistrationId), schema =>
        {
            schema.Type = NJsonSchema.JsonObjectType.String;
            schema.Format = "uuid";
            schema.Example = RegistrationId.New();
        }));

        s.SchemaSettings.TypeMappers.Add(new SmartEnumTypeMapper<Gender>());
    };

    o.UsePropertyNamingPolicy = true;
    o.EnableJWTBearerAuth = false;
    o.ShortSchemaNames = true;

    o.SerializerSettings = s
        => s.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

var keyVaultUrl = builder.Configuration.GetValue<string>("KEYVAULT_URL");

if (!string.IsNullOrEmpty(keyVaultUrl))
{
    builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), new DefaultAzureCredential());
}

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(builder.Environment.ApplicationName))
    .WithTracing(tracing => tracing
        .AddHttpClientInstrumentation()
        .AddAspNetCoreInstrumentation()
        .AddEntityFrameworkCoreInstrumentation())
    .WithMetrics(metrics => metrics
        .AddHttpClientInstrumentation()
        .AddAspNetCoreInstrumentation()
        .AddRuntimeInstrumentation()
        .AddMeter("Microsoft.AspNetCore.Hosting")
        .AddMeter("Microsoft.AspNetCore.Server.Kestrel"));

builder.Logging.AddOpenTelemetry(options =>
{
    options.IncludeScopes = true;
    options.IncludeFormattedMessage = true;
});

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddOpenTelemetry().UseOtlpExporter();
}
else
{
    builder.Services.AddOpenTelemetry().UseAzureMonitor();
}

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseOpenApi(c => c.Path = "/openapi/{documentName}.json");
    app.MapScalarApiReference();
    
    if (app.Environment.IsDevelopment())
    {
        app.UseHttpsRedirection();

        using var scope = app.Services.CreateScope();
        await scope.ApplyMigrationsAsync();
    }
}

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            results = report.Entries.Select(e => new
            {
                key = e.Key,
                status = e.Value.Status.ToString(),
                description = e.Value.Description,
                data = e.Value.Data
            })
        });
        await context.Response.WriteAsync(result);
    }
});

app.UseMiddleware<RateLimitingMiddleware>();

app.UseDefaultExceptionHandler()
    .UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints(c =>
    {
        c.Versioning.Prefix = "v";
        c.Versioning.DefaultVersion = 1;
        c.Versioning.PrependToRoute = true;

        c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        c.Endpoints.ShortNames = true;
        c.Errors.UseProblemDetails(pd =>
        {
            pd.IndicateErrorCode = true;
            pd.IndicateErrorSeverity = true;
        });

        c.Endpoints.Configurator = endpoints => endpoints
            .Options(options => options
                .AddEndpointFilter<RequestContextLoggingMiddleware>());
    })
    .UseSwaggerGen();

await app.RunAsync();
