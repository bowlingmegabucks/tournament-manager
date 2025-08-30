using Asp.Versioning;
using Asp.Versioning.Builder;

namespace BowlingMegabucks.TournamentManager.Api.Versioning;

internal static class VersioningExtensions
{
    internal const string Version1 = "v1";

    public static WebApplicationBuilder AddVersioning(this WebApplicationBuilder builder)
    {
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return builder;
    }

    internal static ApiVersionSet BuildVersionSet(this IEndpointRouteBuilder app, string name, params int[] versions)
    {
        ApiVersionSetBuilder apiVersionSetBuilder = app.NewApiVersionSet(name);

        foreach (int version in versions)
        {
            apiVersionSetBuilder.HasApiVersion(new ApiVersion(version));
        }

        apiVersionSetBuilder.ReportApiVersions();

        return apiVersionSetBuilder.Build();
    }
}
