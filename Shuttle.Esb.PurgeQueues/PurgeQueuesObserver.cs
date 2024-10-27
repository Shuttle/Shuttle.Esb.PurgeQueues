using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Esb.PurgeQueues;

public class PurgeQueuesObserver : IPipelineObserver<OnAfterCreatePhysicalQueues>
{
    private readonly PurgeQueuesOptions _purgeQueuesOptions;
    private readonly IQueueService _queueService;

    public PurgeQueuesObserver(IOptions<PurgeQueuesOptions> purgeQueuesOptions, IQueueService queueService)
    {
        _purgeQueuesOptions = Guard.AgainstNull(Guard.AgainstNull(purgeQueuesOptions).Value);
        _queueService = Guard.AgainstNull(queueService);
    }

    public async Task ExecuteAsync(IPipelineContext<OnAfterCreatePhysicalQueues> pipelineContext)
    {
        foreach (var uri in _purgeQueuesOptions.Uris)
        {
            await ((_queueService.Get(uri) as IPurgeQueue)?.PurgeAsync() ?? Task.CompletedTask);
        }
    }
}