using Elsa.Workflows.Models;

namespace Elsa.Alterations.Endpoints.Workflows.Restart;

/// <summary>
/// A request to retry one or more faulted workflow instances.
/// </summary>
public class Request
{
    /// <summary>
    /// The IDs of the workflow instances to restart.
    /// </summary>
    public ICollection<string>? WorkflowInstanceIds { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets a value indicating whether this property affects all Executing workflows.
    /// </summary>
    /// <value>
    /// <c>true</c> if this all Executing workflow should be restarted; otherwise, <c>false</c>.
    /// </value>
    public bool AffectAll { get; set; }

    /// <summary>
    /// Represents the execution mode for a workflow.
    /// The default value is <see cref="WorkflowExecutionMode.Asynchronous"/>.
    /// </summary>
    public WorkflowExecutionMode ExecutionMode { get; set; } = WorkflowExecutionMode.Asynchronous;

    /// <summary>
    /// Gets or sets the input data for the property.
    /// </summary>
    /// <value>
    /// A dictionary representing the input data. The keys are strings, and the values can be any object.
    /// </value>
    public IDictionary<string, object>? Input { get; set; }
}