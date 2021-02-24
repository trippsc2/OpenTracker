using OpenTracker.Models.Modes;
using OpenTracker.Models.SaveLoad;
using System.ComponentModel;

namespace OpenTracker.Models.BossPlacements
{
    /// <summary>
    /// This is the class for a boss placement.
    /// </summary>
    public class BossPlacement : IBossPlacement
    {
        private readonly IMode _mode;

        public BossType DefaultBoss { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private BossType? _boss;
        public BossType? Boss
        {
            get => _boss;
            set
            {
                if (_boss != value)
                {
                    _boss = value;
                    OnPropertyChanged(nameof(Boss));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
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
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Returns the current boss type.
        /// </summary>
        /// <returns>
        /// The current boss type for this boss placement.
        /// </returns>
        public BossType? GetCurrentBoss()
        {
            if (_mode.BossShuffle)
            {
                return Boss;
            }

            return DefaultBoss;
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
        public void Load(BossPlacementSaveData saveData)
        {
            Boss = saveData.Boss;
        }
    }
}
