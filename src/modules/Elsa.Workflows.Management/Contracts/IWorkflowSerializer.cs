using System.Diagnostics.CodeAnalysis;
using Elsa.Workflows.Activities;

namespace Elsa.Workflows.Management.Contracts;

/// <summary>
/// Serializes and deserializes workflows.
/// </summary>
public interface IWorkflowSerializer
{
    /// <summary>
    /// Serializes the specified workflow.
    /// </summary>
    /// <param name="workflow">The workflow.</param>
    /// <returns>A string representing the serialized workflow.</returns>
    [RequiresUnreferencedCode("The type of the model must be known at compile time.")]
    string Serialize(Workflow workflow);
    
    /// <summary>
    /// Serializes the specified workflow.
    /// </summary>
    /// <param name="serializedWorkflow">The data representing the workflow.</param>
    /// <returns>A deserialized workflow.</returns>
    [RequiresUnreferencedCode("The type of the model must be known at compile time.")]
    Workflow Deserialize(string serializedWorkflow);
}