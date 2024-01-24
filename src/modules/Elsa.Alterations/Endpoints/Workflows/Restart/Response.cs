using Elsa.Alterations.Core.Results;

namespace Elsa.Alterations.Endpoints.Workflows.Restart;

/// <summary>
/// The response from the <see cref="Retry"/> endpoint.
/// </summary>
public record Response(ICollection<RunAlterationsResult> Results);