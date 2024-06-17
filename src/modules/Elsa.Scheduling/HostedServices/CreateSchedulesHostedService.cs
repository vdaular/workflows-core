using Elsa.Scheduling.Activities;
using Elsa.Scheduling.Contracts;
using Elsa.Workflows.Helpers;
using Elsa.Workflows.Runtime.Contracts;
using Elsa.Workflows.Runtime.Filters;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Timer = Elsa.Scheduling.Activities.Timer;

namespace Elsa.Scheduling.HostedServices;

/// <summary>
/// Creates new schedules when using the default scheduler (which doesn't have its own persistence layer like Quartz or Hangfire).
/// </summary>
[UsedImplicitly]
public class CreateSchedulesHostedService(IServiceScopeFactory scopeFactory) : BackgroundService
{
    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Only schedule bookmarks; triggers will be processed automatically during application startup.
        using var scope = scopeFactory.CreateScope();
        var bookmarkStore = scope.ServiceProvider.GetRequiredService<IBookmarkStore>();
        var bookmarkScheduler = scope.ServiceProvider.GetRequiredService<IBookmarkScheduler>();
        var activityTypes = new[] { typeof(Cron), typeof(Timer), typeof(StartAt), typeof(Delay) };
        var activityTypeNames = activityTypes.Select(ActivityTypeNameHelper.GenerateTypeName).ToList();
        var bookmarkFilter = new BookmarkFilter { ActivityTypeNames = activityTypeNames };
        var bookmarks = (await bookmarkStore.FindManyAsync(bookmarkFilter, stoppingToken)).ToList();
        await bookmarkScheduler.ScheduleAsync(bookmarks, stoppingToken);
    }
}