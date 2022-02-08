using System;

namespace Automation.Core {
    public static class Time {
        private const float TargetTps = 60;

        public static double GetDelta(TimeSpan span) {
            const float targetTicks = 1000 * 10000 / TargetTps;
            return span.Ticks / targetTicks;
        }
    }
}