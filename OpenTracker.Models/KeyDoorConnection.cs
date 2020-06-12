using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class KeyDoorConnection : INotifyPropertyChanged
    {
        private readonly DungeonData _dungeonData;
        private readonly KeyDoor _keyDoor;

        public List<DungeonNode> ConnectedNodes { get; }

        public bool Unlocked => _keyDoor.Unlocked;

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

        public KeyDoorConnection(DungeonData dungeonData, KeyDoor keyDoor)
        {
            _dungeonData = dungeonData ?? throw new ArgumentNullException(nameof(dungeonData));
            _keyDoor = keyDoor ?? throw new ArgumentNullException(nameof(keyDoor));
            ConnectedNodes = new List<DungeonNode>();
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

            foreach (DungeonNode node in ConnectedNodes)
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

        public void Initialize(int index = 0)
        {
            switch (_keyDoor.ID)
            {
                case KeyDoorID.HCEscapeFirstKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.HCFront]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.HCPastEscapeFirstKeyDoor]);
                    }
                    break;
                case KeyDoorID.HCEscapeSecondKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.HCPastEscapeFirstKeyDoor]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.HCPastEscapeSecondKeyDoor]);
                    }
                    break;
                case KeyDoorID.HCDarkCrossRoomKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.HCDarkRoomFront]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.HCPastDarkCrossKeyDoor]);
                    }
                    break;
                case KeyDoorID.HCSewerRatRoomKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.HCPastDarkCrossKeyDoor]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.HCPastSewerRatRoomKeyDoor]);
                    }
                    break;
                case KeyDoorID.ATFirstKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.AT]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.ATPastFirstKeyDoor]);
                    }
                    break;
                case KeyDoorID.ATSecondKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.ATDarkMaze]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.ATPastSecondKeyDoor]);
                    }
                    break;
                case KeyDoorID.ATThirdKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.ATPastSecondKeyDoor]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.ATPastThirdKeyDoor]);
                    }
                    break;
                case KeyDoorID.ATFourthKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.ATPastThirdKeyDoor]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.ATPastFourthKeyDoor]);
                    }
                    break;
                case KeyDoorID.EPRightWingKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.EPRightWingDarkRoom]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.EPPastRightWingKeyDoor]);
                    }
                    break;
                case KeyDoorID.EPBossKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.EPBackDarkRoom]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.EPPastBackKeyDoor]);
                    }
                    break;
                case KeyDoorID.EPBigChest:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.EP]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.EPBigChest]);
                    }
                    break;
                case KeyDoorID.EPBigKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.EP]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.EPPastBigKeyDoor]);
                    }
                    break;
                case KeyDoorID.DPRightWingKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.DPFront]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.DPPastRightWingKeyDoor]);
                    }
                    break;
                case KeyDoorID.DP1FKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.DPBack]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.DP2F]);
                    }
                    break;
                case KeyDoorID.DP2FFirstKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.DP2F]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.DP2FPastFirstKeyDoor]);
                    }
                    break;
                case KeyDoorID.DP2FSecondKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.DP2FPastFirstKeyDoor]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.DP2FPastSecondKeyDoor]);
                    }
                    break;
                case KeyDoorID.DPBigChest:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.DPFront]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.DPBigChest]);
                    }
                    break;
                case KeyDoorID.DPBigKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.DPPastFourTorchWall]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.DPBossRoom]);
                    }
                    break;
                case KeyDoorID.ToHKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.ToH]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.ToHPastKeyDoor]);
                    }
                    break;
                case KeyDoorID.ToHBigKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.ToH]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.ToHPastBigKeyDoor]);
                    }
                    break;
                case KeyDoorID.ToHBigChest:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.ToHPastBigKeyDoor]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.ToHBigChest]);
                    }
                    break;
                case KeyDoorID.PoDFrontKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.PoD]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.PoDLobbyArena]);
                    }
                    break;
                case KeyDoorID.PoDBigKeyChestKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.PoDLobbyArena]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.PoDBigKeyChestArea]);
                    }
                    break;
                case KeyDoorID.PoDCollapsingStairwayKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.PoDLobbyArena]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.PoDPastCollapsingWalkwayKeyDoor]);
                    }
                    break;
                case KeyDoorID.PoDDarkMazeKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.PoDPastCollapsingWalkwayKeyDoor]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.PoDPastDarkMazeKeyDoor]);
                    }
                    break;
                case KeyDoorID.PoDHarmlessHellwayKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.PoDPastCollapsingWalkwayKeyDoor]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.PoDHarmlessHellwayRoom]);
                    }
                    break;
                case KeyDoorID.PoDBossAreaKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.PoDPastHammerBlocks]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.PoDPastBossAreaKeyDoor]);
                    }
                    break;
                case KeyDoorID.PoDBigChest:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.PoDBigChestLedge]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.PoDBigChest]);
                    }
                    break;
                case KeyDoorID.PoDBigKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.PoDPastBossAreaKeyDoor]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.PoDBossRoom]);
                    }
                    break;
                case KeyDoorID.SP1FKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SPAfterRiver]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SPB1]);
                    }
                    break;
                case KeyDoorID.SPFirstRightSideKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SPB1]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SPPastFirstRightSideKeyDoor]);
                    }
                    break;
                case KeyDoorID.SPSecondRightSideKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SPPastFirstRightSideKeyDoor]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SPPastSecondRightSideKeyDoor]);
                    }
                    break;
                case KeyDoorID.SPLeftSideKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SPPastRightSideHammerBlocks]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SPPastLeftSideKeyDoor]);
                    }
                    break;
                case KeyDoorID.SPBackFirstKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SPBack]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SPPastBackFirstKeyDoor]);
                    }
                    break;
                case KeyDoorID.SPBossRoomKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SPPastBackFirstKeyDoor]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SPBossRoom]);
                    }
                    break;
                case KeyDoorID.SPBigChest:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SPPastRightSideHammerBlocks]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SPBigChest]);
                    }
                    break;
                case KeyDoorID.SWFrontLeftKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SWBigChestAreaBottom]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SWFrontLeftSide]);
                    }
                    break;
                case KeyDoorID.SWFrontRightKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SWBigChestAreaBottom]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SWFrontRightSide]);
                    }
                    break;
                case KeyDoorID.SWWorthlessKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SWFrontBackConnector]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SWPastTheWorthlessKeyDoor]);
                    }
                    break;
                case KeyDoorID.SWBackFirstKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SWBack]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SWBackPastFirstKeyDoor]);
                    }
                    break;
                case KeyDoorID.SWBackSeceondKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SWBackPastCurtains]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SWBossRoom]);
                    }
                    break;
                case KeyDoorID.SWBigChest:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SWBigChestAreaTop]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.SWBigChest]);
                    }
                    break;
                case KeyDoorID.TTFirstKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TTPastBigKeyDoor]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TTPastFirstKeyDoor]);
                    }
                    break;
                case KeyDoorID.TTSecondKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TTPastFirstKeyDoor]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TTPastSecondKeyDoor]);
                    }
                    break;
                case KeyDoorID.TTBigChestKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TTPastFirstKeyDoor]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TTPastBigChestRoomKeyDoor]);
                    }
                    break;
                case KeyDoorID.TTBigKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TT]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TTPastBigKeyDoor]);
                    }
                    break;
                case KeyDoorID.TTBigChest:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TTPastHammerBlocks]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TTBigChest]);
                    }
                    break;
                case KeyDoorID.IP1FKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.IPPastEntranceFreezorRoom]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.IPB1LeftSide]);
                    }
                    break;
                case KeyDoorID.IPB2KeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.IPB2LeftSide]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.IPB2PastKeyDoor]);
                    }
                    break;
                case KeyDoorID.IPB3KeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.IPB2PastKeyDoor]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.IPSpikeRoom]);
                    }
                    break;
                case KeyDoorID.IPB4KeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.IPB4IceRoom]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.IPB4PastKeyDoor]);
                    }
                    break;
                case KeyDoorID.IPB5KeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.IPB5PastBigKeyDoor]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.IPB6]);
                    }
                    break;
                case KeyDoorID.IPB6KeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.IPB6]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.IPB6PastKeyDoor]);
                    }
                    break;
                case KeyDoorID.IPBigKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.IPB5]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.IPB5PastBigKeyDoor]);
                    }
                    break;
                case KeyDoorID.IPBigChest:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.IPBigChestArea]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.IPBigChest]);
                    }
                    break;
                case KeyDoorID.MMB1TopRightKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.MMPastEntranceGap]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.MMB1TopSide]);
                    }
                    break;
                case KeyDoorID.MMB1TopLeftKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.MMPastEntranceGap]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.MMB1TopSide]);
                    }
                    break;
                case KeyDoorID.MMB1LeftSideFirstKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.MMPastEntranceGap]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.MMB1LeftSidePastFirstKeyDoor]);
                    }
                    break;
                case KeyDoorID.MMB1LeftSideSecondKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.MMB1LeftSidePastFirstKeyDoor]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.MMB1LeftSidePastSecondKeyDoor]);
                    }
                    break;
                case KeyDoorID.MMB1RightSideKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.MMB1LobbyBeyondBlueBlocks]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.MMB1RightSideBeyondBlueBlocks]);
                    }
                    break;
                case KeyDoorID.MMB2WorthlessKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.MMDarkRoom]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.MMB2PastWorthlessKeyDoor]);
                    }
                    break;
                case KeyDoorID.MMBigChest:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.MMPastEntranceGap]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.MMBigChest]);
                    }
                    break;
                case KeyDoorID.MMPortalBigKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.MMPastEntranceGap]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.MMB1PastPortalBigKeyDoor]);
                    }
                    break;
                case KeyDoorID.MMBridgeBigKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.MMB1TopSide]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.MMB1PastBridgeBigKeyDoor]);
                    }
                    break;
                case KeyDoorID.MMBossRoomBigKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.MMB2PastCaneOfSomariaSwitch]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.MMBossRoom]);
                    }
                    break;
                case KeyDoorID.TR1FFirstKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TRF1FirstKeyDoorArea]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TRF1PastFirstKeyDoor]);
                    }
                    break;
                case KeyDoorID.TR1FSecondKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TRF1PastFirstKeyDoor]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TRF1PastSecondKeyDoor]);
                    }
                    break;
                case KeyDoorID.TR1FThirdKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TRF1PastSecondKeyDoor]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TRB1]);
                    }
                    break;
                case KeyDoorID.TRB1BigKeyChestKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TRB1]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TRB1PastBigKeyChestKeyDoor]);
                    }
                    break;
                case KeyDoorID.TRB1toB2KeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TRB1RightSide]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TRPastB1toB2KeyDoor]);
                    }
                    break;
                case KeyDoorID.TRB2KeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TRB2PastDarkMaze]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TRB2PastKeyDoor]);
                    }
                    break;
                case KeyDoorID.TRBigChest:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TRB1BigChestArea]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TRBigChest]);
                    }
                    break;
                case KeyDoorID.TRB1BigKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TRB1]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TRB1RightSide]);
                    }
                    break;
                case KeyDoorID.TRBossRoomBigKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TRB3BossRoomEntry]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.TRBossRoom]);
                    }
                    break;
                case KeyDoorID.GT1FLeftToRightKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.GT1FLeft]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.GT1FRight]);
                    }
                    break;
                case KeyDoorID.GT1FMapChestRoomKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.GT1FLeftPastBonkableGaps]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.GT1FLeftMapChestRoom]);
                    }
                    break;
                case KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.GT1FLeftPastBonkableGaps]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.GT1FLeftSpikeTrapPortalRoom]);
                    }
                    break;
                case KeyDoorID.GT1FFiresnakeRoomKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.GT1FLeftPastFiresnakeRoomGap]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.GT1FLeftPastFiresnakeRoomKeyDoor]);
                    }
                    break;
                case KeyDoorID.GT1FTileRoomKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.GT1FRightTileRoom]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.GT1FRightFourTorchRoom]);
                    }
                    break;
                case KeyDoorID.GT1FCollapsingWalkwayKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.GT1FRightPastCompassRoomPortal]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.GT1FRightCollapsingWalkway]);
                    }
                    break;
                case KeyDoorID.GT6FFirstKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.GT5FPastFourTorchRooms]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.GT6FPastFirstKeyDoor]);
                    }
                    break;
                case KeyDoorID.GT6FSecondKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.GT6FPastFirstKeyDoor]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.GT6FBossRoom]);
                    }
                    break;
                case KeyDoorID.GTBigChest:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.GT1FBottomRoom]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.GTBigChest]);
                    }
                    break;
                case KeyDoorID.GT3FBigKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.GT3FPastRedGoriyaRooms]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.GT3FPastBigKeyDoor]);
                    }
                    break;
                case KeyDoorID.GT7FBigKeyDoor:
                    {
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.GT6FPastBossRoomGap]);
                        ConnectedNodes.Add(_dungeonData.RequirementNodes[RequirementNodeID.GTFinalBossRoom]);
                    }
                    break;
            }

            foreach (DungeonNode node in ConnectedNodes)
                node.PropertyChanged += OnRequirementChanged;

            UpdateAccessibility();
        }
    }
}
