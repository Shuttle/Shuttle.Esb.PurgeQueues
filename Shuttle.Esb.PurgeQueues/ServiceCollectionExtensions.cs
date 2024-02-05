using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Esb.PurgeQueues
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPurgeQueues(this IServiceCollection services, Action<PurgeQueuesBuilder> builder = null)
        {
            Guard.AgainstNull(services, nameof(services));

            var purgeQueuesBuilder = new PurgeQueuesBuilder(services);

            builder?.Invoke(purgeQueuesBuilder);

            services.TryAddSingleton<PurgeQueuesHostedService, PurgeQueuesHostedService>();
            services.TryAddSingleton<PurgeQueuesObserver, PurgeQueuesObserver>();

            services.AddOptions<PurgeQueuesOptions>().Configure(options =>
            {
                options.Uris = new List<string>(purgeQueuesBuilder.Options.Uris ?? Enumerable.Empty<string>());
            });

            services.AddPipelineModule<PurgeQueuesHostedService>();

            return services;
        }
    }
}