using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class DungeonNode : RequirementNode
    {
        private readonly List<KeyDoor> _smallKeyDoors;
        private readonly List<KeyDoor> _bigKeyDoors;

        public int FreeKeysProvided { get; }

        public event EventHandler RequirementChanged;

        public DungeonNode(Game game, RequirementNodeID iD)  : base(game, iD)
        {
            _smallKeyDoors = new List<KeyDoor>();
            _bigKeyDoors = new List<KeyDoor>();

            switch (ID)
            {
                case RequirementNodeID.HCFront:
                case RequirementNodeID.HCPastEscapeFirstKeyDoor:
                case RequirementNodeID.HCPastDarkCrossKeyDoor:
                case RequirementNodeID.ATPastSecondKeyDoor:
                case RequirementNodeID.ATPastThirdKeyDoor:
                case RequirementNodeID.EPRightWingDarkRoom:
                case RequirementNodeID.EPBackDarkRoom:
                case RequirementNodeID.DPBack:
                case RequirementNodeID.DP2F:
                case RequirementNodeID.DP2FPastFirstKeyDoor:
                case RequirementNodeID.SPB1:
                case RequirementNodeID.SPPastFirstRightSideKeyDoor:
                case RequirementNodeID.SPB1KeyLedge:
                case RequirementNodeID.SPPastLeftSideKeyDoor:
                case RequirementNodeID.SPPastBackFirstKeyDoor:
                case RequirementNodeID.SWFrontBackConnector:
                case RequirementNodeID.SWBackPastCurtains:
                case RequirementNodeID.TTPastBigKeyDoor:
                case RequirementNodeID.TTPastFirstKeyDoor:
                case RequirementNodeID.IPPastEntranceFreezorRoom:
                case RequirementNodeID.IPB2LeftSide:
                case RequirementNodeID.IPB2PastLiftBlock:
                case RequirementNodeID.IPB5:
                case RequirementNodeID.MMPastEntranceGap:
                case RequirementNodeID.MMB1TopSide:
                case RequirementNodeID.MMB1LeftSidePastFirstKeyDoor:
                case RequirementNodeID.TRF1PastFirstKeyDoor:
                case RequirementNodeID.TRB1:
                case RequirementNodeID.GT1FLeft:
                case RequirementNodeID.GT1FLeftPastBonkableGaps:
                case RequirementNodeID.GT1FRightPastCompassRoomPortal:
                case RequirementNodeID.GT1FRightFourTorchRoom:
                    {
                        FreeKeysProvided = 1;
                    }
                    break;
            }
        }

        protected override void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnRequirementChanged(sender, e);
            RequirementChanged?.Invoke(this, new EventArgs());
        }

        private void OnKeyDoorChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        public override AccessibilityLevel GetNodeAccessibility(List<RequirementNodeID> excludedNodes)
        {
            if (excludedNodes == null)
                throw new ArgumentNullException(nameof(excludedNodes));

            AccessibilityLevel nodeAccessibility = base.GetNodeAccessibility(excludedNodes);
            List<RequirementNodeID> newExcludedNodes = excludedNodes.GetRange(0, excludedNodes.Count);
            newExcludedNodes.Add(ID);

            foreach (KeyDoor bigKeyDoor in _bigKeyDoors)
            {
                AccessibilityLevel doorAccessibility = bigKeyDoor.GetDoorAccessibility(newExcludedNodes);

                if (bigKeyDoor.Unlocked && doorAccessibility > nodeAccessibility)
                    nodeAccessibility = doorAccessibility;
            }

            foreach (KeyDoor smallKeyDoor in _smallKeyDoors)
            {
                AccessibilityLevel doorAccessibility = smallKeyDoor.GetDoorAccessibility(newExcludedNodes);

                if (smallKeyDoor.Unlocked && doorAccessibility > nodeAccessibility)
                    nodeAccessibility = doorAccessibility;
            }

            return nodeAccessibility;
        }

        public override void Initialize()
        {
            switch (ID)
            {
                case RequirementNodeID.HCFront:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.HCEscapeFirstKeyDoor]);
                    }
                    break;
                case RequirementNodeID.HCPastEscapeFirstKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.HCEscapeFirstKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.HCEscapeSecondKeyDoor]);
                    }
                    break;
                case RequirementNodeID.HCPastEscapeSecondKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.HCEscapeSecondKeyDoor]);
                    }
                    break;
                case RequirementNodeID.HCDarkRoomFront:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.HCDarkCrossRoomKeyDoor]);
                    }
                    break;
                case RequirementNodeID.HCPastDarkCrossKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.HCDarkCrossRoomKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.HCSewerRatRoomKeyDoor]);
                    }
                    break;
                case RequirementNodeID.HCPastSewerRatRoomKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.HCSewerRatRoomKeyDoor]);
                    }
                    break;
                case RequirementNodeID.AT:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.ATFirstKeyDoor]);
                    }
                    break;
                case RequirementNodeID.ATPastFirstKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.ATFirstKeyDoor]);
                    }
                    break;
                case RequirementNodeID.ATDarkMaze:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.ATSecondKeyDoor]);
                    }
                    break;
                case RequirementNodeID.ATPastSecondKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.ATSecondKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.ATThirdKeyDoor]);
                    }
                    break;
                case RequirementNodeID.ATPastThirdKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.ATThirdKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.ATFourthKeyDoor]);
                    }
                    break;
                case RequirementNodeID.ATPastFourthKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.ATFourthKeyDoor]);
                    }
                    break;
                case RequirementNodeID.EP:
                case RequirementNodeID.EPBigChest:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.EPBigChest]);
                    }
                    break;
                case RequirementNodeID.EPRightWingDarkRoom:
                case RequirementNodeID.EPPastRightWingKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.EPRightWingKeyDoor]);
                    }
                    break;
                case RequirementNodeID.EPPastBigKeyDoor:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.EPBigKeyDoor]);
                    }
                    break;
                case RequirementNodeID.EPBackDarkRoom:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.EPBigKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.EPBossKeyDoor]);
                    }
                    break;
                case RequirementNodeID.EPPastBackKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.EPBossKeyDoor]);
                    }
                    break;
                case RequirementNodeID.DPFront:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.DPRightWingKeyDoor]);
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.DPBigChest]);
                    }
                    break;
                case RequirementNodeID.DPBigChest:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.DPBigChest]);
                    }
                    break;
                case RequirementNodeID.DPPastRightWingKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.DPRightWingKeyDoor]);
                    }
                    break;
                case RequirementNodeID.DPBack:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.DP1FKeyDoor]);
                    }
                    break;
                case RequirementNodeID.DP2F:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.DP1FKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.DP2FFirstKeyDoor]);
                    }
                    break;
                case RequirementNodeID.DP2FPastFirstKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.DP2FFirstKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.DP2FSecondKeyDoor]);
                    }
                    break;
                case RequirementNodeID.DP2FPastSecondKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.DP2FSecondKeyDoor]);
                    }
                    break;
                case RequirementNodeID.DPPastFourTorchWall:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.DPBigKeyDoor]);
                    }
                    break;
                case RequirementNodeID.DPBossRoom:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.DPBigKeyDoor]);
                    }
                    break;
                case RequirementNodeID.ToH:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.ToHKeyDoor]);
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.ToHBigKeyDoor]);
                    }
                    break;
                case RequirementNodeID.ToHPastKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.ToHKeyDoor]);
                    }
                    break;
                case RequirementNodeID.ToHPastBigKeyDoor:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.ToHBigKeyDoor]);
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.ToHBigChest]);
                    }
                    break;
                case RequirementNodeID.ToHBigChest:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.ToHBigChest]);
                    }
                    break;
                case RequirementNodeID.PoD:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.PoDFrontKeyDoor]);
                    }
                    break;
                case RequirementNodeID.PoDPastFirstRedGoriyaRoom:
                    {
                    }
                    break;
                case RequirementNodeID.PoDLobbyArena:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.PoDFrontKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.PoDBigKeyChestKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.PoDCollapsingStairwayKeyDoor]);
                    }
                    break;
                case RequirementNodeID.PoDBigKeyChestArea:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.PoDBigKeyChestKeyDoor]);
                    }
                    break;
                case RequirementNodeID.PoDPastCollapsingWalkwayKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.PoDCollapsingStairwayKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.PoDHarmlessHellwayKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.PoDDarkMazeKeyDoor]);
                    }
                    break;
                case RequirementNodeID.PoDHarmlessHellwayRoom:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.PoDHarmlessHellwayKeyDoor]);
                    }
                    break;
                case RequirementNodeID.PoDPastDarkMazeKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.PoDDarkMazeKeyDoor]);
                    }
                    break;
                case RequirementNodeID.PoDBigChestLedge:
                case RequirementNodeID.PoDBigChest:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.PoDBigChest]);
                    }
                    break;
                case RequirementNodeID.PoDPastHammerBlocks:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.PoDBossAreaKeyDoor]);
                    }
                    break;
                case RequirementNodeID.PoDPastBossAreaKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.PoDBossAreaKeyDoor]);
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.PoDBigKeyDoor]);
                    }
                    break;
                case RequirementNodeID.PoDBossRoom:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.PoDBigKeyDoor]);
                    }
                    break;
                case RequirementNodeID.SPAfterRiver:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.SP1FKeyDoor]);
                    }
                    break;
                case RequirementNodeID.SPB1:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.SP1FKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.SPFirstRightSideKeyDoor]);
                    }
                    break;
                case RequirementNodeID.SPPastFirstRightSideKeyDoor:
                case RequirementNodeID.SPPastSecondRightSideKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.SPFirstRightSideKeyDoor]);
                    }
                    break;
                case RequirementNodeID.SPPastRightSideHammerBlocks:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.SPLeftSideKeyDoor]);
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.SPBigChest]);
                    }
                    break;
                case RequirementNodeID.SPPastLeftSideKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.SPLeftSideKeyDoor]);
                    }
                    break;
                case RequirementNodeID.SPBigChest:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.SPBigChest]);
                    }
                    break;
                case RequirementNodeID.SPBack:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.SPBackFirstKeyDoor]);
                    }
                    break;
                case RequirementNodeID.SPPastBackFirstKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.SPBackFirstKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.SPBossRoomKeyDoor]);
                    }
                    break;
                case RequirementNodeID.SPBossRoom:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.SPBossRoomKeyDoor]);
                    }
                    break;
                case RequirementNodeID.SWBigChestAreaBottom:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.SWFrontLeftKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.SWFrontRightKeyDoor]);
                    }
                    break;
                case RequirementNodeID.SWBigChestAreaTop:
                case RequirementNodeID.SWBigChest:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.SWBigChest]);
                    }
                    break;
                case RequirementNodeID.SWFrontLeftSide:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.SWFrontLeftKeyDoor]);
                    }
                    break;
                case RequirementNodeID.SWFrontRightSide:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.SWFrontRightKeyDoor]);
                    }
                    break;
                case RequirementNodeID.SWFrontBackConnector:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.SWWorthlessKeyDoor]);
                    }
                    break;
                case RequirementNodeID.SWPastTheWorthlessKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.SWWorthlessKeyDoor]);
                    }
                    break;
                case RequirementNodeID.SWBack:
                case RequirementNodeID.SWBackPastFirstKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.SWBackFirstKeyDoor]);
                    }
                    break;
                case RequirementNodeID.SWBackPastCurtains:
                case RequirementNodeID.SWBossRoom:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.SWBackSeceondKeyDoor]);
                    }
                    break;
                case RequirementNodeID.TT:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.TTBigKeyDoor]);
                    }
                    break;
                case RequirementNodeID.TTPastBigKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.TTFirstKeyDoor]);
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.TTBigKeyDoor]);
                    }
                    break;
                case RequirementNodeID.TTPastFirstKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.TTFirstKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.TTSecondKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.TTBigChestKeyDoor]);
                    }
                    break;
                case RequirementNodeID.TTPastSecondKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.TTSecondKeyDoor]);
                    }
                    break;
                case RequirementNodeID.TTPastBigChestRoomKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.TTBigChestKeyDoor]);
                    }
                    break;
                case RequirementNodeID.TTPastHammerBlocks:
                case RequirementNodeID.TTBigChest:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.TTBigChest]);
                    }
                    break;
                case RequirementNodeID.IPPastEntranceFreezorRoom:
                case RequirementNodeID.IPB1LeftSide:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.IP1FKeyDoor]);
                    }
                    break;
                case RequirementNodeID.IPB2LeftSide:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.IPB2KeyDoor]);
                    }
                    break;
                case RequirementNodeID.IPB2PastKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.IPB2KeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.IPB3KeyDoor]);
                    }
                    break;
                case RequirementNodeID.IPSpikeRoom:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.IPB3KeyDoor]);
                    }
                    break;
                case RequirementNodeID.IPB4IceRoom:
                case RequirementNodeID.IPB4PastKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.IPB4KeyDoor]);
                    }
                    break;
                case RequirementNodeID.IPBigChestArea:
                case RequirementNodeID.IPBigChest:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.IPBigChest]);
                    }
                    break;
                case RequirementNodeID.IPB5:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.IPBigKeyDoor]);
                    }
                    break;
                case RequirementNodeID.IPB5PastBigKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.IPB5KeyDoor]);
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.IPBigKeyDoor]);
                    }
                    break;
                case RequirementNodeID.IPB6:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.IPB5KeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.IPB6KeyDoor]);
                    }
                    break;
                case RequirementNodeID.IPB6PastKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.IPB6KeyDoor]);
                    }
                    break;
                case RequirementNodeID.MMPastEntranceGap:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.MMB1TopRightKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.MMB1TopLeftKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.MMB1LeftSideFirstKeyDoor]);
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.MMBigChest]);
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.MMPortalBigKeyDoor]);
                    }
                    break;
                case RequirementNodeID.MMBigChest:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.MMBigChest]);
                    }
                    break;
                case RequirementNodeID.MMB1TopSide:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.MMB1TopRightKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.MMB1TopLeftKeyDoor]);
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.MMBridgeBigKeyDoor]);
                    }
                    break;
                case RequirementNodeID.MMB1LobbyBeyondBlueBlocks:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.MMB1RightSideKeyDoor]);
                    }
                    break;
                case RequirementNodeID.MMB1RightSideBeyondBlueBlocks:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.MMB1RightSideKeyDoor]);
                    }
                    break;
                case RequirementNodeID.MMB1LeftSidePastFirstKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.MMB1LeftSideFirstKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.MMB1LeftSideSecondKeyDoor]);
                    }
                    break;
                case RequirementNodeID.MMB1LeftSidePastSecondKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.MMB1LeftSideSecondKeyDoor]);
                    }
                    break;
                case RequirementNodeID.MMB1PastPortalBigKeyDoor:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.MMPortalBigKeyDoor]);
                    }
                    break;
                case RequirementNodeID.MMB1PastBridgeBigKeyDoor:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.MMBridgeBigKeyDoor]);
                    }
                    break;
                case RequirementNodeID.MMDarkRoom:
                case RequirementNodeID.MMB2PastWorthlessKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.MMB2WorthlessKeyDoor]);
                    }
                    break;
                case RequirementNodeID.MMB2PastCaneOfSomariaSwitch:
                case RequirementNodeID.MMBossRoom:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.MMBossRoomBigKeyDoor]);
                    }
                    break;
                case RequirementNodeID.TRF1FirstKeyDoorArea:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.TR1FFirstKeyDoor]);
                    }
                    break;
                case RequirementNodeID.TRF1PastFirstKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.TR1FFirstKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.TR1FSecondKeyDoor]);
                    }
                    break;
                case RequirementNodeID.TRF1PastSecondKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.TR1FSecondKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.TR1FThirdKeyDoor]);
                    }
                    break;
                case RequirementNodeID.TRB1:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.TR1FThirdKeyDoor]);
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.TRB1BigKeyChestKeyDoor]);
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.TRB1BigKeyDoor]);
                    }
                    break;
                case RequirementNodeID.TRB1PastBigKeyChestKeyDoor:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.TRB1BigKeyChestKeyDoor]);
                    }
                    break;
                case RequirementNodeID.TRB1BigChestArea:
                case RequirementNodeID.TRBigChest:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.TRBigChest]);
                    }
                    break;
                case RequirementNodeID.TRB1RightSide:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.TRB1toB2KeyDoor]);
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.TRB1BigKeyChestKeyDoor]);
                    }
                    break;
                case RequirementNodeID.TRPastB1toB2KeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.TRB1toB2KeyDoor]);
                    }
                    break;
                case RequirementNodeID.TRB2PastDarkMaze:
                case RequirementNodeID.TRB2PastKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.TRB2KeyDoor]);
                    }
                    break;
                case RequirementNodeID.TRB3BossRoomEntry:
                case RequirementNodeID.TRBossRoom:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.TRBossRoomBigKeyDoor]);
                    }
                    break;
                case RequirementNodeID.GT1FLeft:
                case RequirementNodeID.GT1FRight:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.GT1FLeftToRightKeyDoor]);
                    }
                    break;
                case RequirementNodeID.GT1FLeftPastBonkableGaps:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.GT1FMapChestRoomKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor]);
                    }
                    break;
                case RequirementNodeID.GT1FLeftMapChestRoom:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.GT1FMapChestRoomKeyDoor]);
                    }
                    break;
                case RequirementNodeID.GT1FLeftSpikeTrapPortalRoom:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor]);
                    }
                    break;
                case RequirementNodeID.GT1FLeftPastFiresnakeRoomGap:
                case RequirementNodeID.GT1FLeftPastFiresnakeRoomKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.GT1FFiresnakeRoomKeyDoor]);
                    }
                    break;
                case RequirementNodeID.GT1FRightTileRoom:
                case RequirementNodeID.GT1FRightFourTorchRoom:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.GT1FTileRoomKeyDoor]);
                    }
                    break;
                case RequirementNodeID.GT1FRightPastCompassRoomPortal:
                case RequirementNodeID.GT1FRightCollapsingWalkway:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.GT1FCollapsingWalkwayKeyDoor]);
                    }
                    break;
                case RequirementNodeID.GT1FBottomRoom:
                case RequirementNodeID.GTBigChest:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.GTBigChest]);
                    }
                    break;
                case RequirementNodeID.GT3FPastRedGoriyaRooms:
                case RequirementNodeID.GT3FPastBigKeyDoor:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.GT3FBigKeyDoor]);
                    }
                    break;
                case RequirementNodeID.GT5FPastFourTorchRooms:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.GT6FFirstKeyDoor]);
                    }
                    break;
                case RequirementNodeID.GT6FPastFirstKeyDoor:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.GT6FFirstKeyDoor]);
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.GT6FSecondKeyDoor]);
                    }
                    break;
                case RequirementNodeID.GT6FBossRoom:
                    {
                        _smallKeyDoors.Add(Game.KeyDoors[KeyDoorID.GT6FSecondKeyDoor]);
                    }
                    break;
                case RequirementNodeID.GT6FPastBossRoomGap:
                case RequirementNodeID.GTFinalBossRoom:
                    {
                        _bigKeyDoors.Add(Game.KeyDoors[KeyDoorID.GT7FBigKeyDoor]);
                    }
                    break;
            }

            foreach (KeyDoor smallKeyDoor in _smallKeyDoors)
                smallKeyDoor.PropertyChanged += OnKeyDoorChanged;

            foreach (KeyDoor bigKeyDoor in _bigKeyDoors)
                bigKeyDoor.PropertyChanged += OnKeyDoorChanged;

            base.Initialize();
        }
    }
}
