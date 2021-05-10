using OpenTracker.Models.BossPlacements;

namespace OpenTracker.Models.UndoRedo.Boss
{
    /// <summary>
    /// This class contains the <see cref="IUndoable"/> action to change the <see cref="BossType"/> of a
    /// <see cref="IBossPlacement"/>.
    /// </summary>
    public class ChangeBoss : IChangeBoss
    {
        private readonly IBossPlacement _bossPlacement;
        private readonly BossType? _newValue;

        private BossType? _previousValue;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bossPlacement">
        ///     The <see cref="IBossPlacement"/>.
        /// </param>
        /// <param name="newValue">
        ///     The new nullable <see cref="BossType"/> value.
        /// </param>
        public ChangeBoss(IBossPlacement bossPlacement, BossType? newValue)
        {
            _bossPlacement = bossPlacement;
            _newValue = newValue;
        }

        public bool CanExecute()
        {
            return true;
        }

        public void ExecuteDo()
        {
            _previousValue = _bossPlacement.Boss;
            _bossPlacement.Boss = _newValue;
        }

        public void ExecuteUndo()
        {
            _bossPlacement.Boss = _previousValue;
        }
    }
}
