using Elsa.Alterations.Core.Abstractions;
using Elsa.Workflows.Models;

namespace Elsa.Alterations.AlterationTypes;

/// <summary>
/// Schedules an activity for execution.
/// </summary>
public class RestartWorkflow : AlterationBase
{
    /// <summary>
    /// Gets or sets the execution mode for the workflow.
    /// </summary>
    /// <remarks>
    /// The execution mode determines how the workflow will be executed. Defaults to Asynchronous.
    /// </remarks>
    public WorkflowExecutionMode ExecutionMode { get; set; } = WorkflowExecutionMode.Asynchronous;
    
    /// <summary>
    /// Gets or sets a dictionary representing the input properties.
    /// </summary>
    public IDictionary<string, object>? Input { get; set; }
}