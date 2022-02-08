using System;
using System.Threading;
using System.Threading.Tasks;

namespace Automation.Core.Util {
    public static class Scheduler {
        public static void ScheduleTask(Action task, TimeSpan interval, CancellationToken cancellationToken = default) {
            Task.Run(async () => {
                while (!cancellationToken.IsCancellationRequested) {
                    var delayTask = Task.Delay(interval, cancellationToken);
                    task();
                    await delayTask;
                }
            }, cancellationToken);
        }
    }
}