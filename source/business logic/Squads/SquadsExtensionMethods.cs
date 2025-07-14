using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace NortheastMegabuck.Squads;
internal static class SquadsExtensionMethods
{
    public static IServiceCollection AddSquadsModule(this IServiceCollection services)
    {
        services.AddSingleton<IHandicapCalculator, HandicapCalculator>();
        services.AddSingleton<IHandicapCalculatorInternal, HandicapCalculator>();

        return services;
    }
}
