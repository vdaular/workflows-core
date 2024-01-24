using Elsa.Abstractions;
using Elsa.Alterations.AlterationTypes;
using Elsa.Alterations.Core.Contracts;
using Elsa.Alterations.Core.Results;
using Elsa.Workflows;
using Elsa.Workflows.Management.Contracts;
using Elsa.Workflows.Management.Entities;
using Elsa.Workflows.Management.Filters;
using Elsa.Workflows.Runtime.Contracts;
using Elsa.Workflows.Runtime.Requests;
using JetBrains.Annotations;

namespace Elsa.Alterations.Endpoints.Workflows.Restart;

/// <summary>
/// Restarts the specified workflow instances.
/// </summary>
[PublicAPI]
public class Restart : ElsaEndpoint<Request, Response>
{
    private readonly IAlterationRunner _alterationRunner;
    private readonly IWorkflowDispatcher _workflowDispatcher;
    private readonly IWorkflowInstanceStore _workflowInstanceStore;

    /// <inheritdoc />
    public Restart(IAlterationRunner alterationRunner, IWorkflowDispatcher workflowDispatcher, IWorkflowInstanceStore workflowInstanceStore)
    {
        _alterationRunner = alterationRunner;
        _workflowDispatcher = workflowDispatcher;
        _workflowInstanceStore = workflowInstanceStore;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Routes("/alterations/workflows/restart");
        Verbs(FastEndpoints.Http.GET, FastEndpoints.Http.POST);
        ConfigurePermissions("run:alterations");
    }

    /// <inheritdoc />
    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var allResults = new List<RunAlterationsResult>();
        
        // Load each workflow instance, but only those that are in the Executing state.
        var workflowInstances = (await _workflowInstanceStore.FindManyAsync(new WorkflowInstanceFilter
        {
            Ids = request.WorkflowInstanceIds,
            WorkflowSubStatus = WorkflowSubStatus.Executing
        }, cancellationToken)).ToList();

        foreach (var workflowInstance in workflowInstances)
        {
            // Setup an alteration plan.
            var restartWorkflow = new RestartWorkflow
            {
                ExecutionMode = request.ExecutionMode, 
                Input = request.Input
            };
            var alterations = new[] { restartWorkflow };
            
            // Run the plan.
            var results = await _alterationRunner.RunAsync(request.WorkflowInstanceIds!, alterations, cancellationToken);
            allResults.AddRange(results);
            
            // Schedule updated workflow.
            var dispatchRequest = new DispatchWorkflowInstanceRequest(workflowInstance.Id);
            
            await _workflowDispatcher.DispatchAsync(dispatchRequest, cancellationToken);
        }
        
        // Write response.
        var response = new Response(allResults);
        await SendOkAsync(response, cancellationToken);
    }
}