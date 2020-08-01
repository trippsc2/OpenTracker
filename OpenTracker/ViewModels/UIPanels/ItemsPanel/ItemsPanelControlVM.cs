using Avalonia.Layout;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Settings;
using OpenTracker.ViewModels.UIPanels.ItemsPanel.LargeItems;
using OpenTracker.ViewModels.UIPanels.ItemsPanel.SmallItems;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OpenTracker.ViewModels.UIPanels.ItemsPanel
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
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.HyruleCastle, this, HCItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.AgahnimTower, this, ATItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.EasternPalace, this, EPItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.DesertPalace, this, DPItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.TowerOfHera, this, ToHItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.PalaceOfDarkness, this, PoDItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.SwampPalace, this, SPItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.SkullWoods, this, SWItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.ThievesTown, this, TTItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.IcePalace, this, IPItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.MiseryMire, this, MMItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.TurtleRock, this, TRItems);
            SmallItemVMFactory.GetSmallItemControlVMs(
                LocationID.GanonsTower, this, GTItems);

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
