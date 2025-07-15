using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace NortheastMegabuck.Bowlers;
internal static class BowlersExtensions
{
    internal static IServiceCollection AddBowlersModule(this IServiceCollection services)
    {
        services.AddTransient<Retrieve.IAdapter, Retrieve.Adapter>();

        services.AddTransient<Search.IView, Search.Dialog>();
        services.AddTransient<Search.IAdapter, Search.Adapter>();
        services.AddTransient<Search.Dialog>();
        services.AddTransient<Search.Presenter>();

        services.AddTransient<Update.IBowlerNameView, Update.NameForm>();
        services.AddTransient<Update.IView, Update.UpdateForm>();
        services.AddTransient<Update.IAdapter, Update.Adapter>();
        services.AddTransient<Update.NameForm>();
        services.AddTransient<Update.UpdateForm>();
        services.AddTransient<Update.NamePresenter>();
        services.AddTransient<Update.Presenter>();

        return services;
    }
}
