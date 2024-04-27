// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Common.Behaviours;
using CleanArchitecture.Blazor.Application.Common.Interfaces.MultiTenant;
using CleanArchitecture.Blazor.Application.Common.PublishStrategies;
using CleanArchitecture.Blazor.Application.Common.Security;
using CleanArchitecture.Blazor.Application.Services;
using CleanArchitecture.Blazor.Application.Services.DataServices;
using CleanArchitecture.Blazor.Application.Services.MultiTenant;
using CleanArchitecture.Blazor.Application.Services.Picklist;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Blazor.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(config=> {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.NotificationPublisher = new ParallelNoWaitPublisher();
            config.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
            config.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
            config.AddOpenBehavior(typeof(RequestExceptionProcessorBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            config.AddOpenBehavior(typeof(MemoryCacheBehaviour<,>));
            config.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
            config.AddOpenBehavior(typeof(CacheInvalidationBehaviour<,>));
         
            
        });
        services.AddFluxor(options => {
            options.ScanAssemblies(Assembly.GetExecutingAssembly());
            options.UseReduxDevTools();
        });
        services.AddLazyCache();
        services.AddSingleton<PicklistService>();
        services.AddSingleton<IPicklistService>(sp => {
            var service = sp.GetRequiredService<PicklistService>();
            service.Initialize();
            return service;
            });
        services.AddSingleton<TenantService>();
        services.AddSingleton<ITenantService>(sp => {
            var service = sp.GetRequiredService<TenantService>();
            service.Initialize();
            return service;
        });
        services.AddSingleton<BusService>();
        services.AddSingleton<IBusService>(sp => {
            var service = sp.GetRequiredService<BusService>();
            service.Initialize();
            return service;
        });
        services.AddSingleton<PilotService>();
        services.AddSingleton<IPilotService>(sp => {
            var service = sp.GetRequiredService<PilotService>();
            service.Initialize();
            return service;
        });
        services.AddSingleton<SchoolService>();
        services.AddSingleton<ISchoolService>(sp => {
            var service = sp.GetRequiredService<SchoolService>();
            service.Initialize();
            return service;
        });
        services.AddSingleton<ItineraryService>();
        services.AddSingleton<IItineraryService>(sp => {
            var service = sp.GetRequiredService<ItineraryService>();
            service.Initialize();
            return service;
        });
        services.AddSingleton<UserService>();
        services.AddSingleton<IUserService>(sp => {
            var service = sp.GetRequiredService<UserService>();
            service.Initialize();
            return service;
        });





        return services;
    }
   
}
