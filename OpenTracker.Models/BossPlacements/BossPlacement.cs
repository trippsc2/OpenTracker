using OpenTracker.Models.Modes;
using OpenTracker.Models.SaveLoad;
using ReactiveUI;

namespace OpenTracker.Models.BossPlacements
{
    /// <summary>
    /// This class contains boss placement data.
    /// </summary>
    public class BossPlacement : ReactiveObject, IBossPlacement
    {
        private readonly IMode _mode;

        public BossType DefaultBoss { get; }

        private BossType? _boss;
        public BossType? Boss
        {
            get => _boss;
            set => this.RaiseAndSetIfChanged(ref _boss, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="defaultBoss">
        /// The default boss type for the boss placement.
        /// </param>
        public BossPlacement(IMode mode, BossType defaultBoss)
        {
            _mode = mode;

            DefaultBoss = defaultBoss;

            if (DefaultBoss == BossType.Aga)
            {
                Boss = BossType.Aga;
            }
        }

        /// <summary>
        /// Returns the current boss type.
        /// </summary>
        /// <returns>
        /// The current boss type for this boss placement.
        /// </returns>
        public BossType? GetCurrentBoss()
        {
            return _mode.BossShuffle ? Boss : DefaultBoss;
        }

        /// <summary>
        /// Resets the boss placement to its starting values.
        /// </summary>
        public void Reset()
        {
            if (DefaultBoss != BossType.Aga)
            {
                Boss = null;
            }
        }

        /// <summary>
        /// Returns a new boss placement save data instance for this boss placement.
        /// </summary>
        /// <returns>
        /// A new boss placement save data instance.
        /// </returns>
        public BossPlacementSaveData Save()
        {
            return new BossPlacementSaveData()
            {
                Boss = Boss
            };
        }

        /// <summary>
        /// Loads boss placement save data.
        /// </summary>
        public void Load(BossPlacementSaveData? saveData)
        {
            if (saveData == null)
            {
                return;
            }
            
            Boss = saveData.Boss;
        }
    }
}
