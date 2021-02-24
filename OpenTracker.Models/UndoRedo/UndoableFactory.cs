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
    public class UndoableFactory : IUndoableFactory
    {
        private readonly AddConnection.Factory _addConnectionFactory;
        private readonly AddCrystalRequirement.Factory _addCrystalFactory;
        private readonly AddItem.Factory _addItemFactory;
        private readonly AddNote.Factory _addNoteFactory;
        private readonly ChangeBigKeyShuffle.Factory _changeBigKeyShuffleFactory;
        private readonly ChangeBoss.Factory _changeBossFactory;
        private readonly ChangeBossShuffle.Factory _changeBossShuffleFactory;
        private readonly ChangeCompassShuffle.Factory _changeCompassShuffleFactory;
        private readonly ChangeEnemyShuffle.Factory _changeEnemyShuffleFactory;
        private readonly ChangeEntranceShuffle.Factory _changeEntranceShuffleFactory;
        private readonly ChangeGenericKeys.Factory _changeGenericKeysFactory;
        private readonly ChangeGuaranteedBossItems.Factory _changeGuaranteedBossItemsFactory;
        private readonly ChangeItemPlacement.Factory _changeItemPlacementFactory;
        private readonly ChangeKeyDropShuffle.Factory _changeKeyDropShuffleFactory;
        private readonly ChangeMapShuffle.Factory _changeMapShuffleFactory;
        private readonly ChangePrize.Factory _changePrizeFactory;
        private readonly ChangeShopShuffle.Factory _changeShopShuffleFactory;
        private readonly ChangeSmallKeyShuffle.Factory _changeSmallKeyShuffleFactory;
        private readonly ChangeTakeAnyLocations.Factory _changeTakeAnyLocationsFactory;
        private readonly ChangeWorldState.Factory _changeWorldStateFactory;
        private readonly CheckDropdown.Factory _checkDropdownFactory;
        private readonly ClearLocation.Factory _clearLocationFactory;
        private readonly CollectSection.Factory _collectSectionFactory;
        private readonly CycleItem.Factory _cycleItemFactory;
        private readonly PinLocation.Factory _pinLocationFactory;
        private readonly RemoveConnection.Factory _removeConnectionFactory;
        private readonly RemoveCrystalRequirement.Factory _removeCrystalRequirementFactory;
        private readonly RemoveItem.Factory _removeItemFactory;
        private readonly RemoveNote.Factory _removeNoteFactory;
        private readonly SetMarking.Factory _setMarkingFactory;
        private readonly TogglePrize.Factory _togglePrizeFactory;
        private readonly UncheckDropdown.Factory _uncheckDropdownFactory;
        private readonly UncollectSection.Factory _uncollectSectionFactory;
        private readonly UnpinLocation.Factory _unpinLocationFactory;

        public UndoableFactory(
            AddConnection.Factory addConnectionFactory,
            AddCrystalRequirement.Factory addCrystalFactory, AddItem.Factory addItemFactory,
            AddNote.Factory addNoteFactory,
            ChangeBigKeyShuffle.Factory changeBigKeyShuffleFactory,
            ChangeBoss.Factory changeBossFactory,
            ChangeBossShuffle.Factory changeBossShuffleFactory,
            ChangeCompassShuffle.Factory changeCompassShuffleFactory,
            ChangeEnemyShuffle.Factory changeEnemyShuffleFactory,
            ChangeEntranceShuffle.Factory changeEntranceShuffleFactory,
            ChangeGenericKeys.Factory changeGenericKeysFactory,
            ChangeGuaranteedBossItems.Factory changeGuaranteedBossItemsFactory,
            ChangeItemPlacement.Factory changeItemPlacementFactory,
            ChangeKeyDropShuffle.Factory changeKeyDropShuffleFactory,
            ChangeMapShuffle.Factory changeMapShuffleFactory,
            ChangePrize.Factory changePrizeFactory,
            ChangeShopShuffle.Factory changeShopShuffleFactory,
            ChangeSmallKeyShuffle.Factory changeSmallKeyShuffleFactory,
            ChangeTakeAnyLocations.Factory changeTakeAnyLocationsFactory,
            ChangeWorldState.Factory changeWorldStateFactory,
            CheckDropdown.Factory checkDropdownFactory,
            ClearLocation.Factory clearLocationFactory,
            CollectSection.Factory collectSectionFactory, CycleItem.Factory cycleItemFactory,
            PinLocation.Factory pinLocationFactory,
            RemoveConnection.Factory removeConnectionFactory,
            RemoveCrystalRequirement.Factory removeCrystalRequirementFactory,
            RemoveItem.Factory removeItemFactory, RemoveNote.Factory removeNoteFactory,
            SetMarking.Factory setMarkingFactory, TogglePrize.Factory togglePrizeFactory,
            UncheckDropdown.Factory uncheckDropdownFactory,
            UncollectSection.Factory uncollectSectionFactory,
            UnpinLocation.Factory unpinLocationFactory)
        {
            _addConnectionFactory = addConnectionFactory;
            _addCrystalFactory = addCrystalFactory;
            _addItemFactory = addItemFactory;
            _addNoteFactory = addNoteFactory;
            _changeBigKeyShuffleFactory = changeBigKeyShuffleFactory;
            _changeBossFactory = changeBossFactory;
            _changeBossShuffleFactory = changeBossShuffleFactory;
            _changeCompassShuffleFactory = changeCompassShuffleFactory;
            _changeEnemyShuffleFactory = changeEnemyShuffleFactory;
            _changeEntranceShuffleFactory = changeEntranceShuffleFactory;
            _changeGenericKeysFactory = changeGenericKeysFactory;
            _changeGuaranteedBossItemsFactory = changeGuaranteedBossItemsFactory;
            _changeItemPlacementFactory = changeItemPlacementFactory;
            _changeKeyDropShuffleFactory = changeKeyDropShuffleFactory;
            _changeMapShuffleFactory = changeMapShuffleFactory;
            _changePrizeFactory = changePrizeFactory;
            _changeShopShuffleFactory = changeShopShuffleFactory;
            _changeSmallKeyShuffleFactory = changeSmallKeyShuffleFactory;
            _changeTakeAnyLocationsFactory = changeTakeAnyLocationsFactory;
            _changeWorldStateFactory = changeWorldStateFactory;
            _checkDropdownFactory = checkDropdownFactory;
            _clearLocationFactory = clearLocationFactory;
            _collectSectionFactory = collectSectionFactory;
            _cycleItemFactory = cycleItemFactory;
            _pinLocationFactory = pinLocationFactory;
            _removeConnectionFactory = removeConnectionFactory;
            _removeCrystalRequirementFactory = removeCrystalRequirementFactory;
            _removeItemFactory = removeItemFactory;
            _removeNoteFactory = removeNoteFactory;
            _setMarkingFactory = setMarkingFactory;
            _togglePrizeFactory = togglePrizeFactory;
            _uncheckDropdownFactory = uncheckDropdownFactory;
            _uncollectSectionFactory = uncollectSectionFactory;
            _unpinLocationFactory = unpinLocationFactory;
        }

        public IUndoable GetAddConnection(IConnection connection)
        {
            return _addConnectionFactory(connection);
        }

        public IUndoable GetAddCrystalRequirement(ICrystalRequirementItem item)
        {
            return _addCrystalFactory(item);
        }

        public IUndoable GetAddItem(IItem item)
        {
            return _addItemFactory(item);
        }

        public IUndoable GetAddNote(ILocation location)
        {
            return _addNoteFactory(location);
        }

        public IUndoable GetChangeBigKeyShuffle(bool bigKeyShuffle)
        {
            return _changeBigKeyShuffleFactory(bigKeyShuffle);
        }

        public IUndoable GetChangeBoss(IBossPlacement bossPlacement, BossType? boss)
        {
            return _changeBossFactory(bossPlacement, boss);
        }

        public IUndoable GetChangeBossShuffle(bool bossShuffle)
        {
            return _changeBossShuffleFactory(bossShuffle);
        }

        public IUndoable GetChangeCompassShuffle(bool compassShuffle)
        {
            return _changeCompassShuffleFactory(compassShuffle);
        }

        public IUndoable GetChangeEnemyShuffle(bool enemyShuffle)
        {
            return _changeEnemyShuffleFactory(enemyShuffle);
        }

        public IUndoable GetChangeEntranceShuffle(EntranceShuffle entranceShuffle)
        {
            return _changeEntranceShuffleFactory(entranceShuffle);
        }

        public IUndoable GetChangeGenericKeys(bool genericKeys)
        {
            return _changeGenericKeysFactory(genericKeys);
        }

        public IUndoable GetChangeGuaranteedBossItems(bool guaranteedBossItems)
        {
            return _changeGuaranteedBossItemsFactory(guaranteedBossItems);
        }

        public IUndoable GetChangeItemPlacement(ItemPlacement itemPlacement)
        {
            return _changeItemPlacementFactory(itemPlacement);
        }

        public IUndoable GetChangeKeyDropShuffle(bool keyDropShuffle)
        {
            return _changeKeyDropShuffleFactory(keyDropShuffle);
        }

        public IUndoable GetChangeMapShuffle(bool mapShuffle)
        {
            return _changeMapShuffleFactory(mapShuffle);
        }

        public IUndoable GetChangePrize(IPrizePlacement prizePlacement)
        {
            return _changePrizeFactory(prizePlacement);
        }

        public IUndoable GetChangeShopShuffle(bool shopShuffle)
        {
            return _changeShopShuffleFactory(shopShuffle);
        }

        public IUndoable GetChangeSmallKeyShuffle(bool smallKeyShuffle)
        {
            return _changeSmallKeyShuffleFactory(smallKeyShuffle);
        }

        public IUndoable GetChangeTakeAnyLocations(bool takeAnyLocations)
        {
            return _changeTakeAnyLocationsFactory(takeAnyLocations);
        }

        public IUndoable GetChangeWorldState(WorldState worldState)
        {
            return _changeWorldStateFactory(worldState);
        }

        public IUndoable GetCheckDropdown(IDropdown dropdown)
        {
            return _checkDropdownFactory(dropdown);
        }

        public IUndoable GetClearLocation(ILocation location, bool force = false)
        {
            return _clearLocationFactory(location, force);
        }

        public IUndoable GetCollectSection(ISection section, bool force)
        {
            return _collectSectionFactory(section, force);
        }

        public IUndoable GetCycleItem(IItem item)
        {
            return _cycleItemFactory(item);
        }

        public IUndoable GetPinLocation(ILocation pinnedLocation)
        {
            return _pinLocationFactory(pinnedLocation);
        }

        public IUndoable GetRemoveConnection(IConnection connection)
        {
            return _removeConnectionFactory(connection);
        }

        public IUndoable GetRemoveCrystalRequirement(ICrystalRequirementItem item)
        {
            return _removeCrystalRequirementFactory(item);
        }

        public IUndoable GetRemoveItem(IItem item)
        {
            return _removeItemFactory(item);
        }

        public IUndoable GetRemoveNote(IMarking marking, ILocation location)
        {
            return _removeNoteFactory(marking, location);
        }

        public IUndoable GetSetMarking(IMarking marking, MarkType newMarking)
        {
            return _setMarkingFactory(marking, newMarking);
        }

        public IUndoable GetTogglePrize(ISection section, bool force)
        {
            return _togglePrizeFactory(section, force);
        }

        public IUndoable GetUncheckDropdown(IDropdown dropdown)
        {
            return _uncheckDropdownFactory(dropdown);
        }

        public IUndoable GetUncollectSection(ISection section)
        {
            return _uncollectSectionFactory(section);
        }

        public IUndoable GetUnpinLocation(ILocation location)
        {
            return _unpinLocationFactory(location);
        }
    }
}
