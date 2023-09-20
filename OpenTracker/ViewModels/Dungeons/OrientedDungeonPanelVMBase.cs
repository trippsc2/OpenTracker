using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Utils;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.Dungeons;

/// <summary>
/// This is the ViewModel for the small item panel control.
/// </summary>
public abstract class OrientedDungeonPanelVMBase : ViewModel, IOrientedDungeonPanelVMBase
{
    private IMode Mode { get; }

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

    [ObservableAsProperty]
    public bool ATItemsVisible { get; }

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
        Mode = mode;

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

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.Mode.SmallKeyShuffle)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.ATItemsVisible)
                .DisposeWith(disposables);
        });
    }
}