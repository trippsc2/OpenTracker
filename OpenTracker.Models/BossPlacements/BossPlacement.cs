using OpenTracker.Models.Enums;
using System.ComponentModel;

namespace OpenTracker.Models.BossPlacements
{
    /// <summary>
    /// This is the class for the boss placement.
    /// </summary>
    public class BossPlacement : IBossPlacement
    {
        private readonly BossType _defaultBoss;

        public event PropertyChangedEventHandler PropertyChanged;

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
        /// <param name="game">
        /// The game data parent class.
        /// </param>
        /// <param name="iD">
        /// The boss placement ID.
        /// </param>
        public BossPlacement(BossType defaultBoss)
        {
            if (defaultBoss == BossType.Aga)
            {
                Boss = BossType.Aga;
            }

            _defaultBoss = defaultBoss;
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
        /// The boss type.
        /// </returns>
        public BossType? GetCurrentBoss()
        {
            if (Mode.Instance.BossShuffle)
            {
                return Boss;
            }

            return _defaultBoss;
        }

        /// <summary>
        /// Resets the boss placement to its starting values.
        /// </summary>
        public void Reset()
        {
            if (_defaultBoss != BossType.Aga)
            {
                Boss = null;
            }
        }
    }
}
