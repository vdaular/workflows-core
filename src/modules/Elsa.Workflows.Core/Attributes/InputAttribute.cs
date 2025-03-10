namespace Elsa.Workflows.Attributes;

/// <summary>
/// Specifies various metadata about an activity's input property.
/// This metadata can be used by visual designers to control various aspects of the input editor control.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class InputAttribute : Attribute
{
    /// <summary>
    /// The technical name to use for the input property.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// A hint to workflow tooling what input control to use. 
    /// </summary>
    public string? UIHint { get; set; }

    /// <summary>
    /// The user-friendly name of the activity property.
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// A brief description about this property for workflow tooling to use when displaying activity editors.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// A category to group this property with.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// A value representing options specific to a given UI hint.
    /// </summary>
    public object? Options { get; set; }

    /// <summary>
    /// A value to order this property by. Properties are displayed in ascending order (lower appears before higher).
    /// </summary>
    public float Order { get; set; }

    /// <summary>
    /// The default value to set.
    /// </summary>
    public object? DefaultValue { get; set; }

    /// <summary>
    /// A type that implements <see cref="IActivityPropertyDefaultValueProvider"/> and provides a default value. When specified, the <see cref="DefaultValue"/> will be ignored.
    /// </summary>
    public Type? DefaultValueProvider { get; set; }

    /// <summary>
    /// The syntax to use by default when evaluating the value. Only used when the property definition doesn't have a syntax specified. 
    /// </summary>
    public string? DefaultSyntax { get; set; }

    /// <summary>
    /// The syntax to use by default when evaluating the value. Only used when the property definition doesn't have a syntax specified. 
    /// </summary>
    public string[]? SupportedSyntaxes { get; set; }

    /// <summary>
    /// A value indicating whether this property should be displayed as read-only.
    /// </summary>
    public bool IsReadOnly { get; set; }

    /// <summary>
    /// A value indicating whether this property should be visible.
    /// </summary>
    public bool IsBrowsable { get; set; } = true;

    /// <summary>
    /// True if the activity invoker should evaluate the expression, false otherwise.
    /// When set to false, it is up to the activity itself to evaluate its input before using it. 
    /// </summary>
    public bool AutoEvaluate { get; set; } = true;

    /// <summary>
    /// A value indicating whether this input can be serialized as part of the workflow instance,
    /// </summary>
    public bool IsSerializable { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether this input can contain secrets.
    /// When set to true, the input will be treated as a secret and will be encrypted, masked or otherwise protected, depending on the configured policy.
    /// </summary>
    public bool CanContainSecrets { get; set; }

    /// <summary>
    /// A <see cref="IPropertyUIHandler"/> type that can be used to customize the UI for this property.
    /// </summary>
    public Type? UIHandler { get; set; }

    /// <summary>
    /// A set of <see cref="IPropertyUIHandler"/> types that can be used to customize the UI for this property.
    /// </summary>
    public Type[]? UIHandlers { get; set; }
}