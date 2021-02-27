using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.AutoTracking.Logging;
using OpenTracker.Models.AutoTracking.SNESConnectors;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Connections;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Items;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.KeyLayouts;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections;
using OpenTracker.Models.SequenceBreaks;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Dialog;

namespace OpenTracker.UnitTests
{
    public class TestBase
    {
        private static List<string> UtilsSkipTypes =>
            new List<string>();

        private static List<string> UtilsSelfTypes =>
            new List<string>();

        private static List<string> UtilsSingleInstanceTypes =>
            new List<string>
            {
                nameof(DialogService),
                nameof(FileDialogService),
                nameof(MainWindowProvider)
            };

        private static List<string> ModelsSkipTypes =>
            new List<string>
            {
                nameof(AutoTrackValue),
                nameof(AccessibilityRequirement),
                nameof(BooleanRequirement)
            };

        private static List<string> ModelsSelfTypes =>
            new List<string>
            {
                nameof(AutoTrackAddressBool),
                nameof(AutoTrackAddressValue),
                nameof(AutoTrackBitwiseIntegerValue),
                nameof(AutoTrackConditionalValue),
                nameof(AutoTrackFlagBool),
                nameof(AutoTrackItemValue),
                nameof(AutoTrackMultipleDifference),
                nameof(AutoTrackMultipleOverride),
                nameof(AutoTrackMultipleSum),
                nameof(AutoTrackStaticValue),
                nameof(Item),
                nameof(CappedItem),
                nameof(CrystalRequirementItem),
                nameof(KeyItem),
                nameof(BigKeyLayout),
                nameof(EndKeyLayout),
                nameof(SmallKeyLayout),
                nameof(EntryNodeConnection),
                nameof(NodeConnection),
                nameof(AggregateRequirement),
                nameof(AlternativeRequirement),
                nameof(BigKeyShuffleRequirement),
                nameof(BossRequirement),
                nameof(BossShuffleRequirement),
                nameof(CompassShuffleRequirement),
                nameof(CrystalRequirement),
                nameof(EnemyShuffleRequirement),
                nameof(EntranceShuffleRequirement),
                nameof(GenericKeysRequirement),
                nameof(GuaranteedBossItemsRequirement),
                nameof(ItemExactRequirement),
                nameof(ItemPlacementRequirement),
                nameof(ItemRequirement),
                nameof(KeyDoorRequirement),
                nameof(KeyDropShuffleRequirement),
                nameof(MapShuffleRequirement),
                nameof(RaceIllegalTrackingRequirement),
                nameof(RequirementNodeRequirement),
                nameof(SequenceBreakRequirement),
                nameof(ShopShuffleRequirement),
                nameof(SmallKeyRequirement),
                nameof(SmallKeyShuffleRequirement),
                nameof(StaticRequirement),
                nameof(TakeAnyLocationsRequirement),
                nameof(WorldStateRequirement),
                nameof(BossSection),
                nameof(DropdownSection),
                nameof(DungeonEntranceSection),
                nameof(DungeonItemSection),
                nameof(EntranceSection),
                nameof(InsanityEntranceSection),
                nameof(ItemSection),
                nameof(MarkableDungeonItemSection),
                nameof(PrizeSection),
                nameof(ShopSection),
                nameof(TakeAnySection),
                nameof(VisibleItemSection),
                nameof(AddConnection),
                nameof(AddCrystalRequirement),
                nameof(AddItem),
                nameof(AddNote),
                nameof(ChangeBigKeyShuffle),
                nameof(ChangeBoss),
                nameof(ChangeBossShuffle),
                nameof(ChangeCompassShuffle),
                nameof(ChangeEnemyShuffle),
                nameof(ChangeEntranceShuffle),
                nameof(ChangeGenericKeys),
                nameof(ChangeGuaranteedBossItems),
                nameof(ChangeItemPlacement),
                nameof(ChangeKeyDropShuffle),
                nameof(ChangeMapShuffle),
                nameof(ChangePrize),
                nameof(ChangeShopShuffle),
                nameof(ChangeSmallKeyShuffle),
                nameof(ChangeTakeAnyLocations),
                nameof(ChangeWorldState),
                nameof(CheckDropdown),
                nameof(ClearLocation),
                nameof(CollectSection),
                nameof(CycleItem),
                nameof(PinLocation),
                nameof(RemoveConnection),
                nameof(RemoveCrystalRequirement),
                nameof(RemoveItem),
                nameof(RemoveNote),
                nameof(SetMarking),
                nameof(TogglePrize),
                nameof(UncheckDropdown),
                nameof(UncollectSection),
                nameof(UnpinLocation)
            };

        private static List<string> ModelsSingleInstanceTypes =>
            new List<string>
            {
                nameof(AutoTracker),
                nameof(AutoTrackerLogService),
                nameof(SNESConnector),
                nameof(BossPlacementDictionary),
                nameof(BossPlacementFactory),
                nameof(ConnectionCollection),
                nameof(DropdownDictionary),
                nameof(DropdownFactory),
                nameof(DungeonItemFactory),
                nameof(DungeonNodeFactory),
                nameof(DungeonFactory),
                nameof(ItemDictionary),
                nameof(ItemFactory),
                nameof(ItemAutoTrackValueFactory),
                nameof(KeyDoorFactory),
                nameof(KeyLayoutFactory),
                nameof(LocationDictionary),
                nameof(LocationFactory),
                nameof(MapLocationFactory),
                nameof(PinnedLocationCollection),
                nameof(Mode),
                nameof(PrizePlacementDictionary),
                nameof(PrizePlacementFactory),
                nameof(PrizeDictionary),
                nameof(RequirementNodeDictionary),
                nameof(RequirementNodeFactory),
                nameof(RequirementDictionary),
                nameof(RequirementFactory),
                nameof(SaveLoadManager),
                nameof(SectionFactory),
                nameof(SectionAutoTrackingFactory),
                nameof(SequenceBreakDictionary),
                nameof(UndoRedoManager),
                nameof(UndoableFactory)
            };


        /// <summary>
        /// Returns a newly configured Autofac container.
        /// </summary>
        /// <returns>
        /// A new Autofac container.
        /// </returns>
        protected static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            RegisterNamespace(
                Assembly.Load($"{nameof(OpenTracker)}.{nameof(Utils)}"), builder,
                UtilsSkipTypes, UtilsSelfTypes, UtilsSingleInstanceTypes);
            RegisterNamespace(
                Assembly.Load($"{nameof(OpenTracker)}.{nameof(Models)}"), builder,
                ModelsSkipTypes, ModelsSelfTypes, ModelsSingleInstanceTypes);

            return builder.Build();
        }

        /// <summary>
        /// Register all assembly types.
        /// </summary>
        /// <param name="assembly">
        /// The assembly from which types will be registered.
        /// </param>
        /// <param name="builder">
        /// The container builder.
        /// </param>
        /// <param name="skip">
        /// A list of strings representing types that should not be registered.
        /// </param>
        /// <param name="self">
        /// A list of strings representing types that should be registered to themselves.
        /// </param>
        /// <param name="singleInstance">
        /// A list of strings representing types that should be registered as a single instance.
        /// </param>
        private static void RegisterNamespace(
            Assembly assembly, ContainerBuilder builder, List<string> skip, List<string> self,
            List<string> singleInstance)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (skip.Contains(type.Name))
                {
                    continue;
                }

                if (self.Contains(type.Name))
                {
                    builder.RegisterType(type).AsSelf();
                    continue;
                }

                var interfaceName = $"I{type.Name}";
                var interfaceType = type.GetInterfaces().FirstOrDefault(
                    i => i.Name == interfaceName);

                if (interfaceType == null)
                {
                    continue;
                }

                if (singleInstance.Contains(type.Name))
                {
                    builder.RegisterType(type).As(interfaceType).SingleInstance();
                    continue;
                }

                builder.RegisterType(type).As(interfaceType);
            }
        }
    }
}