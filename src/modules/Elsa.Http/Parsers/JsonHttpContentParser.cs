using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elsa.Expressions.Helpers;
using Elsa.Http.Contexts;
using Elsa.Workflows.Serialization.Converters;

namespace Elsa.Http.Parsers;

/// <summary>
/// Reads application/json and text/json content type streams.
/// </summary>
public class JsonHttpContentParser : IHttpContentParser
{
    /// <inheritdoc />
    public int Priority => 0;

    /// <inheritdoc />
    public bool GetSupportsContentType(HttpResponseParserContext context) => context.ContentType.Contains("json", StringComparison.InvariantCultureIgnoreCase);

    /// <inheritdoc />
    public async Task<object> ReadAsync(HttpResponseParserContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        options.Converters.Add(new JsonStringEnumConverter());

        var content = context.Content;
        using var reader = new StreamReader(content, leaveOpen: true);
        var json = await reader.ReadToEndAsync();
        var returnType = context.ReturnType;
        
        if(returnType == typeof(string))
            return json;

        if (returnType == null || returnType.IsPrimitive)
            return json.ConvertTo(returnType ?? typeof(string))!;

        if (returnType != typeof(ExpandoObject) && returnType != typeof(object)) 
            return JsonSerializer.Deserialize(json, returnType, options)!;
        
        options.Converters.Add(new ExpandoObjectConverterFactory());
        return JsonSerializer.Deserialize<object>(json, options)!;
    }
}