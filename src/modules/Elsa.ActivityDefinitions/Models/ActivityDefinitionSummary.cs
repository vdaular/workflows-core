using Elsa.ActivityDefinitions.Entities;

namespace Elsa.ActivityDefinitions.Models;

public record ActivityDefinitionSummary(string Id, string DefinitionId, string Type, string? DisplayName, string? Description, int? Version, bool IsLatest, bool IsPublished, DateTimeOffset CreatedAt)
{
    public static ActivityDefinitionSummary FromDefinition(ActivityDefinition definition) => new(
        definition.Id,
        definition.DefinitionId,
        definition.Type,
        definition.DisplayName,
        definition.Description,
        definition.Version,
        definition.IsLatest,
        definition.IsPublished,
        definition.CreatedAt
    );
}