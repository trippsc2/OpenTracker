using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.RequirementNodes
{
    /// <summary>
    /// This is the class for creating requirement node classes.
    /// </summary>
    public static class RequirementNodeFactory
    {
        /// <summary>
        /// Populates the list of connections to the specified requirement node ID.
        /// </summary>
        /// <param name="id">
        /// The requirement node ID.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <param name="connections">
        /// The list of connections to be populated.
        /// </param>
        public static void PopulateNodeConnections(
            RequirementNodeID id, IRequirementNode node, List<INodeConnection> connections)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (connections == null)
            {
                throw new ArgumentNullException(nameof(connections));
            }

            switch (id)
            {
                case RequirementNodeID.EntranceDungeon:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.Start], node,
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleDungeonOn]));
                    }
                    break;
                case RequirementNodeID.NonEntrance:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.Start], node,
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]));
                    }
                    break;
                case RequirementNodeID.NonEntranceInverted:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.NonEntrance], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.LightWorld:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.Start], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEntry], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainExit], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWKakarikoPortalNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWKakarikoPortalNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Gloves2]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DesertLedge], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.GrassHouse], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BombHut], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.RaceGameLedge], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.SouthOfGroveLedge], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.CheckerboardLedge], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWMirePortal], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWSouthPortalNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWWitchAreaNotBunny], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWEastPortalNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouthInverted], node,
                            RequirementDictionary.Instance[RequirementType.Aga1]));
                    }
                    break;
                case RequirementNodeID.LightWorldInverted:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorld], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.LightWorldInvertedNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldInverted], node,
                            RequirementDictionary.Instance[RequirementType.MoonPearl]));
                    }
                    break;
                case RequirementNodeID.LightWorldStandardOpen:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorld], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.LightWorldInspect:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorld], node,
                            RequirementDictionary.Instance[RequirementType.Inspect]));
                    }
                    break;
                case RequirementNodeID.LightWorldMirror:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorld], node,
                            RequirementDictionary.Instance[RequirementType.LWMirror]));
                    }
                    break;
                case RequirementNodeID.LightWorldNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorld], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.LightWorldNotBunnyOrInspect:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunny], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldInspect], node));
                    }
                    break;
                case RequirementNodeID.LightWorldNotBunnyOrDungeonRevive:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunny], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorld], node,
                            RequirementDictionary.Instance[RequirementType.DungeonRevive]));
                    }
                    break;
                case RequirementNodeID.LightWorldNotBunnyOrSuperBunnyFallInHole:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunny], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorld], node,
                            RequirementDictionary.Instance[RequirementType.SuperBunnyFallInHole]));
                    }
                    break;
                case RequirementNodeID.LightWorldNotBunnyOrSuperBunnyMirror:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunny], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorld], node,
                            RequirementDictionary.Instance[RequirementType.SuperBunnyMirror]));
                    }
                    break;
                case RequirementNodeID.LightWorldDash:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Boots]));
                    }
                    break;
                case RequirementNodeID.LightWorldHammer:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                    }
                    break;
                case RequirementNodeID.LightWorldLift1:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                    }
                    break;
                case RequirementNodeID.Flute:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Flute]));
                    }
                    break;
                case RequirementNodeID.FluteInverted:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.Flute], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.FluteStandardOpen:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.Flute], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.Pedestal:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorld], node,
                            RequirementDictionary.Instance[RequirementType.Pedestal]));
                    }
                    break;
                case RequirementNodeID.LumberjackCaveHole:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldDash], node,
                            RequirementDictionary.Instance[RequirementType.Aga1]));
                    }
                    break;
                case RequirementNodeID.LumberjackCave:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LumberjackCaveHole], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldInspect], node));
                    }
                    break;
                case RequirementNodeID.DeathMountainEntry:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldLift1], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BumperCaveEntry], node,
                            RequirementDictionary.Instance[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.DeathMountainEntryNonEntrance:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEntry], node,
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]));
                    }
                    break;
                case RequirementNodeID.DeathMountainEntryCave:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEntryNonEntrance], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainWestBottomNonEntrance], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BumperCaveEntryNonEntrance], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainWestBottomNonEntrance], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DeathMountainEntryCaveDark:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEntryCave], node,
                            RequirementDictionary.Instance[RequirementType.DarkRoomDeathMountainEntry]));
                    }
                    break;
                case RequirementNodeID.DeathMountainExit:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainExitCaveDark], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BumperCaveBack], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BumperCaveTop], node,
                            RequirementDictionary.Instance[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.DeathMountainExitNonEntrance:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainExit], node,
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]));
                    }
                    break;
                case RequirementNodeID.DeathMountainExitCave:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainExitNonEntrance], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainWestBottomNonEntrance], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.DeathMountainExitCaveDark:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainExitCave], node,
                            RequirementDictionary.Instance[RequirementType.DarkRoomDeathMountainExit]));
                    }
                    break;
                case RequirementNodeID.LWKakarikoPortal:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Gloves2]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldHammer], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWKakarikoPortalInverted], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                    }
                    break;
                case RequirementNodeID.LWKakarikoPortalStandardOpen:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWKakarikoPortal], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.LWKakarikoPortalNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWKakarikoPortal], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.SickKid:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorld], node,
                            RequirementDictionary.Instance[RequirementType.Bottle]));
                    }
                    break;
                case RequirementNodeID.GrassHouse:
                case RequirementNodeID.BombHut:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunny], node));
                    }
                    break;
                case RequirementNodeID.MagicBatLedge:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldHammer], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.HammerPegsArea], node,
                            RequirementDictionary.Instance[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.MagicBat:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.MagicBatLedge], node,
                            RequirementDictionary.Instance[RequirementType.MagicBat]));
                    }
                    break;
                case RequirementNodeID.MagicBatEntrance:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.MagicBatLedge], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldInspect], node));
                    }
                    break;
                case RequirementNodeID.Library:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldDash], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldInspect], node));
                    }
                    break;
                case RequirementNodeID.RaceGameLedge:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouthMirror], node));
                    }
                    break;
                case RequirementNodeID.RaceGame:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.RaceGameLedge], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldInspect], node));
                    }
                    break;
                case RequirementNodeID.SouthOfGroveLedge:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldInverted], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouthMirror], node));
                    }
                    break;
                case RequirementNodeID.SouthOfGrove:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.SouthOfGroveLedge], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.SouthOfGroveLedge], node,
                            RequirementDictionary.Instance[RequirementType.SuperBunnyMirror]));
                    }
                    break;
                case RequirementNodeID.GroveDiggingSpot:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Shovel]));
                    }
                    break;
                case RequirementNodeID.DesertLedge:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DesertBackNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.MireAreaMirror], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DPFrontEntry], node,
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]));
                    }
                    break;
                case RequirementNodeID.DesertLedgeItem:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DesertLedge], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldInspect], node));
                    }
                    break;
                case RequirementNodeID.DesertLedgeNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DesertLedge], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.DesertBack:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DesertLedgeNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.MireAreaMirror], node));
                    }
                    break;
                case RequirementNodeID.DesertBackNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DesertBack], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.CheckerboardLedge:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldInverted], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.MireAreaMirror], node));
                    }
                    break;
                case RequirementNodeID.CheckerboardLedgeNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.CheckerboardLedge], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.CheckerboardCave:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.CheckerboardLedgeNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                    }
                    break;
                case RequirementNodeID.DesertPalaceFrontEntrance:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorld], node,
                            RequirementDictionary.Instance[RequirementType.Book]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.MireAreaMirror], node));
                    }
                    break;
                case RequirementNodeID.BombosTabletLedge:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldInverted], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouthMirror], node));
                    }
                    break;
                case RequirementNodeID.BombosTablet:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BombosTabletLedge], node,
                            RequirementDictionary.Instance[RequirementType.Tablet]));
                    }
                    break;
                case RequirementNodeID.LWMirePortal:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.FluteStandardOpen], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWMirePortalInverted], node,
                            RequirementDictionary.Instance[RequirementType.Gloves2]));
                    }
                    break;
                case RequirementNodeID.LWMirePortalStandardOpen:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWMirePortal], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.LWGraveyard:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunny], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWGraveyardLedge], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.KingsTombNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Gloves2]));
                    }
                    break;
                case RequirementNodeID.LWGraveyardNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWGraveyard], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.LWGraveyardLedge:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWGraveyardNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWGraveyardMirror], node));
                    }
                    break;
                case RequirementNodeID.EscapeGrave:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWGraveyardNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                    }
                    break;
                case RequirementNodeID.SanctuaryGraveEntrance:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.EscapeGrave], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldInspect], node));
                    }
                    break;
                case RequirementNodeID.KingsTomb:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWGraveyardNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Gloves2]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWGraveyardMirror], node));
                    }
                    break;
                case RequirementNodeID.KingsTombNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.KingsTomb], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.KingsTombGrave:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.KingsTombNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Boots]));
                    }
                    break;
                case RequirementNodeID.HyruleCastleTop:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorld], node,
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldEast], node,
                            RequirementDictionary.Instance[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.HyruleCastleTopInverted:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.HyruleCastleTop], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.HyruleCastleTopStandardOpen:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.HyruleCastleTop], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.AgahnimTowerEntrance:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.HyruleCastleTopInverted], node,
                            RequirementDictionary.Instance[RequirementType.GTCrystal]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.HyruleCastleTopStandardOpen], node,
                            RequirementDictionary.Instance[RequirementType.ATBarrier]));
                    }
                    break;
                case RequirementNodeID.GanonHole:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.HyruleCastleTopInverted], node,
                            RequirementDictionary.Instance[RequirementType.Aga2]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldEastStandardOpen], node,
                            RequirementDictionary.Instance[RequirementType.Aga2]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldInspect], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.LWSouthPortal:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldHammer], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWSouthPortalInverted], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                    }
                    break;
                case RequirementNodeID.LWSouthPortalStandardOpen:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWSouthPortal], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.LWSouthPortalNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWSouthPortal], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.ZoraArea:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWLakeHyliaFlippers], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWWitchAreaNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.CatfishArea], node,
                            RequirementDictionary.Instance[RequirementType.DWMirror]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWLakeHyliaFakeFlippers], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWLakeHyliaWaterWalk], node));
                    }
                    break;
                case RequirementNodeID.ZoraLedge:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.ZoraArea], node,
                            RequirementDictionary.Instance[RequirementType.Flippers]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.ZoraArea], node,
                            RequirementDictionary.Instance[RequirementType.FakeFlippersSplashDeletion]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.ZoraArea], node,
                            RequirementDictionary.Instance[RequirementType.WaterWalk]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.ZoraArea], node,
                            RequirementDictionary.Instance[RequirementType.Inspect]));
                    }
                    break;
                case RequirementNodeID.WaterfallFairy:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWLakeHyliaFlippers], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWLakeHyliaFakeFlippers], node,
                            RequirementDictionary.Instance[RequirementType.MoonPearl]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWLakeHyliaWaterWalk], node));
                    }
                    break;
                case RequirementNodeID.WaterfallFairyNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.WaterfallFairy], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.LWWitchArea:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunny], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.ZoraArea], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                    }
                    break;
                case RequirementNodeID.LWWitchAreaNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWWitchArea], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.WitchsHut:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWWitchArea], node,
                            RequirementDictionary.Instance[RequirementType.Mushroom]));
                    }
                    break;
                case RequirementNodeID.Sahasrahla:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorld], node,
                            RequirementDictionary.Instance[RequirementType.GreenPendant]));
                    }
                    break;
                case RequirementNodeID.LWEastPortal:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldHammer], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWEastPortalInverted], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                    }
                    break;
                case RequirementNodeID.LWEastPortalStandardOpen:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWEastPortal], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.LWEastPortalNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWEastPortal], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.LWLakeHyliaFakeFlippers:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.FakeFlippersFairyRevival]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.FakeFlippersScreenTransition]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.FakeFlippersSplashDeletion]));
                    }
                    break;
                case RequirementNodeID.LWLakeHyliaFlippers:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Flippers]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.WaterfallFairyNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Flippers]));
                    }
                    break;
                case RequirementNodeID.LWLakeHyliaWaterWalk:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.WaterWalk]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.WaterfallFairyNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.WaterWalkFromWaterfallCave]));
                    }
                    break;
                case RequirementNodeID.Hobo:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWLakeHyliaFlippers], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWLakeHyliaFakeFlippers], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWLakeHyliaWaterWalk], node));
                    }
                    break;
                case RequirementNodeID.LakeHyliaIsland:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWLakeHyliaFlippers], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWLakeHyliaFlippers], node,
                            RequirementDictionary.Instance[RequirementType.DWMirror]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWLakeHyliaFakeFlippers], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWLakeHyliaWaterWalk], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWLakeHyliaWaterWalk], node,
                            RequirementDictionary.Instance[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.LakeHyliaIslandItem:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LakeHyliaIsland], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldInspect], node));
                    }
                    break;
                case RequirementNodeID.LakeHyliaFairyIsland:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWLakeHyliaFlippers], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.IcePalaceIsland], node,
                            RequirementDictionary.Instance[RequirementType.DWMirror]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWLakeHyliaFakeFlippers], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWLakeHyliaWaterWalk], node));
                    }
                    break;
                case RequirementNodeID.LakeHyliaFairyIslandStandardOpen:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LakeHyliaFairyIsland], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.DeathMountainWestBottom:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.FluteStandardOpen], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEntryCaveDark], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainExitCaveDark], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainWestTop], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastBottomNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Hookshot]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainWestBottomInverted], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainWestBottomMirror], node));
                    }
                    break;
                case RequirementNodeID.DeathMountainWestBottomNonEntrance:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainWestBottom], node,
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]));
                    }
                    break;
                case RequirementNodeID.DeathMountainWestBottomNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainWestBottom], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.SpectacleRockTop:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainWestTop], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainWestBottomMirror], node));
                    }
                    break;
                case RequirementNodeID.SpectacleRockTopItem:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.SpectacleRockTop], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainWestBottom], node,
                            RequirementDictionary.Instance[RequirementType.Inspect]));
                    }
                    break;
                case RequirementNodeID.DeathMountainWestTop:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.SpectacleRockTop], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastTopNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainTopMirror], node));
                    }
                    break;
                case RequirementNodeID.DeathMountainWestTopNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainWestTop], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.EtherTablet:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainWestTop], node,
                            RequirementDictionary.Instance[RequirementType.Tablet]));
                    }
                    break;
                case RequirementNodeID.DeathMountainEastBottom:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainWestBottomNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Hookshot]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastBottomConnector], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.ParadoxCave], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastTop], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.SpiralCaveLedge], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.MimicCaveLedge], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainEastBottom], node,
                            RequirementDictionary.Instance[RequirementType.DWMirror]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainEastBottomInverted], node,
                            RequirementDictionary.Instance[RequirementType.Gloves2]));
                    }
                    break;
                case RequirementNodeID.DeathMountainEastBottomNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastBottom], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.DeathMountainEastBottomLift2:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastBottomNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Gloves2]));
                    }
                    break;
                case RequirementNodeID.DeathMountainEastBottomConnector:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastBottomLift2], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastTopConnector], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainEastBottomConnector], node,
                            RequirementDictionary.Instance[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.ParadoxCave:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastBottom], node,
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastTop], node,
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]));
                    }
                    break;
                case RequirementNodeID.ParadoxCaveSuperBunnyFallInHole:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.ParadoxCave], node,
                            RequirementDictionary.Instance[RequirementType.SuperBunnyFallInHole]));
                    }
                    break;
                case RequirementNodeID.ParadoxCaveNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.ParadoxCave], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.ParadoxCaveTop:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.ParadoxCaveNotBunny], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.ParadoxCaveSuperBunnyFallInHole], node,
                            RequirementDictionary.Instance[RequirementType.Sword2]));
                    }
                    break;
                case RequirementNodeID.DeathMountainEastTop:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainWestTopNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.ParadoxCave], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWTurtleRockTopInvertedNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainTopMirror], node));
                    }
                    break;
                case RequirementNodeID.DeathMountainEastTopInverted:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastTop], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DeathMountainEastTopNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastTop], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.DeathMountainEastTopConnector:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastTop], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.TurtleRockSafetyDoor], node,
                            RequirementDictionary.Instance[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.SpiralCaveLedge:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastTop], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.TurtleRockTunnelMirror], node));
                    }
                    break;
                case RequirementNodeID.SpiralCave:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.SpiralCaveLedge], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.SpiralCaveLedge], node,
                            RequirementDictionary.Instance[RequirementType.SuperBunnyFallInHole]));
                    }
                    break;
                case RequirementNodeID.MimicCaveLedge:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastTopInverted], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.TurtleRockTunnelMirror], node));
                    }
                    break;
                case RequirementNodeID.MimicCaveLedgeNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.MimicCaveLedge], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.MimicCave:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.MimicCaveLedgeNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                    }
                    break;
                case RequirementNodeID.LWFloatingIsland:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastTopInverted], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWFloatingIsland], node,
                            RequirementDictionary.Instance[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.FloatingIsland:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWFloatingIsland], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastTop], node,
                            RequirementDictionary.Instance[RequirementType.Inspect]));
                    }
                    break;
                case RequirementNodeID.LWTurtleRockTop:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastTopNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Gloves2]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWTurtleRockTopInverted], node,
                            RequirementDictionary.Instance[RequirementType.Gloves2]));
                    }
                    break;
                case RequirementNodeID.LWTurtleRockTopInverted:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWTurtleRockTop], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.LWTurtleRockTopInvertedNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWTurtleRockTopInverted], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                    }
                    break;
                case RequirementNodeID.LWTurtleRockTopStandardOpen:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWTurtleRockTop], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.DWKakarikoPortal:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWKakarikoPortalStandardOpen], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWestNotBunny], node));
                    }
                    break;
                case RequirementNodeID.DWKakarikoPortalInverted:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWKakarikoPortal], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DarkWorldWest:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.NonEntranceInverted], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldMirror], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWKakarikoPortal], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BumperCaveEntry], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BumperCaveTop], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.HammerHouseNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouthNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Gloves2]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWWitchAreaNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Hookshot]));
                    }
                    break;
                case RequirementNodeID.DarkWorldWestNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWest], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.DarkWorldWestNotBunnyOrSuperBunnyMirror:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWestNotBunny], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWest], node,
                            RequirementDictionary.Instance[RequirementType.SuperBunnyMirror]));
                    }
                    break;
                case RequirementNodeID.DarkWorldWestLift2:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWestNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Gloves2]));
                    }
                    break;
                case RequirementNodeID.SkullWoodsBack:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWestNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.FireRod]));
                    }
                    break;
                case RequirementNodeID.BumperCaveEntry:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWestNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEntry], node,
                            RequirementDictionary.Instance[RequirementType.LWMirror]));
                    }
                    break;
                case RequirementNodeID.BumperCaveEntryNonEntrance:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BumperCaveEntry], node,
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]));
                    }
                    break;
                case RequirementNodeID.BumperCaveFront:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEntryNonEntrance], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BumperCaveEntryNonEntrance], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.BumperCaveNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BumperCaveFront], node,
                            RequirementDictionary.Instance[RequirementType.MoonPearl]));
                    }
                    break;
                case RequirementNodeID.BumperCavePastGap:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BumperCaveNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.BumperCaveGap]));
                    }
                    break;
                case RequirementNodeID.BumperCaveBack:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BumperCavePastGap], node,
                            RequirementDictionary.Instance[RequirementType.Cape]));
                    }
                    break;
                case RequirementNodeID.BumperCaveTop:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainExit], node,
                            RequirementDictionary.Instance[RequirementType.LWMirror]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BumperCaveBack], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.BumperCaveItem:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BumperCaveTop], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWest], node,
                            RequirementDictionary.Instance[RequirementType.Inspect]));
                    }
                    break;
                case RequirementNodeID.HammerHouse:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWestNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                    }
                    break;
                case RequirementNodeID.HammerHouseNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.HammerHouse], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.HammerPegsArea:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldMirror], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWestLift2], node));
                    }
                    break;
                case RequirementNodeID.HammerPegs:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.HammerPegsArea], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                    }
                    break;
                case RequirementNodeID.PurpleChest:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.HammerPegsArea], node,
                            RequirementDictionary.Instance[RequirementType.LightWorld]));
                    }
                    break;
                case RequirementNodeID.BlacksmithPrison:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldMirror], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWestLift2], node));
                    }
                    break;
                case RequirementNodeID.Blacksmith:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BlacksmithPrison], node,
                            RequirementDictionary.Instance[RequirementType.LightWorld]));
                    }
                    break;
                case RequirementNodeID.DarkWorldSouth:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.NonEntranceInverted], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldMirror], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWSouthPortalNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWest], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldEastHammer], node));
                    }
                    break;
                case RequirementNodeID.DarkWorldSouthInverted:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouth], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DarkWorldSouthStandardOpen:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouth], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.DarkWorldSouthStandardOpenNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouthStandardOpen], node,
                            RequirementDictionary.Instance[RequirementType.MoonPearl]));
                    }
                    break;
                case RequirementNodeID.DarkWorldSouthMirror:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouth], node,
                            RequirementDictionary.Instance[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.DarkWorldSouthNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouth], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.DarkWorldSouthDash:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouthNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Boots]));
                    }
                    break;
                case RequirementNodeID.DarkWorldSouthHammer:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouthNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                    }
                    break;
                case RequirementNodeID.BuyBigBomb:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldInvertedNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.RedCrystal]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouthStandardOpenNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.RedCrystal]));
                    }
                    break;
                case RequirementNodeID.BuyBigBombStandardOpen:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BuyBigBomb], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.BigBombToLightWorld:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BuyBigBomb], node,
                            RequirementDictionary.Instance[RequirementType.DWMirror]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BuyBigBomb], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.BigBombToLightWorldStandardOpen:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BigBombToLightWorld], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.BigBombToDWLakeHylia:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BuyBigBombStandardOpen], node,
                            RequirementDictionary.Instance[RequirementType.BombDuplicationMirror]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BuyBigBombStandardOpen], node,
                            RequirementDictionary.Instance[RequirementType.BombDuplicationAncillaOverload]));
                    }
                    break;
                case RequirementNodeID.BigBombToWall:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BuyBigBombStandardOpen], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BigBombToLightWorld], node,
                            RequirementDictionary.Instance[RequirementType.LWMirror]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BigBombToLightWorldStandardOpen], node,
                            RequirementDictionary.Instance[RequirementType.Aga1]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.BigBombToDWLakeHylia], node));
                    }
                    break;
                case RequirementNodeID.DWSouthPortal:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWSouthPortalStandardOpen], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouthHammer], node));
                    }
                    break;
                case RequirementNodeID.DWSouthPortalInverted:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWSouthPortal], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DWSouthPortalNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWSouthPortal], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.MireArea:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldMirror], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWMirePortal], node));
                    }
                    break;
                case RequirementNodeID.MireAreaMirror:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.MireArea], node,
                            RequirementDictionary.Instance[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.MireAreaNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.MireArea], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.MireAreaNotBunnyOrSuperBunnyMirror:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.MireAreaNotBunny], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.MireArea], node,
                            RequirementDictionary.Instance[RequirementType.SuperBunnyMirror]));
                    }
                    break;
                case RequirementNodeID.MiseryMireEntrance:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.MireAreaNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.MMMedallion]));
                    }
                    break;
                case RequirementNodeID.DWMirePortal:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.FluteInverted], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWMirePortalStandardOpen], node,
                            RequirementDictionary.Instance[RequirementType.Gloves2]));
                    }
                    break;
                case RequirementNodeID.DWMirePortalInverted:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWMirePortal], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DWGraveyard:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWestNotBunny], node));
                    }
                    break;
                case RequirementNodeID.DWGraveyardMirror:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWGraveyard], node,
                            RequirementDictionary.Instance[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.DWWitchArea:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWWitchArea], node,
                            RequirementDictionary.Instance[RequirementType.LWMirror]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldEastNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldEastNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWLakeHyliaFlippers], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWLakeHyliaFakeFlippers], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWLakeHyliaWaterWalk], node));
                    }
                    break;
                case RequirementNodeID.DWWitchAreaNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWWitchArea], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.CatfishArea:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.ZoraArea], node,
                            RequirementDictionary.Instance[RequirementType.LWMirror]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWWitchAreaNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                    }
                    break;
                case RequirementNodeID.DarkWorldEast:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldStandardOpen], node,
                            RequirementDictionary.Instance[RequirementType.Aga1]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldMirror], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWWitchAreaNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWWitchAreaNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouthHammer], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWEastPortalNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWLakeHyliaFlippers], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWLakeHyliaFakeFlippers], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWLakeHyliaWaterWalk], node));
                    }
                    break;
                case RequirementNodeID.DarkWorldEastStandardOpen:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldEast], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.DarkWorldEastNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldEast], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.DarkWorldEastHammer:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldEastNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                    }
                    break;
                case RequirementNodeID.FatFairyEntrance:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldEastNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.RedCrystal]));
                    }
                    break;
                case RequirementNodeID.DWEastPortal:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWEastPortalStandardOpen], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldEastHammer], node));
                    }
                    break;
                case RequirementNodeID.DWEastPortalInverted:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWEastPortal], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DWEastPortalNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWEastPortal], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.DWLakeHyliaFlippers:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWestNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Flippers]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouthNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Flippers]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWWitchAreaNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Flippers]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldEastNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Flippers]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouthEastNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Flippers]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.IcePalaceIslandInverted], node,
                            RequirementDictionary.Instance[RequirementType.Flippers]));
                    }
                    break;
                case RequirementNodeID.DWLakeHyliaFakeFlippers:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWestNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.FakeFlippersFairyRevival]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWestNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.FakeFlippersQirnJump]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWestNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.FakeFlippersSplashDeletion]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouthNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.FakeFlippersFairyRevival]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouthNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.FakeFlippersSplashDeletion]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWWitchAreaNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.FakeFlippersFairyRevival]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWWitchAreaNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.FakeFlippersSplashDeletion]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldEastNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.FakeFlippersFairyRevival]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldEastNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.FakeFlippersSplashDeletion]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouthEastNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.FakeFlippersFairyRevival]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouthEastNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.FakeFlippersSplashDeletion]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.IcePalaceIslandInverted], node,
                            RequirementDictionary.Instance[RequirementType.FakeFlippersFairyRevival]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.IcePalaceIslandInverted], node,
                            RequirementDictionary.Instance[RequirementType.FakeFlippersSplashDeletion]));
                    }
                    break;
                case RequirementNodeID.DWLakeHyliaWaterWalk:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWestNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.WaterWalk]));
                    }
                    break;
                case RequirementNodeID.IcePalaceIsland:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LakeHyliaFairyIsland], node,
                            RequirementDictionary.Instance[RequirementType.LWMirror]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LakeHyliaFairyIslandStandardOpen], node,
                            RequirementDictionary.Instance[RequirementType.Gloves2]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWLakeHyliaFlippers], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWLakeHyliaFakeFlippers], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWLakeHyliaWaterWalk], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.IcePalaceIslandInverted:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.IcePalaceIsland], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DarkWorldSouthEast:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldMirror], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWLakeHyliaFlippers], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWLakeHyliaFakeFlippers], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWLakeHyliaWaterWalk], node));
                    }
                    break;
                case RequirementNodeID.DarkWorldSouthEastNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouthEast], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.DarkWorldSouthEastLift1:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouthEastNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainWestBottom:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.FluteInverted], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEntryCaveDark], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainWestBottom], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainWestBottom], node,
                            RequirementDictionary.Instance[RequirementType.LWMirror]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainTop], node));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainWestBottomInverted:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainWestBottom], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainWestBottomNonEntrance:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainWestBottom], node,
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainWestBottomMirror:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainWestBottom], node,
                            RequirementDictionary.Instance[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainWestBottomNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainWestBottom], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.SpikeCavePastHammerBlocks:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainWestBottomNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                    }
                    break;
                case RequirementNodeID.SpikeCavePastSpikes:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.SpikeCavePastHammerBlocks], node,
                            RequirementDictionary.Instance[RequirementType.SpikeCave]));
                    }
                    break;
                case RequirementNodeID.SpikeCaveChest:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.SpikeCavePastSpikes], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainTop:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainWestTop], node,
                            RequirementDictionary.Instance[RequirementType.LWMirror]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastTop], node,
                            RequirementDictionary.Instance[RequirementType.LWMirror]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainWestBottomInverted], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.SuperBunnyCave], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWFloatingIsland], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWTurtleRockTop], node));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainTopInverted:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainTop], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainTopStandardOpen:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainTop], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainTopMirror:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainTop], node,
                            RequirementDictionary.Instance[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainTopNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainTop], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.SuperBunnyCave:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainTop], node,
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainEastBottom], node,
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]));
                    }
                    break;
                case RequirementNodeID.SuperBunnyCaveChests:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.SuperBunnyCave], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.SuperBunnyCave], node,
                            RequirementDictionary.Instance[RequirementType.SuperBunnyFallInHole]));
                    }
                    break;
                case RequirementNodeID.GanonsTowerEntrance:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainTopInverted], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainTopStandardOpen], node,
                            RequirementDictionary.Instance[RequirementType.GTCrystal]));
                    }
                    break;
                case RequirementNodeID.GanonsTowerEntranceStandardOpen:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.GanonsTowerEntrance], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.DWFloatingIsland:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWFloatingIsland], node,
                            RequirementDictionary.Instance[RequirementType.LWMirror]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.HookshotCaveEntrance], node,
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleNotAll]));
                    }
                    break;
                case RequirementNodeID.HookshotCaveEntrance:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainTopNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Gloves1]));
                    }
                    break;
                case RequirementNodeID.HookshotCaveEntranceHookshot:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.HookshotCaveEntrance], node,
                            RequirementDictionary.Instance[RequirementType.Hookshot]));
                    }
                    break;
                case RequirementNodeID.HookshotCaveEntranceHover:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.HookshotCaveEntrance], node,
                            RequirementDictionary.Instance[RequirementType.Hover]));
                    }
                    break;
                case RequirementNodeID.HookshotCaveBonkableChest:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.HookshotCaveEntranceHookshot], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.HookshotCaveEntrance], node,
                            RequirementDictionary.Instance[RequirementType.BonkOverLedge]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.HookshotCaveEntranceHover], node));
                    }
                    break;
                case RequirementNodeID.HookshotCaveBack:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.HookshotCaveEntranceHookshot], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.HookshotCaveEntranceHover], node));
                    }
                    break;
                case RequirementNodeID.DWTurtleRockTop:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LWTurtleRockTopStandardOpen], node,
                            RequirementDictionary.Instance[RequirementType.Hammer]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainTopInverted], node));
                    }
                    break;
                case RequirementNodeID.DWTurtleRockTopInverted:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWTurtleRockTop], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DWTurtleRockTopNotBunny:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWTurtleRockTop], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.TurtleRockFrontEntrance:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DWTurtleRockTopNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.TRMedallion]));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainEastBottom:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastBottom], node,
                            RequirementDictionary.Instance[RequirementType.LWMirror]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastBottomLift2], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainTop], node));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainEastBottomInverted:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainEastBottom], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.DarkDeathMountainEastBottomConnector:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkDeathMountainEastBottom], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW]));
                    }
                    break;
                case RequirementNodeID.TurtleRockTunnel:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.SpiralCaveLedge], node,
                            RequirementDictionary.Instance[RequirementType.LWMirror]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.MimicCaveLedge], node,
                            RequirementDictionary.Instance[RequirementType.LWMirror]));
                    }
                    break;
                case RequirementNodeID.TurtleRockTunnelMirror:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.TurtleRockTunnel], node,
                            RequirementDictionary.Instance[RequirementType.DWMirror]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.TRKeyDoorsToMiddleExit], node,
                            RequirementDictionary.Instance[RequirementType.DWMirror]));
                    }
                    break;
                case RequirementNodeID.TurtleRockSafetyDoor:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainEastTopConnector], node,
                            RequirementDictionary.Instance[RequirementType.LWMirror]));
                    }
                    break;
                case RequirementNodeID.HCSanctuaryEntry:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.EntranceDungeon], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunnyOrSuperBunnyMirror], node));
                    }
                    break;
                case RequirementNodeID.HCFrontEntry:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.EntranceDungeon], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunnyOrDungeonRevive], node));
                    }
                    break;
                case RequirementNodeID.HCBackEntry:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.EntranceDungeon], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.EscapeGrave], node));
                    }
                    break;
                case RequirementNodeID.ATEntry:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.EntranceDungeon], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.AgahnimTowerEntrance], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.GanonsTowerEntrance], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                    }
                    break;
                case RequirementNodeID.EPEntry:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.EntranceDungeon], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldNotBunnyOrDungeonRevive], node));
                    }
                    break;
                case RequirementNodeID.DPFrontEntry:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.EntranceDungeon], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DesertPalaceFrontEntrance], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyLW]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DesertPalaceFrontEntrance], node,
                            RequirementDictionary.Instance[RequirementType.DungeonRevive]));
                    }
                    break;
                case RequirementNodeID.DPLeftEntry:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.EntranceDungeon], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DesertLedge], node));
                    }
                    break;
                case RequirementNodeID.DPBackEntry:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.EntranceDungeon], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DesertBack], node));
                    }
                    break;
                case RequirementNodeID.ToHEntry:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.EntranceDungeon], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainWestTopNotBunny], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DeathMountainWestTop], node,
                            RequirementDictionary.Instance[RequirementType.DungeonRevive]));
                    }
                    break;
                case RequirementNodeID.PoDEntry:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.EntranceDungeon], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldEastNotBunny], node));
                    }
                    break;
                case RequirementNodeID.SPEntry:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.EntranceDungeon], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.LightWorldInvertedNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Mirror]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldSouthStandardOpenNotBunny], node,
                            RequirementDictionary.Instance[RequirementType.Mirror]));
                    }
                    break;
                case RequirementNodeID.SWFrontEntry:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWestNotBunny], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWest], node,
                            RequirementDictionary.Instance[RequirementType.DungeonRevive]));
                    }
                    break;
                case RequirementNodeID.SWBackEntry:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.EntranceDungeon], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.SkullWoodsBack], node));
                    }
                    break;
                case RequirementNodeID.TTEntry:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.EntranceDungeon], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.DarkWorldWestNotBunny], node));
                    }
                    break;
                case RequirementNodeID.IPEntry:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.EntranceDungeon], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.IcePalaceIsland], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.IcePalaceIsland], node,
                            RequirementDictionary.Instance[RequirementType.DungeonRevive]));
                    }
                    break;
                case RequirementNodeID.MMEntry:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.EntranceDungeon], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.MiseryMireEntrance], node));
                    }
                    break;
                case RequirementNodeID.TRFrontEntry:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.EntranceDungeon], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.TurtleRockFrontEntrance], node));
                    }
                    break;
                case RequirementNodeID.TRFrontEntryStandardOpen:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.TRFrontEntry], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateStandardOpen]));
                    }
                    break;
                case RequirementNodeID.TRFrontEntryStandardOpenNonEntrance:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.TRFrontEntryStandardOpen], node,
                            RequirementDictionary.Instance[RequirementType.EntranceShuffleNone]));
                    }
                    break;
                case RequirementNodeID.TRFrontToKeyDoors:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.TRFrontEntryStandardOpenNonEntrance], node,
                            RequirementDictionary.Instance[RequirementType.CaneOfSomaria]));
                    }
                    break;
                case RequirementNodeID.TRKeyDoorsToMiddleExit:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.TRFrontToKeyDoors], node,
                            RequirementDictionary.Instance[RequirementType.TRKeyDoorsToMiddleExit]));
                    }
                    break;
                case RequirementNodeID.TRMiddleEntry:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.EntranceDungeon], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.TurtleRockTunnel], node));
                    }
                    break;
                case RequirementNodeID.TRBackEntry:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.EntranceDungeon], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.TurtleRockSafetyDoor], node));
                    }
                    break;
                case RequirementNodeID.GTEntry:
                    {
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.EntranceDungeon], node));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.AgahnimTowerEntrance], node,
                            RequirementDictionary.Instance[RequirementType.WorldStateInverted]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.GanonsTowerEntranceStandardOpen], node,
                            RequirementDictionary.Instance[RequirementType.NotBunnyDW]));
                        connections.Add(new NodeConnection(
                            RequirementNodeDictionary.Instance[RequirementNodeID.GanonsTowerEntranceStandardOpen], node,
                            RequirementDictionary.Instance[RequirementType.DungeonRevive]));
                    }
                    break;
            }
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
        public static IRequirementNode GetRequirementNode(RequirementNodeID id)
        {
            return new RequirementNode(id, id == RequirementNodeID.Start);
        }
    }
}
