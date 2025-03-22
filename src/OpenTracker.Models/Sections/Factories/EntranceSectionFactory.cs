using System;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Sections.Entrance;

namespace OpenTracker.Models.Sections.Factories
{
    /// <summary>
    /// This class contains the creation logic for <see cref="IEntranceSection"/> objects.
    /// </summary>
    public class EntranceSectionFactory : IEntranceSectionFactory
    {
        private readonly IAggregateRequirementDictionary _aggregateRequirements;
        private readonly IAlternativeRequirementDictionary _alternativeRequirements;
        private readonly IEntranceShuffleRequirementDictionary _entranceShuffleRequirements;
        private readonly IWorldStateRequirementDictionary _worldStateRequirements;

        private readonly IOverworldNodeDictionary _overworldNodes;
        
        private readonly IEntranceSection.Factory _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="aggregateRequirements">
        ///     The <see cref="IAggregateRequirementDictionary"/>.
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
        /// <param name="overworldNodes">
        ///     The <see cref="IOverworldNodeDictionary"/>.
        /// </param>
        /// <param name="factory">
        ///     An Autofac factory for creating new <see cref="IEntranceSection"/> objects.
        /// </param>
        public EntranceSectionFactory(
            IAggregateRequirementDictionary aggregateRequirements,
            IAlternativeRequirementDictionary alternativeRequirements,
            IEntranceShuffleRequirementDictionary entranceShuffleRequirements,
            IWorldStateRequirementDictionary worldStateRequirements, IOverworldNodeDictionary overworldNodes,
            IEntranceSection.Factory factory)
        {
            _aggregateRequirements = aggregateRequirements;
            _alternativeRequirements = alternativeRequirements;
            _entranceShuffleRequirements = entranceShuffleRequirements;
            _worldStateRequirements = worldStateRequirements;
            
            _overworldNodes = overworldNodes;
            
            _factory = factory;
        }

        public IEntranceSection GetEntranceSection(LocationID id)
        {
            var entranceShuffleLevel = GetEntranceShuffleLevel(id);

            return _factory(
                GetName(id), entranceShuffleLevel, GetRequirement(id, entranceShuffleLevel), GetNode(id),
                GetExitProvided(id));
        }

        /// <summary>
        /// Returns the section name for the specified <see cref="LocationID"/>.
        /// </summary>
        /// <param name="id">
        ///     The <see cref="LocationID"/>.
        /// </param>
        /// <returns>
        ///     A <see cref="string"/> representing the section name.
        /// </returns>
        private static string GetName(LocationID id)
        {
            switch (id)
            {
                case LocationID.LumberjackHouseEntrance:
                case LocationID.KakarikoFortuneTellerEntrance:
                case LocationID.WomanLeftDoor:
                case LocationID.WomanRightDoor:
                case LocationID.LeftSnitchHouseEntrance:
                case LocationID.RightSnitchHouseEntrance:
                case LocationID.BlindsHouseEntrance:
                case LocationID.ChickenHouseEntrance:
                case LocationID.GrassHouseEntrance:
                case LocationID.TavernFront:
                case LocationID.KakarikoShopEntrance:
                case LocationID.BombHutEntrance:
                case LocationID.SickKidEntrance:
                case LocationID.BlacksmithHouse:
                case LocationID.ChestGameEntrance:
                case LocationID.RaceHouseLeft:
                case LocationID.RaceHouseRight:
                case LocationID.LibraryEntrance:
                case LocationID.WitchsHutEntrance:
                case LocationID.SahasrahlasHutEntrance:
                case LocationID.CShapedHouseEntrance:
                case LocationID.HammerHouse:
                case LocationID.DarkVillageFortuneTellerEntrance:
                case LocationID.ShieldShop:
                case LocationID.DarkLumberjack:
                case LocationID.TreasureGameEntrance:
                case LocationID.BombableShackEntrance:
                case LocationID.HammerPegsEntrance:
                case LocationID.BombShop:
                case LocationID.ArrowGameEntrance:
                case LocationID.DarkHyliaFortuneTeller:
                case LocationID.DarkSahasrahlaEntrance:
                case LocationID.DarkWitchsHut:
                case LocationID.FortuneTellerEntrance:
                case LocationID.LakeShop:
                case LocationID.MireShackEntrance:
                case LocationID.MireRightShackEntrance:
                case LocationID.LinksHouseEntrance:
                    return "House";
                case LocationID.ForestChestGameEntrance:
                    return "Log";
                case LocationID.CastleMainEntrance:
                case LocationID.CastleLeftEntrance:
                case LocationID.CastleRightEntrance:
                case LocationID.CastleTowerEntrance:
                case LocationID.EasternPalaceEntrance:
                case LocationID.SanctuaryGrave:
                case LocationID.DesertLeftEntrance:
                case LocationID.DesertBackEntrance:
                case LocationID.DesertRightEntrance:
                case LocationID.DesertFrontEntrance:
                case LocationID.SkullWoodsBack:
                case LocationID.ThievesTownEntrance:
                case LocationID.SwampPalaceEntrance:
                case LocationID.PalaceOfDarknessEntrance:
                case LocationID.IcePalaceEntrance:
                case LocationID.MiseryMireEntrance:
                case LocationID.TowerOfHeraEntrance:
                case LocationID.TurtleRockEntrance:
                case LocationID.GanonsTowerEntrance:
                    return "Dungeon";
                case LocationID.DamEntrance:
                    return "Dam";
                case LocationID.WaterfallFairyEntrance:
                    return "Waterfall Cave";
                case LocationID.CentralBonkRocksEntrance:
                case LocationID.NorthBonkRocks:
                case LocationID.DarkCentralBonkRocksEntrance:
                    return "Cave Under Rocks";
                case LocationID.KingsTombEntrance:
                    return "Crypt";
                case LocationID.FatFairyEntrance:
                    return "Big Bomb";
                case LocationID.ForestHideoutExit:
                    return "Stump";
                case LocationID.CastleSecretExit:
                    return "Secret Passage";
                case LocationID.Sanctuary:
                    return "Sanc";
                case LocationID.LumberjackCaveExit:
                case LocationID.TheWellExit:
                case LocationID.MagicBatExit:
                case LocationID.HoulihanHoleExit:
                case LocationID.GanonHoleExit:
                case LocationID.SkullWoodsWestEntrance:
                case LocationID.SkullWoodsCenterEntrance:
                case LocationID.SkullWoodsEastEntrance:
                    return "Skull";
                case LocationID.SkullWoodsNWHole:
                case LocationID.SkullWoodsSWHole:
                case LocationID.SkullWoodsSEHole:
                case LocationID.SkullWoodsNEHole:
                    return "Dropdown";
                default:
                    return "Cave";
            }
        }

        /// <summary>
        /// Returns the <see cref="EntranceShuffle"/> level for the specified <see cref="LocationID"/>.
        /// </summary>
        /// <param name="id">
        ///     The <see cref="LocationID"/>.
        /// </param>
        /// <returns>
        ///     The <see cref="EntranceShuffle"/> level of the section.
        /// </returns>
        private static EntranceShuffle GetEntranceShuffleLevel(LocationID id)
        {
            switch (id)
            {
                case LocationID.LumberjackHouseEntrance:
                case LocationID.DeathMountainEntryCave:
                case LocationID.DeathMountainExitCave:
                case LocationID.KakarikoFortuneTellerEntrance:
                case LocationID.WomanLeftDoor:
                case LocationID.WomanRightDoor:
                case LocationID.LeftSnitchHouseEntrance:
                case LocationID.RightSnitchHouseEntrance:
                case LocationID.BlindsHouseEntrance:
                case LocationID.ChickenHouseEntrance:
                case LocationID.GrassHouseEntrance:
                case LocationID.TavernFront:
                case LocationID.KakarikoShopEntrance:
                case LocationID.BombHutEntrance:
                case LocationID.SickKidEntrance:
                case LocationID.BlacksmithHouse:
                case LocationID.ChestGameEntrance:
                case LocationID.RaceHouseLeft:
                case LocationID.RaceHouseRight:
                case LocationID.LibraryEntrance:
                case LocationID.ForestChestGameEntrance:
                case LocationID.DamEntrance:
                case LocationID.CentralBonkRocksEntrance:
                case LocationID.WitchsHutEntrance:
                case LocationID.WaterfallFairyEntrance:
                case LocationID.SahasrahlasHutEntrance:
                case LocationID.TreesFairyCaveEntrance:
                case LocationID.PegsFairyCaveEntrance:
                case LocationID.NorthBonkRocks:
                case LocationID.KingsTombEntrance:
                case LocationID.GraveyardLedgeEntrance:
                case LocationID.AginahsCaveEntrance:
                case LocationID.ThiefCaveEntrance:
                case LocationID.RupeeCaveEntrance:
                case LocationID.CShapedHouseEntrance:
                case LocationID.HammerHouse:
                case LocationID.DarkVillageFortuneTellerEntrance:
                case LocationID.DarkChapelEntrance:
                case LocationID.ShieldShop:
                case LocationID.DarkLumberjack:
                case LocationID.TreasureGameEntrance:
                case LocationID.BombableShackEntrance:
                case LocationID.HammerPegsEntrance:
                case LocationID.BumperCaveExit:
                case LocationID.BumperCaveEntrance:
                case LocationID.HypeCaveEntrance:
                case LocationID.DarkCentralBonkRocksEntrance:
                case LocationID.SouthOfGroveEntrance:
                case LocationID.BombShop:
                case LocationID.ArrowGameEntrance:
                case LocationID.DarkHyliaFortuneTeller:
                case LocationID.DarkTreesFairyCaveEntrance:
                case LocationID.DarkSahasrahlaEntrance:
                case LocationID.DarkWitchsHut:
                case LocationID.DarkFluteSpotFiveEntrance:
                case LocationID.FatFairyEntrance:
                case LocationID.DarkIceRodCaveEntrance:
                case LocationID.DarkFakeIceRodCaveEntrance:
                case LocationID.DarkIceRodRockEntrance:
                case LocationID.HypeFairyCaveEntrance:
                case LocationID.FortuneTellerEntrance:
                case LocationID.LakeShop:
                case LocationID.UpgradeFairy:
                case LocationID.MiniMoldormCaveEntrance:
                case LocationID.IceRodCaveEntrance:
                case LocationID.IceBeeCaveEntrance:
                case LocationID.IceFairyCaveEntrance:
                case LocationID.MireShackEntrance:
                case LocationID.MireRightShackEntrance:
                case LocationID.MireCaveEntrance:
                case LocationID.CheckerboardCaveEntrance:
                case LocationID.DeathMountainEntranceBack:
                case LocationID.OldManResidence:
                case LocationID.OldManBackResidence:
                case LocationID.DeathMountainExitFront:
                case LocationID.SpectacleRockLeft:
                case LocationID.SpectacleRockRight:
                case LocationID.SpectacleRockTop:
                case LocationID.SpikeCaveEntrance:
                case LocationID.DarkMountainFairyEntrance:
                case LocationID.SpiralCaveBottom:
                case LocationID.EDMFairyCaveEntrance:
                case LocationID.ParadoxCaveMiddle:
                case LocationID.ParadoxCaveBottom:
                case LocationID.EDMConnectorBottom:
                case LocationID.SpiralCaveTop:
                case LocationID.MimicCaveEntrance:
                case LocationID.EDMConnectorTop:
                case LocationID.ParadoxCaveTop:
                case LocationID.SuperBunnyCaveBottom:
                case LocationID.DeathMountainShopEntrance:
                case LocationID.SuperBunnyCaveTop:
                case LocationID.HookshotCaveEntrance:
                case LocationID.LinksHouseEntrance:
                case LocationID.HookshotCaveTop:
                    return EntranceShuffle.All;
                case LocationID.CastleMainEntrance:
                case LocationID.CastleLeftEntrance:
                case LocationID.CastleRightEntrance:
                case LocationID.CastleTowerEntrance:
                case LocationID.EasternPalaceEntrance:
                case LocationID.DesertLeftEntrance:
                case LocationID.DesertBackEntrance:
                case LocationID.DesertRightEntrance:
                case LocationID.DesertFrontEntrance:
                case LocationID.SkullWoodsBack:
                case LocationID.ThievesTownEntrance:
                case LocationID.SwampPalaceEntrance:
                case LocationID.PalaceOfDarknessEntrance:
                case LocationID.IcePalaceEntrance:
                case LocationID.MiseryMireEntrance:
                case LocationID.TowerOfHeraEntrance:
                case LocationID.TurtleRockEntrance:
                case LocationID.GanonsTowerEntrance:
                case LocationID.TRLedgeLeft:
                case LocationID.TRLedgeRight:
                case LocationID.TRSafetyDoor:
                    return EntranceShuffle.Dungeon;
                case LocationID.LumberjackCaveExit:
                case LocationID.TheWellExit:
                case LocationID.MagicBatExit:
                case LocationID.ForestHideoutExit:
                case LocationID.CastleSecretExit:
                case LocationID.HoulihanHoleExit:
                case LocationID.Sanctuary:
                case LocationID.GanonHoleExit:
                case LocationID.SkullWoodsWestEntrance:
                case LocationID.SkullWoodsCenterEntrance:
                case LocationID.SkullWoodsEastEntrance:
                case LocationID.SkullWoodsNWHole:
                case LocationID.SkullWoodsSWHole:
                case LocationID.SkullWoodsSEHole:
                case LocationID.SkullWoodsNEHole:
                    return EntranceShuffle.Insanity;
                default:
                    return EntranceShuffle.None;
            }
        }

        /// <summary>
        /// Returns the <see cref="IRequirement"/> for the section to be active for the specified
        /// <see cref="LocationID"/> and <see cref="EntranceShuffle"/> level.
        /// </summary>
        /// <param name="id">
        ///     The <see cref="LocationID"/>.
        /// </param>
        /// <param name="entranceShuffleLevel">
        ///     The <see cref="EntranceShuffle"/> level.
        /// </param>
        /// <returns>
        ///     The <see cref="IRequirement"/> for the section to be active.
        /// </returns>
        private IRequirement GetRequirement(LocationID id, EntranceShuffle entranceShuffleLevel)
        {
            switch (id)
            {
                case LocationID.LinksHouseEntrance:
                    return _aggregateRequirements[
                        [
                            _alternativeRequirements[
                                [
                                    _entranceShuffleRequirements[EntranceShuffle.All],
                                    _entranceShuffleRequirements[EntranceShuffle.Insanity]
                                ]
                            ],
                            _worldStateRequirements[WorldState.Inverted]
                        ]
                    ];
                case LocationID.GanonHoleExit:
                    return _aggregateRequirements[
                        [
                            _entranceShuffleRequirements[EntranceShuffle.Insanity],
                            _worldStateRequirements[WorldState.Inverted]
                        ]
                    ];
            }

            switch (entranceShuffleLevel)
            {
                case EntranceShuffle.Dungeon:
                    return _alternativeRequirements[
                        [
                            _entranceShuffleRequirements[EntranceShuffle.Dungeon],
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity]
                        ]
                    ];
                case EntranceShuffle.All:
                    return _alternativeRequirements[
                        [
                            _entranceShuffleRequirements[EntranceShuffle.All],
                            _entranceShuffleRequirements[EntranceShuffle.Insanity]
                        ]
                    ];
                case EntranceShuffle.Insanity:
                    return _entranceShuffleRequirements[EntranceShuffle.Insanity];
                default:
                    throw new ArgumentOutOfRangeException(nameof(entranceShuffleLevel), entranceShuffleLevel, null);
            }
        }

        /// <summary>
        /// Returns the <see cref="INode"/> for the specified <see cref="LocationID"/>.
        /// </summary>
        /// <param name="id">
        ///     The <see cref="LocationID"/>.
        /// </param>
        /// <returns>
        ///     The <see cref="INode"/> to which the section belongs.
        /// </returns>
        private INode GetNode(LocationID id)
        {
            switch (id)
            {
                case LocationID.DeathMountainEntryCave:
                    return _overworldNodes[OverworldNodeID.DeathMountainEntry];
                case LocationID.DeathMountainExitCave:
                    return _overworldNodes[OverworldNodeID.DeathMountainExit];
                case LocationID.GrassHouseEntrance:
                    return _overworldNodes[OverworldNodeID.GrassHouse];
                case LocationID.BombHutEntrance:
                    return _overworldNodes[OverworldNodeID.BombHut];
                case LocationID.RaceHouseLeft:
                    return _overworldNodes[OverworldNodeID.RaceGameLedge];
                case LocationID.CastleLeftEntrance:
                case LocationID.CastleRightEntrance:
                    return _overworldNodes[OverworldNodeID.HyruleCastleTop];
                case LocationID.CastleTowerEntrance:
                    return _overworldNodes[OverworldNodeID.AgahnimTowerEntrance];
                case LocationID.CentralBonkRocksEntrance:
                case LocationID.NorthBonkRocks:
                    return _overworldNodes[OverworldNodeID.LightWorldDash];
                case LocationID.WitchsHutEntrance:
                    return _overworldNodes[OverworldNodeID.LWWitchArea];
                case LocationID.WaterfallFairyEntrance:
                    return _overworldNodes[OverworldNodeID.WaterfallFairy];
                case LocationID.KingsTombEntrance:
                    return _overworldNodes[OverworldNodeID.KingsTombGrave];
                case LocationID.GraveyardLedgeEntrance:
                    return _overworldNodes[OverworldNodeID.LWGraveyardLedge];
                case LocationID.DesertLeftEntrance:
                    return _overworldNodes[OverworldNodeID.DesertLedge];
                case LocationID.DesertBackEntrance:
                    return _overworldNodes[OverworldNodeID.DesertBack];
                case LocationID.DesertRightEntrance:
                    return _overworldNodes[OverworldNodeID.Inaccessible];
                case LocationID.DesertFrontEntrance:
                    return _overworldNodes[OverworldNodeID.DesertPalaceFrontEntrance];
                case LocationID.RupeeCaveEntrance:
                case LocationID.IceFairyCaveEntrance:
                    return _overworldNodes[OverworldNodeID.LightWorldLift1];
                case LocationID.SkullWoodsBack:
                    return _overworldNodes[OverworldNodeID.SkullWoodsBack];
                case LocationID.ThievesTownEntrance:
                case LocationID.BombableShackEntrance:
                case LocationID.SkullWoodsNEHole:
                    return _overworldNodes[OverworldNodeID.DarkWorldWestNotBunny];
                case LocationID.CShapedHouseEntrance:
                case LocationID.DarkVillageFortuneTellerEntrance:
                case LocationID.DarkChapelEntrance:
                case LocationID.ShieldShop:
                case LocationID.DarkLumberjack:
                case LocationID.TreasureGameEntrance:
                case LocationID.SkullWoodsCenterEntrance:
                case LocationID.SkullWoodsEastEntrance:
                case LocationID.SkullWoodsSWHole:
                case LocationID.SkullWoodsSEHole:
                    return _overworldNodes[OverworldNodeID.DarkWorldWest];
                case LocationID.HammerHouse:
                    return _overworldNodes[OverworldNodeID.HammerHouse];
                case LocationID.HammerPegsEntrance:
                    return _overworldNodes[OverworldNodeID.HammerPegs];
                case LocationID.BumperCaveExit:
                    return _overworldNodes[OverworldNodeID.BumperCaveTop];
                case LocationID.BumperCaveEntrance:
                    return _overworldNodes[OverworldNodeID.BumperCaveEntry];
                case LocationID.HypeCaveEntrance:
                    return _overworldNodes[OverworldNodeID.DarkWorldSouthNotBunny];
                case LocationID.SwampPalaceEntrance:
                case LocationID.BombShop:
                case LocationID.ArrowGameEntrance:
                case LocationID.DarkHyliaFortuneTeller:
                    return _overworldNodes[OverworldNodeID.DarkWorldSouth];
                case LocationID.DarkCentralBonkRocksEntrance:
                    return _overworldNodes[OverworldNodeID.DarkWorldSouthDash];
                case LocationID.SouthOfGroveEntrance:
                    return _overworldNodes[OverworldNodeID.SouthOfGroveLedge];
                case LocationID.DarkTreesFairyCaveEntrance:
                case LocationID.DarkSahasrahlaEntrance:
                case LocationID.DarkFluteSpotFiveEntrance:
                    return _overworldNodes[OverworldNodeID.DarkWorldEast];
                case LocationID.PalaceOfDarknessEntrance:
                    return _overworldNodes[OverworldNodeID.DarkWorldEastNotBunny];
                case LocationID.DarkWitchsHut:
                    return _overworldNodes[OverworldNodeID.DWWitchArea];
                case LocationID.FatFairyEntrance:
                    return _overworldNodes[OverworldNodeID.FatFairyEntrance];
                case LocationID.DarkIceRodCaveEntrance:
                    return _overworldNodes[OverworldNodeID.DarkWorldSouthEastNotBunny];
                case LocationID.DarkFakeIceRodCaveEntrance:
                    return _overworldNodes[OverworldNodeID.DarkWorldSouthEast];
                case LocationID.DarkIceRodRockEntrance:
                    return _overworldNodes[OverworldNodeID.DarkWorldSouthEastLift1];
                case LocationID.HypeFairyCaveEntrance:
                case LocationID.MiniMoldormCaveEntrance:
                case LocationID.IceRodCaveEntrance:
                    return _overworldNodes[OverworldNodeID.LightWorldNotBunny];
                case LocationID.UpgradeFairy:
                    return _overworldNodes[OverworldNodeID.LakeHyliaFairyIsland];
                case LocationID.IcePalaceEntrance:
                    return _overworldNodes[OverworldNodeID.IcePalaceIsland];
                case LocationID.MiseryMireEntrance:
                    return _overworldNodes[OverworldNodeID.MiseryMireEntrance];
                case LocationID.MireShackEntrance:
                case LocationID.MireRightShackEntrance:
                case LocationID.MireCaveEntrance:
                    return _overworldNodes[OverworldNodeID.MireArea];
                case LocationID.CheckerboardCaveEntrance:
                    return _overworldNodes[OverworldNodeID.CheckerboardCave];
                case LocationID.DeathMountainEntranceBack:
                case LocationID.OldManResidence:
                case LocationID.OldManBackResidence:
                case LocationID.DeathMountainExitFront:
                case LocationID.SpectacleRockLeft:
                case LocationID.SpectacleRockRight:
                case LocationID.SpectacleRockTop:
                    return _overworldNodes[OverworldNodeID.DeathMountainWestBottom];
                case LocationID.SpikeCaveEntrance:
                case LocationID.DarkMountainFairyEntrance:
                    return _overworldNodes[OverworldNodeID.DarkDeathMountainWestBottom];
                case LocationID.TowerOfHeraEntrance:
                    return _overworldNodes[OverworldNodeID.DeathMountainWestTop];
                case LocationID.SpiralCaveBottom:
                case LocationID.EDMFairyCaveEntrance:
                case LocationID.ParadoxCaveMiddle:
                case LocationID.ParadoxCaveBottom:
                    return _overworldNodes[OverworldNodeID.DeathMountainEastBottom];
                case LocationID.EDMConnectorBottom:
                    return _overworldNodes[OverworldNodeID.DeathMountainEastBottomConnector];
                case LocationID.SpiralCaveTop:
                    return _overworldNodes[OverworldNodeID.SpiralCaveLedge];
                case LocationID.MimicCaveEntrance:
                    return _overworldNodes[OverworldNodeID.MimicCaveLedge];
                case LocationID.EDMConnectorTop:
                    return _overworldNodes[OverworldNodeID.DeathMountainEastTopConnector];
                case LocationID.ParadoxCaveTop:
                    return _overworldNodes[OverworldNodeID.DeathMountainEastTop];
                case LocationID.SuperBunnyCaveBottom:
                case LocationID.DeathMountainShopEntrance:
                    return _overworldNodes[OverworldNodeID.DarkDeathMountainEastBottom];
                case LocationID.SuperBunnyCaveTop:
                    return _overworldNodes[OverworldNodeID.DarkDeathMountainTop];
                case LocationID.HookshotCaveEntrance:
                    return _overworldNodes[OverworldNodeID.HookshotCaveEntrance];
                case LocationID.TurtleRockEntrance:
                    return _overworldNodes[OverworldNodeID.TurtleRockFrontEntrance];
                case LocationID.GanonsTowerEntrance:
                    return _overworldNodes[OverworldNodeID.GanonsTowerEntrance];
                case LocationID.TRLedgeLeft:
                case LocationID.TRLedgeRight:
                    return _overworldNodes[OverworldNodeID.TurtleRockTunnel];
                case LocationID.TRSafetyDoor:
                    return _overworldNodes[OverworldNodeID.TurtleRockSafetyDoor];
                case LocationID.HookshotCaveTop:
                    return _overworldNodes[OverworldNodeID.DWFloatingIsland];
                case LocationID.GanonHoleExit:
                    return _overworldNodes[OverworldNodeID.LightWorldInverted];
                case LocationID.SkullWoodsWestEntrance:
                case LocationID.SkullWoodsNWHole:
                    return _overworldNodes[OverworldNodeID.SkullWoodsBackArea];
                default:
                    return _overworldNodes[OverworldNodeID.LightWorld];
            }
        }

        /// <summary>
        /// Returns the <see cref="IOverworldNode"/> exit that the section provides for the specified
        /// <see cref="LocationID"/>.
        /// </summary>
        /// <param name="id">
        ///     The <see cref="LocationID"/>.
        /// </param>
        /// <returns>
        ///     The <see cref="IOverworldNode"/> exit that the section provides.
        /// </returns>
        private IOverworldNode? GetExitProvided(LocationID id)
        {
            switch (id)
            {
                case LocationID.LumberjackHouseEntrance:
                case LocationID.KakarikoFortuneTellerEntrance:
                case LocationID.WomanLeftDoor:
                case LocationID.WomanRightDoor:
                case LocationID.LeftSnitchHouseEntrance:
                case LocationID.RightSnitchHouseEntrance:
                case LocationID.BlindsHouseEntrance:
                case LocationID.ChickenHouseEntrance:
                case LocationID.TavernFront:
                case LocationID.KakarikoShopEntrance:
                case LocationID.SickKidEntrance:
                case LocationID.BlacksmithHouse:
                case LocationID.ChestGameEntrance:
                case LocationID.RaceHouseRight:
                case LocationID.LibraryEntrance:
                case LocationID.ForestChestGameEntrance:
                case LocationID.CastleMainEntrance:
                case LocationID.DamEntrance:
                case LocationID.CentralBonkRocksEntrance:
                case LocationID.SahasrahlasHutEntrance:
                case LocationID.TreesFairyCaveEntrance:
                case LocationID.PegsFairyCaveEntrance:
                case LocationID.EasternPalaceEntrance:
                case LocationID.NorthBonkRocks:
                case LocationID.DesertRightEntrance:
                case LocationID.AginahsCaveEntrance:
                case LocationID.ThiefCaveEntrance:
                case LocationID.RupeeCaveEntrance:
                case LocationID.HypeFairyCaveEntrance:
                case LocationID.FortuneTellerEntrance:
                case LocationID.LakeShop:
                case LocationID.MiniMoldormCaveEntrance:
                case LocationID.IceRodCaveEntrance:
                case LocationID.IceBeeCaveEntrance:
                case LocationID.IceFairyCaveEntrance:
                case LocationID.LinksHouseEntrance:
                case LocationID.LumberjackCaveExit:
                case LocationID.TheWellExit:
                case LocationID.MagicBatExit:
                case LocationID.ForestHideoutExit:
                case LocationID.HoulihanHoleExit:
                case LocationID.Sanctuary:
                case LocationID.GanonHoleExit:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.LightWorld];
                case LocationID.DeathMountainEntryCave:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.DeathMountainEntry];
                case LocationID.DeathMountainExitCave:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.DeathMountainExit];
                case LocationID.GrassHouseEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.GrassHouse];
                case LocationID.BombHutEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.BombHut];
                case LocationID.RaceHouseLeft:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.RaceGameLedge];
                case LocationID.CastleLeftEntrance:
                case LocationID.CastleRightEntrance:
                case LocationID.CastleTowerEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.HyruleCastleTop];
                case LocationID.WitchsHutEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.LWWitchArea];
                case LocationID.WaterfallFairyEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.WaterfallFairy];
                case LocationID.KingsTombEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.KingsTomb];
                case LocationID.GraveyardLedgeEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.LWGraveyardLedge];
                case LocationID.DesertLeftEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.DesertLedge];
                case LocationID.DesertBackEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.DesertBack];
                case LocationID.SkullWoodsBack:
                case LocationID.SkullWoodsWestEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.SkullWoodsBackArea];
                case LocationID.HammerHouse:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.HammerHouse];
                case LocationID.ThievesTownEntrance:
                case LocationID.CShapedHouseEntrance:
                case LocationID.DarkVillageFortuneTellerEntrance:
                case LocationID.DarkChapelEntrance:
                case LocationID.ShieldShop:
                case LocationID.DarkLumberjack:
                case LocationID.TreasureGameEntrance:
                case LocationID.BombableShackEntrance:
                case LocationID.SkullWoodsCenterEntrance:
                case LocationID.SkullWoodsEastEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.DarkWorldWest];
                case LocationID.HammerPegsEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.HammerPegsArea];
                case LocationID.BumperCaveExit:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.BumperCaveTop];
                case LocationID.BumperCaveEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.BumperCaveEntry];
                case LocationID.HypeCaveEntrance:
                case LocationID.SwampPalaceEntrance:
                case LocationID.DarkCentralBonkRocksEntrance:
                case LocationID.BombShop:
                case LocationID.ArrowGameEntrance:
                case LocationID.DarkHyliaFortuneTeller:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.DarkWorldSouth];
                case LocationID.SouthOfGroveEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.SouthOfGroveLedge];
                case LocationID.DarkTreesFairyCaveEntrance:
                case LocationID.DarkSahasrahlaEntrance:
                case LocationID.PalaceOfDarknessEntrance:
                case LocationID.DarkFluteSpotFiveEntrance:
                case LocationID.FatFairyEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.DarkWorldEast];
                case LocationID.DarkWitchsHut:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.DWWitchArea];
                case LocationID.DarkIceRodCaveEntrance:
                case LocationID.DarkFakeIceRodCaveEntrance:
                case LocationID.DarkIceRodRockEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.DarkWorldSouthEast];
                case LocationID.UpgradeFairy:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.LakeHyliaFairyIsland];
                case LocationID.IcePalaceEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.IcePalaceIsland];
                case LocationID.MiseryMireEntrance:
                case LocationID.MireShackEntrance:
                case LocationID.MireRightShackEntrance:
                case LocationID.MireCaveEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.MireArea];
                case LocationID.CheckerboardCaveEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.CheckerboardLedge];
                case LocationID.DeathMountainEntranceBack:
                case LocationID.OldManResidence:
                case LocationID.OldManBackResidence:
                case LocationID.DeathMountainExitFront:
                case LocationID.SpectacleRockLeft:
                case LocationID.SpectacleRockRight:
                case LocationID.SpectacleRockTop:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.DeathMountainWestBottom];
                case LocationID.SpikeCaveEntrance:
                case LocationID.DarkMountainFairyEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.DarkDeathMountainWestBottom];
                case LocationID.TowerOfHeraEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.DeathMountainWestTop];
                case LocationID.SpiralCaveBottom:
                case LocationID.EDMFairyCaveEntrance:
                case LocationID.ParadoxCaveMiddle:
                case LocationID.ParadoxCaveBottom:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.DeathMountainEastBottom];
                case LocationID.EDMConnectorBottom:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.DeathMountainEastBottomConnector];
                case LocationID.SpiralCaveTop:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.SpiralCaveLedge];
                case LocationID.MimicCaveEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.MimicCaveLedge];
                case LocationID.EDMConnectorTop:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.DeathMountainEastTopConnector];
                case LocationID.ParadoxCaveTop:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.DeathMountainEastTop];
                case LocationID.SuperBunnyCaveBottom:
                case LocationID.DeathMountainShopEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.DarkDeathMountainEastBottom];
                case LocationID.SuperBunnyCaveTop:
                case LocationID.HookshotCaveEntrance:
                case LocationID.TurtleRockEntrance:
                case LocationID.GanonsTowerEntrance:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.DarkDeathMountainTop];
                case LocationID.TRLedgeLeft:
                case LocationID.TRLedgeRight:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.TurtleRockTunnel];
                case LocationID.TRSafetyDoor:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.TurtleRockSafetyDoor];
                case LocationID.HookshotCaveTop:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.DWFloatingIsland];
                case LocationID.CastleSecretExit:
                    return (IOverworldNode)_overworldNodes[OverworldNodeID.CastleSecretExitArea];
                default:
                    return null;
            }
        }
    }
}