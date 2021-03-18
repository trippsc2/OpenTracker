using System.Collections.Generic;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.RequirementNodes
{
    /// <summary>
    /// This class contains creation logic for requirement node data.
    /// </summary>
    public class RequirementNodeFactory : IRequirementNodeFactory
    {
        private readonly IRequirementDictionary _requirements;
        private readonly IRequirementNodeDictionary _requirementNodes;
        
        private readonly IRequirementNode.Factory _factory;
        private readonly IStartRequirementNode.Factory _startFactory;
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
        public RequirementNodeFactory(
            IRequirementDictionary requirements, IRequirementNodeDictionary requirementNodes,
            IRequirementNode.Factory factory, IStartRequirementNode.Factory startFactory,
            NodeConnection.Factory connectionFactory)
        {
            _requirements = requirements;
            _requirementNodes = requirementNodes;
            _factory = factory;
            _connectionFactory = connectionFactory;
            _startFactory = startFactory;
        }

        /// <summary>
        /// Populates the list of connections to the specified requirement node ID.
        /// </summary>
        /// <param name="id">
        /// The requirement node ID.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        public IEnumerable<INodeConnection> GetNodeConnections(RequirementNodeID id, IRequirementNode node)
        {
            var connections = new List<INodeConnection>();
            
            switch (id)
            {
                case RequirementNodeID.EntranceDungeonAllInsanity:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.Start], node,
                            _requirements[RequirementType.EntranceShuffleDungeonAllInsanity]));
                    }
                    break;
                case RequirementNodeID.EntranceNone:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.Start], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                    }
                    break;
                case RequirementNodeID.EntranceNoneInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EntranceNone], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.Flute:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.Start], node,
                            _requirements[RequirementType.Flute]));
                    }
                    break;
                case RequirementNodeID.FluteActivated:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.Flute], node,
                            _requirements[RequirementType.FluteActivated]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldFlute], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.LightWorld:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.Start], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEntry], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainExit], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWKakarikoPortalNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWKakarikoPortalNotBunny], node,
                            _requirements[RequirementType.Gloves2]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DesertLedge], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.GrassHouse], node,
                            _requirements[RequirementType.NotBunnyLW]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BombHut], node,
                            _requirements[RequirementType.NotBunnyLW]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.RaceGameLedge], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.CastleSecretExitArea], node,
                            _requirements[RequirementType.NotBunnyLW]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.SouthOfGroveLedge], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.CheckerboardLedge], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWMirePortal], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWSouthPortalNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWWitchAreaNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWEastPortalNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouthInverted], node,
                            _requirements[RequirementType.Aga1]));
                    }
                    break;
                case RequirementNodeID.LightWorldInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorld], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.LightWorldInvertedNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldInverted], node,
                            _requirements[RequirementType.MoonPearl]));
                    }
                    break;
                case RequirementNodeID.LightWorldStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorld], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.LightWorldMirror:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorld], node,
                            _requirements[RequirementType.LWMirror]));
                    }
                    break;
                case RequirementNodeID.LightWorldNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorld], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.LightWorldNotBunnyOrDungeonRevive:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorld], node,
                            _requirements[RequirementType.DungeonRevive]));
                    }
                    break;
                case RequirementNodeID.LightWorldNotBunnyOrSuperBunnyFallInHole:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorld], node,
                            _requirements[RequirementType.SuperBunnyFallInHole]));
                    }
                    break;
                case RequirementNodeID.LightWorldNotBunnyOrSuperBunnyMirror:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorld], node,
                            _requirements[RequirementType.SuperBunnyMirror]));
                    }
                    break;
                case RequirementNodeID.LightWorldDash:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.Boots]));
                    }
                    break;
                case RequirementNodeID.LightWorldHammer:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                    }
                    break;
                case RequirementNodeID.LightWorldLift1:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case RequirementNodeID.LightWorldFlute:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.Flute]));
                    }
                    break;
                case RequirementNodeID.FluteInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.FluteActivated], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.FluteStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.FluteActivated], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.Pedestal:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorld], node,
                            _requirements[RequirementType.Pedestal]));
                    }
                    break;
                case RequirementNodeID.LumberjackCaveHole:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldDash], node,
                            _requirements[RequirementType.Aga1]));
                    }
                    break;
                case RequirementNodeID.DeathMountainEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldLift1], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BumperCaveEntry], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.DeathMountainEntryNonEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEntry], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                    }
                    break;
                case RequirementNodeID.DeathMountainEntryCave:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEntryNonEntrance], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainWestBottomNonEntrance], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BumperCaveEntryNonEntrance], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainWestBottomNonEntrance], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DeathMountainEntryCaveDark:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEntryCave], node,
                            _requirements[RequirementType.DarkRoomDeathMountainEntry]));
                    }
                    break;
                case RequirementNodeID.DeathMountainExit:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainExitCaveDark], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BumperCaveBack], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BumperCaveTop], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.DeathMountainExitNonEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainExit], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                    }
                    break;
                case RequirementNodeID.DeathMountainExitCave:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainExitNonEntrance], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainWestBottomNonEntrance], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.DeathMountainExitCaveDark:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainExitCave], node,
                            _requirements[RequirementType.DarkRoomDeathMountainExit]));
                    }
                    break;
                case RequirementNodeID.LWKakarikoPortal:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.Gloves2]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldHammer], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWKakarikoPortalInverted], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case RequirementNodeID.LWKakarikoPortalStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWKakarikoPortal], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.LWKakarikoPortalNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWKakarikoPortal], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.SickKid:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorld], node,
                            _requirements[RequirementType.Bottle]));
                    }
                    break;
                case RequirementNodeID.GrassHouse:
                case RequirementNodeID.BombHut:
                case RequirementNodeID.CastleSecretExitArea:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.MagicBatLedge:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldHammer], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.HammerPegsArea], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.MagicBat:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.MagicBatLedge], node,
                            _requirements[RequirementType.MagicBat]));
                    }
                    break;
                case RequirementNodeID.RaceGameLedge:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouthMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.RaceGameLedgeNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.RaceGameLedge], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.SouthOfGroveLedge:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouthMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.SouthOfGrove:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.SouthOfGroveLedge], node,
                            _requirements[RequirementType.NotBunnyLW]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.SouthOfGroveLedge], node,
                            _requirements[RequirementType.SuperBunnyMirror]));
                    }
                    break;
                case RequirementNodeID.GroveDiggingSpot:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.Shovel]));
                    }
                    break;
                case RequirementNodeID.DesertLedge:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DesertBackNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.MireAreaMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DPFrontEntry], node,
                            _requirements[RequirementType.EntranceShuffleNone]));
                    }
                    break;
                case RequirementNodeID.DesertLedgeNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DesertLedge], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.DesertBack:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DesertLedgeNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.MireAreaMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.DesertBackNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DesertBack], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.CheckerboardLedge:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.MireAreaMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.CheckerboardLedgeNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.CheckerboardLedge], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.CheckerboardCave:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.CheckerboardLedgeNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case RequirementNodeID.DesertPalaceFrontEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorld], node,
                            _requirements[RequirementType.Book]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.MireAreaMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.BombosTabletLedge:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouthMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.BombosTablet:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BombosTabletLedge], node,
                            _requirements[RequirementType.Tablet]));
                    }
                    break;
                case RequirementNodeID.LWMirePortal:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.FluteStandardOpen], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWMirePortalInverted], node,
                            _requirements[RequirementType.Gloves2]));
                    }
                    break;
                case RequirementNodeID.LWMirePortalStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWMirePortal], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.LWGraveyard:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWGraveyardLedge], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.KingsTombNotBunny], node,
                            _requirements[RequirementType.Gloves2]));
                    }
                    break;
                case RequirementNodeID.LWGraveyardNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWGraveyard], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.LWGraveyardLedge:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWGraveyardNotBunny], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWGraveyardMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.EscapeGrave:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWGraveyardNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case RequirementNodeID.KingsTomb:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWGraveyardNotBunny], node,
                            _requirements[RequirementType.Gloves2]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWGraveyardMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.KingsTombNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.KingsTomb], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.KingsTombGrave:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.KingsTombNotBunny], node,
                            _requirements[RequirementType.Boots]));
                    }
                    break;
                case RequirementNodeID.HyruleCastleTop:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorld], node,
                            _requirements[RequirementType.EntranceShuffleNone]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldEast], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.HyruleCastleTopInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.HyruleCastleTop], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.HyruleCastleTopStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.HyruleCastleTop], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.AgahnimTowerEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.HyruleCastleTopInverted], node,
                            _requirements[RequirementType.GTCrystal]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.HyruleCastleTopStandardOpen], node,
                            _requirements[RequirementType.ATBarrier]));
                    }
                    break;
                case RequirementNodeID.GanonHole:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.HyruleCastleTopInverted], node,
                            _requirements[RequirementType.Aga2]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldEastStandardOpen], node,
                            _requirements[RequirementType.Aga2]));
                    }
                    break;
                case RequirementNodeID.LWSouthPortal:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldHammer], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWSouthPortalInverted], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case RequirementNodeID.LWSouthPortalStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWSouthPortal], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.LWSouthPortalNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWSouthPortal], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.ZoraArea:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWLakeHyliaFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWWitchAreaNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.CatfishArea], node,
                            _requirements[RequirementType.DWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWLakeHyliaFakeFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWLakeHyliaWaterWalk], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.ZoraLedge:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.ZoraArea], node,
                            _requirements[RequirementType.Flippers]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.ZoraArea], node,
                            _requirements[RequirementType.FakeFlippersSplashDeletion]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.ZoraArea], node,
                            _requirements[RequirementType.WaterWalk]));
                    }
                    break;
                case RequirementNodeID.WaterfallFairy:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWLakeHyliaFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWLakeHyliaFakeFlippers], node,
                            _requirements[RequirementType.MoonPearl]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWLakeHyliaWaterWalk], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.WaterfallFairyNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.WaterfallFairy], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.LWWitchArea:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.ZoraArea], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case RequirementNodeID.LWWitchAreaNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWWitchArea], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.WitchsHut:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWWitchArea], node,
                            _requirements[RequirementType.Mushroom]));
                    }
                    break;
                case RequirementNodeID.Sahasrahla:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorld], node,
                            _requirements[RequirementType.GreenPendant]));
                    }
                    break;
                case RequirementNodeID.LWEastPortal:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldHammer], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWEastPortalInverted], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case RequirementNodeID.LWEastPortalStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWEastPortal], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.LWEastPortalNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWEastPortal], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.LWLakeHyliaFakeFlippers:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.FakeFlippersFairyRevival]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.FakeFlippersScreenTransition]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.FakeFlippersSplashDeletion]));
                    }
                    break;
                case RequirementNodeID.LWLakeHyliaFlippers:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.Flippers]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.WaterfallFairyNotBunny], node,
                            _requirements[RequirementType.Flippers]));
                    }
                    break;
                case RequirementNodeID.LWLakeHyliaWaterWalk:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldNotBunny], node,
                            _requirements[RequirementType.WaterWalk]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.WaterfallFairyNotBunny], node,
                            _requirements[RequirementType.WaterWalkFromWaterfallCave]));
                    }
                    break;
                case RequirementNodeID.Hobo:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWLakeHyliaFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWLakeHyliaFakeFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWLakeHyliaWaterWalk], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.LakeHyliaIsland:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWLakeHyliaFlippers], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWLakeHyliaFlippers], node,
                            _requirements[RequirementType.DWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWLakeHyliaFakeFlippers], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWLakeHyliaWaterWalk], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWLakeHyliaWaterWalk], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.LakeHyliaFairyIsland:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWLakeHyliaFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.IcePalaceIsland], node,
                            _requirements[RequirementType.DWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWLakeHyliaFakeFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWLakeHyliaWaterWalk], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.LakeHyliaFairyIslandStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LakeHyliaFairyIsland], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.DeathMountainWestBottom:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.FluteStandardOpen], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEntryCaveDark], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainExitCaveDark], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainWestTop], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEastBottomNotBunny], node,
                            _requirements[RequirementType.Hookshot]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainWestBottomInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainWestBottomMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.DeathMountainWestBottomNonEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainWestBottom], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                    }
                    break;
                case RequirementNodeID.DeathMountainWestBottomNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainWestBottom], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.SpectacleRockTop:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainWestTop], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainWestBottomMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.DeathMountainWestTop:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.SpectacleRockTop], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEastTopNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainTopMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.DeathMountainWestTopNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainWestTop], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.EtherTablet:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainWestTop], node,
                            _requirements[RequirementType.Tablet]));
                    }
                    break;
                case RequirementNodeID.DeathMountainEastBottom:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainWestBottomNotBunny], node,
                            _requirements[RequirementType.Hookshot]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEastBottomConnector], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.ParadoxCave], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEastTop], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.SpiralCaveLedge], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.MimicCaveLedge], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainEastBottom], node,
                            _requirements[RequirementType.DWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainEastBottomInverted], node,
                            _requirements[RequirementType.Gloves2]));
                    }
                    break;
                case RequirementNodeID.DeathMountainEastBottomNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEastBottom], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.DeathMountainEastBottomLift2:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEastBottomNotBunny], node,
                            _requirements[RequirementType.Gloves2]));
                    }
                    break;
                case RequirementNodeID.DeathMountainEastBottomConnector:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEastBottomLift2], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEastTopConnector], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainEastBottomConnector], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.ParadoxCave:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEastBottom], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEastTop], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                    }
                    break;
                case RequirementNodeID.ParadoxCaveSuperBunnyFallInHole:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.ParadoxCave], node,
                            _requirements[RequirementType.SuperBunnyFallInHole]));
                    }
                    break;
                case RequirementNodeID.ParadoxCaveNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.ParadoxCave], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.ParadoxCaveTop:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.ParadoxCaveNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.ParadoxCaveSuperBunnyFallInHole], node,
                            _requirements[RequirementType.Sword2]));
                    }
                    break;
                case RequirementNodeID.DeathMountainEastTop:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainWestTopNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.ParadoxCave], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWTurtleRockTopInvertedNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainTopMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.DeathMountainEastTopInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEastTop], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DeathMountainEastTopNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEastTop], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.DeathMountainEastTopConnector:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEastTop], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.TurtleRockSafetyDoor], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.SpiralCaveLedge:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEastTop], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.TurtleRockTunnelMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.SpiralCave:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.SpiralCaveLedge], node,
                            _requirements[RequirementType.NotBunnyLW]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.SpiralCaveLedge], node,
                            _requirements[RequirementType.SuperBunnyFallInHole]));
                    }
                    break;
                case RequirementNodeID.MimicCaveLedge:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEastTopInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.TurtleRockTunnelMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.MimicCaveLedgeNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.MimicCaveLedge], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.MimicCave:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.MimicCaveLedgeNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                    }
                    break;
                case RequirementNodeID.LWFloatingIsland:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEastTopInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWFloatingIsland], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.LWTurtleRockTop:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEastTopNotBunny], node,
                            _requirements[RequirementType.Gloves2]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWTurtleRockTopInverted], node,
                            _requirements[RequirementType.Gloves2]));
                    }
                    break;
                case RequirementNodeID.LWTurtleRockTopInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWTurtleRockTop], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.LWTurtleRockTopInvertedNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWTurtleRockTopInverted], node,
                            _requirements[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.LWTurtleRockTopStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWTurtleRockTop], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.DWKakarikoPortal:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWKakarikoPortalStandardOpen], node,
                            _requirements[RequirementType.Gloves1]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.DWKakarikoPortalInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWKakarikoPortal], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DarkWorldWest:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EntranceNoneInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.FluteInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWKakarikoPortal], node,
                            _requirements[RequirementType.NotBunnyDW]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.SkullWoodsBackArea], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeonAll]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BumperCaveEntry], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BumperCaveTop], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.HammerHouseNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouthNotBunny], node,
                            _requirements[RequirementType.Gloves2]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWWitchAreaNotBunny], node,
                            _requirements[RequirementType.Hookshot]));
                    }
                    break;
                case RequirementNodeID.DarkWorldWestNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldWest], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.DarkWorldWestNotBunnyOrSuperBunnyMirror:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldWest], node,
                            _requirements[RequirementType.SuperBunnyMirror]));
                    }
                    break;
                case RequirementNodeID.DarkWorldWestLift2:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.Gloves2]));
                    }
                    break;
                case RequirementNodeID.SkullWoodsBackArea:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldWest], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeonAll]));
                    }
                    break;
                case RequirementNodeID.SkullWoodsBackAreaNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.SkullWoodsBackArea], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.SkullWoodsBack:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.SkullWoodsBackAreaNotBunny], node,
                            _requirements[RequirementType.FireRod]));
                    }
                    break;
                case RequirementNodeID.BumperCaveEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEntry], node,
                            _requirements[RequirementType.LWMirror]));
                    }
                    break;
                case RequirementNodeID.BumperCaveEntryNonEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BumperCaveEntry], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                    }
                    break;
                case RequirementNodeID.BumperCaveFront:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEntryNonEntrance], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BumperCaveEntryNonEntrance], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.BumperCaveNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BumperCaveFront], node,
                            _requirements[RequirementType.MoonPearl]));
                    }
                    break;
                case RequirementNodeID.BumperCavePastGap:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BumperCaveNotBunny], node,
                            _requirements[RequirementType.BumperCaveGap]));
                    }
                    break;
                case RequirementNodeID.BumperCaveBack:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BumperCavePastGap], node,
                            _requirements[RequirementType.Cape]));
                    }
                    break;
                case RequirementNodeID.BumperCaveTop:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainExit], node,
                            _requirements[RequirementType.LWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BumperCaveBack], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.HammerHouse:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                    }
                    break;
                case RequirementNodeID.HammerHouseNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.HammerHouse], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.HammerPegsArea:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldWestLift2], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.HammerPegs:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.HammerPegsArea], node,
                            _requirements[RequirementType.Hammer]));
                    }
                    break;
                case RequirementNodeID.PurpleChest:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.Blacksmith], node,
                            _requirements[RequirementType.HammerPegsArea]));
                    }
                    break;
                case RequirementNodeID.BlacksmithPrison:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldWestLift2], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.Blacksmith:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BlacksmithPrison], node,
                            _requirements[RequirementType.LightWorld]));
                    }
                    break;
                case RequirementNodeID.DarkWorldSouth:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EntranceNoneInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.FluteInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWSouthPortalNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldWest], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldEastHammer], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.DarkWorldSouthInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouth], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DarkWorldSouthStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouth], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.DarkWorldSouthStandardOpenNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouthStandardOpen], node,
                            _requirements[RequirementType.MoonPearl]));
                    }
                    break;
                case RequirementNodeID.DarkWorldSouthMirror:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouth], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.DarkWorldSouthNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouth], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.DarkWorldSouthDash:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouthNotBunny], node,
                            _requirements[RequirementType.Boots]));
                    }
                    break;
                case RequirementNodeID.DarkWorldSouthHammer:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouthNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                    }
                    break;
                case RequirementNodeID.BuyBigBomb:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldInvertedNotBunny], node,
                            _requirements[RequirementType.RedCrystal]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouthStandardOpenNotBunny], node,
                            _requirements[RequirementType.RedCrystal]));
                    }
                    break;
                case RequirementNodeID.BuyBigBombStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BuyBigBomb], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.BigBombToLightWorld:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BuyBigBomb], node,
                            _requirements[RequirementType.DWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BuyBigBomb], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.BigBombToLightWorldStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BigBombToLightWorld], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.BigBombToDWLakeHylia:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BuyBigBombStandardOpen], node,
                            _requirements[RequirementType.BombDuplicationMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BuyBigBombStandardOpen], node,
                            _requirements[RequirementType.BombDuplicationAncillaOverload]));
                    }
                    break;
                case RequirementNodeID.BigBombToWall:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BuyBigBombStandardOpen], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BigBombToLightWorld], node,
                            _requirements[RequirementType.LWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BigBombToLightWorldStandardOpen], node,
                            _requirements[RequirementType.Aga1]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.BigBombToDWLakeHylia], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.DWSouthPortal:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWSouthPortalStandardOpen], node,
                            _requirements[RequirementType.Gloves1]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouthHammer], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.DWSouthPortalInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWSouthPortal], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DWSouthPortalNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWSouthPortal], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.MireArea:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWMirePortal], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.MireAreaMirror:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.MireArea], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.MireAreaNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.MireArea], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.MireAreaNotBunnyOrSuperBunnyMirror:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.MireAreaNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.MireArea], node,
                            _requirements[RequirementType.SuperBunnyMirror]));
                    }
                    break;
                case RequirementNodeID.MiseryMireEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.MireAreaNotBunny], node,
                            _requirements[RequirementType.MMMedallion]));
                    }
                    break;
                case RequirementNodeID.DWMirePortal:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.FluteInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWMirePortalStandardOpen], node,
                            _requirements[RequirementType.Gloves2]));
                    }
                    break;
                case RequirementNodeID.DWMirePortalInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWMirePortal], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DWGraveyard:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.DWGraveyardMirror:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWGraveyard], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.DWWitchArea:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWWitchArea], node,
                            _requirements[RequirementType.LWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.FluteInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldEastNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldEastNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWLakeHyliaFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWLakeHyliaFakeFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWLakeHyliaWaterWalk], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.DWWitchAreaNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWWitchArea], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.CatfishArea:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.ZoraArea], node,
                            _requirements[RequirementType.LWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWWitchAreaNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case RequirementNodeID.DarkWorldEast:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldStandardOpen], node,
                            _requirements[RequirementType.Aga1]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.FluteInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWWitchAreaNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWWitchAreaNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouthHammer], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWEastPortalNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWLakeHyliaFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWLakeHyliaFakeFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWLakeHyliaWaterWalk], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.DarkWorldEastStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldEast], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.DarkWorldEastNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldEast], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.DarkWorldEastHammer:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldEastNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                    }
                    break;
                case RequirementNodeID.FatFairyEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldEastNotBunny], node,
                            _requirements[RequirementType.RedCrystal]));
                    }
                    break;
                case RequirementNodeID.DWEastPortal:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWEastPortalStandardOpen], node,
                            _requirements[RequirementType.Gloves1]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldEastHammer], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.DWEastPortalInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWEastPortal], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DWEastPortalNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWEastPortal], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.DWLakeHyliaFlippers:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.Flippers]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouthNotBunny], node,
                            _requirements[RequirementType.Flippers]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWWitchAreaNotBunny], node,
                            _requirements[RequirementType.Flippers]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldEastNotBunny], node,
                            _requirements[RequirementType.Flippers]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouthEastNotBunny], node,
                            _requirements[RequirementType.Flippers]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.IcePalaceIslandInverted], node,
                            _requirements[RequirementType.Flippers]));
                    }
                    break;
                case RequirementNodeID.DWLakeHyliaFakeFlippers:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.FakeFlippersFairyRevival]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.FakeFlippersQirnJump]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.FakeFlippersSplashDeletion]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouthNotBunny], node,
                            _requirements[RequirementType.FakeFlippersFairyRevival]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouthNotBunny], node,
                            _requirements[RequirementType.FakeFlippersSplashDeletion]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWWitchAreaNotBunny], node,
                            _requirements[RequirementType.FakeFlippersFairyRevival]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWWitchAreaNotBunny], node,
                            _requirements[RequirementType.FakeFlippersSplashDeletion]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldEastNotBunny], node,
                            _requirements[RequirementType.FakeFlippersFairyRevival]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldEastNotBunny], node,
                            _requirements[RequirementType.FakeFlippersSplashDeletion]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouthEastNotBunny], node,
                            _requirements[RequirementType.FakeFlippersFairyRevival]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouthEastNotBunny], node,
                            _requirements[RequirementType.FakeFlippersSplashDeletion]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.IcePalaceIslandInverted], node,
                            _requirements[RequirementType.FakeFlippersFairyRevival]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.IcePalaceIslandInverted], node,
                            _requirements[RequirementType.FakeFlippersSplashDeletion]));
                    }
                    break;
                case RequirementNodeID.DWLakeHyliaWaterWalk:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.WaterWalk]));
                    }
                    break;
                case RequirementNodeID.IcePalaceIsland:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LakeHyliaFairyIsland], node,
                            _requirements[RequirementType.LWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LakeHyliaFairyIslandStandardOpen], node,
                            _requirements[RequirementType.Gloves2]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWLakeHyliaFlippers], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWLakeHyliaFakeFlippers], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWLakeHyliaWaterWalk], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.IcePalaceIslandInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.IcePalaceIsland], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DarkWorldSouthEast:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.FluteInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWLakeHyliaFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWLakeHyliaFakeFlippers], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWLakeHyliaWaterWalk], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.DarkWorldSouthEastNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouthEast], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.DarkWorldSouthEastLift1:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouthEastNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainWestBottom:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.FluteInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEntryCaveDark], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainWestBottom], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainWestBottom], node,
                            _requirements[RequirementType.LWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainTop], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.DarkDeathMountainWestBottomInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainWestBottom], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainWestBottomNonEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainWestBottom], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainWestBottomMirror:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainWestBottom], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainWestBottomNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainWestBottom], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.SpikeCavePastHammerBlocks:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainWestBottomNotBunny], node,
                            _requirements[RequirementType.Hammer]));
                    }
                    break;
                case RequirementNodeID.SpikeCavePastSpikes:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.SpikeCavePastHammerBlocks], node,
                            _requirements[RequirementType.SpikeCave]));
                    }
                    break;
                case RequirementNodeID.SpikeCaveChest:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.SpikeCavePastSpikes], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainTop:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainWestTop], node,
                            _requirements[RequirementType.LWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEastTop], node,
                            _requirements[RequirementType.LWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainWestBottomInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.SuperBunnyCave], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWFloatingIsland], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWTurtleRockTop], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.DarkDeathMountainTopInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainTop], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainTopStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainTop], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainTopMirror:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainTop], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainTopNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainTop], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.SuperBunnyCave:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainTop], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainEastBottom], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                    }
                    break;
                case RequirementNodeID.SuperBunnyCaveChests:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.SuperBunnyCave], node,
                            _requirements[RequirementType.NotBunnyDW]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.SuperBunnyCave], node,
                            _requirements[RequirementType.SuperBunnyFallInHole]));
                    }
                    break;
                case RequirementNodeID.GanonsTowerEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainTopInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainTopStandardOpen], node,
                            _requirements[RequirementType.GTCrystal]));
                    }
                    break;
                case RequirementNodeID.GanonsTowerEntranceStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.GanonsTowerEntrance], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.DWFloatingIsland:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWFloatingIsland], node,
                            _requirements[RequirementType.LWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.HookshotCaveEntrance], node,
                            _requirements[RequirementType.EntranceShuffleNoneDungeon]));
                    }
                    break;
                case RequirementNodeID.HookshotCaveEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainTopNotBunny], node,
                            _requirements[RequirementType.Gloves1]));
                    }
                    break;
                case RequirementNodeID.HookshotCaveEntranceHookshot:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.HookshotCaveEntrance], node,
                            _requirements[RequirementType.Hookshot]));
                    }
                    break;
                case RequirementNodeID.HookshotCaveEntranceHover:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.HookshotCaveEntrance], node,
                            _requirements[RequirementType.Hover]));
                    }
                    break;
                case RequirementNodeID.HookshotCaveBonkableChest:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.HookshotCaveEntranceHookshot], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.HookshotCaveEntrance], node,
                            _requirements[RequirementType.BonkOverLedge]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.HookshotCaveEntranceHover], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.HookshotCaveBack:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.HookshotCaveEntranceHookshot], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.HookshotCaveEntranceHover], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.DWTurtleRockTop:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LWTurtleRockTopStandardOpen], node,
                            _requirements[RequirementType.Hammer]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainTopInverted], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.DWTurtleRockTopInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWTurtleRockTop], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DWTurtleRockTopNotBunny:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWTurtleRockTop], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.TurtleRockFrontEntrance:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DWTurtleRockTopNotBunny], node,
                            _requirements[RequirementType.TRMedallion]));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainEastBottom:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEastBottom], node,
                            _requirements[RequirementType.LWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEastBottomLift2], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainTop], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.DarkDeathMountainEastBottomInverted:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainEastBottom], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainEastBottomConnector:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkDeathMountainEastBottom], node,
                            _requirements[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.TurtleRockTunnel:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.SpiralCaveLedge], node,
                            _requirements[RequirementType.LWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.MimicCaveLedge], node,
                            _requirements[RequirementType.LWMirror]));
                    }
                    break;
                case RequirementNodeID.TurtleRockTunnelMirror:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.TurtleRockTunnel], node,
                            _requirements[RequirementType.DWMirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.TRKeyDoorsToMiddleExit], node,
                            _requirements[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.TurtleRockSafetyDoor:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainEastTopConnector], node,
                            _requirements[RequirementType.LWMirror]));
                    }
                    break;
                case RequirementNodeID.HCSanctuaryEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldNotBunnyOrSuperBunnyMirror], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.HCFrontEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldNotBunnyOrDungeonRevive], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.HCBackEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EscapeGrave], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.ATEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.AgahnimTowerEntrance], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.GanonsTowerEntrance], node,
                            _requirements[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.EPEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldNotBunnyOrDungeonRevive], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.DPFrontEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DesertPalaceFrontEntrance], node,
                            _requirements[RequirementType.NotBunnyLW]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DesertPalaceFrontEntrance], node,
                            _requirements[RequirementType.DungeonRevive]));
                    }
                    break;
                case RequirementNodeID.DPLeftEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DesertLedge], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.DPBackEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DesertBack], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.ToHEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainWestTopNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DeathMountainWestTop], node,
                            _requirements[RequirementType.DungeonRevive]));
                    }
                    break;
                case RequirementNodeID.PoDEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldEastNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.SPEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.LightWorldInvertedNotBunny], node,
                            _requirements[RequirementType.Mirror]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldSouthStandardOpenNotBunny], node,
                            _requirements[RequirementType.Mirror]));
                    }
                    break;
                case RequirementNodeID.SWFrontEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldWest], node,
                            _requirements[RequirementType.DungeonRevive]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.Start], node,
                            _requirements[RequirementType.EntranceShuffleInsanity]));
                    }
                    break;
                case RequirementNodeID.SWBackEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.SkullWoodsBack], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.TTEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.DarkWorldWestNotBunny], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.IPEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.IcePalaceIsland], node,
                            _requirements[RequirementType.NotBunnyDW]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.IcePalaceIsland], node,
                            _requirements[RequirementType.DungeonRevive]));
                    }
                    break;
                case RequirementNodeID.MMEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.MiseryMireEntrance], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.TRFrontEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.TurtleRockFrontEntrance], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.TRFrontEntryStandardOpen:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.TRFrontEntry], node,
                            _requirements[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.TRFrontEntryStandardOpenEntranceNone:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.TRFrontEntryStandardOpen], node,
                            _requirements[RequirementType.EntranceShuffleNone]));
                    }
                    break;
                case RequirementNodeID.TRFrontToKeyDoors:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.TRFrontEntryStandardOpenEntranceNone], node,
                            _requirements[RequirementType.CaneOfSomaria]));
                    }
                    break;
                case RequirementNodeID.TRKeyDoorsToMiddleExit:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.TRFrontToKeyDoors], node,
                            _requirements[RequirementType.TRKeyDoorsToMiddleExit]));
                    }
                    break;
                case RequirementNodeID.TRMiddleEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.TurtleRockTunnel], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.TRBackEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.TurtleRockSafetyDoor], node,
                            _requirements[RequirementType.NoRequirement]));

                    }
                    break;
                case RequirementNodeID.GTEntry:
                    {
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.EntranceDungeonAllInsanity], node,
                            _requirements[RequirementType.NoRequirement]));

                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.AgahnimTowerEntrance], node,
                            _requirements[RequirementType.WorldStateInverted]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.GanonsTowerEntranceStandardOpen], node,
                            _requirements[RequirementType.NotBunnyDW]));
                        connections.Add(_connectionFactory(
                            _requirementNodes[RequirementNodeID.GanonsTowerEntranceStandardOpen], node,
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
        /// The requirement node ID.
        /// </param>
        /// <returns>
        /// A new requirement node.
        /// </returns>
        public IRequirementNode GetRequirementNode(RequirementNodeID id)
        {
            return id == RequirementNodeID.Start ? _startFactory() : _factory();
        }
    }
}
