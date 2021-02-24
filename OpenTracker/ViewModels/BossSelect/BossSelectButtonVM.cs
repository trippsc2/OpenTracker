using OpenTracker.Models.BossPlacements;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.BossSelect
{
    /// <summary>
    /// This is the ViewModel for the boss select button control.
    /// </summary>
    public class BossSelectButtonVM : ViewModelBase, IBossSelectButtonVM
    {
        private readonly IBossPlacement _bossPlacement;

        public BossType? Boss { get; }
        public string ImageSource =>
            Boss.HasValue ? 
            "avares://OpenTracker/Assets/Images/Bosses/" +
            $"{Boss.ToString()!.ToLowerInvariant()}1.png" :
            "avares://OpenTracker/Assets/Images/Bosses/" +
            $"{_bossPlacement.DefaultBoss.ToString().ToLowerInvariant()}0.png";

        public delegate IBossSelectButtonVM Factory(IBossPlacement bossPlacement, BossType? boss);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="boss">
        /// The boss to be represented by this button.
        /// </param>
        /// <param name="bossPlacement">
        /// The boss placement to be manipulated.
        /// </param>
        public BossSelectButtonVM(IBossPlacement bossPlacement, BossType? boss)
        {
            _bossPlacement = bossPlacement;

            Boss = boss;
        }
    }
}
