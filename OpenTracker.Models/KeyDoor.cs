using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class KeyDoor : INotifyPropertyChanged
    {
        private readonly Game _game;
        private readonly List<DungeonNode> _connectedNodes;

        public KeyDoorID ID { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _unlocked;
        public bool Unlocked
        {
            get => _unlocked;
            set
            {
                if (_unlocked != value)
                {
                    _unlocked = value;
                    OnPropertyChanged(nameof(Unlocked));
                }
            }
        }

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

        public KeyDoor(Game game, KeyDoorID iD)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            ID = iD;
            _connectedNodes = new List<DungeonNode>();
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
            Accessibility = GetDoorAccessibility(new List<RequirementNodeID>());
        }

        public AccessibilityLevel GetDoorAccessibility(List<RequirementNodeID> excludedNodes)
        {
            if (excludedNodes == null)
                throw new ArgumentNullException(nameof(excludedNodes));

            AccessibilityLevel accessibility = AccessibilityLevel.None;

            foreach (DungeonNode node in _connectedNodes)
            {
                if (excludedNodes.Contains(node.ID))
                    continue;

                if (node.Accessibility > accessibility)
                    accessibility = node.GetNodeAccessibility(excludedNodes);

                if (accessibility == AccessibilityLevel.Normal)
                    break;
            }

            return accessibility;
        }

        public void Initialize()
        {
            switch (ID)
            {
                case KeyDoorID.HCEscapeFirstKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.HCFront]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.HCPastEscapeFirstKeyDoor]);
                    }
                    break;
                case KeyDoorID.HCEscapeSecondKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.HCPastEscapeFirstKeyDoor]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.HCPastEscapeSecondKeyDoor]);
                    }
                    break;
                case KeyDoorID.HCDarkCrossRoomKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.HCDarkRoomFront]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.HCPastDarkCrossKeyDoor]);
                    }
                    break;
                case KeyDoorID.HCSewerRatRoomKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.HCPastDarkCrossKeyDoor]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.HCBack]);
                    }
                    break;
                case KeyDoorID.ATFirstKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.AT]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.ATPastFirstKeyDoor]);
                    }
                    break;
                case KeyDoorID.ATSecondKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.ATDarkMaze]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.ATPastSecondKeyDoor]);
                    }
                    break;
                case KeyDoorID.ATThirdKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.ATPastSecondKeyDoor]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.ATPastThirdKeyDoor]);
                    }
                    break;
                case KeyDoorID.ATFourthKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.ATPastThirdKeyDoor]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.ATPastFourthKeyDoor]);
                    }
                    break;
                case KeyDoorID.EPRightWingKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.EPRightWingDarkRoom]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.EPPastRightWingKeyDoor]);
                    }
                    break;
                case KeyDoorID.EPBossKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.EPBackDarkRoom]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.EPPastBackKeyDoor]);
                    }
                    break;
                case KeyDoorID.EPBigChest:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.EP]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.EPBigChest]);
                    }
                    break;
                case KeyDoorID.EPBigKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.EP]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.EPPastBigKeyDoor]);
                    }
                    break;
                case KeyDoorID.DPRightWingKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.DPFront]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.DPPastRightWingKeyDoor]);
                    }
                    break;
                case KeyDoorID.DP1FKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.DPBack]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.DP2F]);
                    }
                    break;
                case KeyDoorID.DP2FFirstKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.DP2F]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.DP2FPastFirstKeyDoor]);
                    }
                    break;
                case KeyDoorID.DP2FSecondKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.DP2FPastFirstKeyDoor]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.DP2FPastSecondKeyDoor]);
                    }
                    break;
                case KeyDoorID.DPBigChest:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.DPFront]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.DPBigChest]);
                    }
                    break;
                case KeyDoorID.DPBigKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.DPPastFourTorchWall]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.DPBossRoom]);
                    }
                    break;
                case KeyDoorID.ToHKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.ToH]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.ToHPastKeyDoor]);
                    }
                    break;
                case KeyDoorID.ToHBigKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.ToH]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.ToHPastBigKeyDoor]);
                    }
                    break;
                case KeyDoorID.ToHBigChest:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.ToHPastBigKeyDoor]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.ToHBigChest]);
                    }
                    break;
                case KeyDoorID.PoDFrontKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.PoD]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.PoDLobbyArena]);
                    }
                    break;
                case KeyDoorID.PoDBigKeyChestKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.PoDLobbyArena]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.PoDBigKeyChestArea]);
                    }
                    break;
                case KeyDoorID.PoDCollapsingStairwayKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.PoDLobbyArena]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.PoDPastCollapsingWalkwayKeyDoor]);
                    }
                    break;
                case KeyDoorID.PoDDarkMazeKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.PoDPastCollapsingWalkwayKeyDoor]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.PoDPastDarkMazeKeyDoor]);
                    }
                    break;
                case KeyDoorID.PoDHarmlessHellwayKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.PoDPastCollapsingWalkwayKeyDoor]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.PoDHarmlessHellwayRoom]);
                    }
                    break;
                case KeyDoorID.PoDBossAreaKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.PoDPastHammerBlocks]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.PoDPastBossAreaKeyDoor]);
                    }
                    break;
                case KeyDoorID.PoDBigChest:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.PoDBigChestLedge]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.PoDBigChest]);
                    }
                    break;
                case KeyDoorID.PoDBigKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.PoDPastBossAreaKeyDoor]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.PoDBossRoom]);
                    }
                    break;
                case KeyDoorID.SP1FKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SPAfterRiver]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SPB1]);
                    }
                    break;
                case KeyDoorID.SPFirstRightSideKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SPB1]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SPPastFirstRightSideKeyDoor]);
                    }
                    break;
                case KeyDoorID.SPSecondRightSideKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SPPastFirstRightSideKeyDoor]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SPPastSecondRightSideKeyDoor]);
                    }
                    break;
                case KeyDoorID.SPLeftSideKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SPPastRightSideHammerBlocks]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SPPastLeftSideKeyDoor]);
                    }
                    break;
                case KeyDoorID.SPBackFirstKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SPBack]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SPPastBackFirstKeyDoor]);
                    }
                    break;
                case KeyDoorID.SPBossRoomKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SPPastBackFirstKeyDoor]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SPBossRoom]);
                    }
                    break;
                case KeyDoorID.SPBigChest:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SPPastRightSideHammerBlocks]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SPBigChest]);
                    }
                    break;
                case KeyDoorID.SWFrontLeftKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SWBigChestAreaBottom]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SWFrontLeftSide]);
                    }
                    break;
                case KeyDoorID.SWFrontRightKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SWBigChestAreaBottom]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SWFrontRightSide]);
                    }
                    break;
                case KeyDoorID.SWWorthlessKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SWFrontBackConnector]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SWPastTheWorthlessKeyDoor]);
                    }
                    break;
                case KeyDoorID.SWBackFirstKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SWBack]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SWBackPastFirstKeyDoor]);
                    }
                    break;
                case KeyDoorID.SWBackSeceondKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SWBackPastCurtains]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SWBossRoom]);
                    }
                    break;
                case KeyDoorID.SWBigChest:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SWBigChestAreaTop]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.SWBigChest]);
                    }
                    break;
                case KeyDoorID.TTFirstKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TTPastBigKeyDoor]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TTPastFirstKeyDoor]);
                    }
                    break;
                case KeyDoorID.TTSecondKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TTPastFirstKeyDoor]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TTPastSecondKeyDoor]);
                    }
                    break;
                case KeyDoorID.TTBigChestKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TTPastFirstKeyDoor]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TTPastBigChestRoomKeyDoor]);
                    }
                    break;
                case KeyDoorID.TTBigKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TT]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TTPastBigKeyDoor]);
                    }
                    break;
                case KeyDoorID.TTBigChest:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TTPastHammerBlocks]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TTBigChest]);
                    }
                    break;
                case KeyDoorID.IP1FKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.IPPastEntranceFreezorRoom]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.IPB1LeftSide]);
                    }
                    break;
                case KeyDoorID.IPB2KeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.IPB2LeftSide]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.IPB2PastKeyDoor]);
                    }
                    break;
                case KeyDoorID.IPB3KeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.IPB2PastKeyDoor]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.IPSpikeRoom]);
                    }
                    break;
                case KeyDoorID.IPB4KeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.IPB4IceRoom]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.IPB4PastKeyDoor]);
                    }
                    break;
                case KeyDoorID.IPB5KeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.IPB5PastBigKeyDoor]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.IPB6]);
                    }
                    break;
                case KeyDoorID.IPB6KeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.IPB6]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.IPB6PastKeyDoor]);
                    }
                    break;
                case KeyDoorID.IPBigKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.IPB5]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.IPB5PastBigKeyDoor]);
                    }
                    break;
                case KeyDoorID.IPBigChest:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.IPBigChestArea]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.IPBigChest]);
                    }
                    break;
                case KeyDoorID.MMB1TopRightKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.MMPastEntranceGap]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.MMB1TopSide]);
                    }
                    break;
                case KeyDoorID.MMB1TopLeftKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.MMPastEntranceGap]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.MMB1TopSide]);
                    }
                    break;
                case KeyDoorID.MMB1LeftSideFirstKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.MMPastEntranceGap]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.MMB1LeftSidePastFirstKeyDoor]);
                    }
                    break;
                case KeyDoorID.MMB1LeftSideSecondKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.MMB1LeftSidePastFirstKeyDoor]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.MMB1LeftSidePastSecondKeyDoor]);
                    }
                    break;
                case KeyDoorID.MMB1RightSideKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.MMB1LobbyBeyondBlueBlocks]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.MMB1RightSideBeyondBlueBlocks]);
                    }
                    break;
                case KeyDoorID.MMB2WorthlessKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.MMDarkRoom]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.MMB2PastWorthlessKeyDoor]);
                    }
                    break;
                case KeyDoorID.MMBigChest:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.MMPastEntranceGap]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.MMBigChest]);
                    }
                    break;
                case KeyDoorID.MMPortalBigKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.MMPastEntranceGap]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.MMB1PastPortalBigKeyDoor]);
                    }
                    break;
                case KeyDoorID.MMBridgeBigKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.MMB1TopSide]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.MMB1PastBridgeBigKeyDoor]);
                    }
                    break;
                case KeyDoorID.MMBossRoomBigKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.MMB2PastCaneOfSomariaSwitch]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.MMBossRoom]);
                    }
                    break;
                case KeyDoorID.TR1FFirstKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TRF1FirstKeyDoorArea]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TRF1PastFirstKeyDoor]);
                    }
                    break;
                case KeyDoorID.TR1FSecondKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TRF1PastFirstKeyDoor]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TRF1PastSecondKeyDoor]);
                    }
                    break;
                case KeyDoorID.TR1FThirdKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TRF1PastSecondKeyDoor]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TRB1]);
                    }
                    break;
                case KeyDoorID.TRB1BigKeyChestKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TRB1]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TRB1PastBigKeyChestKeyDoor]);
                    }
                    break;
                case KeyDoorID.TRB1toB2KeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TRB1RightSide]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TRPastB1toB2KeyDoor]);
                    }
                    break;
                case KeyDoorID.TRB2KeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TRB2PastDarkMaze]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TRB2PastKeyDoor]);
                    }
                    break;
                case KeyDoorID.TRBigChest:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TRB1BigChestArea]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TRBigChest]);
                    }
                    break;
                case KeyDoorID.TRB1BigKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TRB1]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TRB1RightSide]);
                    }
                    break;
                case KeyDoorID.TRBossRoomBigKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TRB3BossRoomEntry]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.TRBossRoom]);
                    }
                    break;
                case KeyDoorID.GT1FLeftToRightKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.GT1FLeft]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.GT1FRight]);
                    }
                    break;
                case KeyDoorID.GT1FMapChestRoomKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.GT1FLeftPastBonkableGaps]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.GT1FLeftMapChestRoom]);
                    }
                    break;
                case KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.GT1FLeftPastBonkableGaps]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.GT1FLeftSpikeTrapPortalRoom]);
                    }
                    break;
                case KeyDoorID.GT1FFiresnakeRoomKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.GT1FLeftPastFiresnakeRoomGap]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.GT1FLeftPastFiresnakeRoomKeyDoor]);
                    }
                    break;
                case KeyDoorID.GT1FTileRoomKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.GT1FRightTileRoom]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.GT1FRightFourTorchRoom]);
                    }
                    break;
                case KeyDoorID.GT1FCollapsingWalkwayKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.GT1FRightPastCompassRoomPortal]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.GT1FRightCollapsingWalkway]);
                    }
                    break;
                case KeyDoorID.GT6FFirstKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.GT5FPastFourTorchRooms]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.GT6FPastFirstKeyDoor]);
                    }
                    break;
                case KeyDoorID.GT6FSecondKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.GT6FPastFirstKeyDoor]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.GT6FBossRoom]);
                    }
                    break;
                case KeyDoorID.GTBigChest:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.GT1FBottomRoom]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.GTBigChest]);
                    }
                    break;
                case KeyDoorID.GT3FBigKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.GT3FPastRedGoriyaRooms]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.GT3FPastBigKeyDoor]);
                    }
                    break;
                case KeyDoorID.GT7FBigKeyDoor:
                    {
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.GT6FPastBossRoomGap]);
                        _connectedNodes.Add((DungeonNode)_game.RequirementNodes[RequirementNodeID.GTFinalBossRoom]);
                    }
                    break;
            }

            foreach (DungeonNode node in _connectedNodes)
                node.PropertyChanged += OnRequirementChanged;

            UpdateAccessibility();
        }
    }
}
