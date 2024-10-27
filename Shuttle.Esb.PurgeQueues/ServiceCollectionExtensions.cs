using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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

        services.AddOptions<PurgeQueuesOptions>().Configure(options =>
        {
            options.Uris = new(purgeQueuesBuilder.Options.Uris);
        });

        services.AddHostedService<PurgeQueuesHostedService>();

        return services;
    }
}