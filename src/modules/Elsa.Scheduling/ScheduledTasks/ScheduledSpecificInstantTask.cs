using System.Timers;
using Elsa.Common.Contracts;
using Elsa.Mediator.Contracts;
using Elsa.Scheduling.Commands;
using Elsa.Scheduling.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Timer = System.Timers.Timer;

namespace Elsa.Scheduling.ScheduledTasks;

/// <summary>
/// A task that is scheduled to execute at a specific instant.
/// </summary>
public class ScheduledSpecificInstantTask : IScheduledTask
{
    private readonly ITask _task;
    private readonly ISystemClock _systemClock;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly DateTimeOffset _startAt;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private Timer? _timer;

    /// <summary>
    /// Initializes a new instance of <see cref="ScheduledSpecificInstantTask"/>.
    /// </summary>
    public ScheduledSpecificInstantTask(ITask task, DateTimeOffset startAt, ISystemClock systemClock, IServiceScopeFactory scopeFactory)
    {
        _task = task;
        _systemClock = systemClock;
        _scopeFactory = scopeFactory;
        _startAt = startAt;
        _cancellationTokenSource = new CancellationTokenSource();

        Schedule();
    }

    /// <inheritdoc />
    public void Cancel() => _timer?.Dispose();

    private void Schedule()
    {
        if (_timer != null)
        {
            _timer.Elapsed -= OnElapsed;
            _timer.Dispose();
        }

        var now = _systemClock.UtcNow;
        var delay = _startAt - now;

        if (delay.Milliseconds <= 0)
            delay = TimeSpan.FromMilliseconds(1);

        _timer = new Timer(delay.TotalMilliseconds)
        {
            Enabled = true,
            AutoReset = false
        };

        _timer.Elapsed += OnElapsed;
    }

    private async void OnElapsed(object? sender, ElapsedEventArgs e)
    {
        using var scope = _scopeFactory.CreateScope();
        var commandSender = scope.ServiceProvider.GetRequiredService<ICommandSender>();

        var cancellationToken = _cancellationTokenSource.Token;
        if (!cancellationToken.IsCancellationRequested)
            await commandSender.SendAsync(new RunScheduledTask(_task), cancellationToken);
    }
}