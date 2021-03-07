using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Connections;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Modes;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.Sections;

namespace OpenTracker.Models.UndoRedo
{
    public interface IUndoableFactory
    {
        IUndoable GetAddConnection(IConnection connection);
        IUndoable GetAddCrystalRequirement(ICrystalRequirementItem item);
        IUndoable GetAddItem(IItem item);
        IUndoable GetAddNote(ILocation location);
        IUndoable GetChangeBigKeyShuffle(bool bigKeyShuffle);
        IUndoable GetChangeBoss(IBossPlacement bossPlacement, BossType? boss);
        IUndoable GetChangeBossShuffle(bool bossShuffle);
        IUndoable GetChangeCompassShuffle(bool compassShuffle);
        IUndoable GetChangeEnemyShuffle(bool enemyShuffle);
        IUndoable GetChangeEntranceShuffle(EntranceShuffle entranceShuffle);
        IUndoable GetChangeGenericKeys(bool genericKeys);
        IUndoable GetChangeGuaranteedBossItems(bool guaranteedBossItems);
        IUndoable GetChangeItemPlacement(ItemPlacement itemPlacement);
        IUndoable GetChangeKeyDropShuffle(bool keyDropShuffle);
        IUndoable GetChangeMapShuffle(bool mapShuffle);
        IUndoable GetChangePrize(IPrizePlacement prizePlacement);
        IUndoable GetChangeShopShuffle(bool shopShuffle);
        IUndoable GetChangeSmallKeyShuffle(bool smallKeyShuffle);
        IUndoable GetChangeTakeAnyLocations(bool takeAnyLocations);
        IUndoable GetChangeWorldState(WorldState worldState);
        IUndoable GetCheckDropdown(IDropdown dropdown);
        IUndoable GetClearLocation(ILocation location, bool force = false);
        IUndoable GetCollectSection(ISection section, bool force);
        IUndoable GetCycleItem(IItem item);
        IUndoable GetPinLocation(ILocation pinnedLocation);
        IUndoable GetRemoveConnection(IConnection connection);
        IUndoable GetRemoveCrystalRequirement(ICrystalRequirementItem item);
        IUndoable GetRemoveItem(IItem item);
        IUndoable GetRemoveNote(IMarking marking, ILocation location);
        IUndoable GetSetMarking(IMarking marking, MarkType newMarking);
        IUndoable GetTogglePrize(ISection section, bool force);
        IUndoable GetUncheckDropdown(IDropdown dropdown);
        IUndoable GetUncollectSection(ISection section);
        IUndoable GetUnpinLocation(ILocation location);
    }
}