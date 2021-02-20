using OpenTracker.Models.BossPlacements;
using OpenTracker.Utils;
using System;

namespace OpenTracker.ViewModels.BossSelect
{
    /// <summary>
    /// This is the ViewModel for the boss select button control.
    /// </summary>
    public class BossSelectButtonVM : ViewModelBase
    {
        private readonly IBossPlacement _bossPlacement;

        public BossType? Boss { get; }
        public string ImageSource =>
            Boss.HasValue ? 
            "avares://OpenTracker/Assets/Images/Bosses/" +
            $"{Boss.ToString().ToLowerInvariant()}1.png" :
            "avares://OpenTracker/Assets/Images/Bosses/" +
            $"{_bossPlacement.DefaultBoss.ToString().ToLowerInvariant()}0.png";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="boss">
        /// The boss to be represented by this button.
        /// </param>
        /// <param name="bossPlacement">
        /// The boss placement to be manipulated.
        /// </param>
        public BossSelectButtonVM(BossType? boss, IBossPlacement bossPlacement)
        {
            Boss = boss;
            _bossPlacement = bossPlacement ??
                throw new ArgumentNullException(nameof(bossPlacement));
        }
    }
}
