using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Items.Small
{
    /// <summary>
    /// This is the ViewModel for the small item panel control.
    /// </summary>
    public class SmallItemPanelVM : ViewModelBase
    {
        public bool ATItemsVisible =>
            Mode.Instance.SmallKeyShuffle;

        public ObservableCollection<SmallItemVMBase> HCItems { get; }
        public ObservableCollection<SmallItemVMBase> ATItems { get; }
        public ObservableCollection<SmallItemVMBase> EPItems { get; }
        public ObservableCollection<SmallItemVMBase> DPItems { get; }
        public ObservableCollection<SmallItemVMBase> ToHItems { get; }
        public ObservableCollection<SmallItemVMBase> PoDItems { get; }
        public ObservableCollection<SmallItemVMBase> SPItems { get; }
        public ObservableCollection<SmallItemVMBase> SWItems { get; }
        public ObservableCollection<SmallItemVMBase> TTItems { get; }
        public ObservableCollection<SmallItemVMBase> IPItems { get; }
        public ObservableCollection<SmallItemVMBase> MMItems { get; }
        public ObservableCollection<SmallItemVMBase> TRItems { get; }
        public ObservableCollection<SmallItemVMBase> GTItems { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public SmallItemPanelVM()
        {
            HCItems = SmallItemVMFactory.GetSmallItemControlVMs(LocationID.HyruleCastle);
            ATItems = SmallItemVMFactory.GetSmallItemControlVMs(LocationID.AgahnimTower);
            EPItems = SmallItemVMFactory.GetSmallItemControlVMs(LocationID.EasternPalace);
            DPItems = SmallItemVMFactory.GetSmallItemControlVMs(LocationID.DesertPalace);
            ToHItems = SmallItemVMFactory.GetSmallItemControlVMs(LocationID.TowerOfHera);
            PoDItems = SmallItemVMFactory.GetSmallItemControlVMs(LocationID.PalaceOfDarkness);
            SPItems = SmallItemVMFactory.GetSmallItemControlVMs(LocationID.SwampPalace);
            SWItems = SmallItemVMFactory.GetSmallItemControlVMs(LocationID.SkullWoods);
            TTItems = SmallItemVMFactory.GetSmallItemControlVMs(LocationID.ThievesTown);
            IPItems = SmallItemVMFactory.GetSmallItemControlVMs(LocationID.IcePalace);
            MMItems = SmallItemVMFactory.GetSmallItemControlVMs(LocationID.MiseryMire);
            TRItems = SmallItemVMFactory.GetSmallItemControlVMs(LocationID.TurtleRock);
            GTItems = SmallItemVMFactory.GetSmallItemControlVMs(LocationID.GanonsTower);

            Mode.Instance.PropertyChanged += OnModeChanged;
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
    }
}
