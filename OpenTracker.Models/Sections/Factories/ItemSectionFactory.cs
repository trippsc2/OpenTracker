using System;
using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Sections.Item;

namespace OpenTracker.Models.Sections.Factories
{
    /// <summary>
    /// This class contains the creation logic for <see cref="IItemSection"/> objects.
    /// </summary>
    public class ItemSectionFactory : IItemSectionFactory
    {
        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly IAlternativeRequirementDictionary _alternativeRequirements;
        private readonly IEntranceShuffleRequirementDictionary _entranceShuffleRequirements;
        private readonly IWorldStateRequirementDictionary _worldStateRequirements;
        
        private readonly IItemSection.Factory _factory;
        private readonly IMarking.Factory _markingFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="overworldNodes">
        ///     The <see cref="IOverworldNodeDictionary"/>.
        /// </param>
        /// <param name="alternativeRequirements">
        ///     The <see cref="IAlternativeRequirementDictionary"/>.
        /// </param>
        /// <param name="entranceShuffleRequirements">
        ///     The <see cref="IEntranceShuffleRequirementDictionary"/>.
        /// </param>
        /// <param name="worldStateRequirements">
        ///     The <see cref="IWorldStateRequirementDictionary"/>.
        /// </param>
        /// <param name="factory">
        ///     An Autofac factory for creating new <see cref="IItemSection"/> objects.
        /// </param>
        /// <param name="markingFactory">
        ///     An Autofac factory for creating new <see cref="IMarking"/> objects.
        /// </param>
        public ItemSectionFactory(
            IOverworldNodeDictionary overworldNodes, IAlternativeRequirementDictionary alternativeRequirements,
            IEntranceShuffleRequirementDictionary entranceShuffleRequirements,
            IWorldStateRequirementDictionary worldStateRequirements, IItemSection.Factory factory,
            IMarking.Factory markingFactory)
        {
            _overworldNodes = overworldNodes;

            _alternativeRequirements = alternativeRequirements;
            _entranceShuffleRequirements = entranceShuffleRequirements;
            _worldStateRequirements = worldStateRequirements;
            
            _factory = factory;
            _markingFactory = markingFactory;
        }

        public IItemSection GetItemSection(IAutoTrackValue? autoTrackValue, LocationID id, int index = 0)
        {
            var visibleNode = GetVisibleNode(id, index);
            var marking = visibleNode is null ? null : _markingFactory();

            return _factory(
                GetName(id, index), GetNode(id, index), GetTotal(id, index), autoTrackValue, marking,
                GetRequirement(id, index), visibleNode);
        }

        /// <summary>
        /// Returns the section name for the specified <see cref="LocationID"/> and section index.
        /// </summary>
        /// <param name="id">
        ///     The <see cref="LocationID"/>.
        /// </param>
        /// <param name="index">
        ///     A <see cref="int"/> representing the section index.
        /// </param>
        /// <returns>
        ///     A <see cref="string"/> representing the section name.
        /// </returns>
        private static string GetName(LocationID id, int index)
        {
            switch (id)
            {
                case LocationID.LinksHouse:
                    return "By The Door";
                case LocationID.Pedestal:
                    return "Pedestal";
                case LocationID.BlindsHouse when index == 0:
                    return "Main";
                case LocationID.BlindsHouse:
                case LocationID.TheWell when index == 1:
                    return "Bomb";
                case LocationID.BottleVendor:
                    return "Man";
                case LocationID.ChickenHouse:
                    return "Bombable Wall";
                case LocationID.Tavern:
                case LocationID.SahasrahlasHut when index == 0:
                    return "Back Room";
                case LocationID.SickKid:
                    return "By The Bed";
                case LocationID.MagicBat:
                    return "Magic Bowl";
                case LocationID.RaceGame:
                    return "Take This Trash";
                case LocationID.Library:
                    return "On The Shelf";
                case LocationID.MushroomSpot:
                    return "Shroom";
                case LocationID.ForestHideout:
                    return "Hideout";
                case LocationID.CastleSecret when index == 0:
                    return "Uncle";
                case LocationID.CastleSecret:
                    return "Hallway";
                case LocationID.WitchsHut:
                    return "Assistant";
                case LocationID.SahasrahlasHut:
                    return "Saha";
                case LocationID.KingsTomb:
                    return "The Crypt";
                case LocationID.GroveDiggingSpot:
                    return "Hidden Treasure";
                case LocationID.Dam when index == 0:
                    return "Inside";
                case LocationID.Dam:
                    return "Outside";
                case LocationID.Hobo:
                    return "Under The Bridge";
                case LocationID.PyramidLedge:
                case LocationID.ZoraArea when index == 0:
                case LocationID.DesertLedge:
                case LocationID.BumperCave:
                    return "Ledge";
                case LocationID.FatFairy:
                    return "Big Bomb Spot";
                case LocationID.HauntedGrove:
                    return "Stumpy";
                case LocationID.BombosTablet:
                case LocationID.EtherTablet:
                    return "Tablet";
                case LocationID.SouthOfGrove:
                    return "Circle of Bushes";
                case LocationID.DiggingGame:
                    return "Dig For Treasure";
                case LocationID.WaterfallFairy:
                    return "Waterfall Cave";
                case LocationID.ZoraArea:
                    return "King Zora";
                case LocationID.Catfish:
                    return "Ring of Stones";
                case LocationID.CShapedHouse:
                    return "House";
                case LocationID.TreasureGame:
                    return "Prize";
                case LocationID.BombableShack:
                    return "Downstairs";
                case LocationID.Blacksmith:
                    return "Bring Frog Home";
                case LocationID.PurpleChest:
                    return "Gary";
                case LocationID.LakeHyliaIsland:
                case LocationID.FloatingIsland:
                    return "Island";
                case LocationID.MireShack:
                    return "Shack";
                case LocationID.OldMan:
                    return "Old Man";
                case LocationID.SpectacleRock when index == 0:
                case LocationID.ParadoxCave when index == 1:
                    return "Top";
                case LocationID.ParadoxCave:
                    return "Bottom";
                case LocationID.HookshotCave when index == 0:
                    return "Bonkable Chest";
                case LocationID.HookshotCave:
                    return "Back";
                default:
                    return "Cave";
            }
        }

        /// <summary>
        /// Returns the <see cref="INode"/> to which the section belongs for the specified <see cref="LocationID"/> and
        /// section index.
        /// </summary>
        /// <param name="id">
        ///     The <see cref="LocationID"/>.
        /// </param>
        /// <param name="index">
        ///     A <see cref="int"/> representing the section index.
        /// </param>
        /// <returns>
        ///     The <see cref="INode"/> to which the section belongs.
        /// </returns>
        private INode GetNode(LocationID id, int index)
        {
            switch (id)
            {
                case LocationID.LinksHouse:
                    return _overworldNodes[OverworldNodeID.Start];
                case LocationID.Pedestal:
                    return _overworldNodes[OverworldNodeID.Pedestal];
                case LocationID.LumberjackCave:
                    return _overworldNodes[OverworldNodeID.LumberjackCaveHole];
                case LocationID.BlindsHouse when index == 0:
                case LocationID.Tavern:
                case LocationID.Dam when index == 0:
                    return _overworldNodes[OverworldNodeID.LightWorldNotBunnyOrSuperBunnyMirror];
                case LocationID.BlindsHouse:
                case LocationID.TheWell when index == 1:
                case LocationID.ChickenHouse:
                case LocationID.MushroomSpot:
                case LocationID.ForestHideout:
                case LocationID.CastleSecret:
                case LocationID.SahasrahlasHut when index == 0:
                case LocationID.AginahsCave:
                case LocationID.Dam:
                case LocationID.MiniMoldormCave:
                case LocationID.IceRodCave:
                    return _overworldNodes[OverworldNodeID.LightWorldNotBunny];
                case LocationID.TheWell when index == 0:
                    return _overworldNodes[OverworldNodeID.LightWorldNotBunnyOrSuperBunnyFallInHole];
                case LocationID.BottleVendor:
                    return _overworldNodes[OverworldNodeID.LightWorld];
                case LocationID.SickKid:
                    return _overworldNodes[OverworldNodeID.SickKid];
                case LocationID.MagicBat:
                    return _overworldNodes[OverworldNodeID.MagicBat];
                case LocationID.RaceGame:
                    return _overworldNodes[OverworldNodeID.RaceGameLedgeNotBunny];
                case LocationID.Library:
                case LocationID.BonkRocks:
                    return _overworldNodes[OverworldNodeID.LightWorldDash];
                case LocationID.WitchsHut:
                    return _overworldNodes[OverworldNodeID.WitchsHut];
                case LocationID.SahasrahlasHut:
                    return _overworldNodes[OverworldNodeID.Sahasrahla];
                case LocationID.KingsTomb:
                    return _overworldNodes[OverworldNodeID.KingsTombGrave];
                case LocationID.GroveDiggingSpot:
                    return _overworldNodes[OverworldNodeID.GroveDiggingSpot];
                case LocationID.Hobo:
                    return _overworldNodes[OverworldNodeID.Hobo];
                case LocationID.PyramidLedge:
                    return _overworldNodes[OverworldNodeID.DarkWorldEast];
                case LocationID.FatFairy:
                    return _overworldNodes[OverworldNodeID.BigBombToWall];
                case LocationID.HauntedGrove:
                    return _overworldNodes[OverworldNodeID.DarkWorldSouthNotBunny];
                case LocationID.HypeCave:
                case LocationID.DiggingGame:
                    return _overworldNodes[OverworldNodeID.DarkWorldSouthNotBunny];
                case LocationID.BombosTablet:
                    return _overworldNodes[OverworldNodeID.BombosTablet];
                case LocationID.SouthOfGrove:
                    return _overworldNodes[OverworldNodeID.SouthOfGrove];
                case LocationID.WaterfallFairy:
                    return _overworldNodes[OverworldNodeID.WaterfallFairy];
                case LocationID.ZoraArea when index == 0:
                    return _overworldNodes[OverworldNodeID.ZoraLedge];
                case LocationID.ZoraArea:
                    return _overworldNodes[OverworldNodeID.ZoraArea];
                case LocationID.Catfish:
                    return _overworldNodes[OverworldNodeID.CatfishArea];
                case LocationID.GraveyardLedge:
                    return _overworldNodes[OverworldNodeID.LWGraveyardLedge];
                case LocationID.DesertLedge:
                    return _overworldNodes[OverworldNodeID.DesertLedge];
                case LocationID.CShapedHouse:
                case LocationID.TreasureGame:
                    return _overworldNodes[OverworldNodeID.DarkWorldWestNotBunnyOrSuperBunnyMirror];
                case LocationID.BombableShack:
                    return _overworldNodes[OverworldNodeID.DarkWorldWestNotBunny];
                case LocationID.Blacksmith:
                    return _overworldNodes[OverworldNodeID.Blacksmith];
                case LocationID.PurpleChest:
                    return _overworldNodes[OverworldNodeID.PurpleChest];
                case LocationID.HammerPegs:
                    return _overworldNodes[OverworldNodeID.HammerPegs];
                case LocationID.BumperCave:
                    return _overworldNodes[OverworldNodeID.BumperCaveTop];
                case LocationID.LakeHyliaIsland:
                    return _overworldNodes[OverworldNodeID.LakeHyliaIsland];
                case LocationID.MireShack:
                    return _overworldNodes[OverworldNodeID.MireAreaNotBunnyOrSuperBunnyMirror];
                case LocationID.CheckerboardCave:
                    return _overworldNodes[OverworldNodeID.CheckerboardCave];
                case LocationID.OldMan:
                    return _overworldNodes[OverworldNodeID.DeathMountainEntryCaveDark];
                case LocationID.SpectacleRock when index == 0:
                    return _overworldNodes[OverworldNodeID.SpectacleRockTop];
                case LocationID.SpectacleRock:
                    return _overworldNodes[OverworldNodeID.DeathMountainWestBottom];
                case LocationID.EtherTablet:
                    return _overworldNodes[OverworldNodeID.EtherTablet];
                case LocationID.SpikeCave:
                    return _overworldNodes[OverworldNodeID.SpikeCaveChest];
                case LocationID.SpiralCave:
                    return _overworldNodes[OverworldNodeID.SpiralCave];
                case LocationID.ParadoxCave when index == 0:
                    return _overworldNodes[OverworldNodeID.ParadoxCaveNotBunny];
                case LocationID.ParadoxCave:
                    return _overworldNodes[OverworldNodeID.ParadoxCaveTop];
                case LocationID.SuperBunnyCave:
                    return _overworldNodes[OverworldNodeID.SuperBunnyCaveChests];
                case LocationID.HookshotCave when index == 0:
                    return _overworldNodes[OverworldNodeID.HookshotCaveBonkableChest];
                case LocationID.HookshotCave:
                    return _overworldNodes[OverworldNodeID.HookshotCaveBack];
                case LocationID.FloatingIsland:
                    return _overworldNodes[OverworldNodeID.LWFloatingIsland];
                case LocationID.MimicCave:
                    return _overworldNodes[OverworldNodeID.MimicCave];
                default:
                    return _overworldNodes[OverworldNodeID.Inaccessible];
            }
        }

        /// <summary>
        /// Returns the item section total for the specified <see cref="LocationID"/> and section index.
        /// </summary>
        /// <param name="id">
        ///     The <see cref="LocationID"/>.
        /// </param>
        /// <param name="index">
        ///     A <see cref="int"/> representing the section index.
        /// </param>
        /// <returns>
        ///     A <see cref="int"/> representing the item section total.
        /// </returns>
        private static int GetTotal(LocationID id, int index)
        {
            switch (id)
            {
                case LocationID.BlindsHouse when index == 0:
                case LocationID.TheWell when index == 0:
                    return 4;
                case LocationID.SahasrahlasHut when index == 0:
                case LocationID.HookshotCave when index == 1:
                    return 3;
                case LocationID.MiniMoldormCave:
                case LocationID.HypeCave:
                case LocationID.ParadoxCave when index == 1:
                    return 5;
                case LocationID.FatFairy:
                case LocationID.WaterfallFairy:
                case LocationID.ParadoxCave:
                case LocationID.SuperBunnyCave:
                case LocationID.MireShack:
                    return 2;
                default:
                    return 1;
            }
        }

        /// <summary>
        /// Returns the <see cref="INode"/> that provides Inspect <see cref="AccessibilityLevel"/> for the specified
        /// <see cref="LocationID"/> and section index.
        /// </summary>
        /// <param name="id">
        ///     The <see cref="LocationID"/>.
        /// </param>
        /// <param name="index">
        ///     A <see cref="int"/> representing the section index.
        /// </param>
        /// <returns>
        ///     The nullable <see cref="INode"/> that provides Inspect <see cref="AccessibilityLevel"/> for the section.
        /// </returns>
        private INode? GetVisibleNode(LocationID id, int index)
        {
            switch (id)
            {
                case LocationID.LumberjackCave:
                case LocationID.RaceGame:
                case LocationID.Library:
                case LocationID.MushroomSpot:
                case LocationID.ForestHideout:
                case LocationID.DesertLedge:
                case LocationID.LakeHyliaIsland:
                case LocationID.LumberjackCaveEntrance:
                case LocationID.TheWellEntrance:
                case LocationID.MagicBatEntrance:
                case LocationID.ForestHideoutEntrance:
                case LocationID.HoulihanHole:
                case LocationID.SanctuaryGrave: 
                    return _overworldNodes[OverworldNodeID.LightWorld];
                case LocationID.ZoraArea when index == 0: 
                    return _overworldNodes[OverworldNodeID.ZoraArea];
                case LocationID.BumperCave: 
                    return _overworldNodes[OverworldNodeID.DarkWorldWest];
                case LocationID.SpectacleRock when index == 0:
                    return _overworldNodes[OverworldNodeID.DeathMountainWestBottom];
                case LocationID.FloatingIsland:
                    return _overworldNodes[OverworldNodeID.DeathMountainEastTop];
                case LocationID.CastleSecretEntrance:
                    return _overworldNodes[OverworldNodeID.CastleSecretExitArea];
                case LocationID.GanonHole:
                    return _overworldNodes[OverworldNodeID.LightWorldInverted];
                default:
                    return null;
            }
        }

        /// <summary>
        /// Returns the <see cref="IRequirement"/> for the specified <see cref="LocationID"/> and section index.
        /// </summary>
        /// <param name="id">
        ///     The <see cref="LocationID"/>.
        /// </param>
        /// <param name="index">
        ///     A <see cref="int"/> representing the section index.
        /// </param>
        /// <returns>
        ///     The nullable <see cref="IRequirement"/> for the section to be visible.
        /// </returns>
        private IRequirement? GetRequirement(LocationID id, int index)
        {
            switch (id)
            {
                case LocationID.LinksHouse:
                    return _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _worldStateRequirements[WorldState.StandardOpen],
                        _alternativeRequirements[new HashSet<IRequirement>
                        {
                            _entranceShuffleRequirements[EntranceShuffle.None],
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                        }]
                    }];
                case LocationID.Pedestal:
                case LocationID.BottleVendor:
                case LocationID.Tavern:
                case LocationID.RaceGame:
                case LocationID.MushroomSpot:
                case LocationID.Dam when index == 1:
                case LocationID.Hobo:
                case LocationID.PyramidLedge:
                case LocationID.HauntedGrove:
                case LocationID.BombosTablet:
                case LocationID.DiggingGame:
                case LocationID.ZoraArea:
                case LocationID.Catfish:
                case LocationID.DesertLedge:
                case LocationID.Blacksmith:
                case LocationID.PurpleChest:
                case LocationID.LakeHyliaIsland:
                case LocationID.OldMan:
                case LocationID.SpectacleRock when index == 0:
                case LocationID.EtherTablet:
                case LocationID.FloatingIsland:
                    return null;
                case LocationID.LumberjackCave:
                case LocationID.BlindsHouse:
                case LocationID.TheWell:
                case LocationID.ChickenHouse:
                case LocationID.SickKid:
                case LocationID.MagicBat:
                case LocationID.Library:
                case LocationID.ForestHideout:
                case LocationID.CastleSecret:
                case LocationID.WitchsHut:
                case LocationID.SahasrahlasHut:
                case LocationID.BonkRocks:
                case LocationID.KingsTomb:
                case LocationID.AginahsCave:
                case LocationID.GroveDiggingSpot:
                case LocationID.Dam:
                case LocationID.MiniMoldormCave:
                case LocationID.IceRodCave:
                case LocationID.FatFairy:
                case LocationID.HypeCave:
                case LocationID.SouthOfGrove:
                case LocationID.WaterfallFairy:
                case LocationID.GraveyardLedge:
                case LocationID.CShapedHouse:
                case LocationID.TreasureGame:
                case LocationID.BombableShack:
                case LocationID.HammerPegs:
                case LocationID.BumperCave:
                case LocationID.MireShack:
                case LocationID.CheckerboardCave:
                case LocationID.SpectacleRock:
                case LocationID.SpikeCave:
                case LocationID.SpiralCave:
                case LocationID.ParadoxCave:
                case LocationID.SuperBunnyCave:
                case LocationID.HookshotCave:
                case LocationID.MimicCave:
                    return _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _entranceShuffleRequirements[EntranceShuffle.None],
                        _entranceShuffleRequirements[EntranceShuffle.Dungeon]
                    }];
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }
        }
    }
}