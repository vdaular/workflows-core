using System.Diagnostics.CodeAnalysis;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Contracts;
using Elsa.Workflows.Management.Contracts;
using Elsa.Workflows.Management.Mappers;
using Elsa.Workflows.Management.Models;
using Elsa.Workflows.Serialization.Converters;

namespace Elsa.Workflows.Management.Services;

/// <summary>
/// A serializer that parses JSON.
/// </summary>
public class WorkflowSerializer(IApiSerializer apiSerializer, WorkflowDefinitionMapper workflowDefinitionMapper) : IWorkflowSerializer
{
    /// <inheritdoc />
    [RequiresUnreferencedCode("The type of the model must be known at compile time.")]
    public string Serialize(Workflow workflow)
    {
        var model = workflowDefinitionMapper.Map(workflow);
        return apiSerializer.Serialize(model);
    }

    /// <inheritdoc />
    [RequiresUnreferencedCode("The type of the model must be known at compile time.")]
    public Workflow Deserialize(string serializedWorkflow)
    {
        var model = apiSerializer.Deserialize<WorkflowDefinitionModel>(serializedWorkflow);
        return workflowDefinitionMapper.Map(model);
    }
}