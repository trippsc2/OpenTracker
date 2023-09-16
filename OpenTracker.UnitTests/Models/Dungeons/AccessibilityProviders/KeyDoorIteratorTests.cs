using System.Collections.Concurrent;
using System.Collections.Generic;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.Items.Keys;
using OpenTracker.Utils;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.AccessibilityProviders;

public class KeyDoorIteratorTests
{
    private readonly IDungeonState.Factory _stateFactory =
        (unlockedDoors, keysCollected, bigKeyCollected, sequenceBreak) => new DungeonState(
            unlockedDoors, keysCollected, bigKeyCollected, sequenceBreak);

    private readonly IDungeon _dungeon = Substitute.For<IDungeon>();
    private readonly IMutableDungeonQueue _mutableDungeonQueue = Substitute.For<IMutableDungeonQueue>();

    private readonly ISmallKeyItem _smallKey = Substitute.For<ISmallKeyItem>();
    private readonly IBigKeyItem _bigKey = Substitute.For<IBigKeyItem>();
    private readonly List<KeyDoorID> _smallKeyDoors = new();

    private readonly IMutableDungeon _dungeonData = Substitute.For<IMutableDungeon>();

    private readonly KeyDoorIterator _sut;

    public KeyDoorIteratorTests()
    {
        _dungeon.SmallKey.Returns(_smallKey);
        _dungeon.BigKey.Returns(_bigKey);
        _dungeon.SmallKeyDoors.Returns(_smallKeyDoors);

        _smallKey.Current.Returns(0);
        _smallKey.Maximum.Returns(0);
        _smallKey.EffectiveCurrent.Returns(0);
        _smallKey.GetKeyValues().Returns(new List<int> {0});

        _bigKey.Current.Returns(0);
        _bigKey.Maximum.Returns(0);
        _bigKey.GetKeyValues().Returns(new List<bool> {false});

        _mutableDungeonQueue.GetNext().Returns(_dungeonData);
            
        _sut = new KeyDoorIterator(
            new ConstrainedTaskScheduler(1), _stateFactory, _dungeon, _mutableDungeonQueue);
    }

    [Fact]
    public void ProcessKeyDoorPermutations_ShouldCompleteAdding()
    {
        var finalQueue = new BlockingCollection<IDungeonState>();
        _sut.ProcessKeyDoorPermutations(finalQueue);
            
        Assert.True(finalQueue.IsAddingCompleted);
    }

    [Fact]
    public void ProcessKeyDoorPermutations_ShouldReturn2Permutations_WhenNoKeyDoors()
    {
        var finalQueue = new BlockingCollection<IDungeonState>();
        _sut.ProcessKeyDoorPermutations(finalQueue);
            
        Assert.Equal(2, finalQueue.Count);
    }

    [Fact]
    public void ProcessKeyDoorPermutations_ShouldReturn2Permutations_WhenNoAccessibleKeyDoors()
    {
        _smallKeyDoors.Add(KeyDoorID.ATFirstKeyDoor);
        _dungeonData.GetAccessibleKeyDoors(Arg.Any<bool>()).Returns(new List<KeyDoorID>());
        var finalQueue = new BlockingCollection<IDungeonState>();
        _sut.ProcessKeyDoorPermutations(finalQueue);
            
        Assert.Equal(2, finalQueue.Count);
    }

    [Fact]
    public void ProcessKeyDoorPermutations_ShouldReturn2Permutations_When1AccessibleKeyDoorAnd1AvailableKey()
    {
        _smallKeyDoors.Add(KeyDoorID.ATFirstKeyDoor);
        _dungeonData.GetAvailableSmallKeys(Arg.Any<bool>()).Returns(1);

        var processKeyDoorPermutationsCalled = 0;

        List<KeyDoorID> GetAccessibleKeyDoors()
        {
            var accessibleKeyDoors = new List<KeyDoorID>();

            if (processKeyDoorPermutationsCalled < 2)
            {
                accessibleKeyDoors.Add(KeyDoorID.ATFirstKeyDoor);
            }
                
            processKeyDoorPermutationsCalled++;
            return accessibleKeyDoors;
        }

        _dungeonData.GetAccessibleKeyDoors(Arg.Any<bool>()).Returns(
            _ => GetAccessibleKeyDoors());
        var finalQueue = new BlockingCollection<IDungeonState>();
        _sut.ProcessKeyDoorPermutations(finalQueue);
            
        Assert.Equal(2, finalQueue.Count);
    }
        
    [Fact]
    public void ProcessKeyDoorPermutations_ShouldReturn4Permutations_When1AccessibleKeyDoorsAnd2AvailableKeys()
    {
        _smallKeyDoors.Add(KeyDoorID.ATFirstKeyDoor);
        _smallKeyDoors.Add(KeyDoorID.ATSecondKeyDoor);
        _dungeonData.GetAvailableSmallKeys(Arg.Any<bool>()).Returns(2);

        var processKeyDoorPermutationsCalled = 0;

        List<KeyDoorID> GetAccessibleKeyDoors()
        {
            var accessibleKeyDoors = new List<KeyDoorID>();

            if (processKeyDoorPermutationsCalled < 2)
            {
                accessibleKeyDoors.Add(KeyDoorID.ATFirstKeyDoor);
            }
                
            processKeyDoorPermutationsCalled++;
            return accessibleKeyDoors;
        }

        _dungeonData.GetAccessibleKeyDoors(Arg.Any<bool>()).Returns(
            _ => GetAccessibleKeyDoors());
        var finalQueue = new BlockingCollection<IDungeonState>();
        _sut.ProcessKeyDoorPermutations(finalQueue);
            
        Assert.Equal(2, finalQueue.Count);
    }

    [Fact]
    public void ProcessKeyDoorPermutations_ShouldReturn4Permutations_When2AccessibleKeyDoorsAnd2AvailableKeys()
    {
        _smallKeyDoors.Add(KeyDoorID.ATFirstKeyDoor);
        _smallKeyDoors.Add(KeyDoorID.ATSecondKeyDoor);
        _dungeonData.GetAvailableSmallKeys(Arg.Any<bool>()).Returns(2);

        var processKeyDoorPermutationsCalled = 0;

        List<KeyDoorID> GetAccessibleKeyDoors()
        {
            var accessibleKeyDoors = new List<KeyDoorID>();

            switch (processKeyDoorPermutationsCalled)
            {
                case 0:
                case 1:
                    accessibleKeyDoors.Add(KeyDoorID.ATFirstKeyDoor);
                    accessibleKeyDoors.Add(KeyDoorID.ATSecondKeyDoor);
                    break;
                case 2:
                case 4:
                    accessibleKeyDoors.Add(KeyDoorID.ATSecondKeyDoor);
                    break;
                case 3:
                case 5:
                    accessibleKeyDoors.Add(KeyDoorID.ATFirstKeyDoor);
                    break;
            }
                
            processKeyDoorPermutationsCalled++;
            return accessibleKeyDoors;
        }

        _dungeonData.GetAccessibleKeyDoors(Arg.Any<bool>()).Returns(
            _ => GetAccessibleKeyDoors());
        var finalQueue = new BlockingCollection<IDungeonState>();
        _sut.ProcessKeyDoorPermutations(finalQueue);
            
        Assert.Equal(4, finalQueue.Count);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IKeyDoorIterator.Factory>();
        var sut = factory(_dungeon, _mutableDungeonQueue);
            
        Assert.NotNull(sut as KeyDoorIterator);
    }
}