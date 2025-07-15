using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace NortheastMegabuck.Squads;
internal static class SquadsExtensions
{
    public static IServiceCollection AddSquadsModule(this IServiceCollection services)
    {
        services.AddTransient<Add.IAdapter, Add.Adapter>();
        services.AddTransient<Add.Form>();

        services.AddTransient<Complete.IAdapter, Complete.Adapter>();

        services.AddTransient<Portal.Form>();

        services.AddTransient<Results.IAdapter, Results.Adapter>();
        services.AddTransient<Results.Form>();

        services.AddTransient<Retrieve.IAdapter, Retrieve.Adapter>();
        services.AddTransient<Retrieve.Form>();

        return services;
    }
}
