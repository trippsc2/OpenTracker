using System;
using System.Collections.Generic;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Item.Prizes;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.SequenceBreaks;

namespace OpenTracker.Models.Nodes.Factories
{
    public class LightWorldConnectionFactory : ILightWorldConnectionFactory
    {
        private readonly IComplexRequirementDictionary _complexRequirements;
        private readonly IItemRequirementDictionary _itemRequirements;
        private readonly IPrizeRequirementDictionary _prizeRequirements;
        private readonly ISequenceBreakRequirementDictionary _sequenceBreakRequirements;
        private readonly IWorldStateRequirementDictionary _worldStateRequirements;
        
        private readonly IOverworldNodeDictionary _overworldNodes;

        private readonly INodeConnection.Factory _connectionFactory;
        
        public IEnumerable<INodeConnection> GetNodeConnections(OverworldNodeID id, INode node)
        {
            return id switch
            {
                OverworldNodeID.LightWorld => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.Start], node,
                        _worldStateRequirements[WorldState.StandardOpen]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainEntry], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DeathMountainExit], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWKakarikoPortalNotBunny], node,
                        _itemRequirements[(ItemType.Hammer, 1)]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWKakarikoPortalNotBunny], node,
                        _itemRequirements[(ItemType.Gloves, 2)]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DesertLedge], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.GrassHouse], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyLW]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.BombHut], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyLW]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.RaceGameLedge], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.CastleSecretExitArea], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyLW]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.SouthOfGroveLedge], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.CheckerboardLedge], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWMirePortal], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWSouthPortalNotBunny], node,
                        _itemRequirements[(ItemType.Hammer, 1)]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWWitchAreaNotBunny], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LWEastPortalNotBunny], node,
                        _itemRequirements[(ItemType.Hammer, 1)]),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.DarkWorldSouthInverted], node,
                        _prizeRequirements[(PrizeType.Aga1, 1)]),
                },
                OverworldNodeID.LightWorldInverted => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorld], node,
                        _worldStateRequirements[WorldState.Inverted])
                },
                OverworldNodeID.LightWorldInvertedNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorldInverted], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.LightWorldStandardOpen => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorld], node,
                        _worldStateRequirements[WorldState.StandardOpen])
                },
                OverworldNodeID.LightWorldMirror => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorld], node,
                        _complexRequirements[ComplexRequirementType.LWMirror]),
                },
                OverworldNodeID.LightWorldNotBunny => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorld], node,
                        _complexRequirements[ComplexRequirementType.NotBunnyLW])
                },
                OverworldNodeID.LightWorldNotBunnyOrDungeonRevive => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorldNotBunny], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorld], node,
                        _sequenceBreakRequirements[SequenceBreakType.DungeonRevive])
                },
                OverworldNodeID.LightWorldNotBunnyOrSuperBunnyFallInHole => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorldNotBunny], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorld], node,
                        _sequenceBreakRequirements[SequenceBreakType.SuperBunnyFallInHole])
                },
                OverworldNodeID.LightWorldNotBunnyOrSuperBunnyMirror => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorldNotBunny], node),
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorld], node,
                        _complexRequirements[ComplexRequirementType.SuperBunnyMirror])
                },
                OverworldNodeID.LightWorldDash => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                        _itemRequirements[(ItemType.Boots, 1)])
                },
                OverworldNodeID.LightWorldHammer => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                        _itemRequirements[(ItemType.Hammer, 1)])
                },
                OverworldNodeID.LightWorldLift1 => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                        _itemRequirements[(ItemType.Gloves, 1)])
                },
                OverworldNodeID.LightWorldFlute => new List<INodeConnection>
                {
                    _connectionFactory(
                        _overworldNodes[OverworldNodeID.LightWorldNotBunny], node,
                        _itemRequirements[(ItemType.Flute, 1)])
                },
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }
    }
}