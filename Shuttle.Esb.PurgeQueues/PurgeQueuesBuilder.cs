using System;
using Microsoft.Extensions.DependencyInjection;
using Shuttle.Core.Contract;

namespace Shuttle.Esb.PurgeQueues;

public class PurgeQueuesBuilder
{
    private PurgeQueuesOptions _purgeQueuesOptions = new();

    public PurgeQueuesBuilder(IServiceCollection services)
    {
        Services = Guard.AgainstNull(services);
    }

    public PurgeQueuesOptions Options
    {
        get => _purgeQueuesOptions;
        set => _purgeQueuesOptions = Guard.AgainstNull(value);
    }

    public IServiceCollection Services { get; }
}