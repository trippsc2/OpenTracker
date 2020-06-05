using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class DungeonItem : INotifyPropertyChanged
    {
        private readonly Game _game;
        private readonly DungeonItemID _iD;
        private readonly List<RequirementNodeConnection> _connections;

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

        public DungeonItem(Game game, DungeonItemID iD)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _iD = iD;
            _connections = new List<RequirementNodeConnection>();

            switch (_iD)
            {
                case DungeonItemID.HCSanctuary:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HCSanctuary,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.HCMapChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HCFrontEntry,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.HCBoomerangChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HCPastEscapeFirstKeyDoor,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.HCZeldasCell:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HCPastEscapeSecondKeyDoor,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.HCDarkCross:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HCDarkRoomFront,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.HCSecretRoomLeft:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HCBack,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.HCSecretRoomMiddle:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HCBack,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.HCSecretRoomRight:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HCBack,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.ATRoom03:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.AT,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.ATDarkMaze:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.ATDarkMaze,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.ATBoss:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.ATBoss,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.EPCannonballChest:
                case DungeonItemID.EPMapChest:
                case DungeonItemID.EPCompassChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.EP,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.EPBigChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.EPBigChest,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.EPBigKeyChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.EPPastRightWingKeyDoor,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.EPBoss:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.EPBoss,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.DPMapChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DPFront,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.DPTorch:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DPFront,
                            RequirementType.Torch, new Mode()));
                    }
                    break;
                case DungeonItemID.DPBigChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DPBigChest,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.DPCompassChest:
                case DungeonItemID.DPBigKeyChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DPPastRightWingKeyDoor,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.DPBoss:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DPBoss,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.ToHBasementCage:
                case DungeonItemID.ToHMapChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.ToH,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.ToHBigKeyChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.ToHBasementTorchRoom,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.ToHCompassChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.ToHPastBigKeyDoor,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.ToHBigChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.ToHBigChest,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.ToHBoss:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.ToHBoss,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.PoDShooterRoom:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.PoD,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.PoDMapChest:
                case DungeonItemID.PoDArenaLedge:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.PoDPastFirstRedGoriyaRoom,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.PoDBigKeyChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.PoDBigKeyChestArea,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.PoDStalfosBasement:
                case DungeonItemID.PoDArenaBridge:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.PoDLobbyArena,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.PoDCompassChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.PoDPastCollapsingWalkwayKeyDoor,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.PoDDarkBasementLeft:
                case DungeonItemID.PoDDarkBasementRight:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.PoDDarkBasement,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.PoDHarmlessHellway:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.PoDHarmlessHellwayRoom,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.PoDDarkMazeTop:
                case DungeonItemID.PoDDarkMazeBottom:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.PoDDarkMaze,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.PoDBigChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.PoDBigChest,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.PoDBoss:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.PoDBoss,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.SPEntrance:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SPAfterRiver,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.SPMapChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SPB1,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.SPBigChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SPBigChest,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.SPCompassChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SPPastRightSideHammerBlocks,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.SPWestChest:
                case DungeonItemID.SPBigKeyChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SPPastLeftSideKeyDoor,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.SPFloodedRoomLeft:
                case DungeonItemID.SPFloodedRoomRight:
                case DungeonItemID.SPWaterfallRoom:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SPPastBackFirstKeyDoor,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.SPBoss:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SPBoss,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.SWBigKeyChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SWFrontBackConnector,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.SWMapChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SWBigChestAreaBottom,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.SWBigChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SWBigChest,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.SWPotPrison:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SWFrontLeftSide,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.SWCompassChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SWFrontLeftSide,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.SWPinballRoom:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SWFrontRightSide,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.SWBridgeRoom:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SWBack,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.SWBoss:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.SWBoss,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.TTMapChest:
                case DungeonItemID.TTAmbushChest:
                case DungeonItemID.TTCompassChest:
                case DungeonItemID.TTBigKeyChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.TT,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.TTAttic:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.TTPastSecondKeyDoor,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.TTBlindsCell:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.TTPastFirstKeyDoor,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.TTBigChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.TTBigChest,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.TTBoss:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.TTBoss,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.IPCompassChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.IPB1LeftSide,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.IPSpikeRoom:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.IPSpikeRoom,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.IPMapChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.IPB2PastLiftBlock,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.IPBigKeyChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.IPB1RightSide,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.IPFreezorChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.IPB4FreezorRoom,
                            RequirementType.MeltThings, new Mode()));
                    }
                    break;
                case DungeonItemID.IPBigChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.IPBigChest,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.IPIcedTRoom:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.IPB5,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.IPBoss:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.IPBoss,
                            RequirementType.IPBoss, new Mode()));
                    }
                    break;
                case DungeonItemID.MMBridgeChest:
                case DungeonItemID.MMSpikeChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MMPastEntranceGap,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.MMMainLobby:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MMB1LobbyBeyondBlueBlocks,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.MMCompassChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MMB1PastFourTorchRoom,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.MMBigKeyChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MMF1PastFourTorchRoom,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.MMBigChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MMBigChest,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.MMMapChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MMB1RightSideBeyondBlueBlocks,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.MMBoss:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MMBoss,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.TRCompassChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.TRF1CompassChestArea,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.TRRollerRoomLeft:
                case DungeonItemID.TRRollerRoomRight:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.TRF1RollerRoom,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.TRChainChomps:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.TRF1PastSecondKeyDoor,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.TRBigKeyChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.TRB1PastBigKeyChestKeyDoor,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.TRBigChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.TRBigChest,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.TRCrystarollerRoom:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.TRB1RightSide,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.TRLaserBridgeTopLeft:
                case DungeonItemID.TRLaserBridgeTopRight:
                case DungeonItemID.TRLaserBridgeBottomLeft:
                case DungeonItemID.TRLaserBrdigeBottomRight:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.TRB2PastDarkMaze,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.TRBoss:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.TRBoss,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.GTHopeRoomLeft:
                case DungeonItemID.GTHopeRoomRight:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FRight,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.GTBobsTorch:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeft,
                            RequirementType.Torch, new Mode()));
                    }
                    break;
                case DungeonItemID.GTDMsRoomTopLeft:
                case DungeonItemID.GTDMsRoomTopRight:
                case DungeonItemID.GTDMsRoomBottomLeft:
                case DungeonItemID.GTDMsRoomBottomRight:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeftDMsRoom,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.GTMapChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeftMapChestRoom,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.GTFiresnakeRoom:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeftPastFiresnakeRoomGap,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.GTRandomizerRoomTopLeft:
                case DungeonItemID.GTRandomizerRoomTopRight:
                case DungeonItemID.GTRandomizerRoomBottomLeft:
                case DungeonItemID.GTRandomizerRoomBottomRight:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeftRandomizerRoom,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.GTTileRoom:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FRightTileRoom,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.GTCompassRoomTopLeft:
                case DungeonItemID.GTCompassRoomTopRight:
                case DungeonItemID.GTCompassRoomBottomLeft:
                case DungeonItemID.GTCompassRoomBottomRight:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FRightCompassRoom,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.GTBobsChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FBottomRoom,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.GTBigKeyRoomTopLeft:
                case DungeonItemID.GTBigKeyRoomTopRight:
                case DungeonItemID.GTBigKeyChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GTB1BossChests,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.GTBigChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GTBigChest,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.GTMiniHelmasaurRoomLeft:
                case DungeonItemID.GTMiniHelmasaurRoomRight:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GT5FPastFourTorchRooms,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.GTPreMoldormChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GT6FPastFirstKeyDoor,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case DungeonItemID.GTMoldormChest:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GT6FPastBossRoomGap,
                            RequirementType.None, new Mode()));
                    }
                    break;
            }

            List<RequirementNodeID> nodeSubscriptions = new List<RequirementNodeID>();
            List<RequirementType> requirementSubscriptions = new List<RequirementType>();

            foreach (RequirementNodeConnection connection in _connections)
            {
                if (!nodeSubscriptions.Contains(connection.FromNode))
                {
                    _game.RequirementNodes[connection.FromNode].PropertyChanged += OnRequirementChanged;
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

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        private void UpdateAccessibility()
        {
            Accessibility = GetItemAccessibility();
        }

        public AccessibilityLevel GetItemAccessibility()
        {
            AccessibilityLevel finalAccessibility = AccessibilityLevel.None;

            foreach (RequirementNodeConnection connection in _connections)
            {
                AccessibilityLevel nodeAccessibility = AccessibilityLevel.Normal;
                
                nodeAccessibility = (AccessibilityLevel)Math.Min((byte)nodeAccessibility,
                    (byte)_game.RequirementNodes[connection.FromNode].GetNodeAccessibility(new List<RequirementNodeID>()));

                if (nodeAccessibility < AccessibilityLevel.SequenceBreak)
                    continue;

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
                    finalAccessibility = finalConnectionAccessibility;
            }

            return finalAccessibility;
        }
    }
}
