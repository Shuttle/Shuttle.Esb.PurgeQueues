using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Esb.PurgeQueues;

public class PurgeQueuesHostedService : IHostedService
{
    private readonly IPipelineFactory _pipelineFactory;
    private readonly PurgeQueuesObserver _purgeQueuesObserver;
    private readonly string _startupPipelineName = Guard.AgainstNull(typeof(StartupPipeline).FullName);

    public PurgeQueuesHostedService(IPipelineFactory pipelineFactory, PurgeQueuesObserver purgeQueuesObserver)
    {
        _pipelineFactory = Guard.AgainstNull(pipelineFactory, nameof(pipelineFactory));
        _purgeQueuesObserver = Guard.AgainstNull(purgeQueuesObserver, nameof(purgeQueuesObserver));

        _pipelineFactory.PipelineCreated += OnPipelineCreated;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _pipelineFactory.PipelineCreated += OnPipelineCreated;

        await Task.CompletedTask;
    }

    private void OnPipelineCreated(object? sender, PipelineEventArgs e)
    {
        if (!(e.Pipeline.GetType().FullName ?? string.Empty)
            .Equals(_startupPipelineName, StringComparison.InvariantCultureIgnoreCase))
        {
            return;
        }

        e.Pipeline.AddObserver(_purgeQueuesObserver);
    }
}