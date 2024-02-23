using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Elsa.Common.Serialization;
using Elsa.Workflows.Contracts;
using Elsa.Workflows.Serialization.Converters;

namespace Elsa.Workflows.Serialization.Serializers;

/// <inheritdoc cref="IActivitySerializer" />
public class JsonActivitySerializer : ConfigurableSerializer, IActivitySerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JsonActivitySerializer"/> class.
    /// </summary>
    public JsonActivitySerializer(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <inheritdoc />
    [RequiresUnreferencedCode("The type of the activity must be known at compile time.")]
    public string Serialize(IActivity activity)
    {
        var options = CreateOptions();
        options.Converters.Add(CreateInstance<JsonIgnoreCompositeRootConverterFactory>());
        return JsonSerializer.Serialize(activity, activity.GetType(), options);
    }

    /// <inheritdoc />
    [RequiresUnreferencedCode("The type of the activity must be known at compile time.")]
    public string Serialize(object value)
    {
        var options = CreateOptions();
        options.Converters.Add(CreateInstance<JsonIgnoreCompositeRootConverterFactory>());
        return JsonSerializer.Serialize(value, options);
    }

    /// <inheritdoc />
    [RequiresUnreferencedCode("The type of the activity must be known at compile time.")]
    public IActivity Deserialize(string serializedActivity) => JsonSerializer.Deserialize<IActivity>(serializedActivity, CreateOptions())!;

    /// <inheritdoc />
    [RequiresUnreferencedCode("The type of the activity must be known at compile time.")]
    public object Deserialize(string serializedValue, Type type) => JsonSerializer.Deserialize(serializedValue, type, CreateOptions())!;

    /// <inheritdoc />
    [RequiresUnreferencedCode("The type of the activity must be known at compile time.")]
    public T Deserialize<T>(string serializedValue) => JsonSerializer.Deserialize<T>(serializedValue, CreateOptions())!;

    /// <inheritdoc />
    protected override void AddConverters(JsonSerializerOptions options)
    {
        options.Converters.Add(CreateInstance<TypeJsonConverter>());
        options.Converters.Add(CreateInstance<InputJsonConverterFactory>());
    }
}