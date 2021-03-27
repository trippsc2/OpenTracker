using OpenTracker.Models.Modes;
using OpenTracker.Utils;

namespace OpenTracker.Models.Dungeons.AccessibilityProvider
{
    /// <summary>
    /// This class contains the logic for updating the dungeon accessibility.
    /// </summary>
    public class DungeonAccessibilityProvider
    {
        private readonly IMode _mode;
        private readonly ConstrainedTaskScheduler _taskScheduler;
        
        private readonly IDungeon _dungeon;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings data.
        /// </param>
        /// <param name="taskScheduler">
        /// The task scheduler for all accessibility tasks.
        /// </param>
        /// <param name="dungeon">
        /// The dungeon data.
        /// </param>
        public DungeonAccessibilityProvider(IMode mode, ConstrainedTaskScheduler taskScheduler, IDungeon dungeon)
        {
            _mode = mode;
            _taskScheduler = taskScheduler;

            _dungeon = dungeon;
        }
        
        
    }
}