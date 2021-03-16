using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.RequirementNodes;
using System;

namespace OpenTracker.Models.KeyDoors
{
    /// <summary>
    /// This class contains creation logic for key door data.
    /// </summary>
    public class KeyDoorFactory : IKeyDoorFactory
    {
        private readonly IKeyDoor.Factory _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// An Autofac factory for creating key doors.
        /// </param>
        public KeyDoorFactory(IKeyDoor.Factory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Returns the requirement for the specified key door.
        /// </summary>
        /// <param name="id">
        /// The key door ID.
        /// </param>
        /// <param name="dungeonData">
        /// The mutable dungeon data.
        /// </param>
        /// <returns>
        /// The requirement for the specified key door.
        /// </returns>
        public IRequirementNode GetKeyDoorNode(KeyDoorID id, IMutableDungeon dungeonData)
        {
            return id switch
            {
                KeyDoorID.HCEscapeFirstKeyDoor => dungeonData.Nodes[DungeonNodeID.HCEscapeFirstKeyDoor],
                KeyDoorID.HCEscapeSecondKeyDoor => dungeonData.Nodes[DungeonNodeID.HCEscapeSecondKeyDoor],
                KeyDoorID.HCDarkCrossKeyDoor => dungeonData.Nodes[DungeonNodeID.HCDarkCrossKeyDoor],
                KeyDoorID.HCSewerRatRoomKeyDoor => dungeonData.Nodes[DungeonNodeID.HCSewerRatRoomKeyDoor],
                KeyDoorID.HCZeldasCellDoor => dungeonData.Nodes[DungeonNodeID.HCZeldasCellDoor],
                KeyDoorID.ATFirstKeyDoor => dungeonData.Nodes[DungeonNodeID.ATDarkMaze],
                KeyDoorID.ATSecondKeyDoor => dungeonData.Nodes[DungeonNodeID.ATSecondKeyDoor],
                KeyDoorID.ATThirdKeyDoor => dungeonData.Nodes[DungeonNodeID.ATPastSecondKeyDoor],
                KeyDoorID.ATFourthKeyDoor => dungeonData.Nodes[DungeonNodeID.ATFourthKeyDoor],
                KeyDoorID.EPRightKeyDoor => dungeonData.Nodes[DungeonNodeID.EPRightKeyDoor],
                KeyDoorID.EPBackKeyDoor => dungeonData.Nodes[DungeonNodeID.EPBackDarkRoom],
                KeyDoorID.EPBigChest => dungeonData.Nodes[DungeonNodeID.EP],
                KeyDoorID.EPBigKeyDoor => dungeonData.Nodes[DungeonNodeID.EPBigKeyDoor],
                KeyDoorID.DPRightKeyDoor => dungeonData.Nodes[DungeonNodeID.DPRightKeyDoor],
                KeyDoorID.DP1FKeyDoor => dungeonData.Nodes[DungeonNodeID.DPBack],
                KeyDoorID.DP2FFirstKeyDoor => dungeonData.Nodes[DungeonNodeID.DP2FFirstKeyDoor],
                KeyDoorID.DP2FSecondKeyDoor => dungeonData.Nodes[DungeonNodeID.DP2FSecondKeyDoor],
                KeyDoorID.DPBigChest => dungeonData.Nodes[DungeonNodeID.DPFront],
                KeyDoorID.DPBigKeyDoor => dungeonData.Nodes[DungeonNodeID.DPPastFourTorchWall],
                KeyDoorID.ToHKeyDoor => dungeonData.Nodes[DungeonNodeID.ToH],
                KeyDoorID.ToHBigKeyDoor => dungeonData.Nodes[DungeonNodeID.ToH],
                KeyDoorID.ToHBigChest => dungeonData.Nodes[DungeonNodeID.ToHPastBigKeyDoor],
                KeyDoorID.PoDFrontKeyDoor => dungeonData.Nodes[DungeonNodeID.PoDFrontKeyDoor],
                KeyDoorID.PoDBigKeyChestKeyDoor => dungeonData.Nodes[DungeonNodeID.PoDLobbyArena],
                KeyDoorID.PoDCollapsingWalkwayKeyDoor => dungeonData.Nodes[DungeonNodeID.PoDCollapsingWalkwayKeyDoor],
                KeyDoorID.PoDDarkMazeKeyDoor => dungeonData.Nodes[DungeonNodeID.PoDDarkMazeKeyDoor],
                KeyDoorID.PoDHarmlessHellwayKeyDoor => dungeonData.Nodes[DungeonNodeID.PoDHarmlessHellwayKeyDoor],
                KeyDoorID.PoDBossAreaKeyDoor => dungeonData.Nodes[DungeonNodeID.PoDBossAreaKeyDoor],
                KeyDoorID.PoDBigChest => dungeonData.Nodes[DungeonNodeID.PoDBigChestLedge],
                KeyDoorID.PoDBigKeyDoor => dungeonData.Nodes[DungeonNodeID.PoDPastBossAreaKeyDoor],
                KeyDoorID.SP1FKeyDoor => dungeonData.Nodes[DungeonNodeID.SPAfterRiver],
                KeyDoorID.SPB1FirstRightKeyDoor => dungeonData.Nodes[DungeonNodeID.SPB1FirstRightKeyDoor],
                KeyDoorID.SPB1SecondRightKeyDoor => dungeonData.Nodes[DungeonNodeID.SPB1SecondRightKeyDoor],
                KeyDoorID.SPB1LeftKeyDoor => dungeonData.Nodes[DungeonNodeID.SPB1LeftKeyDoor],
                KeyDoorID.SPB1BackFirstKeyDoor => dungeonData.Nodes[DungeonNodeID.SPB1BackFirstKeyDoor],
                KeyDoorID.SPBossRoomKeyDoor => dungeonData.Nodes[DungeonNodeID.SPBossRoomKeyDoor],
                KeyDoorID.SPBigChest => dungeonData.Nodes[DungeonNodeID.SPB1PastRightHammerBlocks],
                KeyDoorID.SWFrontLeftKeyDoor => dungeonData.Nodes[DungeonNodeID.SWFrontLeftKeyDoor],
                KeyDoorID.SWFrontRightKeyDoor => dungeonData.Nodes[DungeonNodeID.SWFrontRightKeyDoor],
                KeyDoorID.SWWorthlessKeyDoor => dungeonData.Nodes[DungeonNodeID.SWWorthlessKeyDoor],
                KeyDoorID.SWBackFirstKeyDoor => dungeonData.Nodes[DungeonNodeID.SWBackFirstKeyDoor],
                KeyDoorID.SWBackSecondKeyDoor => dungeonData.Nodes[DungeonNodeID.SWBackSecondKeyDoor],
                KeyDoorID.SWBigChest => dungeonData.Nodes[DungeonNodeID.SWBigChestAreaTop],
                KeyDoorID.TTFirstKeyDoor => dungeonData.Nodes[DungeonNodeID.TTFirstKeyDoor],
                KeyDoorID.TTSecondKeyDoor => dungeonData.Nodes[DungeonNodeID.TTPastFirstKeyDoor],
                KeyDoorID.TTBigChestKeyDoor => dungeonData.Nodes[DungeonNodeID.TTBigChestKeyDoor],
                KeyDoorID.TTBigKeyDoor => dungeonData.Nodes[DungeonNodeID.TTBigChestKeyDoor],
                KeyDoorID.TTBigChest => dungeonData.Nodes[DungeonNodeID.TTPastHammerBlocks],
                KeyDoorID.IP1FKeyDoor => dungeonData.Nodes[DungeonNodeID.IPPastEntranceFreezorRoom],
                KeyDoorID.IPB2KeyDoor => dungeonData.Nodes[DungeonNodeID.IPB2KeyDoor],
                KeyDoorID.IPB3KeyDoor => dungeonData.Nodes[DungeonNodeID.IPB3KeyDoor],
                KeyDoorID.IPB4KeyDoor => dungeonData.Nodes[DungeonNodeID.IPB4KeyDoor],
                KeyDoorID.IPB5KeyDoor => dungeonData.Nodes[DungeonNodeID.IPB5PastBigKeyDoor],
                KeyDoorID.IPB6KeyDoor => dungeonData.Nodes[DungeonNodeID.IPB6KeyDoor],
                KeyDoorID.IPBigKeyDoor => dungeonData.Nodes[DungeonNodeID.IPB5],
                KeyDoorID.IPBigChest => dungeonData.Nodes[DungeonNodeID.IPBigChestArea],
                KeyDoorID.MMB1TopRightKeyDoor => dungeonData.Nodes[DungeonNodeID.MMB1TopRightKeyDoor],
                KeyDoorID.MMB1TopLeftKeyDoor => dungeonData.Nodes[DungeonNodeID.MMB1TopLeftKeyDoor],
                KeyDoorID.MMB1LeftSideFirstKeyDoor => dungeonData.Nodes[DungeonNodeID.MMB1LeftSideFirstKeyDoor],
                KeyDoorID.MMB1LeftSideSecondKeyDoor => dungeonData.Nodes[DungeonNodeID.MMB1LeftSideSecondKeyDoor],
                KeyDoorID.MMB1RightSideKeyDoor => dungeonData.Nodes[DungeonNodeID.MMB1RightSideKeyDoor],
                KeyDoorID.MMB2WorthlessKeyDoor => dungeonData.Nodes[DungeonNodeID.MMB2WorthlessKeyDoor],
                KeyDoorID.MMBigChest => dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap],
                KeyDoorID.MMPortalBigKeyDoor => dungeonData.Nodes[DungeonNodeID.MMPastEntranceGap],
                KeyDoorID.MMBridgeBigKeyDoor => dungeonData.Nodes[DungeonNodeID.MMBridgeBigKeyDoor],
                KeyDoorID.MMBossRoomBigKeyDoor => dungeonData.Nodes[DungeonNodeID.MMB2PastCaneOfSomariaSwitch],
                KeyDoorID.TR1FFirstKeyDoor => dungeonData.Nodes[DungeonNodeID.TRF1FirstKeyDoor],
                KeyDoorID.TR1FSecondKeyDoor => dungeonData.Nodes[DungeonNodeID.TRF1SecondKeyDoor],
                KeyDoorID.TR1FThirdKeyDoor => dungeonData.Nodes[DungeonNodeID.TRF1PastSecondKeyDoor],
                KeyDoorID.TRB1BigKeyChestKeyDoor => dungeonData.Nodes[DungeonNodeID.TRB1BigKeyChestKeyDoor],
                KeyDoorID.TRB1ToB2KeyDoor => dungeonData.Nodes[DungeonNodeID.TRB1RightSide],
                KeyDoorID.TRB2KeyDoor => dungeonData.Nodes[DungeonNodeID.TRB2KeyDoor],
                KeyDoorID.TRBigChest => dungeonData.Nodes[DungeonNodeID.TRB1BigChestArea],
                KeyDoorID.TRB1BigKeyDoor => dungeonData.Nodes[DungeonNodeID.TRB1BigKeyDoor],
                KeyDoorID.TRBossRoomBigKeyDoor => dungeonData.Nodes[DungeonNodeID.TRB3BossRoomEntry],
                KeyDoorID.GT1FLeftToRightKeyDoor => dungeonData.Nodes[DungeonNodeID.GT1FLeftToRightKeyDoor],
                KeyDoorID.GT1FMapChestRoomKeyDoor => dungeonData.Nodes[DungeonNodeID.GT1FMapChestRoomKeyDoor],
                KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor => dungeonData.Nodes[DungeonNodeID.GT1FSpikeTrapPortalRoomKeyDoor],
                KeyDoorID.GT1FFiresnakeRoomKeyDoor => dungeonData.Nodes[DungeonNodeID.GT1FFiresnakeRoomKeyDoor],
                KeyDoorID.GT1FTileRoomKeyDoor => dungeonData.Nodes[DungeonNodeID.GT1FTileRoomKeyDoor],
                KeyDoorID.GT1FCollapsingWalkwayKeyDoor => dungeonData.Nodes[DungeonNodeID.GT1FCollapsingWalkwayKeyDoor],
                KeyDoorID.GT6FFirstKeyDoor => dungeonData.Nodes[DungeonNodeID.GT6FFirstKeyDoor],
                KeyDoorID.GT6FSecondKeyDoor => dungeonData.Nodes[DungeonNodeID.GT6FSecondKeyDoor],
                KeyDoorID.GTBigChest => dungeonData.Nodes[DungeonNodeID.GT1FBottomRoom],
                KeyDoorID.GT3FBigKeyDoor => dungeonData.Nodes[DungeonNodeID.GT3FBigKeyDoor],
                KeyDoorID.GT7FBigKeyDoor => dungeonData.Nodes[DungeonNodeID.GT6FPastBossRoomGap],
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };
        }

        /// <summary>
        /// Returns a new key door for the specified key door ID.
        /// </summary>
        /// <param name="dungeonData">
        /// The mutable dungeon data parent class.
        /// </param>
        /// <returns>
        /// A new key door for the specified key door ID.
        /// </returns>
        public IKeyDoor GetKeyDoor(IMutableDungeon dungeonData)
        {
            return _factory(dungeonData);
        }
    }
}
