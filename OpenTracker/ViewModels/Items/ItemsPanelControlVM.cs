using Avalonia.Layout;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Settings;
using OpenTracker.ViewModels.Items.Large;
using OpenTracker.ViewModels.Items.Small;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Items
{
    /// <summary>
    /// This is the ViewModel class for the Items panel control.
    /// </summary>
    public class ItemsPanelControlVM : ViewModelBase
    {
        public double Scale =>
            AppSettings.Instance.Layout.UIScale;
        public bool ATItemsVisible =>
            Mode.Instance.SmallKeyShuffle;
        public Orientation Orientation =>
            AppSettings.Instance.Layout.CurrentLayoutOrientation;
        public bool HorizontalOrientation =>
            Orientation == Orientation.Horizontal;
        public ModeSettingsVM ModeSettings { get; }

        public ObservableCollection<LargeItemVMBase> Items { get; } =
            new ObservableCollection<LargeItemVMBase>();
        public ObservableCollection<SmallItemVMBase> HCItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> ATItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> EPItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> DPItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> ToHItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> PoDItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> SPItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> SWItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> TTItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> IPItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> MMItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> TRItems { get; } =
            new ObservableCollection<SmallItemVMBase>();
        public ObservableCollection<SmallItemVMBase> GTItems { get; } =
            new ObservableCollection<SmallItemVMBase>();

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemsPanelControlVM()
        {
            ModeSettings = new ModeSettingsVM();

            LargeItemControlVMFactory.GetLargeItemControlVMs(Items);
            SmallItemVMFactory.GetSmallItemControlVMs(LocationID.HyruleCastle, HCItems);
            SmallItemVMFactory.GetSmallItemControlVMs(LocationID.AgahnimTower, ATItems);
            SmallItemVMFactory.GetSmallItemControlVMs(LocationID.EasternPalace, EPItems);
            SmallItemVMFactory.GetSmallItemControlVMs(LocationID.DesertPalace, DPItems);
            SmallItemVMFactory.GetSmallItemControlVMs(LocationID.TowerOfHera, ToHItems);
            SmallItemVMFactory.GetSmallItemControlVMs(LocationID.PalaceOfDarkness, PoDItems);
            SmallItemVMFactory.GetSmallItemControlVMs(LocationID.SwampPalace, SPItems);
            SmallItemVMFactory.GetSmallItemControlVMs(LocationID.SkullWoods, SWItems);
            SmallItemVMFactory.GetSmallItemControlVMs(LocationID.ThievesTown, TTItems);
            SmallItemVMFactory.GetSmallItemControlVMs(LocationID.IcePalace, IPItems);
            SmallItemVMFactory.GetSmallItemControlVMs(LocationID.MiseryMire, MMItems);
            SmallItemVMFactory.GetSmallItemControlVMs(LocationID.TurtleRock, TRItems);
            SmallItemVMFactory.GetSmallItemControlVMs(LocationID.GanonsTower, GTItems);

            PropertyChanged += OnPropertyChanged;
            Mode.Instance.PropertyChanged += OnModeChanged;
            AppSettings.Instance.Layout.PropertyChanged += OnLayoutChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on this class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Orientation))
            {
                this.RaisePropertyChanged(nameof(HorizontalOrientation));
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Mode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.WorldState) ||
                e.PropertyName == nameof(Mode.DungeonItemShuffle))
            {
                this.RaisePropertyChanged(nameof(ATItemsVisible));
            }
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
        private void OnLayoutChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LayoutSettings.CurrentLayoutOrientation))
            {
                this.RaisePropertyChanged(nameof(Orientation));
            }

            if (e.PropertyName == nameof(LayoutSettings.UIScale))
            {
                this.RaisePropertyChanged(nameof(Scale));
            }
        }
    }
}
