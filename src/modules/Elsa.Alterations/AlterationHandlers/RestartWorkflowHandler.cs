using Elsa.Alterations.AlterationTypes;
using Elsa.Alterations.Core.Abstractions;
using Elsa.Alterations.Core.Contexts;
using Elsa.Workflows;
using Elsa.Workflows.Models;
using Elsa.Workflows.Runtime.Contracts;
using Elsa.Workflows.Runtime.Options;
using Elsa.Workflows.Runtime.Requests;

namespace Elsa.Alterations.AlterationHandlers;

/// <summary>
/// Schedules an activity for execution.
/// </summary>
public class RestartWorkflowHandler(
    IWorkflowDispatcher workflowDispatcher,
    IWorkflowRuntime workflowRuntime) : AlterationHandlerBase<RestartWorkflow>
{
    /// <inheritdoc />
    protected override async ValueTask HandleAsync(AlterationContext context, RestartWorkflow alteration)
    {
        var workflowExecutionContext = context.WorkflowExecutionContext;

        if (workflowExecutionContext.SubStatus != WorkflowSubStatus.Executing)
        {
            context.Fail(
                "The workflow is not in an Executing state. This alteration is designed only for restarting interrupted workflows (i.e. workflows that are in the Executing state but are not actually executing)");
            return;
        }

        if (alteration.Input != null)
            foreach (var entry in alteration.Input)
                workflowExecutionContext.Input[entry.Key] = entry.Value;

        // var workflowInstanceId = context.WorkflowExecutionContext.Id;
        // var cancellationToken = context.CancellationToken;

        // if (alteration.ExecutionMode == WorkflowExecutionMode.Asynchronous)
        // {
        //     var request = new DispatchWorkflowInstanceRequest(workflowInstanceId) { Input = alteration.Input, };
        //     await workflowDispatcher.DispatchAsync(request, cancellationToken);
        // }
        // else
        // {
        //     var options = new ResumeWorkflowRuntimeOptions
        //     {
        //         Input = alteration.Input, CancellationTokens = cancellationToken
        //     };
        //     //await workflowRuntime.ResumeWorkflowAsync(workflowInstanceId, options);
        // }

        context.Succeed();
    }
}