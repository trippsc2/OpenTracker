using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Utils;
using ReactiveUI;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Items.Small
{
    /// <summary>
    /// This is the ViewModel for the small item panel control.
    /// </summary>
    public abstract class SmallItemPanelVM : ViewModelBase, ISmallItemPanelVM
    {
        private readonly IMode _mode;

        public bool ATItemsVisible =>
            _mode.SmallKeyShuffle;

        public List<ISmallItemVMBase> HCItems { get; }
        public List<ISmallItemVMBase> ATItems { get; }
        public List<ISmallItemVMBase> EPItems { get; }
        public List<ISmallItemVMBase> DPItems { get; }
        public List<ISmallItemVMBase> ToHItems { get; }
        public List<ISmallItemVMBase> PoDItems { get; }
        public List<ISmallItemVMBase> SPItems { get; }
        public List<ISmallItemVMBase> SWItems { get; }
        public List<ISmallItemVMBase> TTItems { get; }
        public List<ISmallItemVMBase> IPItems { get; }
        public List<ISmallItemVMBase> MMItems { get; }
        public List<ISmallItemVMBase> TRItems { get; }
        public List<ISmallItemVMBase> GTItems { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public SmallItemPanelVM(IMode mode, ISmallItemVMFactory factory)
        {
            _mode = mode;

            HCItems = factory.GetSmallItemControlVMs(LocationID.HyruleCastle);
            ATItems = factory.GetSmallItemControlVMs(LocationID.AgahnimTower);
            EPItems = factory.GetSmallItemControlVMs(LocationID.EasternPalace);
            DPItems = factory.GetSmallItemControlVMs(LocationID.DesertPalace);
            ToHItems = factory.GetSmallItemControlVMs(LocationID.TowerOfHera);
            PoDItems = factory.GetSmallItemControlVMs(LocationID.PalaceOfDarkness);
            SPItems = factory.GetSmallItemControlVMs(LocationID.SwampPalace);
            SWItems = factory.GetSmallItemControlVMs(LocationID.SkullWoods);
            TTItems = factory.GetSmallItemControlVMs(LocationID.ThievesTown);
            IPItems = factory.GetSmallItemControlVMs(LocationID.IcePalace);
            MMItems = factory.GetSmallItemControlVMs(LocationID.MiseryMire);
            TRItems = factory.GetSmallItemControlVMs(LocationID.TurtleRock);
            GTItems = factory.GetSmallItemControlVMs(LocationID.GanonsTower);

            _mode.PropertyChanged += OnModeChanged;
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
            if (e.PropertyName == nameof(IMode.SmallKeyShuffle))
            {
                this.RaisePropertyChanged(nameof(ATItemsVisible));
            }
        }
    }
}
