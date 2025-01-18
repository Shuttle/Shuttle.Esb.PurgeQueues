using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Shuttle.Core.Contract;

namespace Shuttle.Esb.PurgeQueues;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPurgeQueues(this IServiceCollection services, Action<PurgeQueuesBuilder>? builder = null)
    {
        var purgeQueuesBuilder = new PurgeQueuesBuilder(Guard.AgainstNull(services));

        builder?.Invoke(purgeQueuesBuilder);

        services.TryAddSingleton<PurgeQueuesHostedService, PurgeQueuesHostedService>();
        services.TryAddSingleton<PurgeQueuesObserver, PurgeQueuesObserver>();

        services.AddSingleton(Options.Create(purgeQueuesBuilder.Options));

        services.AddHostedService<PurgeQueuesHostedService>();

        return services;
    }
}