using Elsa.Abstractions;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Management.Contracts;

namespace Elsa.Server.Web.Endpoints.DynamicWorkflowExport.Get;

public class DynamicWorkflowExportEndpoint(IWorkflowSerializer workflowSerializer) : ElsaEndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/export-dynamic-workflow");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var workflow = new Workflow { Root = new Sequence { Activities = { new WriteLine("Hello World!") } } };
        var serializedWorkflow = workflowSerializer.Serialize(workflow);
        await SendStringAsync(serializedWorkflow, 200, "application/json", ct);
    }
}