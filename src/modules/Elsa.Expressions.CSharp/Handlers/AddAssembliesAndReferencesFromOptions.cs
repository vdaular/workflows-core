using Elsa.Expressions.CSharp.Notifications;
using Elsa.Expressions.CSharp.Options;
using Elsa.Mediator.Contracts;
using JetBrains.Annotations;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.Options;

namespace Elsa.Expressions.CSharp.Handlers;

/// <summary>
/// This handler adds assemblies and namespaces from the <see cref="CSharpOptions"/> to the <see cref="ScriptOptions"/>.
/// </summary>
[UsedImplicitly]
public class AddAssembliesAndReferencesFromOptions : INotificationHandler<EvaluatingCSharp>
{
    private readonly CSharpOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddAssembliesAndReferencesFromOptions"/> class.
    /// </summary>
    public AddAssembliesAndReferencesFromOptions(IOptions<CSharpOptions> options)
    {
        _options = options.Value;
    }

    /// <inheritdoc />
    public Task HandleAsync(EvaluatingCSharp notification, CancellationToken cancellationToken)
    {
        _options.Assemblies.RemoveWhere(x => x is null);
        _options.Namespaces.RemoveWhere(x => x is null);
        notification.ConfigureScriptOptions(scriptOptions => scriptOptions
            .AddReferences(_options.Assemblies)
            .AddImports(_options.Namespaces));

        return Task.CompletedTask;
    }
}
