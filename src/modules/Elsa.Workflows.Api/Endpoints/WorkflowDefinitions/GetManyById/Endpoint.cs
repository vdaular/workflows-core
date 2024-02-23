using Elsa.Abstractions;
using Elsa.Models;
using Elsa.Workflows.Management.Contracts;
using Elsa.Workflows.Management.Filters;
using Elsa.Workflows.Management.Mappers;
using Elsa.Workflows.Management.Models;
using JetBrains.Annotations;

namespace Elsa.Workflows.Api.Endpoints.WorkflowDefinitions.GetManyById;

[PublicAPI]
internal class GetManyById(IWorkflowDefinitionStore store, WorkflowDefinitionMapper mapper) : ElsaEndpoint<Request>
{
    public override void Configure()
    {
        Get("/workflow-definitions/many-by-id");
        ConfigurePermissions("read:workflow-definitions");
    }
    
    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var filter = new WorkflowDefinitionFilter
        {
            Ids = request.Ids
        };

        var definitions = (await store.FindManyAsync(filter, cancellationToken)).ToList();
        var models = (await mapper.MapAsync(definitions, cancellationToken)).ToList();
        var response = new ListResponse<WorkflowDefinitionModel>(models);
        await SendOkAsync(response, cancellationToken);
    }
}