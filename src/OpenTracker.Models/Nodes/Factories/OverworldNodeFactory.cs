using System;
using System.Collections.Generic;
using OpenTracker.Models.Nodes.Connections;

namespace OpenTracker.Models.Nodes.Factories
{
    /// <summary>
    /// This class contains creation logic for <see cref="IStartNode"/> and <see cref="IOverworldNode"/> objects.
    /// </summary>
    public class OverworldNodeFactory : IOverworldNodeFactory
    {
        private readonly IStartConnectionFactory _startConnectionFactory;
        private readonly ILightWorldConnectionFactory _lightWorldConnectionFactory;
        private readonly INWLightWorldConnectionFactory _nwLightWorldConnectionFactory;
        private readonly ISLightWorldConnectionFactory _sLightWorldConnectionFactory;
        private readonly INELightWorldConnectionFactory _neLightWorldConnectionFactory;
        private readonly ILWDeathMountainConnectionFactory _lwDeathMountainConnectionFactory;
        private readonly INWDarkWorldConnectionFactory _nwDarkWorldConnectionFactory;
        private readonly ISDarkWorldConnectionFactory _sDarkWorldConnectionFactory;
        private readonly INEDarkWorldConnectionFactory _neDarkWorldConnectionFactory;
        private readonly IDWDeathMountainConnectionFactory _dwDeathMountainConnectionFactory;
        private readonly IDungeonEntryConnectionFactory _dungeonEntryConnectionFactory;

        private readonly IOverworldNode.Factory _factory;
        private readonly IStartNode.Factory _startFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="startConnectionFactory">
        ///     The <see cref="IStartConnectionFactory"/>. 
        /// </param>
        /// <param name="lightWorldConnectionFactory">
        ///     The <see cref="ILightWorldConnectionFactory"/>. 
        /// </param>
        /// <param name="nwLightWorldConnectionFactory">
        ///     The <see cref="INWLightWorldConnectionFactory"/>.
        /// </param>
        /// <param name="sLightWorldConnectionFactory">
        ///     The <see cref="ISLightWorldConnectionFactory"/>.
        /// </param>
        /// <param name="neLightWorldConnectionFactory">
        ///     The <see cref="INELightWorldConnectionFactory"/>.
        /// </param>
        /// <param name="lwDeathMountainConnectionFactory">
        ///     The <see cref="ILWDeathMountainConnectionFactory"/>.
        /// </param>
        /// <param name="nwDarkWorldConnectionFactory">
        ///     The <see cref="INWDarkWorldConnectionFactory"/>.
        /// </param>
        /// <param name="sDarkWorldConnectionFactory">
        ///     The <see cref="ISDarkWorldConnectionFactory"/>.
        /// </param>
        /// <param name="neDarkWorldConnectionFactory">
        ///     The <see cref="INEDarkWorldConnectionFactory"/>.
        /// </param>
        /// <param name="dwDeathMountainConnectionFactory">
        ///     The <see cref="IDWDeathMountainConnectionFactory"/>.
        /// </param>
        /// <param name="dungeonEntryConnectionFactory">
        ///     The <see cref="IDungeonEntryConnectionFactory"/>.
        /// </param>
        /// <param name="factory">
        ///     An Autofac factory for creating new <see cref="IOverworldNode"/> objects.
        /// </param>
        /// <param name="startFactory">
        ///     An Autofac factory for creating new <see cref="IStartNode"/> objects.
        /// </param>
        public OverworldNodeFactory(
            IStartConnectionFactory startConnectionFactory, ILightWorldConnectionFactory lightWorldConnectionFactory,
            INWLightWorldConnectionFactory nwLightWorldConnectionFactory,
            ISLightWorldConnectionFactory sLightWorldConnectionFactory,
            INELightWorldConnectionFactory neLightWorldConnectionFactory,
            ILWDeathMountainConnectionFactory lwDeathMountainConnectionFactory,
            INWDarkWorldConnectionFactory nwDarkWorldConnectionFactory,
            ISDarkWorldConnectionFactory sDarkWorldConnectionFactory,
            INEDarkWorldConnectionFactory neDarkWorldConnectionFactory,
            IDWDeathMountainConnectionFactory dwDeathMountainConnectionFactory,
            IDungeonEntryConnectionFactory dungeonEntryConnectionFactory, IOverworldNode.Factory factory,
            IStartNode.Factory startFactory)
        {
            _startConnectionFactory = startConnectionFactory;
            _lightWorldConnectionFactory = lightWorldConnectionFactory;
            _nwLightWorldConnectionFactory = nwLightWorldConnectionFactory;
            _sLightWorldConnectionFactory = sLightWorldConnectionFactory;
            _neLightWorldConnectionFactory = neLightWorldConnectionFactory;
            _lwDeathMountainConnectionFactory = lwDeathMountainConnectionFactory;
            _nwDarkWorldConnectionFactory = nwDarkWorldConnectionFactory;
            _sDarkWorldConnectionFactory = sDarkWorldConnectionFactory;
            _neDarkWorldConnectionFactory = neDarkWorldConnectionFactory;
            _dwDeathMountainConnectionFactory = dwDeathMountainConnectionFactory;
            _dungeonEntryConnectionFactory = dungeonEntryConnectionFactory;

            _startFactory = startFactory;
            _factory = factory;
        }

        public INode GetOverworldNode(OverworldNodeID id)
        {
            return id == OverworldNodeID.Start ? _startFactory() : _factory();
        }

        public IEnumerable<INodeConnection> GetNodeConnections(OverworldNodeID id, INode node)
        {
            switch (id)
            {
                case OverworldNodeID.Inaccessible:
                    return new List<INodeConnection>();
                case OverworldNodeID.EntranceDungeonAllInsanity:
                case OverworldNodeID.EntranceNone:
                case OverworldNodeID.EntranceNoneInverted:
                case OverworldNodeID.Flute:
                case OverworldNodeID.FluteActivated:
                case OverworldNodeID.FluteInverted:
                case OverworldNodeID.FluteStandardOpen:
                    return _startConnectionFactory.GetNodeConnections(id, node);
                case OverworldNodeID.LightWorld:
                case OverworldNodeID.LightWorldInverted:
                case OverworldNodeID.LightWorldInvertedNotBunny:
                case OverworldNodeID.LightWorldStandardOpen:
                case OverworldNodeID.LightWorldMirror:
                case OverworldNodeID.LightWorldNotBunny:
                case OverworldNodeID.LightWorldNotBunnyOrDungeonRevive:
                case OverworldNodeID.LightWorldNotBunnyOrSuperBunnyFallInHole:
                case OverworldNodeID.LightWorldNotBunnyOrSuperBunnyMirror:
                case OverworldNodeID.LightWorldDash:
                case OverworldNodeID.LightWorldHammer:
                case OverworldNodeID.LightWorldLift1:
                case OverworldNodeID.LightWorldFlute:
                case OverworldNodeID.LightWorldBook:
                    return _lightWorldConnectionFactory.GetNodeConnections(id, node);
                case OverworldNodeID.Pedestal:
                case OverworldNodeID.LumberjackCaveHole:
                case OverworldNodeID.DeathMountainEntry:
                case OverworldNodeID.DeathMountainEntryNonEntrance:
                case OverworldNodeID.DeathMountainEntryCave:
                case OverworldNodeID.DeathMountainEntryCaveDark:
                case OverworldNodeID.DeathMountainExit:
                case OverworldNodeID.DeathMountainExitNonEntrance:
                case OverworldNodeID.DeathMountainExitCave:
                case OverworldNodeID.DeathMountainExitCaveDark:
                case OverworldNodeID.LWKakarikoPortal:
                case OverworldNodeID.LWKakarikoPortalStandardOpen:
                case OverworldNodeID.LWKakarikoPortalNotBunny:
                case OverworldNodeID.SickKid:
                case OverworldNodeID.GrassHouse:
                case OverworldNodeID.BombHut:
                case OverworldNodeID.MagicBatLedge:
                case OverworldNodeID.MagicBat:
                case OverworldNodeID.LWGraveyard:
                case OverworldNodeID.LWGraveyardNotBunny:
                case OverworldNodeID.LWGraveyardLedge:
                case OverworldNodeID.EscapeGrave:
                case OverworldNodeID.KingsTomb:
                case OverworldNodeID.KingsTombNotBunny:
                case OverworldNodeID.KingsTombGrave:
                    return _nwLightWorldConnectionFactory.GetNodeConnections(id, node);
                case OverworldNodeID.RaceGameLedge:
                case OverworldNodeID.RaceGameLedgeNotBunny:
                case OverworldNodeID.SouthOfGroveLedge:
                case OverworldNodeID.SouthOfGrove:
                case OverworldNodeID.GroveDiggingSpot:
                case OverworldNodeID.DesertLedge:
                case OverworldNodeID.DesertLedgeNotBunny:
                case OverworldNodeID.DesertBack:
                case OverworldNodeID.DesertBackNotBunny:
                case OverworldNodeID.CheckerboardLedge:
                case OverworldNodeID.CheckerboardLedgeNotBunny:
                case OverworldNodeID.CheckerboardCave:
                case OverworldNodeID.DesertPalaceFrontEntrance:
                case OverworldNodeID.BombosTabletLedge:
                case OverworldNodeID.BombosTabletLedgeBook:
                case OverworldNodeID.BombosTablet:
                case OverworldNodeID.LWMirePortal:
                case OverworldNodeID.LWMirePortalStandardOpen:
                case OverworldNodeID.LWSouthPortal:
                case OverworldNodeID.LWSouthPortalStandardOpen:
                case OverworldNodeID.LWSouthPortalNotBunny:
                case OverworldNodeID.LWLakeHyliaFakeFlippers:
                case OverworldNodeID.LWLakeHyliaFlippers:
                case OverworldNodeID.LWLakeHyliaWaterWalk:
                case OverworldNodeID.Hobo:
                case OverworldNodeID.LakeHyliaIsland:
                case OverworldNodeID.LakeHyliaFairyIsland:
                case OverworldNodeID.LakeHyliaFairyIslandStandardOpen:
                    return _sLightWorldConnectionFactory.GetNodeConnections(id, node);
                case OverworldNodeID.HyruleCastleTop:
                case OverworldNodeID.HyruleCastleTopInverted:
                case OverworldNodeID.HyruleCastleTopStandardOpen:
                case OverworldNodeID.AgahnimTowerEntrance:
                case OverworldNodeID.CastleSecretExitArea:
                case OverworldNodeID.ZoraArea:
                case OverworldNodeID.ZoraLedge:
                case OverworldNodeID.WaterfallFairy:
                case OverworldNodeID.WaterfallFairyNotBunny:
                case OverworldNodeID.LWWitchArea:
                case OverworldNodeID.LWWitchAreaNotBunny:
                case OverworldNodeID.WitchsHut:
                case OverworldNodeID.Sahasrahla:
                case OverworldNodeID.LWEastPortal:
                case OverworldNodeID.LWEastPortalStandardOpen:
                case OverworldNodeID.LWEastPortalNotBunny:
                    return _neLightWorldConnectionFactory.GetNodeConnections(id, node);
                case OverworldNodeID.DeathMountainWestBottom:
                case OverworldNodeID.DeathMountainWestBottomNonEntrance:
                case OverworldNodeID.DeathMountainWestBottomNotBunny:
                case OverworldNodeID.SpectacleRockTop:
                case OverworldNodeID.DeathMountainWestTop:
                case OverworldNodeID.DeathMountainWestTopBook:
                case OverworldNodeID.DeathMountainWestTopNotBunny:
                case OverworldNodeID.EtherTablet:
                case OverworldNodeID.DeathMountainEastBottom:
                case OverworldNodeID.DeathMountainEastBottomNotBunny:
                case OverworldNodeID.DeathMountainEastBottomLift2:
                case OverworldNodeID.DeathMountainEastBottomConnector:
                case OverworldNodeID.ParadoxCave:
                case OverworldNodeID.ParadoxCaveSuperBunnyFallInHole:
                case OverworldNodeID.ParadoxCaveNotBunny:
                case OverworldNodeID.ParadoxCaveTop:
                case OverworldNodeID.DeathMountainEastTop:
                case OverworldNodeID.DeathMountainEastTopInverted:
                case OverworldNodeID.DeathMountainEastTopNotBunny:
                case OverworldNodeID.DeathMountainEastTopConnector:
                case OverworldNodeID.SpiralCaveLedge:
                case OverworldNodeID.SpiralCave:
                case OverworldNodeID.MimicCaveLedge:
                case OverworldNodeID.MimicCaveLedgeNotBunny:
                case OverworldNodeID.MimicCave:
                case OverworldNodeID.LWFloatingIsland:
                case OverworldNodeID.LWTurtleRockTop:
                case OverworldNodeID.LWTurtleRockTopInverted:
                case OverworldNodeID.LWTurtleRockTopInvertedNotBunny:
                case OverworldNodeID.LWTurtleRockTopStandardOpen:
                    return _lwDeathMountainConnectionFactory.GetNodeConnections(id, node);
                case OverworldNodeID.DWKakarikoPortal:
                case OverworldNodeID.DWKakarikoPortalInverted:
                case OverworldNodeID.DarkWorldWest:
                case OverworldNodeID.DarkWorldWestNotBunny:
                case OverworldNodeID.DarkWorldWestNotBunnyOrSuperBunnyMirror:
                case OverworldNodeID.DarkWorldWestLift2:
                case OverworldNodeID.SkullWoodsBackArea:
                case OverworldNodeID.SkullWoodsBackAreaNotBunny:
                case OverworldNodeID.SkullWoodsBack:
                case OverworldNodeID.BumperCaveEntry:
                case OverworldNodeID.BumperCaveEntryNonEntrance:
                case OverworldNodeID.BumperCaveFront:
                case OverworldNodeID.BumperCaveNotBunny:
                case OverworldNodeID.BumperCavePastGap:
                case OverworldNodeID.BumperCaveBack:
                case OverworldNodeID.BumperCaveTop:
                case OverworldNodeID.HammerHouse:
                case OverworldNodeID.HammerHouseNotBunny:
                case OverworldNodeID.HammerPegsArea:
                case OverworldNodeID.HammerPegs:
                case OverworldNodeID.PurpleChest:
                case OverworldNodeID.BlacksmithPrison:
                case OverworldNodeID.Blacksmith:
                case OverworldNodeID.DWGraveyard:
                case OverworldNodeID.DWGraveyardMirror:
                    return _nwDarkWorldConnectionFactory.GetNodeConnections(id, node);
                case OverworldNodeID.DarkWorldSouth:
                case OverworldNodeID.DarkWorldSouthInverted:
                case OverworldNodeID.DarkWorldSouthStandardOpen:
                case OverworldNodeID.DarkWorldSouthStandardOpenNotBunny:
                case OverworldNodeID.DarkWorldSouthMirror:
                case OverworldNodeID.DarkWorldSouthNotBunny:
                case OverworldNodeID.DarkWorldSouthDash:
                case OverworldNodeID.DarkWorldSouthHammer:
                case OverworldNodeID.BuyBigBomb:
                case OverworldNodeID.BuyBigBombStandardOpen:
                case OverworldNodeID.BigBombToLightWorld:
                case OverworldNodeID.BigBombToLightWorldStandardOpen:
                case OverworldNodeID.BigBombToDWLakeHylia:
                case OverworldNodeID.BigBombToWall:
                case OverworldNodeID.DWSouthPortal:
                case OverworldNodeID.DWSouthPortalInverted:
                case OverworldNodeID.DWSouthPortalNotBunny:
                case OverworldNodeID.MireArea:
                case OverworldNodeID.MireAreaMirror:
                case OverworldNodeID.MireAreaNotBunny:
                case OverworldNodeID.MireAreaNotBunnyOrSuperBunnyMirror:
                case OverworldNodeID.MiseryMireEntrance:
                case OverworldNodeID.DWMirePortal:
                case OverworldNodeID.DWMirePortalInverted:
                case OverworldNodeID.DWLakeHyliaFlippers:
                case OverworldNodeID.DWLakeHyliaFakeFlippers:
                case OverworldNodeID.DWLakeHyliaWaterWalk:
                case OverworldNodeID.IcePalaceIsland:
                case OverworldNodeID.IcePalaceIslandInverted:
                case OverworldNodeID.DarkWorldSouthEast:
                case OverworldNodeID.DarkWorldSouthEastNotBunny:
                case OverworldNodeID.DarkWorldSouthEastLift1:
                    return _sDarkWorldConnectionFactory.GetNodeConnections(id, node);
                case OverworldNodeID.DWWitchArea:
                case OverworldNodeID.DWWitchAreaNotBunny:
                case OverworldNodeID.CatfishArea:
                case OverworldNodeID.DarkWorldEast:
                case OverworldNodeID.DarkWorldEastStandardOpen:
                case OverworldNodeID.DarkWorldEastNotBunny:
                case OverworldNodeID.DarkWorldEastHammer:
                case OverworldNodeID.FatFairyEntrance:
                case OverworldNodeID.DWEastPortal:
                case OverworldNodeID.DWEastPortalInverted:
                case OverworldNodeID.DWEastPortalNotBunny:
                    return _neDarkWorldConnectionFactory.GetNodeConnections(id, node);
                case OverworldNodeID.DarkDeathMountainWestBottom:
                case OverworldNodeID.DarkDeathMountainWestBottomInverted:
                case OverworldNodeID.DarkDeathMountainWestBottomNonEntrance:
                case OverworldNodeID.DarkDeathMountainWestBottomMirror:
                case OverworldNodeID.DarkDeathMountainWestBottomNotBunny:
                case OverworldNodeID.SpikeCavePastHammerBlocks:
                case OverworldNodeID.SpikeCavePastSpikes:
                case OverworldNodeID.SpikeCaveChest:
                case OverworldNodeID.DarkDeathMountainTop:
                case OverworldNodeID.DarkDeathMountainTopInverted:
                case OverworldNodeID.DarkDeathMountainTopStandardOpen:
                case OverworldNodeID.DarkDeathMountainTopMirror:
                case OverworldNodeID.DarkDeathMountainTopNotBunny:
                case OverworldNodeID.SuperBunnyCave:
                case OverworldNodeID.SuperBunnyCaveChests:
                case OverworldNodeID.GanonsTowerEntrance:
                case OverworldNodeID.GanonsTowerEntranceStandardOpen:
                case OverworldNodeID.DWFloatingIsland:
                case OverworldNodeID.HookshotCaveEntrance:
                case OverworldNodeID.HookshotCaveEntranceHookshot:
                case OverworldNodeID.HookshotCaveEntranceHover:
                case OverworldNodeID.HookshotCaveBonkableChest:
                case OverworldNodeID.HookshotCaveBack:
                case OverworldNodeID.DWTurtleRockTop:
                case OverworldNodeID.DWTurtleRockTopInverted:
                case OverworldNodeID.DWTurtleRockTopNotBunny:
                case OverworldNodeID.TurtleRockFrontEntrance:
                case OverworldNodeID.DarkDeathMountainEastBottom:
                case OverworldNodeID.DarkDeathMountainEastBottomInverted:
                case OverworldNodeID.DarkDeathMountainEastBottomConnector:
                case OverworldNodeID.TurtleRockTunnel:
                case OverworldNodeID.TurtleRockTunnelMirror:
                case OverworldNodeID.TurtleRockSafetyDoor:
                    return _dwDeathMountainConnectionFactory.GetNodeConnections(id, node); 
                case OverworldNodeID.GanonHole:
                case OverworldNodeID.HCSanctuaryEntry:
                case OverworldNodeID.HCFrontEntry:
                case OverworldNodeID.HCBackEntry:
                case OverworldNodeID.ATEntry:
                case OverworldNodeID.EPEntry:
                case OverworldNodeID.DPFrontEntry:
                case OverworldNodeID.DPLeftEntry:
                case OverworldNodeID.DPBackEntry:
                case OverworldNodeID.ToHEntry:
                case OverworldNodeID.PoDEntry:
                case OverworldNodeID.SPEntry:
                case OverworldNodeID.SWFrontEntry:
                case OverworldNodeID.SWBackEntry:
                case OverworldNodeID.TTEntry:
                case OverworldNodeID.IPEntry:
                case OverworldNodeID.MMEntry:
                case OverworldNodeID.TRFrontEntry:
                case OverworldNodeID.TRFrontEntryStandardOpen:
                case OverworldNodeID.TRFrontEntryStandardOpenEntranceNone:
                case OverworldNodeID.TRFrontToKeyDoors:
                case OverworldNodeID.TRKeyDoorsToMiddleExit:
                case OverworldNodeID.TRMiddleEntry:
                case OverworldNodeID.TRBackEntry:
                case OverworldNodeID.GTEntry:
                    return _dungeonEntryConnectionFactory.GetNodeConnections(id, node);
            }

            throw new ArgumentOutOfRangeException(nameof(id), id, null);
        }
    }
}
