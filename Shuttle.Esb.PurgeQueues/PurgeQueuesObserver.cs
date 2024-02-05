using Microsoft.Extensions.Options;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Esb.PurgeQueues
{
    public class PurgeQueuesObserver : IPipelineObserver<OnAfterConfigure>
    {
        private readonly PurgeQueuesOptions _purgeQueuesOptions;
        private readonly IQueueService _queueService;

        public PurgeQueuesObserver(IOptions<PurgeQueuesOptions> purgeQueuesOptions, IQueueService queueService)
        {
            Guard.AgainstNull(purgeQueuesOptions, nameof(purgeQueuesOptions));
            Guard.AgainstNull(purgeQueuesOptions.Value, nameof(purgeQueuesOptions.Value));
            Guard.AgainstNull(queueService, nameof(queueService));

            _purgeQueuesOptions = purgeQueuesOptions.Value;
            _queueService = queueService;
        }

        public void Execute(OnAfterConfigure pipelineEvent)
        {
            foreach (var uri in _purgeQueuesOptions.Uris)
            {
                (_queueService.Get(uri) as IPurgeQueue)?.Purge();
            }
        }
    }
}