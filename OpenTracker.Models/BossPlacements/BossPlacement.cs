using OpenTracker.Models.Modes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Boss;
using ReactiveUI;

namespace OpenTracker.Models.BossPlacements
{
    /// <summary>
    /// This class contains boss placement data.
    /// </summary>
    public class BossPlacement : ReactiveObject, IBossPlacement
    {
        private readonly IMode _mode;
        private readonly IUndoRedoManager _undoRedoManager;

        private readonly IChangeBoss.Factory _changeBossFactory;

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
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="changeBossFactory">
        /// An Autofac factory for creating change boss undoable actions.
        /// </param>
        /// <param name="defaultBoss">
        /// The default boss type for the boss placement.
        /// </param>
        public BossPlacement(
            IMode mode, IUndoRedoManager undoRedoManager, IChangeBoss.Factory changeBossFactory, BossType defaultBoss)
        {
            _mode = mode;

            DefaultBoss = defaultBoss;
            _undoRedoManager = undoRedoManager;
            _changeBossFactory = changeBossFactory;

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
        /// Creates an undoable action to change the current boss and sends it to the undo/redo manager.
        /// </summary>
        /// <param name="boss">
        /// The new nullable boss type.
        /// </param>
        public void ChangeBoss(BossType? boss)
        {
            _undoRedoManager.NewAction(_changeBossFactory(this, boss));
        }

        /// <summary>
        /// Resets the boss placement to its starting values.
        /// </summary>
        public void Reset()
        {
            if (DefaultBoss == BossType.Aga)
            {
                Boss = BossType.Aga;
                return;
            }

            Boss = null;
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
