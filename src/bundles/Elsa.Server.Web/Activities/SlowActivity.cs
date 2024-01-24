using Elsa.Workflows;
using Elsa.Workflows.Attributes;

namespace Elsa.Server.Web.Activities;

[Activity("Demo", "Demo", "A slow activity that takes 10 seconds to complete. Talk about slugs!")]
public class SlowActivity : CodeActivity
{
    protected override async ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        await Task.Delay(TimeSpan.FromSeconds(10), context.CancellationToken);
    }
}