using System.Text.Json;
using BowlingMegabucks.TournamentManager.Api.Authentication;
using BowlingMegabucks.TournamentManager.Models;
using FastEndpoints.Swagger;
using NJsonSchema.Generation.TypeMappers;
using Scalar.AspNetCore;

namespace BowlingMegabucks.TournamentManager.Api.Extensions;

internal static class OpenApiExtensions
{
    public static WebApplicationBuilder AddOpenApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi()
            .SwaggerDocument(ConfigureSwaggerDocument);

        return builder;
    }

    private static void ConfigureSwaggerDocument(DocumentOptions o)
    {
        o.ReleaseVersion = 1;
        o.ExcludeNonFastEndpoints = true;
        
        o.DocumentSettings = s =>
        {
            s.DocumentName = "v1";
            s.Title = "Northeast Megabuck Tournament API";
            s.Version = "v1";
            s.AddAuth(ApiKeyAuthentication._schemeName, new()
            {
                Name = ApiKeyAuthentication._headerName,
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
    }

    public static IApplicationBuilder UseOpenApiDocumentation(this WebApplication app)
    { 
        app.UseOpenApi(c => c.Path = "/openapi/{documentName}.json");
        app.MapScalarApiReference();

        return app;
    }
}