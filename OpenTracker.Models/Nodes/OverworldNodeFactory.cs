using System.Collections.Generic;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Nodes
{
    /// <summary>
    /// This class contains creation logic for requirement node data.
    /// </summary>
    public class OverworldNodeFactory : IOverworldNodeFactory
    {
        private readonly IRequirementDictionary _requirements;
        private readonly IOverworldNodeDictionary _requirementNodes;
        
        private readonly IOverworldNode.Factory _factory;
        private readonly IStartNode.Factory _startFactory;
        private readonly NodeConnection.Factory _connectionFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requirements">
        /// The requirement dictionary.
        /// </param>
        /// <param name="requirementNodes">
        /// The requirement node dictionary.
        /// </param>
        /// <param name="factory">
        /// An Autofac factory for creating requirement nodes.
        /// </param>
        /// <param name="startFactory">
        /// An Autofac factory for creating the start requirement node.
        /// </param>
        /// <param name="connectionFactory">
        /// An Autofac factory for creating node connections.
        /// </param>
        public OverworldNodeFactory(
            IRequirementDictionary requirements, IOverworldNodeDictionary requirementNodes,
            IOverworldNode.Factory factory, IStartNode.Factory startFactory,
            NodeConnection.Factory connectionFactory)
        {
            _requirements = requirements;
            _requirementNodes = requirementNodes;
            _factory = factory;
            _connectionFactory = connectionFactory;
            _startFactory = startFactory;
        }

        /// <summary>
        ///     Populates the list of connections to the specified requirement node ID.
        /// </summary>
        /// <param name="id">
        ///     The requirement node ID.
        /// </param>
        /// <param name="node">
        ///     The node.
        /// </param>
        public IEnumerable<INodeConnection> GetNodeConnections(OverworldNodeID id, INode node)
        {
            var connections = new List<INodeConnection>();
            
            switch (id)
            {
                case OverworldNodeID.EntranceDungeonAllInsanity:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.Start], node,
                            _requirements[RequirementType.EntranceShuffleDungeonAllInsanity]));
                    }
                    break;
                case OverworldNodeID.EntranceNone:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.Start], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                    }
                    break;
                case OverworldNodeID.EntranceNoneInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EntranceNone], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case OverworldNodeID.Flute:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.Start], node,
                            _requirements[RequirementType.Flute]));
                    }
                    break;
                case OverworldNodeID.FluteActivated:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.Flute], node,
                            _requirements[RequirementType.FluteActivated]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldFlute], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.LightWorld:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.Start], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEntry], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainExit], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWKakarikoPortalNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWKakarikoPortalNotBunny], node,
                            _requirements[RequirementType.Gloves2]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DesertLedge], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.GrassHouse], node,
                            _requirements[RequirementType.NotBunnyLW]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BombHut], node,
                            _requirements[RequirementType.NotBunnyLW]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.RaceGameLedge], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.CastleSecretExitArea], node,
                            _requirements[RequirementType.NotBunnyLW]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.SouthOfGroveLedge], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.CheckerboardLedge], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWMirePortal], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWSouthPortalNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWWitchAreaNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWEastPortalNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouthInverted], node,
                            _requirements[RequirementType.Aga1]));
                    }
                    break;
                case OverworldNodeID.LightWorldInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorld], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case OverworldNodeID.LightWorldInvertedNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldInverted], node,
                            _requirements[RequirementType.MoonPearl]));
                    }
                    break;
                case OverworldNodeID.LightWorldStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorld], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case OverworldNodeID.LightWorldMirror:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorld], node,
                            _requirements[RequirementType.LWMirror]));
                    }
                    break;
                case OverworldNodeID.LightWorldNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorld], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case OverworldNodeID.LightWorldNotBunnyOrDungeonRevive:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorld], node,
                            _requirements[RequirementType.DungeonRevive]));
                    }
                    break;
                case OverworldNodeID.LightWorldNotBunnyOrSuperBunnyFallInHole:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorld], node,
                            _requirements[RequirementType.SuperBunnyFallInHole]));
                    }
                    break;
                case OverworldNodeID.LightWorldNotBunnyOrSuperBunnyMirror:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorld], node,
                            _requirements[RequirementType.SuperBunnyMirror]));
                    }
                    break;
                case OverworldNodeID.LightWorldDash:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.Boots]));
                    }
                    break;
                case OverworldNodeID.LightWorldHammer:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                    }
                    break;
                case OverworldNodeID.LightWorldLift1:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case OverworldNodeID.LightWorldFlute:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.Flute]));
                    }
                    break;
                case OverworldNodeID.FluteInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.FluteActivated], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case OverworldNodeID.FluteStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.FluteActivated], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case OverworldNodeID.Pedestal:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorld], node,
                            _requirements[RequirementType.Pedestal]));
                    }
                    break;
                case OverworldNodeID.LumberjackCaveHole:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldDash], node,
                            _requirements[RequirementType.Aga1]));
                    }
                    break;
                case OverworldNodeID.DeathMountainEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldLift1], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BumperCaveEntry], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case OverworldNodeID.DeathMountainEntryNonEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEntry], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                    }
                    break;
                case OverworldNodeID.DeathMountainEntryCave:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEntryNonEntrance], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainWestBottomNonEntrance], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BumperCaveEntryNonEntrance], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainWestBottomNonEntrance], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case OverworldNodeID.DeathMountainEntryCaveDark:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEntryCave], node,
                            _requirements[RequirementType.DarkRoomDeathMountainEntry]));
                    }
                    break;
                case OverworldNodeID.DeathMountainExit:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainExitCaveDark], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BumperCaveBack], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BumperCaveTop], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case OverworldNodeID.DeathMountainExitNonEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainExit], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                    }
                    break;
                case OverworldNodeID.DeathMountainExitCave:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainExitNonEntrance], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainWestBottomNonEntrance], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case OverworldNodeID.DeathMountainExitCaveDark:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainExitCave], node,
                            _requirements[RequirementType.DarkRoomDeathMountainExit]));
                    }
                    break;
                case OverworldNodeID.LWKakarikoPortal:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.Gloves2]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldHammer], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWKakarikoPortalInverted], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case OverworldNodeID.LWKakarikoPortalStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWKakarikoPortal], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case OverworldNodeID.LWKakarikoPortalNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWKakarikoPortal], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case OverworldNodeID.SickKid:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorld], node,
                            _requirements[RequirementType.Bottle]));
                    }
                    break;
                case OverworldNodeID.GrassHouse:
                case OverworldNodeID.BombHut:
                case OverworldNodeID.CastleSecretExitArea:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.MagicBatLedge:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldHammer], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.HammerPegsArea], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case OverworldNodeID.MagicBat:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.MagicBatLedge], node,
                            _requirements[RequirementType.MagicBat]));
                    }
                    break;
                case OverworldNodeID.RaceGameLedge:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouthMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.RaceGameLedgeNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.RaceGameLedge], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case OverworldNodeID.SouthOfGroveLedge:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouthMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.SouthOfGrove:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.SouthOfGroveLedge], node,
                            _requirements[RequirementType.NotBunnyLW]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.SouthOfGroveLedge], node,
                            _requirements[RequirementType.SuperBunnyMirror]));
                    }
                    break;
                case OverworldNodeID.GroveDiggingSpot:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.Shovel]));
                    }
                    break;
                case OverworldNodeID.DesertLedge:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DesertBackNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.MireAreaMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DPFrontEntry], node,
                            _requirements[RequirementType.EntranceShuffleNone]));
                    }
                    break;
                case OverworldNodeID.DesertLedgeNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DesertLedge], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case OverworldNodeID.DesertBack:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DesertLedgeNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.MireAreaMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.DesertBackNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DesertBack], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case OverworldNodeID.CheckerboardLedge:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.MireAreaMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.CheckerboardLedgeNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.CheckerboardLedge], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case OverworldNodeID.CheckerboardCave:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.CheckerboardLedgeNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case OverworldNodeID.DesertPalaceFrontEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorld], node,
                            _requirements[RequirementType.Book]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.MireAreaMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.BombosTabletLedge:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouthMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.BombosTablet:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BombosTabletLedge], node,
                            _requirements[RequirementType.Tablet]));
                    }
                    break;
                case OverworldNodeID.LWMirePortal:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.FluteStandardOpen], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWMirePortalInverted], node,
                            _requirements[RequirementType.Gloves2]));
                    }
                    break;
                case OverworldNodeID.LWMirePortalStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWMirePortal], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case OverworldNodeID.LWGraveyard:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWGraveyardLedge], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.KingsTombNotBunny], node,
                            _requirements[RequirementType.Gloves2]));
                    }
                    break;
                case OverworldNodeID.LWGraveyardNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWGraveyard], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case OverworldNodeID.LWGraveyardLedge:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWGraveyardNotBunny], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWGraveyardMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.EscapeGrave:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWGraveyardNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case OverworldNodeID.KingsTomb:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWGraveyardNotBunny], node,
                            _requirements[RequirementType.Gloves2]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWGraveyardMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.KingsTombNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.KingsTomb], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case OverworldNodeID.KingsTombGrave:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.KingsTombNotBunny], node,
                            _requirements[RequirementType.Boots]));
                    }
                    break;
                case OverworldNodeID.HyruleCastleTop:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorld], node,
                            _requirements[RequirementType.EntranceShuffleNone]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldEast], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case OverworldNodeID.HyruleCastleTopInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.HyruleCastleTop], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case OverworldNodeID.HyruleCastleTopStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.HyruleCastleTop], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case OverworldNodeID.AgahnimTowerEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.HyruleCastleTopInverted], node,
                            _requirements[RequirementType.GTCrystal]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.HyruleCastleTopStandardOpen], node,
                            _requirements[RequirementType.ATBarrier]));
                    }
                    break;
                case OverworldNodeID.GanonHole:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.HyruleCastleTopInverted], node,
                            _requirements[RequirementType.Aga2]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldEastStandardOpen], node,
                            _requirements[RequirementType.Aga2]));
                    }
                    break;
                case OverworldNodeID.LWSouthPortal:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldHammer], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWSouthPortalInverted], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case OverworldNodeID.LWSouthPortalStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWSouthPortal], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case OverworldNodeID.LWSouthPortalNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWSouthPortal], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case OverworldNodeID.ZoraArea:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWLakeHyliaFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWWitchAreaNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.CatfishArea], node,
                            _requirements[RequirementType.DWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWLakeHyliaFakeFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWLakeHyliaWaterWalk], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.ZoraLedge:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.ZoraArea], node,
                            _requirements[RequirementType.Flippers]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.ZoraArea], node,
                            _requirements[RequirementType.FakeFlippersSplashDeletion]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.ZoraArea], node,
                            _requirements[RequirementType.WaterWalk]));
                    }
                    break;
                case OverworldNodeID.WaterfallFairy:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWLakeHyliaFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWLakeHyliaFakeFlippers], node,
                            _requirements[RequirementType.MoonPearl]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWLakeHyliaWaterWalk], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.WaterfallFairyNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.WaterfallFairy], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case OverworldNodeID.LWWitchArea:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.ZoraArea], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case OverworldNodeID.LWWitchAreaNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWWitchArea], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case OverworldNodeID.WitchsHut:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWWitchArea], node,
                            _requirements[RequirementType.Mushroom]));
                    }
                    break;
                case OverworldNodeID.Sahasrahla:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorld], node,
                            _requirements[RequirementType.GreenPendant]));
                    }
                    break;
                case OverworldNodeID.LWEastPortal:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldHammer], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWEastPortalInverted], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case OverworldNodeID.LWEastPortalStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWEastPortal], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case OverworldNodeID.LWEastPortalNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWEastPortal], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case OverworldNodeID.LWLakeHyliaFakeFlippers:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.FakeFlippersFairyRevival]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.FakeFlippersScreenTransition]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.FakeFlippersSplashDeletion]));
                    }
                    break;
                case OverworldNodeID.LWLakeHyliaFlippers:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.Flippers]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.WaterfallFairyNotBunny], node,
                            _requirements[RequirementType.Flippers]));
                    }
                    break;
                case OverworldNodeID.LWLakeHyliaWaterWalk:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.WaterWalk]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.WaterfallFairyNotBunny], node,
                            _requirements[RequirementType.WaterWalkFromWaterfallCave]));
                    }
                    break;
                case OverworldNodeID.Hobo:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWLakeHyliaFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWLakeHyliaFakeFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWLakeHyliaWaterWalk], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.LakeHyliaIsland:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWLakeHyliaFlippers], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWLakeHyliaFlippers], node,
                            _requirements[RequirementType.DWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWLakeHyliaFakeFlippers], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWLakeHyliaWaterWalk], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWLakeHyliaWaterWalk], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case OverworldNodeID.LakeHyliaFairyIsland:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWLakeHyliaFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.IcePalaceIsland], node,
                            _requirements[RequirementType.DWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWLakeHyliaFakeFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWLakeHyliaWaterWalk], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.LakeHyliaFairyIslandStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LakeHyliaFairyIsland], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case OverworldNodeID.DeathMountainWestBottom:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.FluteStandardOpen], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEntryCaveDark], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainExitCaveDark], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainWestTop], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEastBottomNotBunny], node,
                            _requirements[RequirementType.Hookshot]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainWestBottomInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainWestBottomMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.DeathMountainWestBottomNonEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainWestBottom], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                    }
                    break;
                case OverworldNodeID.DeathMountainWestBottomNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainWestBottom], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case OverworldNodeID.SpectacleRockTop:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainWestTop], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainWestBottomMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.DeathMountainWestTop:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.SpectacleRockTop], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEastTopNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainTopMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.DeathMountainWestTopNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainWestTop], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case OverworldNodeID.EtherTablet:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainWestTop], node,
                            _requirements[RequirementType.Tablet]));
                    }
                    break;
                case OverworldNodeID.DeathMountainEastBottom:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainWestBottomNotBunny], node,
                            _requirements[RequirementType.Hookshot]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEastBottomConnector], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.ParadoxCave], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEastTop], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.SpiralCaveLedge], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.MimicCaveLedge], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainEastBottom], node,
                            _requirements[RequirementType.DWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainEastBottomInverted], node,
                            _requirements[RequirementType.Gloves2]));
                    }
                    break;
                case OverworldNodeID.DeathMountainEastBottomNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEastBottom], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case OverworldNodeID.DeathMountainEastBottomLift2:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEastBottomNotBunny], node,
                            _requirements[RequirementType.Gloves2]));
                    }
                    break;
                case OverworldNodeID.DeathMountainEastBottomConnector:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEastBottomLift2], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEastTopConnector], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainEastBottomConnector], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case OverworldNodeID.ParadoxCave:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEastBottom], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEastTop], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                    }
                    break;
                case OverworldNodeID.ParadoxCaveSuperBunnyFallInHole:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.ParadoxCave], node,
                            _requirements[RequirementType.SuperBunnyFallInHole]));
                    }
                    break;
                case OverworldNodeID.ParadoxCaveNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.ParadoxCave], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case OverworldNodeID.ParadoxCaveTop:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.ParadoxCaveNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.ParadoxCaveSuperBunnyFallInHole], node,
                            _requirements[RequirementType.Sword2]));
                    }
                    break;
                case OverworldNodeID.DeathMountainEastTop:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainWestTopNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.ParadoxCave], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWTurtleRockTopInvertedNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainTopMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.DeathMountainEastTopInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEastTop], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case OverworldNodeID.DeathMountainEastTopNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEastTop], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case OverworldNodeID.DeathMountainEastTopConnector:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEastTop], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.TurtleRockSafetyDoor], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case OverworldNodeID.SpiralCaveLedge:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEastTop], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.TurtleRockTunnelMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.SpiralCave:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.SpiralCaveLedge], node,
                            _requirements[RequirementType.NotBunnyLW]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.SpiralCaveLedge], node,
                            _requirements[RequirementType.SuperBunnyFallInHole]));
                    }
                    break;
                case OverworldNodeID.MimicCaveLedge:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEastTopInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.TurtleRockTunnelMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.MimicCaveLedgeNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.MimicCaveLedge], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case OverworldNodeID.MimicCave:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.MimicCaveLedgeNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                    }
                    break;
                case OverworldNodeID.LWFloatingIsland:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEastTopInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWFloatingIsland], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case OverworldNodeID.LWTurtleRockTop:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEastTopNotBunny], node,
                            _requirements[RequirementType.Gloves2]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWTurtleRockTopInverted], node,
                            _requirements[RequirementType.Gloves2]));
                    }
                    break;
                case OverworldNodeID.LWTurtleRockTopInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWTurtleRockTop], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case OverworldNodeID.LWTurtleRockTopInvertedNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWTurtleRockTopInverted], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case OverworldNodeID.LWTurtleRockTopStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWTurtleRockTop], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case OverworldNodeID.DWKakarikoPortal:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWKakarikoPortalStandardOpen], node,
                            _requirements[RequirementType.Gloves1]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.DWKakarikoPortalInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWKakarikoPortal], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case OverworldNodeID.DarkWorldWest:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EntranceNoneInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.FluteInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWKakarikoPortal], node,
                            _requirements[RequirementType.NotBunnyDW]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.SkullWoodsBackArea], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeonAll]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BumperCaveEntry], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BumperCaveTop], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.HammerHouseNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouthNotBunny], node,
                            _requirements[RequirementType.Gloves2]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWWitchAreaNotBunny], node,
                            _requirements[RequirementType.Hookshot]));
                    }
                    break;
                case OverworldNodeID.DarkWorldWestNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldWest], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case OverworldNodeID.DarkWorldWestNotBunnyOrSuperBunnyMirror:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldWest], node,
                            _requirements[RequirementType.SuperBunnyMirror]));
                    }
                    break;
                case OverworldNodeID.DarkWorldWestLift2:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.Gloves2]));
                    }
                    break;
                case OverworldNodeID.SkullWoodsBackArea:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldWest], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeonAll]));
                    }
                    break;
                case OverworldNodeID.SkullWoodsBackAreaNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.SkullWoodsBackArea], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case OverworldNodeID.SkullWoodsBack:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.SkullWoodsBackAreaNotBunny], node,
                            _requirements[RequirementType.FireRod]));
                    }
                    break;
                case OverworldNodeID.BumperCaveEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEntry], node,
                            _requirements[RequirementType.LWMirror]));
                    }
                    break;
                case OverworldNodeID.BumperCaveEntryNonEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BumperCaveEntry], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                    }
                    break;
                case OverworldNodeID.BumperCaveFront:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEntryNonEntrance], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BumperCaveEntryNonEntrance], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case OverworldNodeID.BumperCaveNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BumperCaveFront], node,
                            _requirements[RequirementType.MoonPearl]));
                    }
                    break;
                case OverworldNodeID.BumperCavePastGap:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BumperCaveNotBunny], node,
                            _requirements[RequirementType.BumperCaveGap]));
                    }
                    break;
                case OverworldNodeID.BumperCaveBack:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BumperCavePastGap], node,
                            _requirements[RequirementType.Cape]));
                    }
                    break;
                case OverworldNodeID.BumperCaveTop:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainExit], node,
                            _requirements[RequirementType.LWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BumperCaveBack], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case OverworldNodeID.HammerHouse:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                    }
                    break;
                case OverworldNodeID.HammerHouseNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.HammerHouse], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case OverworldNodeID.HammerPegsArea:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldWestLift2], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.HammerPegs:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.HammerPegsArea], node,
                            _requirements[RequirementType.Hammer]));
                    }
                    break;
                case OverworldNodeID.PurpleChest:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.Blacksmith], node,
                            _requirements[RequirementType.HammerPegsArea]));
                    }
                    break;
                case OverworldNodeID.BlacksmithPrison:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldWestLift2], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.Blacksmith:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BlacksmithPrison], node,
                            _requirements[RequirementType.LightWorld]));
                    }
                    break;
                case OverworldNodeID.DarkWorldSouth:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EntranceNoneInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.FluteInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWSouthPortalNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldWest], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldEastHammer], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.DarkWorldSouthInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouth], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case OverworldNodeID.DarkWorldSouthStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouth], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case OverworldNodeID.DarkWorldSouthStandardOpenNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouthStandardOpen], node,
                            _requirements[RequirementType.MoonPearl]));
                    }
                    break;
                case OverworldNodeID.DarkWorldSouthMirror:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouth], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case OverworldNodeID.DarkWorldSouthNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouth], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case OverworldNodeID.DarkWorldSouthDash:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouthNotBunny], node,
                            _requirements[RequirementType.Boots]));
                    }
                    break;
                case OverworldNodeID.DarkWorldSouthHammer:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouthNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                    }
                    break;
                case OverworldNodeID.BuyBigBomb:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldInvertedNotBunny], node,
                            _requirements[RequirementType.RedCrystal]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouthStandardOpenNotBunny], node,
                            _requirements[RequirementType.RedCrystal]));
                    }
                    break;
                case OverworldNodeID.BuyBigBombStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BuyBigBomb], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case OverworldNodeID.BigBombToLightWorld:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BuyBigBomb], node,
                            _requirements[RequirementType.DWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BuyBigBomb], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case OverworldNodeID.BigBombToLightWorldStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BigBombToLightWorld], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case OverworldNodeID.BigBombToDWLakeHylia:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BuyBigBombStandardOpen], node,
                            _requirements[RequirementType.BombDuplicationMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BuyBigBombStandardOpen], node,
                            _requirements[RequirementType.BombDuplicationAncillaOverload]));
                    }
                    break;
                case OverworldNodeID.BigBombToWall:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BuyBigBombStandardOpen], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BigBombToLightWorld], node,
                            _requirements[RequirementType.LWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BigBombToLightWorldStandardOpen], node,
                            _requirements[RequirementType.Aga1]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.BigBombToDWLakeHylia], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.DWSouthPortal:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWSouthPortalStandardOpen], node,
                            _requirements[RequirementType.Gloves1]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouthHammer], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.DWSouthPortalInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWSouthPortal], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case OverworldNodeID.DWSouthPortalNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWSouthPortal], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case OverworldNodeID.MireArea:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWMirePortal], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.MireAreaMirror:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.MireArea], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case OverworldNodeID.MireAreaNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.MireArea], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case OverworldNodeID.MireAreaNotBunnyOrSuperBunnyMirror:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.MireAreaNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.MireArea], node,
                            _requirements[RequirementType.SuperBunnyMirror]));
                    }
                    break;
                case OverworldNodeID.MiseryMireEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.MireAreaNotBunny], node,
                            _requirements[RequirementType.MMMedallion]));
                    }
                    break;
                case OverworldNodeID.DWMirePortal:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.FluteInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWMirePortalStandardOpen], node,
                            _requirements[RequirementType.Gloves2]));
                    }
                    break;
                case OverworldNodeID.DWMirePortalInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWMirePortal], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case OverworldNodeID.DWGraveyard:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.DWGraveyardMirror:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWGraveyard], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case OverworldNodeID.DWWitchArea:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWWitchArea], node,
                            _requirements[RequirementType.LWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.FluteInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldEastNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldEastNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWLakeHyliaFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWLakeHyliaFakeFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWLakeHyliaWaterWalk], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.DWWitchAreaNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWWitchArea], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case OverworldNodeID.CatfishArea:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.ZoraArea], node,
                            _requirements[RequirementType.LWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWWitchAreaNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case OverworldNodeID.DarkWorldEast:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldStandardOpen], node,
                            _requirements[RequirementType.Aga1]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.FluteInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWWitchAreaNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWWitchAreaNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouthHammer], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWEastPortalNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWLakeHyliaFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWLakeHyliaFakeFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWLakeHyliaWaterWalk], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.DarkWorldEastStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldEast], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case OverworldNodeID.DarkWorldEastNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldEast], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case OverworldNodeID.DarkWorldEastHammer:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldEastNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                    }
                    break;
                case OverworldNodeID.FatFairyEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldEastNotBunny], node,
                            _requirements[RequirementType.RedCrystal]));
                    }
                    break;
                case OverworldNodeID.DWEastPortal:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWEastPortalStandardOpen], node,
                            _requirements[RequirementType.Gloves1]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldEastHammer], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.DWEastPortalInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWEastPortal], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case OverworldNodeID.DWEastPortalNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWEastPortal], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case OverworldNodeID.DWLakeHyliaFlippers:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.Flippers]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouthNotBunny], node,
                            _requirements[RequirementType.Flippers]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWWitchAreaNotBunny], node,
                            _requirements[RequirementType.Flippers]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldEastNotBunny], node,
                            _requirements[RequirementType.Flippers]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouthEastNotBunny], node,
                            _requirements[RequirementType.Flippers]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.IcePalaceIslandInverted], node,
                            _requirements[RequirementType.Flippers]));
                    }
                    break;
                case OverworldNodeID.DWLakeHyliaFakeFlippers:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.FakeFlippersFairyRevival]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.FakeFlippersQirnJump]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.FakeFlippersSplashDeletion]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouthNotBunny], node,
                            _requirements[RequirementType.FakeFlippersFairyRevival]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouthNotBunny], node,
                            _requirements[RequirementType.FakeFlippersSplashDeletion]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWWitchAreaNotBunny], node,
                            _requirements[RequirementType.FakeFlippersFairyRevival]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWWitchAreaNotBunny], node,
                            _requirements[RequirementType.FakeFlippersSplashDeletion]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldEastNotBunny], node,
                            _requirements[RequirementType.FakeFlippersFairyRevival]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldEastNotBunny], node,
                            _requirements[RequirementType.FakeFlippersSplashDeletion]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouthEastNotBunny], node,
                            _requirements[RequirementType.FakeFlippersFairyRevival]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouthEastNotBunny], node,
                            _requirements[RequirementType.FakeFlippersSplashDeletion]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.IcePalaceIslandInverted], node,
                            _requirements[RequirementType.FakeFlippersFairyRevival]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.IcePalaceIslandInverted], node,
                            _requirements[RequirementType.FakeFlippersSplashDeletion]));
                    }
                    break;
                case OverworldNodeID.DWLakeHyliaWaterWalk:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.WaterWalk]));
                    }
                    break;
                case OverworldNodeID.IcePalaceIsland:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LakeHyliaFairyIsland], node,
                            _requirements[RequirementType.LWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LakeHyliaFairyIslandStandardOpen], node,
                            _requirements[RequirementType.Gloves2]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWLakeHyliaFlippers], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWLakeHyliaFakeFlippers], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWLakeHyliaWaterWalk], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case OverworldNodeID.IcePalaceIslandInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.IcePalaceIsland], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case OverworldNodeID.DarkWorldSouthEast:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.FluteInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWLakeHyliaFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWLakeHyliaFakeFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWLakeHyliaWaterWalk], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.DarkWorldSouthEastNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouthEast], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case OverworldNodeID.DarkWorldSouthEastLift1:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouthEastNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case OverworldNodeID.DarkDeathMountainWestBottom:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.FluteInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEntryCaveDark], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainWestBottom], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainWestBottom], node,
                            _requirements[RequirementType.LWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainTop], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.DarkDeathMountainWestBottomInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainWestBottom], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case OverworldNodeID.DarkDeathMountainWestBottomNonEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainWestBottom], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                    }
                    break;
                case OverworldNodeID.DarkDeathMountainWestBottomMirror:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainWestBottom], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case OverworldNodeID.DarkDeathMountainWestBottomNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainWestBottom], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case OverworldNodeID.SpikeCavePastHammerBlocks:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainWestBottomNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                    }
                    break;
                case OverworldNodeID.SpikeCavePastSpikes:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.SpikeCavePastHammerBlocks], node,
                            _requirements[RequirementType.SpikeCave]));
                    }
                    break;
                case OverworldNodeID.SpikeCaveChest:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.SpikeCavePastSpikes], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case OverworldNodeID.DarkDeathMountainTop:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainWestTop], node,
                            _requirements[RequirementType.LWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEastTop], node,
                            _requirements[RequirementType.LWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainWestBottomInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.SuperBunnyCave], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWFloatingIsland], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWTurtleRockTop], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.DarkDeathMountainTopInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainTop], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case OverworldNodeID.DarkDeathMountainTopStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainTop], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case OverworldNodeID.DarkDeathMountainTopMirror:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainTop], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case OverworldNodeID.DarkDeathMountainTopNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainTop], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case OverworldNodeID.SuperBunnyCave:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainTop], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainEastBottom], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                    }
                    break;
                case OverworldNodeID.SuperBunnyCaveChests:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.SuperBunnyCave], node,
                            _requirements[RequirementType.NotBunnyDW]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.SuperBunnyCave], node,
                            _requirements[RequirementType.SuperBunnyFallInHole]));
                    }
                    break;
                case OverworldNodeID.GanonsTowerEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainTopInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainTopStandardOpen], node,
                            _requirements[RequirementType.GTCrystal]));
                    }
                    break;
                case OverworldNodeID.GanonsTowerEntranceStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.GanonsTowerEntrance], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case OverworldNodeID.DWFloatingIsland:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWFloatingIsland], node,
                            _requirements[RequirementType.LWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.HookshotCaveEntrance], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                    }
                    break;
                case OverworldNodeID.HookshotCaveEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainTopNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case OverworldNodeID.HookshotCaveEntranceHookshot:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.HookshotCaveEntrance], node,
                            _requirements[RequirementType.Hookshot]));
                    }
                    break;
                case OverworldNodeID.HookshotCaveEntranceHover:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.HookshotCaveEntrance], node,
                            _requirements[RequirementType.Hover]));
                    }
                    break;
                case OverworldNodeID.HookshotCaveBonkableChest:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.HookshotCaveEntranceHookshot], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.HookshotCaveEntrance], node,
                            _requirements[RequirementType.BonkOverLedge]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.HookshotCaveEntranceHover], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.HookshotCaveBack:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.HookshotCaveEntranceHookshot], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.HookshotCaveEntranceHover], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.DWTurtleRockTop:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LWTurtleRockTopStandardOpen], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainTopInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.DWTurtleRockTopInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWTurtleRockTop], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case OverworldNodeID.DWTurtleRockTopNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWTurtleRockTop], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case OverworldNodeID.TurtleRockFrontEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DWTurtleRockTopNotBunny], node,
                            _requirements[RequirementType.TRMedallion]));
                    }
                    break;
                case OverworldNodeID.DarkDeathMountainEastBottom:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEastBottom], node,
                            _requirements[RequirementType.LWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEastBottomLift2], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainTop], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.DarkDeathMountainEastBottomInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainEastBottom], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case OverworldNodeID.DarkDeathMountainEastBottomConnector:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkDeathMountainEastBottom], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case OverworldNodeID.TurtleRockTunnel:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.SpiralCaveLedge], node,
                            _requirements[RequirementType.LWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.MimicCaveLedge], node,
                            _requirements[RequirementType.LWMirror]));
                    }
                    break;
                case OverworldNodeID.TurtleRockTunnelMirror:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.TurtleRockTunnel], node,
                            _requirements[RequirementType.DWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.TRKeyDoorsToMiddleExit], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case OverworldNodeID.TurtleRockSafetyDoor:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainEastTopConnector], node,
                            _requirements[RequirementType.LWMirror]));
                    }
                    break;
                case OverworldNodeID.HCSanctuaryEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldNotBunnyOrSuperBunnyMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.HCFrontEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldNotBunnyOrDungeonRevive], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.HCBackEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EscapeGrave], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.ATEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.AgahnimTowerEntrance], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.GanonsTowerEntrance], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case OverworldNodeID.EPEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldNotBunnyOrDungeonRevive], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.DPFrontEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DesertPalaceFrontEntrance], node,
                            _requirements[RequirementType.NotBunnyLW]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DesertPalaceFrontEntrance], node,
                            _requirements[RequirementType.DungeonRevive]));
                    }
                    break;
                case OverworldNodeID.DPLeftEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DesertLedge], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.DPBackEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DesertBack], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.ToHEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainWestTopNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DeathMountainWestTop], node,
                            _requirements[RequirementType.DungeonRevive]));
                    }
                    break;
                case OverworldNodeID.PoDEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldEastNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.SPEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.LightWorldInvertedNotBunny], node,
                            _requirements[RequirementType.Mirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldSouthStandardOpenNotBunny], node,
                            _requirements[RequirementType.Mirror]));
                    }
                    break;
                case OverworldNodeID.SWFrontEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldWest], node,
                            _requirements[RequirementType.DungeonRevive]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.Start], node,
                            _requirements[RequirementType.EntranceShuffleInsanity]));
                    }
                    break;
                case OverworldNodeID.SWBackEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.SkullWoodsBack], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.TTEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.IPEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.IcePalaceIsland], node,
                            _requirements[RequirementType.NotBunnyDW]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.IcePalaceIsland], node,
                            _requirements[RequirementType.DungeonRevive]));
                    }
                    break;
                case OverworldNodeID.MMEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.MiseryMireEntrance], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.TRFrontEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.TurtleRockFrontEntrance], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.TRFrontEntryStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.TRFrontEntry], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case OverworldNodeID.TRFrontEntryStandardOpenEntranceNone:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.TRFrontEntryStandardOpen], node,
                            _requirements[RequirementType.EntranceShuffleNone]));
                    }
                    break;
                case OverworldNodeID.TRFrontToKeyDoors:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.TRFrontEntryStandardOpenEntranceNone], node,
                            _requirements[RequirementType.CaneOfSomaria]));
                    }
                    break;
                case OverworldNodeID.TRKeyDoorsToMiddleExit:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.TRFrontToKeyDoors], node,
                            _requirements[RequirementType.TRKeyDoorsToMiddleExit]));
                    }
                    break;
                case OverworldNodeID.TRMiddleEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.TurtleRockTunnel], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.TRBackEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.TurtleRockSafetyDoor], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case OverworldNodeID.GTEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.AgahnimTowerEntrance], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.GanonsTowerEntranceStandardOpen], node,
                            _requirements[RequirementType.NotBunnyDW]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[OverworldNodeID.GanonsTowerEntranceStandardOpen], node,
                            _requirements[RequirementType.DungeonRevive]));
                    }
                    break;
            }

            return connections;
        }

        /// <summary>
        /// Returns a new requirement node for the specified requirement node ID.
        /// </summary>
        /// <param name="id">
        ///     The requirement node ID.
        /// </param>
        /// <returns>
        /// A new requirement node.
        /// </returns>
        public INode GetOverworldNode(OverworldNodeID id)
        {
            return id == OverworldNodeID.Start ? _startFactory() : _factory();
        }
    }
}
