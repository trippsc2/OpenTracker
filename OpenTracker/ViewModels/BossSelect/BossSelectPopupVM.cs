using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;
using System;
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
        private readonly IBossPlacement _bossPlacement;

        public double Scale =>
            AppSettings.Instance.Layout.UIScale;

        public ObservableCollection<BossSelectButtonVM> Buttons { get; }

        private bool _popupOpen;
        public bool PopupOpen
        {
            get => _popupOpen;
            set => this.RaiseAndSetIfChanged(ref _popupOpen, value);
        }

        public ReactiveCommand<BossType?, Unit> ChangeBossCommand { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bossPlacement">
        /// The marking to be represented.
        /// </param>
        /// <param name="buttons">
        /// The observable collection of boss select button control ViewModel instances.
        /// </param>
        public BossSelectPopupVM(
            IBossPlacement bossPlacement, ObservableCollection<BossSelectButtonVM> buttons)
        {
            _bossPlacement = bossPlacement ??
                throw new ArgumentNullException(nameof(bossPlacement));
            Buttons = buttons ?? throw new ArgumentNullException(nameof(buttons));
            ChangeBossCommand = ReactiveCommand.Create<BossType?>(ChangeBoss);

            AppSettings.Instance.Layout.PropertyChanged += OnAppSettingsChanged;
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
            if (e.PropertyName == nameof(LayoutSettings.UIScale))
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
            UndoRedoManager.Instance.Execute(new ChangeBoss(_bossPlacement, boss));
            PopupOpen = false;
        }
    }
}
