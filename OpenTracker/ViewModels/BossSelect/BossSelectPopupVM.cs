using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Threading;

namespace OpenTracker.ViewModels.BossSelect
{
    /// <summary>
    /// This class contains the boss select popup control ViewModel data.
    /// </summary>
    public class BossSelectPopupVM : ViewModelBase, IBossSelectPopupVM
    {
        private readonly ILayoutSettings _layoutSettings;
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly IBossPlacement _bossPlacement;

        public double Scale =>
            _layoutSettings.UIScale;
        public List<IBossSelectButtonVM> Buttons { get; }

        private bool _popupOpen;
        public bool PopupOpen
        {
            get => _popupOpen;
            set => this.RaiseAndSetIfChanged(ref _popupOpen, value);
        }

        public ReactiveCommand<BossType?, Unit> ChangeBossCommand { get; }

        private readonly ObservableAsPropertyHelper<bool> _isChangingBoss;
        public bool IsChangingBoss =>
            _isChangingBoss.Value;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="layoutSettings">
        /// The layout settings data.
        /// </param>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="undoableFactory">
        /// A factory for creating undoable actions.
        /// </param>
        /// <param name="factory">
        /// A factory for creating boss select controls.
        /// </param>
        /// <param name="bossPlacement">
        /// The boss placement to be manipulated.
        /// </param>
        public BossSelectPopupVM(
            ILayoutSettings layoutSettings, IUndoRedoManager undoRedoManager,
            IUndoableFactory undoableFactory, IBossSelectFactory factory,
            IBossPlacement bossPlacement)
        {
            _layoutSettings = layoutSettings;
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _bossPlacement = bossPlacement;

            Buttons = factory.GetBossSelectButtonVMs(_bossPlacement);

            ChangeBossCommand = ReactiveCommand.CreateFromTask<BossType?>(ChangeBoss);
            ChangeBossCommand.IsExecuting.ToProperty(
                this, x => x.IsChangingBoss, out _isChangingBoss);

            _layoutSettings.PropertyChanged += OnAppSettingsChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the LayoutSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnAppSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ILayoutSettings.UIScale))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Scale)));
            }
        }

        /// <summary>
        /// Changes the boss of the section to the specified boss.
        /// </summary>
        /// <param name="boss">
        /// The boss to be set.
        /// </param>
        private async Task ChangeBoss(BossType? boss)
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                _undoRedoManager.NewAction(_undoableFactory.GetChangeBoss(_bossPlacement, boss));
                PopupOpen = false;
            });
        }
    }
}
