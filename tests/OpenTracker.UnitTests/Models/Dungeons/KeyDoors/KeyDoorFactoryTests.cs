using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Nodes;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.KeyDoors;

public class KeyDoorFactoryTests
{
    private readonly IMutableDungeon _dungeonData = Substitute.For<IMutableDungeon>();
        
    private readonly KeyDoorFactory _sut;

    private INode? _factoryCall; 
        
    private static readonly Dictionary<KeyDoorID, DungeonNodeID> ExpectedValues = new();

    public KeyDoorFactoryTests()
    {
        IKeyDoor Factory(INode node)
        {
            _factoryCall = node;
            return Substitute.For<IKeyDoor>();
        }

        _sut = new KeyDoorFactory(Factory);
    }

    private static void PopulateExpectedValues()
    {
        ExpectedValues.Clear();

        foreach (KeyDoorID id in Enum.GetValues(typeof(KeyDoorID)))
        {
            var nodeID = id switch
            {
                KeyDoorID.HCEscapeFirstKeyDoor => DungeonNodeID.HCEscapeFirstKeyDoor,
                KeyDoorID.HCEscapeSecondKeyDoor => DungeonNodeID.HCEscapeSecondKeyDoor,
                KeyDoorID.HCDarkCrossKeyDoor => DungeonNodeID.HCDarkCrossKeyDoor,
                KeyDoorID.HCSewerRatRoomKeyDoor => DungeonNodeID.HCSewerRatRoomKeyDoor,
                KeyDoorID.HCZeldasCellDoor => DungeonNodeID.HCZeldasCellDoor,
                KeyDoorID.ATFirstKeyDoor => DungeonNodeID.ATDarkMaze,
                KeyDoorID.ATSecondKeyDoor => DungeonNodeID.ATSecondKeyDoor,
                KeyDoorID.ATThirdKeyDoor => DungeonNodeID.ATPastSecondKeyDoor,
                KeyDoorID.ATFourthKeyDoor => DungeonNodeID.ATFourthKeyDoor,
                KeyDoorID.EPRightKeyDoor => DungeonNodeID.EPRightKeyDoor,
                KeyDoorID.EPBackKeyDoor => DungeonNodeID.EPBackDarkRoom,
                KeyDoorID.EPBigChest => DungeonNodeID.EP,
                KeyDoorID.EPBigKeyDoor => DungeonNodeID.EPBigKeyDoor,
                KeyDoorID.DPRightKeyDoor => DungeonNodeID.DPRightKeyDoor,
                KeyDoorID.DP1FKeyDoor => DungeonNodeID.DPBack,
                KeyDoorID.DP2FFirstKeyDoor => DungeonNodeID.DP2FFirstKeyDoor,
                KeyDoorID.DP2FSecondKeyDoor => DungeonNodeID.DP2FSecondKeyDoor,
                KeyDoorID.DPBigChest => DungeonNodeID.DPFront,
                KeyDoorID.DPBigKeyDoor => DungeonNodeID.DPPastFourTorchWall,
                KeyDoorID.ToHKeyDoor => DungeonNodeID.ToH,
                KeyDoorID.ToHBigKeyDoor => DungeonNodeID.ToH,
                KeyDoorID.ToHBigChest => DungeonNodeID.ToHPastBigKeyDoor,
                KeyDoorID.PoDFrontKeyDoor => DungeonNodeID.PoDFrontKeyDoor,
                KeyDoorID.PoDBigKeyChestKeyDoor => DungeonNodeID.PoDLobbyArena,
                KeyDoorID.PoDCollapsingWalkwayKeyDoor => DungeonNodeID.PoDCollapsingWalkwayKeyDoor,
                KeyDoorID.PoDDarkMazeKeyDoor => DungeonNodeID.PoDDarkMazeKeyDoor,
                KeyDoorID.PoDHarmlessHellwayKeyDoor => DungeonNodeID.PoDHarmlessHellwayKeyDoor,
                KeyDoorID.PoDBossAreaKeyDoor => DungeonNodeID.PoDBossAreaKeyDoor,
                KeyDoorID.PoDBigChest => DungeonNodeID.PoDBigChestLedge,
                KeyDoorID.PoDBigKeyDoor => DungeonNodeID.PoDPastBossAreaKeyDoor,
                KeyDoorID.SP1FKeyDoor => DungeonNodeID.SPAfterRiver,
                KeyDoorID.SPB1FirstRightKeyDoor => DungeonNodeID.SPB1FirstRightKeyDoor,
                KeyDoorID.SPB1SecondRightKeyDoor => DungeonNodeID.SPB1SecondRightKeyDoor,
                KeyDoorID.SPB1LeftKeyDoor => DungeonNodeID.SPB1LeftKeyDoor,
                KeyDoorID.SPB1BackFirstKeyDoor => DungeonNodeID.SPB1BackFirstKeyDoor,
                KeyDoorID.SPBossRoomKeyDoor => DungeonNodeID.SPBossRoomKeyDoor,
                KeyDoorID.SPBigChest => DungeonNodeID.SPB1PastRightHammerBlocks,
                KeyDoorID.SWFrontLeftKeyDoor => DungeonNodeID.SWFrontLeftKeyDoor,
                KeyDoorID.SWFrontRightKeyDoor => DungeonNodeID.SWFrontRightKeyDoor,
                KeyDoorID.SWWorthlessKeyDoor => DungeonNodeID.SWWorthlessKeyDoor,
                KeyDoorID.SWBackFirstKeyDoor => DungeonNodeID.SWBackFirstKeyDoor,
                KeyDoorID.SWBackSecondKeyDoor => DungeonNodeID.SWBackSecondKeyDoor,
                KeyDoorID.SWBigChest => DungeonNodeID.SWBigChestAreaTop,
                KeyDoorID.TTFirstKeyDoor => DungeonNodeID.TTFirstKeyDoor,
                KeyDoorID.TTSecondKeyDoor => DungeonNodeID.TTPastFirstKeyDoor,
                KeyDoorID.TTBigChestKeyDoor => DungeonNodeID.TTBigChestKeyDoor,
                KeyDoorID.TTBigKeyDoor => DungeonNodeID.TTBigChestKeyDoor,
                KeyDoorID.TTBigChest => DungeonNodeID.TTPastHammerBlocks,
                KeyDoorID.IP1FKeyDoor => DungeonNodeID.IPPastEntranceFreezorRoom,
                KeyDoorID.IPB2KeyDoor => DungeonNodeID.IPB2KeyDoor,
                KeyDoorID.IPB3KeyDoor => DungeonNodeID.IPB3KeyDoor,
                KeyDoorID.IPB4KeyDoor => DungeonNodeID.IPB4KeyDoor,
                KeyDoorID.IPB5KeyDoor => DungeonNodeID.IPB5PastBigKeyDoor,
                KeyDoorID.IPB6KeyDoor => DungeonNodeID.IPB6KeyDoor,
                KeyDoorID.IPBigKeyDoor => DungeonNodeID.IPB5,
                KeyDoorID.IPBigChest => DungeonNodeID.IPBigChestArea,
                KeyDoorID.MMB1TopRightKeyDoor => DungeonNodeID.MMB1TopRightKeyDoor,
                KeyDoorID.MMB1TopLeftKeyDoor => DungeonNodeID.MMB1TopLeftKeyDoor,
                KeyDoorID.MMB1LeftSideFirstKeyDoor => DungeonNodeID.MMB1LeftSideFirstKeyDoor,
                KeyDoorID.MMB1LeftSideSecondKeyDoor => DungeonNodeID.MMB1LeftSideSecondKeyDoor,
                KeyDoorID.MMB1RightSideKeyDoor => DungeonNodeID.MMB1RightSideKeyDoor,
                KeyDoorID.MMB2WorthlessKeyDoor => DungeonNodeID.MMB2WorthlessKeyDoor,
                KeyDoorID.MMBigChest => DungeonNodeID.MMPastEntranceGap,
                KeyDoorID.MMPortalBigKeyDoor => DungeonNodeID.MMPastEntranceGap,
                KeyDoorID.MMBridgeBigKeyDoor => DungeonNodeID.MMBridgeBigKeyDoor,
                KeyDoorID.MMBossRoomBigKeyDoor => DungeonNodeID.MMB2PastCaneOfSomariaSwitch,
                KeyDoorID.TR1FFirstKeyDoor => DungeonNodeID.TRF1FirstKeyDoor,
                KeyDoorID.TR1FSecondKeyDoor => DungeonNodeID.TRF1SecondKeyDoor,
                KeyDoorID.TR1FThirdKeyDoor => DungeonNodeID.TRF1PastSecondKeyDoor,
                KeyDoorID.TRB1BigKeyChestKeyDoor => DungeonNodeID.TRB1BigKeyChestKeyDoor,
                KeyDoorID.TRB1ToB2KeyDoor => DungeonNodeID.TRB1RightSide,
                KeyDoorID.TRB2KeyDoor => DungeonNodeID.TRB2KeyDoor,
                KeyDoorID.TRBigChest => DungeonNodeID.TRB1BigChestArea,
                KeyDoorID.TRB1BigKeyDoor => DungeonNodeID.TRB1BigKeyDoor,
                KeyDoorID.TRBossRoomBigKeyDoor => DungeonNodeID.TRB3BossRoomEntry,
                KeyDoorID.GT1FLeftToRightKeyDoor => DungeonNodeID.GT1FLeftToRightKeyDoor,
                KeyDoorID.GT1FMapChestRoomKeyDoor => DungeonNodeID.GT1FMapChestRoomKeyDoor,
                KeyDoorID.GT1FSpikeTrapPortalRoomKeyDoor => DungeonNodeID.GT1FSpikeTrapPortalRoomKeyDoor,
                KeyDoorID.GT1FFiresnakeRoomKeyDoor => DungeonNodeID.GT1FFiresnakeRoomKeyDoor,
                KeyDoorID.GT1FTileRoomKeyDoor => DungeonNodeID.GT1FTileRoomKeyDoor,
                KeyDoorID.GT1FCollapsingWalkwayKeyDoor => DungeonNodeID.GT1FCollapsingWalkwayKeyDoor,
                KeyDoorID.GT6FFirstKeyDoor => DungeonNodeID.GT6FFirstKeyDoor,
                KeyDoorID.GT6FSecondKeyDoor => DungeonNodeID.GT6FSecondKeyDoor,
                KeyDoorID.GTBigChest => DungeonNodeID.GT1FBottomRoom,
                KeyDoorID.GT3FBigKeyDoor => DungeonNodeID.GT3FBigKeyDoor,
                KeyDoorID.GT7FBigKeyDoor => DungeonNodeID.GT6FPastBossRoomGap,
                _ => throw new ArgumentOutOfRangeException(nameof(id))
            };
                
            ExpectedValues.Add(id, nodeID);
        }
    }

    [Fact]
    public void GetKeyDoor_ShouldThrowException_WhenIDIsUnexpected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => _ = _sut.GetKeyDoor((KeyDoorID)int.MaxValue, _dungeonData));
    }
        
    [Theory]
    [MemberData(nameof(GetKeyDoor_ShouldCallFactoryWithExpectedNodeData))]
    public void GetKeyDoor_ShouldCallFactoryWithExpectedNode(DungeonNodeID nodeID, KeyDoorID id)
    {
        _ = _sut.GetKeyDoor(id, _dungeonData);
            
        Assert.Equal(_dungeonData.Nodes[nodeID], _factoryCall);
    }

    public static IEnumerable<object[]> GetKeyDoor_ShouldCallFactoryWithExpectedNodeData()
    {
        PopulateExpectedValues();
            
        return ExpectedValues.Keys.Select(id => new object[] {ExpectedValues[id], id}).ToList();
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IKeyDoorFactory.Factory>();
        var sut = factory();
            
        Assert.NotNull(sut as KeyDoorFactory);
    }
}