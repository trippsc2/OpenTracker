using OpenTracker.Utils;

namespace OpenTracker.Models.Dungeons.AccessibilityProvider
{
    public class DungeonAccessibilityProvider
    {
        private readonly ConstrainedTaskScheduler _taskScheduler;

        public DungeonAccessibilityProvider(ConstrainedTaskScheduler taskScheduler)
        {
            _taskScheduler = taskScheduler;
        }
    }
}