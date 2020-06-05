using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    public class DungeonNode : RequirementNode
    {
        private readonly DungeonData _dungeonData;
        private readonly List<KeyDoor> _smallKeyDoors;
        private readonly List<KeyDoor> _bigKeyDoors;

        public int FreeKeysProvided { get; }
        public List<RequirementNodeConnection> DungeonConnections { get; }

        public DungeonNode(Game game, DungeonData dungeonData, RequirementNodeID iD)  : base(game, iD)
        {
            _dungeonData = dungeonData ?? throw new ArgumentNullException(nameof(dungeonData));
            DungeonConnections = new List<RequirementNodeConnection>();
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

        public override AccessibilityLevel GetNodeAccessibility(List<RequirementNodeID> excludedNodes)
        {
            if (excludedNodes == null)
                throw new ArgumentNullException(nameof(excludedNodes));

            AccessibilityLevel finalAccessibility = base.GetNodeAccessibility(excludedNodes);
            List<RequirementNodeID> newExcludedNodes = excludedNodes.GetRange(0, excludedNodes.Count);
            newExcludedNodes.Add(ID);

            foreach (RequirementNodeConnection dungeonConnection in DungeonConnections)
            {
                if (!Game.Mode.Validate(dungeonConnection.RequiredMode))
                    continue;

                if (newExcludedNodes.Contains(dungeonConnection.FromNode))
                    continue;

                AccessibilityLevel nodeAccessibility =
                    _dungeonData.RequirementNodes[dungeonConnection.FromNode].GetNodeAccessibility(newExcludedNodes);

                if (nodeAccessibility < AccessibilityLevel.SequenceBreak)
                    continue;

                AccessibilityLevel requirementAccessibility =
                    Game.Requirements[dungeonConnection.Requirement].Accessibility;

                AccessibilityLevel finalConnectionAccessibility =
                    (AccessibilityLevel)Math.Min(Math.Min((byte)nodeAccessibility, (byte)requirementAccessibility),
                    (byte)dungeonConnection.MaximumAccessibility);

                if (finalConnectionAccessibility == AccessibilityLevel.Normal)
                    return AccessibilityLevel.Normal;

                if (finalConnectionAccessibility > finalAccessibility)
                    finalAccessibility = finalConnectionAccessibility;
            }

            foreach (KeyDoor bigKeyDoor in _bigKeyDoors)
            {
                AccessibilityLevel doorAccessibility = bigKeyDoor.GetDoorAccessibility(newExcludedNodes);

                if (bigKeyDoor.Unlocked && doorAccessibility > finalAccessibility)
                    finalAccessibility = doorAccessibility;
            }

            foreach (KeyDoor smallKeyDoor in _smallKeyDoors)
            {
                AccessibilityLevel doorAccessibility = smallKeyDoor.GetDoorAccessibility(newExcludedNodes);

                if (smallKeyDoor.Unlocked && doorAccessibility > finalAccessibility)
                    finalAccessibility = doorAccessibility;
            }

            return finalAccessibility;
        }

        public override void Initialize()
        {
            switch (ID)
            {
				case RequirementNodeID.HCDarkRoomFront:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.HCFront,
							RequirementType.DarkRoomWithFireRod, new Mode()));
					}
					break;
				case RequirementNodeID.HCPastSewerRatRoomKeyDoor:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.HCDarkRoomBack,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.HCDarkRoomBack:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.HCPastSewerRatRoomKeyDoor,
							RequirementType.None, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.HCBack,
							RequirementType.DarkRoomWithFireRod, new Mode()));
					}
					break;
				case RequirementNodeID.HCBack:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.HCDarkRoomBack,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.ATPastFirstKeyDoor:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.ATDarkMaze,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.ATDarkMaze:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.ATPastFirstKeyDoor,
							RequirementType.DarkRoom, new Mode()));
					}
					break;
				case RequirementNodeID.ATBossRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.ATPastFourthKeyDoor,
							RequirementType.Curtains, new Mode()));
					}
					break;
				case RequirementNodeID.ATBoss:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.ATBossRoom,
							RequirementType.ATBoss, new Mode()));
					}
					break;
				case RequirementNodeID.EP:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.EPPastRightWingKeyDoor,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.EPRightWingDarkRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.EP,
							RequirementType.DarkRoom, new Mode()));
					}
					break;
				case RequirementNodeID.EPBackDarkRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.EPPastBigKeyDoor,
							RequirementType.DarkRoomWithFireRod, new Mode()));
					}
					break;
				case RequirementNodeID.EPBossRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.EPPastBackKeyDoor,
							RequirementType.RedEyegoreGoriya, new Mode()));
					}
					break;
				case RequirementNodeID.EPBoss:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.EPBossRoom,
							RequirementType.EPBoss, new Mode()));
					}
					break;
				case RequirementNodeID.DPPastFourTorchWall:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.DP2FPastSecondKeyDoor,
							RequirementType.FireSource, new Mode()));
					}
					break;
				case RequirementNodeID.DPBoss:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.DPBossRoom,
							RequirementType.DPBoss, new Mode()));
					}
					break;
				case RequirementNodeID.ToHBasementTorchRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.ToHPastKeyDoor,
							RequirementType.FireSource, new Mode()));
					}
					break;
				case RequirementNodeID.ToHPastBigKeyDoor:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.ToH,
							RequirementType.Hookshot, new Mode(), AccessibilityLevel.SequenceBreak));
					}
					break;
				case RequirementNodeID.ToHBoss:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.ToHPastBigKeyDoor,
							RequirementType.ToHBoss, new Mode()));
					}
					break;
				case RequirementNodeID.PoDPastFirstRedGoriyaRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.PoD,
							RequirementType.RedEyegoreGoriya, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.PoD,
							RequirementType.Bottle, new Mode(), AccessibilityLevel.SequenceBreak));
					}
					break;
				case RequirementNodeID.PoDLobbyArena:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.PoDPastFirstRedGoriyaRoom,
							RequirementType.Hammer, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.PoDBigKeyChestArea,
							RequirementType.None, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.PoDHarmlessHellwayRoom,
							RequirementType.None, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.PoDPastSecondRedGoriyaRoom,
							RequirementType.RedEyegoreGoriya, new Mode()));
					}
					break;
				case RequirementNodeID.PoDPastCollapsingWalkwayKeyDoor:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.PoDDarkBasement,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.PoDDarkBasement:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.PoDPastCollapsingWalkwayKeyDoor,
							RequirementType.DarkRoomWithFireRod, new Mode()));
					}
					break;
				case RequirementNodeID.PoDDarkMaze:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.PoDPastDarkMazeKeyDoor,
							RequirementType.DarkRoom, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.PoDBigChestLedge,
							RequirementType.DarkRoom, new Mode()));
					}
					break;
				case RequirementNodeID.PoDBigChestLedge:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.PoDDarkMaze,
							RequirementType.None, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.PoDPastCollapsingWalkwayKeyDoor,
							RequirementType.None, new Mode(), AccessibilityLevel.SequenceBreak));
					}
					break;
				case RequirementNodeID.PoDPastSecondRedGoriyaRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.PoDLobbyArena,
							RequirementType.RedEyegoreGoriya, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.PoDPastBowStatue,
							RequirementType.Bow, new Mode()));
					}
					break;
				case RequirementNodeID.PoDPastBowStatue:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.PoDPastSecondRedGoriyaRoom,
							RequirementType.Bow, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.PoDBossAreaDarkRooms,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.PoDBossAreaDarkRooms:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.PoDPastBowStatue,
							RequirementType.DarkRoom, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.PoDPastHammerBlocks,
							RequirementType.Hammer, new Mode(), AccessibilityLevel.SequenceBreak));
					}
					break;
				case RequirementNodeID.PoDPastHammerBlocks:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.PoDBossAreaDarkRooms,
							RequirementType.Hammer, new Mode()));
					}
					break;
				case RequirementNodeID.PoDBoss:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.PoDBossRoom,
							RequirementType.PoDBoss, new Mode()));
					}
					break;
				case RequirementNodeID.SP:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.SPAfterRiver,
							RequirementType.SwimNoFakeFlippers, new Mode()));
					}
					break;
				case RequirementNodeID.SPAfterRiver:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.SP,
							RequirementType.SwimNoFakeFlippers, new Mode()));
					}
					break;
				case RequirementNodeID.SPPastSecondRightSideKeyDoor:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.SPPastRightSideHammerBlocks,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.SPPastRightSideHammerBlocks:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.SPPastFirstRightSideKeyDoor,
							RequirementType.Hammer, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.SPB1KeyLedge,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.SPB1KeyLedge:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.SPPastRightSideHammerBlocks,
							RequirementType.Hookshot, new Mode()));
					}
					break;
				case RequirementNodeID.SPBack:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.SPPastRightSideHammerBlocks,
							RequirementType.Hookshot, new Mode()));
					}
					break;
				case RequirementNodeID.SPBoss:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.SPBossRoom,
							RequirementType.SPBoss, new Mode()));
					}
					break;
				case RequirementNodeID.SWBigChestAreaBottom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.SWBigChestAreaTop,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.SWBigChestAreaTop:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.SWBigChestAreaBottom,
							RequirementType.None, new Mode(), AccessibilityLevel.SequenceBreak));
					}
					break;
				case RequirementNodeID.SWFrontRightSide:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.SWFrontLeftSide,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.SWBackPastFourTorchRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.SWBackPastFirstKeyDoor,
							RequirementType.FireRod, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.SWBackPastCurtains,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.SWBackPastCurtains:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.SWBackPastFourTorchRoom,
							RequirementType.Curtains, new Mode()));
					}
					break;
				case RequirementNodeID.SWBoss:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.SWBossRoom,
							RequirementType.SWBoss, new Mode()));
					}
					break;
				case RequirementNodeID.TTPastBigChestRoomKeyDoor:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TTPastHammerBlocks,
							RequirementType.Hammer, new Mode()));
					}
					break;
				case RequirementNodeID.TTPastHammerBlocks:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TTPastBigChestRoomKeyDoor,
							RequirementType.Hammer, new Mode()));
					}
					break;
				case RequirementNodeID.TTBossRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TTPastSecondKeyDoor,
							RequirementType.None, new Mode() { BossShuffle = false }));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TTPastBigKeyDoor,
							RequirementType.None, new Mode() { BossShuffle = true }));
					}
					break;
				case RequirementNodeID.TTBoss:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TTBossRoom,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.IP:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPPastEntranceFreezorRoom,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.IPPastEntranceFreezorRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IP,
							RequirementType.MeltThings, new Mode()));
					}
					break;
				case RequirementNodeID.IPB1RightSide:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPB1LeftSide,
							RequirementType.IceBreaker, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPB2PastLiftBlock,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.IPB2LeftSide:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPB1LeftSide,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.IPB2PastHammerBlocks:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPSpikeRoom,
							RequirementType.Hammer, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPB2PastLiftBlock,
							RequirementType.Lift1, new Mode()));
					}
					break;
				case RequirementNodeID.IPB2PastLiftBlock:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPB2PastHammerBlocks,
							RequirementType.Lift1, new Mode()));
					}
					break;
				case RequirementNodeID.IPSpikeRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPB1RightSide,
							RequirementType.None, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPB2PastHammerBlocks,
							RequirementType.Hammer, new Mode()));
					}
					break;
				case RequirementNodeID.IPB4RightSide:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPSpikeRoom,
							RequirementType.None, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPB4IceRoom,
							RequirementType.None, new Mode(), AccessibilityLevel.SequenceBreak));
					}
					break;
				case RequirementNodeID.IPB4IceRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPB4FreezorRoom,
							RequirementType.None, new Mode(), AccessibilityLevel.SequenceBreak));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPB2PastKeyDoor,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.IPB4FreezorRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPB2PastKeyDoor,
							RequirementType.None, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPB4IceRoom,
							RequirementType.None, new Mode(), AccessibilityLevel.SequenceBreak));
					}
					break;
				case RequirementNodeID.IPB4PastKeyDoor:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPB5,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.IPBigChestArea:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPB4FreezorRoom,
							RequirementType.None, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPB5,
							RequirementType.Hookshot, new Mode(), AccessibilityLevel.SequenceBreak));
					}
					break;
				case RequirementNodeID.IPB5:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPB4FreezorRoom,
							RequirementType.None, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPB4PastKeyDoor,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.IPB6:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPB5,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.IPB6PastHammerBlocks:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPB6,
							RequirementType.Hammer, new Mode()));
					}
					break;
				case RequirementNodeID.IPB6PastLiftBlock:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPB6PastHammerBlocks,
							RequirementType.Lift1, new Mode()));
					}
					break;
				case RequirementNodeID.IPBoss:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.IPB6PastLiftBlock,
							RequirementType.IPBoss, new Mode()));
					}
					break;
				case RequirementNodeID.MM:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.MMPastEntranceGap,
							RequirementType.DashOrHookshot, new Mode()));
					}
					break;
				case RequirementNodeID.MMPastEntranceGap:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.MM,
							RequirementType.DashOrHookshot, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.MMB1PastFourTorchRoom,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.MMB1TopSide:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.MMB1PastPortalBigKeyDoor,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.MMB1LobbyBeyondBlueBlocks:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.MMB1TopSide,
							RequirementType.None, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.MMB1LeftSidePastFirstKeyDoor,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.MMB1RightSideBeyondBlueBlocks:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.MMB1TopSide,
							RequirementType.None, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.MMB1LeftSidePastFirstKeyDoor,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.MMB1LeftSidePastSecondKeyDoor:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.MMB1PastFourTorchRoom,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.MMB1PastFourTorchRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.MMB1LeftSidePastSecondKeyDoor,
							RequirementType.FireSource, new Mode()));
					}
					break;
				case RequirementNodeID.MMF1PastFourTorchRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.MMB1LeftSidePastSecondKeyDoor,
							RequirementType.FireSource, new Mode()));
					}
					break;
				case RequirementNodeID.MMDarkRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.MMB1PastBridgeBigKeyDoor,
							RequirementType.DarkRoom, new Mode()));
					}
					break;
				case RequirementNodeID.MMB2PastCaneOfSomariaSwitch:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.MMDarkRoom,
							RequirementType.CaneOfSomaria, new Mode()));
					}
					break;
				case RequirementNodeID.MMBoss:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.MMBossRoom,
							RequirementType.MMBoss, new Mode()));
					}
					break;
				case RequirementNodeID.TRFront:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRF1CompassChestArea,
							RequirementType.CaneOfSomaria, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRF1FourTorchRoom,
							RequirementType.CaneOfSomaria, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRF1FirstKeyDoorArea,
							RequirementType.CaneOfSomaria, new Mode()));
					}
					break;
				case RequirementNodeID.TRF1CompassChestArea:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRFront,
							RequirementType.CaneOfSomaria, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRF1FourTorchRoom,
							RequirementType.CaneOfSomaria, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRF1FirstKeyDoorArea,
							RequirementType.CaneOfSomaria, new Mode()));
					}
					break;
				case RequirementNodeID.TRF1FourTorchRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRFront,
							RequirementType.CaneOfSomaria, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRF1CompassChestArea,
							RequirementType.CaneOfSomaria, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRF1RollerRoom,
							RequirementType.None, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRF1FirstKeyDoorArea,
							RequirementType.CaneOfSomaria, new Mode()));
					}
					break;
				case RequirementNodeID.TRF1RollerRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRF1FourTorchRoom,
							RequirementType.FireRod, new Mode()));
					}
					break;
				case RequirementNodeID.TRF1FirstKeyDoorArea:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRFront,
							RequirementType.CaneOfSomaria, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRF1CompassChestArea,
							RequirementType.CaneOfSomaria, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRF1FourTorchRoom,
							RequirementType.CaneOfSomaria, new Mode()));
					}
					break;
				case RequirementNodeID.TRF1PastSecondKeyDoor:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRB1,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.TRB1MiddleRightEntranceArea:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRB1,
							RequirementType.None, new Mode() { EntranceShuffle = false }));
					}
					break;
				case RequirementNodeID.TRB1BigChestArea:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRB1MiddleRightEntranceArea,
							RequirementType.CaneOfSomaria, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRB1MiddleRightEntranceArea,
							RequirementType.Hookshot, new Mode()));
					}
					break;
				case RequirementNodeID.TRB1RightSide:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRB1,
							RequirementType.None, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRB1BigChestArea,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.TRPastB1toB2KeyDoor:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRB2DarkRoomTop,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.TRB2DarkRoomTop:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRPastB1toB2KeyDoor,
							RequirementType.DarkRoom, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRB2DarkRoomBottom,
							RequirementType.CaneOfSomaria, new Mode()));
					}
					break;
				case RequirementNodeID.TRB2DarkRoomBottom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRB2DarkRoomTop,
							RequirementType.CaneOfSomaria, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRB2PastDarkMaze,
							RequirementType.DarkRoom, new Mode()));
					}
					break;
				case RequirementNodeID.TRB2PastDarkMaze:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRB2DarkRoomBottom,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.TRB2PastKeyDoor:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRB3,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.TRB3:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRB2PastKeyDoor,
							RequirementType.None, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRB3BossRoomEntry,
							RequirementType.CaneOfSomaria, new Mode()));
					}
					break;
				case RequirementNodeID.TRB3BossRoomEntry:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRB3,
							RequirementType.CaneOfSomaria, new Mode()));
					}
					break;
				case RequirementNodeID.TRBoss:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.TRBossRoom,
							RequirementType.TRBoss, new Mode()));
					}
					break;
				case RequirementNodeID.GT:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeft,
							RequirementType.None, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FRight,
							RequirementType.None, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT3FPastRedGoriyaRooms,
							RequirementType.RedEyegoreGoriya, new Mode()));
					}
					break;
				case RequirementNodeID.GT1FLeft:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT,
							RequirementType.None, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeftPastHammerBlocks,
							RequirementType.None, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FBottomRoom,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.GT1FLeftPastHammerBlocks:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeft,
							RequirementType.Hammer, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeftDMsRoom,
							RequirementType.Hookshot, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeftDMsRoom,
							RequirementType.Dash, new Mode(), AccessibilityLevel.SequenceBreak));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeftPastBonkableGaps,
							RequirementType.Hookshot, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeftPastBonkableGaps,
							RequirementType.Dash, new Mode(), AccessibilityLevel.SequenceBreak));
					}
					break;
				case RequirementNodeID.GT1FLeftDMsRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeftPastHammerBlocks,
							RequirementType.Hookshot, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeftPastHammerBlocks,
							RequirementType.Dash, new Mode(), AccessibilityLevel.SequenceBreak));
					}
					break;
				case RequirementNodeID.GT1FLeftPastBonkableGaps:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeftPastHammerBlocks,
							RequirementType.DashOrHookshot, new Mode()));
					}
					break;
				case RequirementNodeID.GT1FLeftFiresnakeRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeftSpikeTrapPortalRoom,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.GT1FLeftPastFiresnakeRoomGap:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeftFiresnakeRoom,
							RequirementType.Hookshot, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeftFiresnakeRoom,
							RequirementType.Dash, new Mode(), AccessibilityLevel.SequenceBreak));
					}
					break;
				case RequirementNodeID.GT1FLeftPastFiresnakeRoomKeyDoor:
					{
					}
					break;
				case RequirementNodeID.GT1FLeftRandomizerRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeftPastFiresnakeRoomKeyDoor,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.GT1FRight:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT,
							RequirementType.None, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FRightTileRoom,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.GT1FRightTileRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FRight,
							RequirementType.CaneOfSomaria, new Mode()));
					}
					break;
				case RequirementNodeID.GT1FRightFourTorchRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FRightCompassRoom,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.GT1FRightCompassRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FRightFourTorchRoom,
							RequirementType.FireRod, new Mode()));
					}
					break;
				case RequirementNodeID.GT1FRightPastCompassRoomPortal:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FRightCompassRoom,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.GT1FBottomRoom:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FLeftRandomizerRoom,
							RequirementType.None, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FRightCollapsingWalkway,
							RequirementType.None, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GTBoss1,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.GTBoss1:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT1FBottomRoom,
							RequirementType.GTBoss1, new Mode()));
					}
					break;
				case RequirementNodeID.GTB1BossChests:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GTBoss1,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.GT3FPastRedGoriyaRooms:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT,
							RequirementType.RedEyegoreGoriya, new Mode()));
					}
					break;
				case RequirementNodeID.GTBoss2:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT3FPastBigKeyDoor,
							RequirementType.GTBoss2, new Mode()));
					}
					break;
				case RequirementNodeID.GT3FPastBoss2:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GTBoss2,
							RequirementType.None, new Mode()));
					}
					break;
				case RequirementNodeID.GT5FPastFourTorchRooms:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT3FPastBoss2,
							RequirementType.FireSource, new Mode()));
					}
					break;
				case RequirementNodeID.GTBoss3:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT6FBossRoom,
							RequirementType.GTBoss3, new Mode()));
					}
					break;
				case RequirementNodeID.GT6FPastBossRoomGap:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GTBoss3,
							RequirementType.Hookshot, new Mode()));
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GT6FBossRoom,
							RequirementType.Dash, new Mode(), AccessibilityLevel.SequenceBreak));
					}
					break;
				case RequirementNodeID.GTFinalBoss:
					{
						DungeonConnections.Add(new RequirementNodeConnection(RequirementNodeID.GTFinalBossRoom,
							RequirementType.GTFinalBoss, new Mode()));
					}
					break;
			}

			foreach (RequirementNodeConnection connection in DungeonConnections)
            {
                if (!NodeSubscriptions.Contains(connection.FromNode))
                {
                    _dungeonData.RequirementNodes[connection.FromNode].PropertyChanged += OnRequirementChanged;
                    NodeSubscriptions.Add(connection.FromNode);
                }

                if (!RequirementSubscriptions.Contains(connection.Requirement))
                {
                    Game.Requirements[connection.Requirement].PropertyChanged += OnRequirementChanged;
                    RequirementSubscriptions.Add(connection.Requirement);
                }
            }

            foreach (KeyDoor smallKeyDoor in _dungeonData.SmallKeyDoors.Values)
            {
                if (smallKeyDoor.ConnectedNodes.Contains(this))
                    _smallKeyDoors.Add(smallKeyDoor);
            }

            foreach (KeyDoor bigKeyDoor in _dungeonData.BigKeyDoors.Values)
            {
                if (bigKeyDoor.ConnectedNodes.Contains(this))
                    _bigKeyDoors.Add(bigKeyDoor);
            }

            foreach (KeyDoor smallKeyDoor in _smallKeyDoors)
                smallKeyDoor.PropertyChanged += OnRequirementChanged;

            foreach (KeyDoor bigKeyDoor in _bigKeyDoors)
                bigKeyDoor.PropertyChanged += OnRequirementChanged;

            base.Initialize();
        }
    }
}
