using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.AutoTracking.Logging;
using OpenTracker.Models.AutoTracking.Memory;
using OpenTracker.Models.AutoTracking.SNESConnectors;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.KeyLayouts.Factories;
using OpenTracker.Models.Dungeons.Nodes.Factories;
using OpenTracker.Models.Items;
using OpenTracker.Models.Items.Factories;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Locations.Map.Connections;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Factories;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.AlwaysDisplayDungeonItems;
using OpenTracker.Models.Requirements.AutoTracking;
using OpenTracker.Models.Requirements.BigKeyShuffle;
using OpenTracker.Models.Requirements.Boss;
using OpenTracker.Models.Requirements.BossShuffle;
using OpenTracker.Models.Requirements.CaptureWindowOpen;
using OpenTracker.Models.Requirements.CompassShuffle;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.DisplayAllLocations;
using OpenTracker.Models.Requirements.DisplaysMapsCompasses;
using OpenTracker.Models.Requirements.EnemyShuffle;
using OpenTracker.Models.Requirements.GenericKeys;
using OpenTracker.Models.Requirements.GuaranteedBossItems;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Item.Crystal;
using OpenTracker.Models.Requirements.Item.Exact;
using OpenTracker.Models.Requirements.Item.Prize;
using OpenTracker.Models.Requirements.Item.SmallKey;
using OpenTracker.Models.Requirements.ItemsPanelOrientation;
using OpenTracker.Models.Requirements.KeyDropShuffle;
using OpenTracker.Models.Requirements.MapShuffle;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.Requirements.ShopShuffle;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using OpenTracker.Models.Requirements.TakeAnyLocations;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using OpenTracker.Models.Settings;
using OpenTracker.Models.Reset;
using OpenTracker.Models.Sections.Factories;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Dialog;
using OpenTracker.Utils.Themes;
using OpenTracker.ViewModels;
using OpenTracker.ViewModels.Areas;
using OpenTracker.ViewModels.AutoTracking;
using OpenTracker.ViewModels.BossSelect;
using OpenTracker.ViewModels.Capture;
using OpenTracker.ViewModels.Capture.Design;
using OpenTracker.ViewModels.ColorSelect;
using OpenTracker.ViewModels.Dialogs;
using OpenTracker.ViewModels.Dropdowns;
using OpenTracker.ViewModels.Dungeons;
using OpenTracker.ViewModels.Items;
using OpenTracker.ViewModels.Items.Adapters;
using OpenTracker.ViewModels.MapLocations;
using OpenTracker.ViewModels.Maps;
using OpenTracker.ViewModels.Markings;
using OpenTracker.ViewModels.Markings.Images;
using OpenTracker.ViewModels.Menus;
using OpenTracker.ViewModels.PinnedLocations;
using OpenTracker.ViewModels.PinnedLocations.Sections;
using OpenTracker.ViewModels.SequenceBreaks;
using OpenTracker.ViewModels.UIPanels;

namespace OpenTracker
{
    /// <summary>
    /// This class contains the logic to create and configure the Autofac container.
    /// </summary>
    public static class ContainerConfig
    {
        private static List<string> UtilsSkipTypes => new();

        private static List<string> UtilsSelfTypes => new()
        {
            nameof(ConstrainedTaskScheduler)
        };

        private static List<string> UtilsSingleInstanceTypes => new()
        {
            nameof(ConstrainedTaskScheduler),
            nameof(DialogService),
            nameof(FileDialogService),
            nameof(JsonConverter),
            nameof(ThemeManager),
            nameof(MainWindowProvider)
        };

        private static List<string> ModelsSkipTypes => new()
        {
            nameof(AutoTrackValueBase),
            nameof(AccessibilityRequirement),
            nameof(BooleanRequirement)
        };

        private static List<string> ModelsSelfTypes => new();

        private static List<string> ModelsSingleInstanceTypes => new()
        {
            nameof(AutoTrackerLogService),
            nameof(MemoryAddressProvider),
            nameof(AutoTracker),
            nameof(SNESConnector),
            nameof(BossPlacementDictionary),
            nameof(BossPlacementFactory),
            nameof(ConnectionCollection),
            nameof(DropdownDictionary),
            nameof(DropdownFactory),
            nameof(DungeonItemFactory),
            nameof(DungeonNodeFactory),
            nameof(DungeonDictionary),
            nameof(DungeonFactory),
            nameof(CappedItemFactory),
            nameof(ItemFactory),
            nameof(ItemAutoTrackValueFactory),
            nameof(KeyItemFactory),
            nameof(ItemDictionary),
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
            nameof(OverworldNodeDictionary),
            nameof(OverworldNodeFactory),
            nameof(AggregateRequirementDictionary),
            nameof(AlternativeRequirementDictionary),
            nameof(RaceIllegalTrackingRequirement),
            nameof(BigKeyShuffleRequirementDictionary),
            nameof(BossRequirementDictionary),
            nameof(BossTypeRequirementDictionary),
            nameof(BossTypeRequirementFactory),
            nameof(BossShuffleRequirementDictionary),
            nameof(BossTypeRequirementFactory),
            nameof(CompassShuffleRequirementDictionary),
            nameof(ComplexRequirementDictionary),
            nameof(ComplexRequirementFactory),
            nameof(EnemyShuffleRequirementDictionary),
            nameof(GenericKeysRequirementDictionary),
            nameof(GuaranteedBossItemsRequirementDictionary),
            nameof(CrystalRequirement),
            nameof(ItemRequirementDictionary),
            nameof(ItemExactRequirementDictionary),
            nameof(PrizeRequirementDictionary),
            nameof(SmallKeyRequirementDictionary),
            nameof(KeyDropShuffleRequirementDictionary),
            nameof(MapShuffleRequirementDictionary),
            nameof(EntranceShuffleRequirementDictionary),
            nameof(ItemPlacementRequirementDictionary),
            nameof(WorldStateRequirementDictionary),
            nameof(SequenceBreakRequirementDictionary),
            nameof(ShopShuffleRequirementDictionary),
            nameof(SmallKeyShuffleRequirementDictionary),
            nameof(TakeAnyLocationsRequirementDictionary),
            nameof(SaveLoadManager),
            nameof(SectionFactory),
            nameof(SectionAutoTrackingFactory),
            nameof(SequenceBreakDictionary),
            nameof(UndoRedoManager)
        };

        private static List<string> GUISkipTypes => new()
        {
            nameof(OrientedDungeonPanelVMBase)
        };

        private static List<string> GUISelfTypes => new()
        {
            nameof(CaptureWindowOpenRequirement),
            nameof(DisplayAllLocationsRequirement),
            nameof(HorizontalItemsPanelPlacementRequirement),
            nameof(HorizontalUIPanelPlacementRequirement),
            nameof(LayoutOrientationRequirement),
            nameof(MapOrientationRequirement),
            nameof(ShowItemCountsOnMapRequirement),
            nameof(ThemeSelectedRequirement),
            nameof(UIScaleRequirement),
            nameof(VerticalItemsPanelPlacementRequirement),
            nameof(VerticalUIPanelPlacementRequirement),
            nameof(BossAdapter),
            nameof(CrystalRequirementAdapter),
            nameof(DropdownAdapter),
            nameof(DungeonSmallKeyAdapter),
            nameof(ItemAdapter),
            nameof(PairItemAdapter),
            nameof(PrizeAdapter),
            nameof(SmallKeyAdapter),
            nameof(StaticPrizeAdapter),
            nameof(EntranceMapLocationVM),
            nameof(ShopMapLocationVM),
            nameof(StandardMapLocationVM),
            nameof(TakeAnyMapLocationVM),
            nameof(MarkingSelectButtonVM),
            nameof(MarkingSelectSpacerVM),
            nameof(ItemMarkingImageVM),
            nameof(MarkingImageVM),
            nameof(MenuItemCheckBoxVM),
            nameof(BossSectionIconVM),
            nameof(MarkingSectionIconVM),
            nameof(PrizeSectionIconVM)
        };

        private static List<string> GUISingleInstanceTypes => new()
        {
            nameof(AlwaysDisplayDungeonItemsRequirementDictionary),
            nameof(DisplayMapsCompassesRequirementDictionary),
            nameof(ItemsPanelOrientationRequirementDictionary),
            nameof(ResetManager),
            nameof(AppSettings),
            nameof(BoundsSettings),
            nameof(ColorSettings),
            nameof(LayoutSettings),
            nameof(TrackerSettings),
            nameof(MapAreaVM),
            nameof(MapAreaFactory),
            nameof(UIPanelAreaVM),
            nameof(AutoTrackerDialogVM),
            nameof(AutoTrackerLogVM),
            nameof(AutoTrackerStatusVM),
            nameof(BossSelectFactory),
            nameof(CaptureDesignDialogVM),
            nameof(CaptureManager),
            nameof(CaptureWindowCollection),
            nameof(ColorSelectDialogVM),
            nameof(DropdownVMFactory),
            nameof(DungeonPanelVM),
            nameof(DungeonVMDictionary),
            nameof(DungeonVMFactory),
            nameof(HorizontalDungeonPanelVM),
            nameof(VerticalDungeonPanelVM),
            nameof(ItemVMDictionary),
            nameof(ItemVMFactory),
            nameof(MapLocationVMFactory),
            nameof(MapConnectionCollection),
            nameof(MarkingSelectFactory),
            nameof(MarkingImageDictionary),
            nameof(MarkingImageFactory),
            nameof(CaptureWindowMenuCollection),
            nameof(MenuItemFactory),
            nameof(PinnedLocationDictionary),
            nameof(PinnedLocationsPanelVM),
            nameof(PinnedLocationVMCollection),
            nameof(PinnedLocationVMFactory),
            nameof(SectionVMFactory),
            nameof(SequenceBreakControlFactory),
            nameof(SequenceBreakDialogVM),
            nameof(UIPanelFactory),
            nameof(AboutDialogVM),
            nameof(MainWindowVM),
            nameof(ModeSettingsVM),
            nameof(StatusBarVM)
        };

        public static ContainerBuilder GetContainerBuilder()
        {
            var builder = new ContainerBuilder();

            RegisterAssemblyTypes(
                Assembly.Load($"{nameof(OpenTracker)}.{nameof(Utils)}"), builder, UtilsSkipTypes, UtilsSelfTypes,
                UtilsSingleInstanceTypes);
            RegisterAssemblyTypes(
                Assembly.Load($"{nameof(OpenTracker)}.{nameof(Models)}"), builder, ModelsSkipTypes, ModelsSelfTypes,
                ModelsSingleInstanceTypes);
            RegisterAssemblyTypes(
                Assembly.Load(nameof(OpenTracker)), builder, GUISkipTypes, GUISelfTypes, GUISingleInstanceTypes);

            return builder;
        }

        /// <summary>
        /// Returns a newly configured Autofac container.
        /// </summary>
        /// <returns>
        /// A new Autofac container.
        /// </returns>
        public static IContainer Configure()
        {
            var builder = GetContainerBuilder();
            
            return builder.Build();
        }

        /// <summary>
        /// Register all relevant types in the specified assembly.
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
        private static void RegisterAssemblyTypes(
            Assembly assembly, ContainerBuilder builder, ICollection<string> skip, ICollection<string> self,
            ICollection<string> singleInstance)
        {
            foreach (var type in assembly.GetTypes())
            {
                RegisterType(builder, skip, self, singleInstance, type);
            }
        }

        /// <summary>
        /// Register the specified type.
        /// </summary>
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
        /// <param name="type">
        /// The type to be registered.
        /// </param>
        private static void RegisterType(
            ContainerBuilder builder, ICollection<string> skip, ICollection<string> self,
            ICollection<string> singleInstance, Type type)
        {
            if (skip.Contains(type.Name))
            {
                return;
            }

            if (self.Contains(type.Name))
            {
                RegisterTypeAsSelf(builder, singleInstance, type);
                return;
            }

            RegisterTypeAsInterface(builder, singleInstance, type);
        }

        /// <summary>
        /// Register the specified type as itself.
        /// </summary>
        /// <param name="builder">
        /// The container builder.
        /// </param>
        /// <param name="singleInstance">
        /// A list of strings representing types that should be registered as a single instance.
        /// </param>
        /// <param name="type">
        /// The type to be registered.
        /// </param>
        private static void RegisterTypeAsSelf(ContainerBuilder builder, ICollection<string> singleInstance, Type type)
        {
            if (singleInstance.Contains(type.Name))
            {
                builder.RegisterType(type).AsSelf().SingleInstance();
                return;
            }

            builder.RegisterType(type).AsSelf();
        }

        /// <summary>
        /// Register the specified type as the matching interface.
        /// </summary>
        /// <param name="builder">
        /// The container builder.
        /// </param>
        /// <param name="singleInstance">
        /// A list of strings representing types that should be registered as a single instance.
        /// </param>
        /// <param name="type">
        /// The type to be registered.
        /// </param>
        private static void RegisterTypeAsInterface(
            ContainerBuilder builder, ICollection<string> singleInstance, Type type)
        {
            var interfaceName = $"I{type.Name}";
            var interfaceType = type.GetInterfaces().FirstOrDefault(
                i => i.Name == interfaceName);

            if (interfaceType is null)
            {
                return;
            }

            if (singleInstance.Contains(type.Name))
            {
                builder.RegisterType(type).As(interfaceType).SingleInstance();
                return;
            }

            builder.RegisterType(type).As(interfaceType);
        }
    }
}
