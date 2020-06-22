using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models
{
    /// <summary>
    /// This is the mutable dungeon item data.
    /// </summary>
    public class DungeonItem : INotifyPropertyChanged
    {
        private readonly Game _game;
        private readonly DungeonData _dungeonData;
        private readonly DungeonItemID _iD;

        public List<RequirementNodeConnection> Connections { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set
            {
                if (_accessibility != value)
                {
                    _accessibility = value;
                    OnPropertyChanged(nameof(Accessibility));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">
        /// The game data.
        /// </param>
        /// <param name="dungeonData">
        /// The mutable dungeon data parent class.
        /// </param>
        /// <param name="iD">
        /// The item identity.
        /// </param>
        public DungeonItem(Game game, DungeonData dungeonData, DungeonItemID iD)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _dungeonData = dungeonData ?? throw new ArgumentNullException(nameof(dungeonData));
            _iD = iD;
            Connections = new List<RequirementNodeConnection>();

            switch (_iD)
            {
                case DungeonItemID.HCSanctuary:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.HCSanctuary,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.HCMapChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.HCFront,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.HCBoomerangChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.HCPastEscapeFirstKeyDoor,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.HCZeldasCell:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.HCPastEscapeSecondKeyDoor,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.HCDarkCross:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.HCDarkRoomFront,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.HCSecretRoomLeft:
                case DungeonItemID.HCSecretRoomMiddle:
                case DungeonItemID.HCSecretRoomRight:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.HCBack,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.ATRoom03:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.AT,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.ATDarkMaze:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.ATDarkMaze,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.ATBoss:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.ATBoss,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.EPCannonballChest:
                case DungeonItemID.EPMapChest:
                case DungeonItemID.EPCompassChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.EP,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.EPBigChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.EPBigChest,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.EPBigKeyChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.EPPastRightWingKeyDoor,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.EPBoss:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.EPBoss,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.DPMapChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DPFront,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.DPTorch:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DPFront,
                            RequirementType.Torch, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.DPBigChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DPBigChest,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.DPCompassChest:
                case DungeonItemID.DPBigKeyChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DPPastRightWingKeyDoor,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.DPBoss:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.DPBoss,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.ToHBasementCage:
                case DungeonItemID.ToHMapChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.ToH,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.ToHBigKeyChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.ToHBasementTorchRoom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.ToHCompassChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.ToHPastBigKeyDoor,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.ToHBigChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.ToHBigChest,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.ToHBoss:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.ToHBoss,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.PoDShooterRoom:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.PoD,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.PoDMapChest:
                case DungeonItemID.PoDArenaLedge:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.PoDPastFirstRedGoriyaRoom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.PoDBigKeyChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.PoDBigKeyChestArea,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.PoDStalfosBasement:
                case DungeonItemID.PoDArenaBridge:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.PoDLobbyArena,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.PoDCompassChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.PoDPastCollapsingWalkwayKeyDoor,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.PoDDarkBasementLeft:
                case DungeonItemID.PoDDarkBasementRight:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.PoDDarkBasement,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.PoDHarmlessHellway:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.PoDHarmlessHellwayRoom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.PoDDarkMazeTop:
                case DungeonItemID.PoDDarkMazeBottom:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.PoDDarkMaze,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.PoDBigChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.PoDBigChest,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.PoDBoss:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.PoDBoss,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.SPEntrance:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SPAfterRiver,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.SPMapChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SPB1,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.SPBigChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SPBigChest,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.SPCompassChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SPPastRightSideHammerBlocks,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.SPWestChest:
                case DungeonItemID.SPBigKeyChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SPPastLeftSideKeyDoor,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.SPFloodedRoomLeft:
                case DungeonItemID.SPFloodedRoomRight:
                case DungeonItemID.SPWaterfallRoom:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SPPastBackFirstKeyDoor,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.SPBoss:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SPBoss,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.SWBigKeyChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SWFrontBackConnector,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.SWMapChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SWBigChestAreaBottom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.SWBigChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SWBigChest,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.SWPotPrison:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SWFrontLeftSide,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.SWCompassChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SWFrontLeftSide,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.SWPinballRoom:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SWFrontRightSide,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.SWBridgeRoom:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SWBack,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.SWBoss:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.SWBoss,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.TTMapChest:
                case DungeonItemID.TTAmbushChest:
                case DungeonItemID.TTCompassChest:
                case DungeonItemID.TTBigKeyChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TT,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.TTAttic:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TTPastSecondKeyDoor,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.TTBlindsCell:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TTPastFirstKeyDoor,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.TTBigChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TTBigChest,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.TTBoss:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TTBoss,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.IPCompassChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.IPB1LeftSide,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.IPSpikeRoom:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.IPSpikeRoom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.IPMapChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.IPB2PastLiftBlock,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.IPBigKeyChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.IPB1RightSide,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.IPFreezorChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.IPB4FreezorRoom,
                            RequirementType.MeltThings, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.IPBigChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.IPBigChest,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.IPIcedTRoom:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.IPB5,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.IPBoss:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.IPBoss,
                            RequirementType.IPBoss, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.MMBridgeChest:
                case DungeonItemID.MMSpikeChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.MMPastEntranceGap,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.MMMainLobby:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.MMB1LobbyBeyondBlueBlocks,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.MMCompassChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.MMB1PastFourTorchRoom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.MMBigKeyChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.MMF1PastFourTorchRoom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.MMBigChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.MMBigChest,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.MMMapChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.MMB1RightSideBeyondBlueBlocks,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.MMBoss:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.MMBoss,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.TRCompassChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TRF1CompassChestArea,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.TRRollerRoomLeft:
                case DungeonItemID.TRRollerRoomRight:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TRF1RollerRoom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.TRChainChomps:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TRF1PastSecondKeyDoor,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.TRBigKeyChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TRB1PastBigKeyChestKeyDoor,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.TRBigChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TRBigChest,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.TRCrystarollerRoom:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TRB1RightSide,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.TRLaserBridgeTopLeft:
                case DungeonItemID.TRLaserBridgeTopRight:
                case DungeonItemID.TRLaserBridgeBottomLeft:
                case DungeonItemID.TRLaserBrdigeBottomRight:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TRB2PastDarkMaze,
                            RequirementType.LaserBridge, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.TRBoss:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.TRBoss,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.GTHopeRoomLeft:
                case DungeonItemID.GTHopeRoomRight:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FRight,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.GTBobsTorch:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeft,
                            RequirementType.Torch, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.GTDMsRoomTopLeft:
                case DungeonItemID.GTDMsRoomTopRight:
                case DungeonItemID.GTDMsRoomBottomLeft:
                case DungeonItemID.GTDMsRoomBottomRight:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeftDMsRoom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.GTMapChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeftMapChestRoom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.GTFiresnakeRoom:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeftPastFiresnakeRoomGap,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.GTRandomizerRoomTopLeft:
                case DungeonItemID.GTRandomizerRoomTopRight:
                case DungeonItemID.GTRandomizerRoomBottomLeft:
                case DungeonItemID.GTRandomizerRoomBottomRight:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeftRandomizerRoom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.GTTileRoom:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FRightTileRoom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.GTCompassRoomTopLeft:
                case DungeonItemID.GTCompassRoomTopRight:
                case DungeonItemID.GTCompassRoomBottomLeft:
                case DungeonItemID.GTCompassRoomBottomRight:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FRightCompassRoom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.GTBobsChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FBottomRoom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.GTBigKeyRoomTopLeft:
                case DungeonItemID.GTBigKeyRoomTopRight:
                case DungeonItemID.GTBigKeyChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.GTB1BossChests,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.GTBigChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.GTBigChest,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.GTMiniHelmasaurRoomLeft:
                case DungeonItemID.GTMiniHelmasaurRoomRight:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.GT5FPastFourTorchRooms,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.GTPreMoldormChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.GT6FPastFirstKeyDoor,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.GTMoldormChest:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.GT6FPastBossRoomGap,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.GTBoss1:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.GTBoss1,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.GTBoss2:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.GTBoss2,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.GTBoss3:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.GTBoss3,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case DungeonItemID.GTFinalBoss:
                    {
                        Connections.Add(new RequirementNodeConnection(RequirementNodeID.GTFinalBoss,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
            }

            List<RequirementNodeID> nodeSubscriptions = new List<RequirementNodeID>();
            List<RequirementType> requirementSubscriptions = new List<RequirementType>();

            foreach (RequirementNodeConnection connection in Connections)
            {
                if (!nodeSubscriptions.Contains(connection.FromNode))
                {
                    _dungeonData.RequirementNodes[connection.FromNode].PropertyChanged += OnRequirementChanged;
                    nodeSubscriptions.Add(connection.FromNode);
                }

                if (!requirementSubscriptions.Contains(connection.Requirement))
                {
                    _game.Requirements[connection.Requirement].PropertyChanged += OnRequirementChanged;
                    requirementSubscriptions.Add(connection.Requirement);
                }
            }

            UpdateAccessibility();
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Requirement and RequirementNode
        /// classes that are requirements for dungeon items.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        /// <summary>
        /// Updates the accessibility and number of accessible items.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility = GetItemAccessibility();
        }

        /// <summary>
        /// Returns the accessibility of the item.
        /// </summary>
        /// <returns>The accessibility level of the item.</returns>
        public AccessibilityLevel GetItemAccessibility()
        {
            AccessibilityLevel finalAccessibility = AccessibilityLevel.None;

            foreach (RequirementNodeConnection connection in Connections)
            {
                AccessibilityLevel nodeAccessibility = AccessibilityLevel.Normal;
                
                nodeAccessibility = (AccessibilityLevel)Math.Min((byte)nodeAccessibility,
                    (byte)_dungeonData.RequirementNodes[connection.FromNode]
                    .GetNodeAccessibility(new List<RequirementNodeID>()));

                if (nodeAccessibility < AccessibilityLevel.SequenceBreak)
                {
                    continue;
                }

                AccessibilityLevel requirementAccessibility =
                    _game.Requirements[connection.Requirement].Accessibility;

                AccessibilityLevel finalConnectionAccessibility =
                    (AccessibilityLevel)Math.Min(Math.Min((byte)nodeAccessibility,
                    (byte)requirementAccessibility), (byte)connection.MaximumAccessibility);

                if (finalConnectionAccessibility == AccessibilityLevel.Normal)
                {
                    finalAccessibility = AccessibilityLevel.Normal;
                    break;
                }

                if (finalConnectionAccessibility > finalAccessibility)
                {
                    finalAccessibility = finalConnectionAccessibility;
                }
            }

            return finalAccessibility;
        }
    }
}
