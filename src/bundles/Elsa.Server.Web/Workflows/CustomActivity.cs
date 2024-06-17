using Elsa.Workflows;

namespace Elsa.Server.Web.Workflows
{
    public class CustomActivity : CodeActivity
    {
        public CustomActivity()
        {

        }

        protected override async ValueTask ExecuteAsync(ActivityExecutionContext context)
        {
            Console.WriteLine($"executed at {DateTime.UtcNow}");
        }
    }
}
