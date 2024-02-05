using System;
using Microsoft.Extensions.DependencyInjection;
using Shuttle.Core.Contract;

namespace Shuttle.Esb.PurgeQueues
{
    public class PurgeQueuesBuilder
    {
        private PurgeQueuesOptions _purgeQueuesOptions = new PurgeQueuesOptions();
        public IServiceCollection Services { get; }

        public PurgeQueuesBuilder(IServiceCollection services)
        {
            Guard.AgainstNull(services, nameof(services));

            Services = services;
        }

        public PurgeQueuesOptions Options
        {
            get => _purgeQueuesOptions;
            set => _purgeQueuesOptions = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}