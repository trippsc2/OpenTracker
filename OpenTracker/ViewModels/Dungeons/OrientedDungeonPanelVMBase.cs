using System.Collections.Generic;
using System.ComponentModel;
using Avalonia.Threading;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.Dungeons
{
    /// <summary>
    /// This is the ViewModel for the small item panel control.
    /// </summary>
    public abstract class OrientedDungeonPanelVMBase : ViewModelBase, IOrientedDungeonPanelVMBase
    {
        private readonly IMode _mode;

        public bool ATItemsVisible => _mode.SmallKeyShuffle;

        public List<IDungeonItemVM> HCItems { get; }
        public List<IDungeonItemVM> ATItems { get; }
        public List<IDungeonItemVM> EPItems { get; }
        public List<IDungeonItemVM> DPItems { get; }
        public List<IDungeonItemVM> ToHItems { get; }
        public List<IDungeonItemVM> PoDItems { get; }
        public List<IDungeonItemVM> SPItems { get; }
        public List<IDungeonItemVM> SWItems { get; }
        public List<IDungeonItemVM> TTItems { get; }
        public List<IDungeonItemVM> IPItems { get; }
        public List<IDungeonItemVM> MMItems { get; }
        public List<IDungeonItemVM> TRItems { get; }
        public List<IDungeonItemVM> GTItems { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings data.
        /// </param>
        /// <param name="dungeonItems">
        /// The dungeon items control dictionary.
        /// </param>
        protected OrientedDungeonPanelVMBase(IMode mode, IDungeonVMDictionary dungeonItems)
        {
            _mode = mode;

            HCItems = dungeonItems[LocationID.HyruleCastle];
            ATItems = dungeonItems[LocationID.AgahnimTower];
            EPItems = dungeonItems[LocationID.EasternPalace];
            DPItems = dungeonItems[LocationID.DesertPalace];
            ToHItems = dungeonItems[LocationID.TowerOfHera];
            PoDItems = dungeonItems[LocationID.PalaceOfDarkness];
            SPItems = dungeonItems[LocationID.SwampPalace];
            SWItems = dungeonItems[LocationID.SkullWoods];
            TTItems = dungeonItems[LocationID.ThievesTown];
            IPItems = dungeonItems[LocationID.IcePalace];
            MMItems = dungeonItems[LocationID.MiseryMire];
            TRItems = dungeonItems[LocationID.TurtleRock];
            GTItems = dungeonItems[LocationID.GanonsTower];

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
        private async void OnModeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMode.SmallKeyShuffle))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(ATItemsVisible)));
            }
        }
    }
}
