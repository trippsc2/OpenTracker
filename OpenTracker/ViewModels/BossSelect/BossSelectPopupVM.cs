using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;

namespace OpenTracker.ViewModels.BossSelect
{
    /// <summary>
    /// This is the ViewModel class for the boss select popup control.
    /// </summary>
    public class BossSelectPopupVM : ViewModelBase
    {
        private readonly ILayoutSettings _layoutSettings;
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IBossPlacement _bossPlacement;

        public double Scale =>
            _layoutSettings.UIScale;
        public ObservableCollection<BossSelectButtonVM> Buttons { get; }

        private bool _popupOpen;
        public bool PopupOpen
        {
            get => _popupOpen;
            set => this.RaiseAndSetIfChanged(ref _popupOpen, value);
        }

        public ReactiveCommand<BossType?, Unit> ChangeBossCommand { get; }

        public BossSelectPopupVM(
            IBossPlacement bossPlacement,
            ObservableCollection<BossSelectButtonVM> buttons)
            : this(AppSettings.Instance.Layout, UndoRedoManager.Instance, bossPlacement, buttons)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bossPlacement">
        /// The boss placement to be manipulated.
        /// </param>
        /// <param name="buttons">
        /// The observable collection of boss select button control ViewModel instances.
        /// </param>
        private BossSelectPopupVM(
            ILayoutSettings layoutSettings, IUndoRedoManager undoRedoManager,
            IBossPlacement bossPlacement,
            ObservableCollection<BossSelectButtonVM> buttons)
        {
            _layoutSettings = layoutSettings;
            _undoRedoManager = undoRedoManager;
            _bossPlacement = bossPlacement;
            Buttons = buttons;

            ChangeBossCommand = ReactiveCommand.Create<BossType?>(ChangeBoss);

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
        private void OnAppSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ILayoutSettings.UIScale))
            {
                this.RaisePropertyChanged(nameof(Scale));
            }
        }

        /// <summary>
        /// Changes the boss of the section to the specified boss.
        /// </summary>
        /// <param name="boss">
        /// The boss to be set.
        /// </param>
        private void ChangeBoss(BossType? boss)
        {
            _undoRedoManager.Execute(new ChangeBoss(_bossPlacement, boss));
            PopupOpen = false;
        }
    }
}
