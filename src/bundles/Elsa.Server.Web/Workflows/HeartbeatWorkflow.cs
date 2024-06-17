using Elsa.Scheduling.Activities;
using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Contracts;
using JetBrains.Annotations;

namespace Elsa.Server.Web.Workflows;

[PublicAPI]
public class HeartbeatWorkflow : WorkflowBase
{


    public HeartbeatWorkflow()
    {

    }

    protected override void Build(IWorkflowBuilder builder)
    {
        builder.Root = new Sequence
        {
            Activities =
            {
                new Cron
                {
                    CronExpression = new("*/5 * * * * *"),
                    CanStartWorkflow = true
                },
                new CustomActivity()
            }
        };
    }
}