
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace NortheastMegabuck.Registrations;

internal static class RegistrationExtensions
{
    public static IServiceCollection AddRegistrationModule(this IServiceCollection services)
    {
        services.AddTransient<IRepository, Repository>();
        services.AddSingleton<IEntityMapper, EntityMapper>();

        services.AddSingleton<IValidator<Models.Registration>, Add.Validator>();
        services.AddTransient<Add.IBusinessLogic, Add.BusinessLogic>();
        services.AddTransient<Add.IDataLayer, Add.DataLayer>();

        services.AddTransient<Delete.IBusinessLogic, Delete.BusinessLogic>();
        services.AddTransient<Delete.IDataLayer, Delete.DataLayer>();

        services.AddTransient<Retrieve.IBusinessLogic, Retrieve.BusinessLogic>();
        services.AddTransient<Retrieve.IDataLayer, Retrieve.DataLayer>();

        services.AddSingleton<IValidator<Update.UpdateRegistrationModel>, Update.Validator>();
        services.AddTransient<Update.IBusinessLogic, Update.BusinessLogic>();
        services.AddTransient<Update.IDataLayer, Update.DataLayer>();

        return services;
    }
}
