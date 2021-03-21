using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Connections;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Modes;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo.Boss;
using OpenTracker.Models.UndoRedo.Connections;
using OpenTracker.Models.UndoRedo.Dropdowns;
using OpenTracker.Models.UndoRedo.Items;
using OpenTracker.Models.UndoRedo.Locations;
using OpenTracker.Models.UndoRedo.Markings;
using OpenTracker.Models.UndoRedo.Mode;
using OpenTracker.Models.UndoRedo.Notes;
using OpenTracker.Models.UndoRedo.Prize;
using OpenTracker.Models.UndoRedo.Sections;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains creation logic for undoable action data.
    /// </summary>
    public class UndoableFactory : IUndoableFactory
    {
        private readonly IAddConnection.Factory _addConnectionFactory;
        private readonly IAddCrystalRequirement.Factory _addCrystalFactory;
        private readonly IAddItem.Factory _addItemFactory;
        private readonly IAddNote.Factory _addNoteFactory;
        private readonly IChangeBigKeyShuffle.Factory _changeBigKeyShuffleFactory;
        private readonly IChangeBoss.Factory _changeBossFactory;
        private readonly IChangeBossShuffle.Factory _changeBossShuffleFactory;
        private readonly IChangeCompassShuffle.Factory _changeCompassShuffleFactory;
        private readonly IChangeEnemyShuffle.Factory _changeEnemyShuffleFactory;
        private readonly IChangeEntranceShuffle.Factory _changeEntranceShuffleFactory;
        private readonly IChangeGenericKeys.Factory _changeGenericKeysFactory;
        private readonly IChangeGuaranteedBossItems.Factory _changeGuaranteedBossItemsFactory;
        private readonly IChangeItemPlacement.Factory _changeItemPlacementFactory;
        private readonly IChangeKeyDropShuffle.Factory _changeKeyDropShuffleFactory;
        private readonly IChangeMapShuffle.Factory _changeMapShuffleFactory;
        private readonly IChangePrize.Factory _changePrizeFactory;
        private readonly IChangeShopShuffle.Factory _changeShopShuffleFactory;
        private readonly IChangeSmallKeyShuffle.Factory _changeSmallKeyShuffleFactory;
        private readonly IChangeTakeAnyLocations.Factory _changeTakeAnyLocationsFactory;
        private readonly IChangeWorldState.Factory _changeWorldStateFactory;
        private readonly ICheckDropdown.Factory _checkDropdownFactory;
        private readonly IClearLocation.Factory _clearLocationFactory;
        private readonly ICollectSection.Factory _collectSectionFactory;
        private readonly ICycleItem.Factory _cycleItemFactory;
        private readonly IPinLocation.Factory _pinLocationFactory;
        private readonly IRemoveConnection.Factory _removeConnectionFactory;
        private readonly IRemoveCrystalRequirement.Factory _removeCrystalRequirementFactory;
        private readonly IRemoveItem.Factory _removeItemFactory;
        private readonly IRemoveNote.Factory _removeNoteFactory;
        private readonly ISetMarking.Factory _setMarkingFactory;
        private readonly ITogglePrizeSection.Factory _togglePrizeFactory;
        private readonly IUncheckDropdown.Factory _uncheckDropdownFactory;
        private readonly IUncollectSection.Factory _uncollectSectionFactory;
        private readonly IUnpinLocation.Factory _unpinLocationFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="addConnectionFactory">
        /// An Autofac factory for creating add connection actions.
        /// </param>
        /// <param name="addCrystalFactory">
        /// An Autofac factory for creating add crystal requirement actions.
        /// </param>
        /// <param name="addItemFactory">
        /// An Autofac factory for creating add item actions.
        /// </param>
        /// <param name="addNoteFactory">
        /// An Autofac factory for creating add note actions.
        /// </param>
        /// <param name="changeBigKeyShuffleFactory">
        /// An Autofac factory for creating change big key shuffle actions.
        /// </param>
        /// <param name="changeBossFactory">
        /// An Autofac factory for creating change boss actions.
        /// </param>
        /// <param name="changeBossShuffleFactory">
        /// An Autofac factory for creating change boss shuffle actions.
        /// </param>
        /// <param name="changeCompassShuffleFactory">
        /// An Autofac factory for creating change compass shuffle actions.
        /// </param>
        /// <param name="changeEnemyShuffleFactory">
        /// An Autofac factory for creating change enemy shuffle actions.
        /// </param>
        /// <param name="changeEntranceShuffleFactory">
        /// An Autofac factory for creating change entrance shuffle actions.
        /// </param>
        /// <param name="changeGenericKeysFactory">
        /// An Autofac factory for creating change generic actions.
        /// </param>
        /// <param name="changeGuaranteedBossItemsFactory">
        /// An Autofac factory for creating change guaranteed boss items actions.
        /// </param>
        /// <param name="changeItemPlacementFactory">
        /// An Autofac factory for creating change item placement actions.
        /// </param>
        /// <param name="changeKeyDropShuffleFactory">
        /// An Autofac factory for creating change key drop shuffle actions.
        /// </param>
        /// <param name="changeMapShuffleFactory">
        /// An Autofac factory for creating change map shuffle actions.
        /// </param>
        /// <param name="changePrizeFactory">
        /// An Autofac factory for creating change prize actions.
        /// </param>
        /// <param name="changeShopShuffleFactory">
        /// An Autofac factory for creating change shop shuffle actions.
        /// </param>
        /// <param name="changeSmallKeyShuffleFactory">
        /// An Autofac factory for creating change small key shuffle actions.
        /// </param>
        /// <param name="changeTakeAnyLocationsFactory">
        /// An Autofac factory for creating change take any locations actions.
        /// </param>
        /// <param name="changeWorldStateFactory">
        /// An Autofac factory for creating change world state actions.
        /// </param>
        /// <param name="checkDropdownFactory">
        /// An Autofac factory for creating check dropdown actions.
        /// </param>
        /// <param name="clearLocationFactory">
        /// An Autofac factory for creating clear location actions.
        /// </param>
        /// <param name="collectSectionFactory">
        /// An Autofac factory for creating collect section actions.
        /// </param>
        /// <param name="cycleItemFactory">
        /// An Autofac factory for creating cycle item actions.
        /// </param>
        /// <param name="pinLocationFactory">
        /// An Autofac factory for creating pin location actions.
        /// </param>
        /// <param name="removeConnectionFactory">
        /// An Autofac factory for creating remove connection actions.
        /// </param>
        /// <param name="removeCrystalRequirementFactory">
        /// An Autofac factory for creating remove crystal requirement actions.
        /// </param>
        /// <param name="removeItemFactory">
        /// An Autofac factory for creating remove item actions.
        /// </param>
        /// <param name="removeNoteFactory">
        /// An Autofac factory for creating remove note actions.
        /// </param>
        /// <param name="setMarkingFactory">
        /// An Autofac factory for creating set marking actions.
        /// </param>
        /// <param name="togglePrizeFactory">
        /// An Autofac factory for creating toggle prize actions.
        /// </param>
        /// <param name="uncheckDropdownFactory">
        /// An Autofac factory for creating uncheck dropdown actions.
        /// </param>
        /// <param name="uncollectSectionFactory">
        /// An Autofac factory for creating uncollect section actions.
        /// </param>
        /// <param name="unpinLocationFactory">
        /// An Autofac factory for creating unpin location actions.
        /// </param>
        public UndoableFactory(
            IAddConnection.Factory addConnectionFactory,
            IAddCrystalRequirement.Factory addCrystalFactory, IAddItem.Factory addItemFactory,
            IAddNote.Factory addNoteFactory,
            IChangeBigKeyShuffle.Factory changeBigKeyShuffleFactory,
            IChangeBoss.Factory changeBossFactory,
            IChangeBossShuffle.Factory changeBossShuffleFactory,
            IChangeCompassShuffle.Factory changeCompassShuffleFactory,
            IChangeEnemyShuffle.Factory changeEnemyShuffleFactory,
            IChangeEntranceShuffle.Factory changeEntranceShuffleFactory,
            IChangeGenericKeys.Factory changeGenericKeysFactory,
            IChangeGuaranteedBossItems.Factory changeGuaranteedBossItemsFactory,
            IChangeItemPlacement.Factory changeItemPlacementFactory,
            IChangeKeyDropShuffle.Factory changeKeyDropShuffleFactory,
            IChangeMapShuffle.Factory changeMapShuffleFactory,
            IChangePrize.Factory changePrizeFactory,
            IChangeShopShuffle.Factory changeShopShuffleFactory,
            IChangeSmallKeyShuffle.Factory changeSmallKeyShuffleFactory,
            IChangeTakeAnyLocations.Factory changeTakeAnyLocationsFactory,
            IChangeWorldState.Factory changeWorldStateFactory,
            ICheckDropdown.Factory checkDropdownFactory,
            IClearLocation.Factory clearLocationFactory,
            ICollectSection.Factory collectSectionFactory, ICycleItem.Factory cycleItemFactory,
            IPinLocation.Factory pinLocationFactory,
            IRemoveConnection.Factory removeConnectionFactory,
            IRemoveCrystalRequirement.Factory removeCrystalRequirementFactory,
            IRemoveItem.Factory removeItemFactory, IRemoveNote.Factory removeNoteFactory,
            ISetMarking.Factory setMarkingFactory, ITogglePrizeSection.Factory togglePrizeFactory,
            IUncheckDropdown.Factory uncheckDropdownFactory,
            IUncollectSection.Factory uncollectSectionFactory,
            IUnpinLocation.Factory unpinLocationFactory)
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

        /// <summary>
        /// Returns a new add connection action.
        /// </summary>
        /// <param name="connection">
        /// The connection to be added.
        /// </param>
        /// <returns>
        /// A new add connection action.
        /// </returns>
        public IUndoable GetAddConnection(IConnection connection)
        {
            return _addConnectionFactory(connection);
        }

        /// <summary>
        /// Returns a new add crystal requirement action.
        /// </summary>
        /// <param name="item">
        /// The crystal requirement item.
        /// </param>
        /// <returns>
        /// A new add crystal requirement action.
        /// </returns>
        public IUndoable GetAddCrystalRequirement(ICrystalRequirementItem item)
        {
            return _addCrystalFactory(item);
        }

        /// <summary>
        /// Returns a new add item action.
        /// </summary>
        /// <param name="item">
        /// The item to be added.
        /// </param>
        /// <returns>
        /// A new add item action.
        /// </returns>
        public IUndoable GetAddItem(IItem item)
        {
            return _addItemFactory(item);
        }

        /// <summary>
        /// Returns a new add note action.
        /// </summary>
        /// <param name="location">
        /// The location to be which a note is to be added.
        /// </param>
        /// <returns>
        /// A new add note action.
        /// </returns>
        public IUndoable GetAddNote(ILocation location)
        {
            return _addNoteFactory(location);
        }

        /// <summary>
        /// Returns a new change big key shuffle action.
        /// </summary>
        /// <param name="bigKeyShuffle">
        /// The new big key shuffle value.
        /// </param>
        /// <returns>
        /// A new change big key shuffle action.
        /// </returns>
        public IUndoable GetChangeBigKeyShuffle(bool bigKeyShuffle)
        {
            return _changeBigKeyShuffleFactory(bigKeyShuffle);
        }

        /// <summary>
        /// Returns a new change boss action.
        /// </summary>
        /// <param name="bossPlacement">
        /// The boss placement to be changed.
        /// </param>
        /// <param name="boss">
        /// The new boss type.
        /// </param>
        /// <returns>
        /// A new change boss action.
        /// </returns>
        public IUndoable GetChangeBoss(IBossPlacement bossPlacement, BossType? boss)
        {
            return _changeBossFactory(bossPlacement, boss);
        }

        /// <summary>
        /// Returns a new change boss shuffle action.
        /// </summary>
        /// <param name="bossShuffle">
        /// The new boss shuffle value.
        /// </param>
        /// <returns>
        /// A new change boss shuffle action.
        /// </returns>
        public IUndoable GetChangeBossShuffle(bool bossShuffle)
        {
            return _changeBossShuffleFactory(bossShuffle);
        }

        /// <summary>
        /// Returns a new change compass shuffle action.
        /// </summary>
        /// <param name="compassShuffle">
        /// The new compass shuffle.
        /// </param>
        /// <returns>
        /// A new change compass shuffle action.
        /// </returns>
        public IUndoable GetChangeCompassShuffle(bool compassShuffle)
        {
            return _changeCompassShuffleFactory(compassShuffle);
        }

        /// <summary>
        /// Returns a new change enemy shuffle action.
        /// </summary>
        /// <param name="enemyShuffle">
        /// The new enemy shuffle value.
        /// </param>
        /// <returns>
        /// A new change enemy shuffle action.
        /// </returns>
        public IUndoable GetChangeEnemyShuffle(bool enemyShuffle)
        {
            return _changeEnemyShuffleFactory(enemyShuffle);
        }

        /// <summary>
        /// Returns a new change entrance shuffle action.
        /// </summary>
        /// <param name="entranceShuffle">
        /// The new entrance shuffle value.
        /// </param>
        /// <returns>
        /// A new change entrance shuffle.
        /// </returns>
        public IUndoable GetChangeEntranceShuffle(EntranceShuffle entranceShuffle)
        {
            return _changeEntranceShuffleFactory(entranceShuffle);
        }

        /// <summary>
        /// Returns a new change generic keys action.
        /// </summary>
        /// <param name="genericKeys">
        /// The new generic keys value.
        /// </param>
        /// <returns>
        /// A new change generic keys action.
        /// </returns>
        public IUndoable GetChangeGenericKeys(bool genericKeys)
        {
            return _changeGenericKeysFactory(genericKeys);
        }

        /// <summary>
        /// Returns a new change guaranteed boss items action.
        /// </summary>
        /// <param name="guaranteedBossItems">
        /// The new guaranteed boss items value.
        /// </param>
        /// <returns>
        /// A new change guaranteed boss items action.
        /// </returns>
        public IUndoable GetChangeGuaranteedBossItems(bool guaranteedBossItems)
        {
            return _changeGuaranteedBossItemsFactory(guaranteedBossItems);
        }

        /// <summary>
        /// Returns a new change item placement action.
        /// </summary>
        /// <param name="itemPlacement">
        /// The new item placement value.
        /// </param>
        /// <returns>
        /// A new change item placement action.
        /// </returns>
        public IUndoable GetChangeItemPlacement(ItemPlacement itemPlacement)
        {
            return _changeItemPlacementFactory(itemPlacement);
        }

        /// <summary>
        /// Returns a new change key drop shuffle action.
        /// </summary>
        /// <param name="keyDropShuffle">
        /// The new key drop shuffle value.
        /// </param>
        /// <returns>
        /// A new change key drop shuffle action.
        /// </returns>
        public IUndoable GetChangeKeyDropShuffle(bool keyDropShuffle)
        {
            return _changeKeyDropShuffleFactory(keyDropShuffle);
        }

        /// <summary>
        /// Returns a new change map shuffle action.
        /// </summary>
        /// <param name="mapShuffle">
        /// The new map shuffle value.
        /// </param>
        /// <returns>
        /// A new change map shuffle action.
        /// </returns>
        public IUndoable GetChangeMapShuffle(bool mapShuffle)
        {
            return _changeMapShuffleFactory(mapShuffle);
        }

        /// <summary>
        /// Returns a new change prize action.
        /// </summary>
        /// <param name="prizePlacement">
        /// The prize placement to change.
        /// </param>
        /// <returns>
        /// A new change prize action.
        /// </returns>
        public IUndoable GetChangePrize(IPrizePlacement prizePlacement)
        {
            return _changePrizeFactory(prizePlacement);
        }

        /// <summary>
        /// Returns a new change shop shuffle action.
        /// </summary>
        /// <param name="shopShuffle">
        /// The new shop shuffle value.
        /// </param>
        /// <returns>
        /// A new change shop shuffle action.
        /// </returns>
        public IUndoable GetChangeShopShuffle(bool shopShuffle)
        {
            return _changeShopShuffleFactory(shopShuffle);
        }

        /// <summary>
        /// Returns a new change small key shuffle action.
        /// </summary>
        /// <param name="smallKeyShuffle">
        /// The new small key shuffle value.
        /// </param>
        /// <returns>
        /// A new change small key shuffle action.
        /// </returns>
        public IUndoable GetChangeSmallKeyShuffle(bool smallKeyShuffle)
        {
            return _changeSmallKeyShuffleFactory(smallKeyShuffle);
        }

        /// <summary>
        /// Returns a new change take any locations action.
        /// </summary>
        /// <param name="takeAnyLocations">
        /// The new take any locations value.
        /// </param>
        /// <returns>
        /// A new change take any locations action.
        /// </returns>
        public IUndoable GetChangeTakeAnyLocations(bool takeAnyLocations)
        {
            return _changeTakeAnyLocationsFactory(takeAnyLocations);
        }

        /// <summary>
        /// Returns a new change world state action.
        /// </summary>
        /// <param name="worldState">
        /// The new world state value.
        /// </param>
        /// <returns>
        /// A new change world state action.
        /// </returns>
        public IUndoable GetChangeWorldState(WorldState worldState)
        {
            return _changeWorldStateFactory(worldState);
        }

        /// <summary>
        /// Returns a new check dropdown action.
        /// </summary>
        /// <param name="dropdown">
        /// The dropdown to be checked.
        /// </param>
        /// <returns>
        /// A new check dropdown action.
        /// </returns>
        public IUndoable GetCheckDropdown(IDropdown dropdown)
        {
            return _checkDropdownFactory(dropdown);
        }

        /// <summary>
        /// Returns a new clear location action.
        /// </summary>
        /// <param name="location">
        /// The location to be cleared.
        /// </param>
        /// <param name="force">
        /// A boolean representing whether to ignore the dungeon requirements.
        /// </param>
        /// <returns>
        /// A new clear location action.
        /// </returns>
        public IUndoable GetClearLocation(ILocation location, bool force = false)
        {
            return _clearLocationFactory(location, force);
        }

        /// <summary>
        /// Returns a new collect section action.
        /// </summary>
        /// <param name="section">
        /// The section to be collected.
        /// </param>
        /// <param name="force">
        /// A boolean representing whether to ignore the section requirements.
        /// </param>
        /// <returns>
        /// A new collect section action.
        /// </returns>
        public IUndoable GetCollectSection(ISection section, bool force)
        {
            return _collectSectionFactory(section, force);
        }

        /// <summary>
        /// Returns a new cycle item action.
        /// </summary>
        /// <param name="item">
        /// The item to be cycled.
        /// </param>
        /// <returns>
        /// A new cycle item action.
        /// </returns>
        public IUndoable GetCycleItem(IItem item)
        {
            return _cycleItemFactory(item);
        }

        /// <summary>
        /// Returns a new pin location action.
        /// </summary>
        /// <param name="pinnedLocation">
        /// The location to be pinned.
        /// </param>
        /// <returns>
        /// A new pin location action.
        /// </returns>
        public IUndoable GetPinLocation(ILocation pinnedLocation)
        {
            return _pinLocationFactory(pinnedLocation);
        }

        /// <summary>
        /// Returns a new remove connection action.
        /// </summary>
        /// <param name="connection">
        /// The connection to be removed.
        /// </param>
        /// <returns>
        /// A new remove connection action.
        /// </returns>
        public IUndoable GetRemoveConnection(IConnection connection)
        {
            return _removeConnectionFactory(connection);
        }

        /// <summary>
        /// Returns a new remove crystal requirement action.
        /// </summary>
        /// <param name="item">
        /// The crystal requirement to be removed.
        /// </param>
        /// <returns>
        /// A new remove crystal requirement action.
        /// </returns>
        public IUndoable GetRemoveCrystalRequirement(ICrystalRequirementItem item)
        {
            return _removeCrystalRequirementFactory(item);
        }

        /// <summary>
        /// Returns a new remove item action.
        /// </summary>
        /// <param name="item">
        /// The item to be removed.
        /// </param>
        /// <returns>
        /// A new remove item action.
        /// </returns>
        public IUndoable GetRemoveItem(IItem item)
        {
            return _removeItemFactory(item);
        }

        /// <summary>
        /// Returns a new remove note action.
        /// </summary>
        /// <param name="marking">
        /// The marking note to be removed.
        /// </param>
        /// <param name="location">
        /// The location from which the note is removed.
        /// </param>
        /// <returns>
        /// A new remove note action.
        /// </returns>
        public IUndoable GetRemoveNote(IMarking marking, ILocation location)
        {
            return _removeNoteFactory(marking, location);
        }

        /// <summary>
        /// Returns a new set marking action.
        /// </summary>
        /// <param name="marking">
        /// The marking to be changed.
        /// </param>
        /// <param name="newMarking">
        /// The new mark value.
        /// </param>
        /// <returns>
        /// A new set marking action.
        /// </returns>
        public IUndoable GetSetMarking(IMarking marking, MarkType newMarking)
        {
            return _setMarkingFactory(marking, newMarking);
        }

        /// <summary>
        /// Returns a new toggle prize action.
        /// </summary>
        /// <param name="section">
        /// The prize section to be toggled.
        /// </param>
        /// <param name="force">
        /// A boolean representing whether to ignore the section requirements.
        /// </param>
        /// <returns>
        /// A new toggle prize action.
        /// </returns>
        public IUndoable GetTogglePrize(ISection section, bool force)
        {
            return _togglePrizeFactory(section, force);
        }

        /// <summary>
        /// Returns a new uncheck dropdown action.
        /// </summary>
        /// <param name="dropdown">
        /// The dropdown to be unchecked.
        /// </param>
        /// <returns>
        /// A new uncheck dropdown action.
        /// </returns>
        public IUndoable GetUncheckDropdown(IDropdown dropdown)
        {
            return _uncheckDropdownFactory(dropdown);
        }

        /// <summary>
        /// Returns a new uncollect section action.
        /// </summary>
        /// <param name="section">
        /// The section to be uncollected.
        /// </param>
        /// <returns>
        /// A new uncollect section action.
        /// </returns>
        public IUndoable GetUncollectSection(ISection section)
        {
            return _uncollectSectionFactory(section);
        }

        /// <summary>
        /// Returns a new unpin location action.
        /// </summary>
        /// <param name="location">
        /// The location to be unpinned.
        /// </param>
        /// <returns>
        /// A new unpin location action.
        /// </returns>
        public IUndoable GetUnpinLocation(ILocation location)
        {
            return _unpinLocationFactory(location);
        }
    }
}
