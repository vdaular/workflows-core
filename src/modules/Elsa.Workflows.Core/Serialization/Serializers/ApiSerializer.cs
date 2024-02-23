using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Elsa.Common.Serialization;
using Elsa.Workflows.Contracts;
using Elsa.Workflows.Serialization.Converters;

namespace Elsa.Workflows.Serialization.Serializers;

/// <inheritdoc cref="IApiSerializer" />
public class ApiSerializer : ConfigurableSerializer, IApiSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiSerializer"/> class.
    /// </summary>
    public ApiSerializer(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <inheritdoc />
    [RequiresUnreferencedCode("The type of the model must be known at compile time.")]
    public string Serialize(object model)
    {
        var options = CreateOptions();
        options.Converters.Add(new JsonIgnoreCompositeRootConverterFactory()); // Write-only converter.
        return JsonSerializer.Serialize(model, options);
    }

    /// <inheritdoc />
    [RequiresUnreferencedCode("The type of the model must be known at compile time.")]
    public object Deserialize(string serializedModel) => Deserialize<object>(serializedModel);

    /// <inheritdoc />
    [RequiresUnreferencedCode("The type of the model must be known at compile time.")]
    public T Deserialize<T>(string serializedModel)
    {
        var options = CreateOptions();
        return JsonSerializer.Deserialize<T>(serializedModel, options)!;
    }

    /// <inheritdoc />
    protected override void Configure(JsonSerializerOptions options)
    {
        options.PropertyNameCaseInsensitive = true;
    }

    /// <inheritdoc />
    protected override void AddConverters(JsonSerializerOptions options)
    {
        options.Converters.Add(CreateInstance<TypeJsonConverter>());
    }

    [RequiresUnreferencedCode("The type of the model must be known at compile time.")]
    JsonSerializerOptions IApiSerializer.CreateOptions() => base.CreateOptions();

    [RequiresUnreferencedCode("The type of the model must be known at compile time.")]
    JsonSerializerOptions IApiSerializer.ApplyOptions(JsonSerializerOptions options)
    {
        Apply(options);
        return options;
    }
}