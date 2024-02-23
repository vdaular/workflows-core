using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Elsa.Workflows.Contracts;

/// <summary>
/// Provides serializer options and services for model serialization suitable for APIs.
/// </summary>
public interface IApiSerializer
{
    /// <summary>
    /// Serializes the specified model.
    /// </summary>
    /// <param name="model">The model to serialize.</param>
    /// <returns>The serialized model.</returns>
    [RequiresUnreferencedCode("The type of the model must be known at compile time.")]
    string Serialize(object model);
    
    /// <summary>
    /// Deserializes the specified serialized model.
    /// </summary>
    /// <param name="serializedModel">The serialized model.</param>
    /// <returns>The deserialized model.</returns>
    [RequiresUnreferencedCode("The type of the model must be known at compile time.")]
    object Deserialize(string serializedModel);
    
    /// <summary>
    /// Deserializes the specified serialized model.
    /// </summary>
    /// <param name="serializedModel">The serialized model.</param>
    /// <returns>The deserialized model.</returns>
    [RequiresUnreferencedCode("The type of the model must be known at compile time.")]
    T Deserialize<T>(string serializedModel);
    
    /// <summary>
    /// Returns the serializer options.
    /// </summary>
    /// <returns>The serializer options.</returns>
    [RequiresUnreferencedCode("The type of the model must be known at compile time.")]
    JsonSerializerOptions CreateOptions();

    /// <summary>
    /// Applies the serializer options.
    /// </summary>
    /// <param name="options">The serializer options to apply to.</param>
    /// <returns>The updated serializer options.</returns>
    [RequiresUnreferencedCode("The type of the model must be known at compile time.")]
    JsonSerializerOptions ApplyOptions(JsonSerializerOptions options);
}