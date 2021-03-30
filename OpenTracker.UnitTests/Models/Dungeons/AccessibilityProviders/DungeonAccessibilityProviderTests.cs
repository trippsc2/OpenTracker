using System;
using System.Collections.Generic;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.KeyLayouts;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Utils;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.AccessibilityProviders
{
    public class DungeonAccessibilityProviderTests
    {
        private readonly ConstrainedTaskScheduler _taskScheduler = new(1);
        private readonly IMode _mode = Substitute.For<IMode>();
        private readonly IDungeon _dungeon = Substitute.For<IDungeon>();
        
        private readonly Func<List<KeyDoorID>, int, bool, bool, IDungeonState> _stateFactory =
            Substitute.For<Func<List<KeyDoorID>, int, bool, bool, IDungeonState>>();
        private readonly IBossAccessibilityProvider _bossAccessibilityProvider =
            Substitute.For<IBossAccessibilityProvider>();
        private readonly IMutableDungeonQueue _mutableDungeonQueue = Substitute.For<IMutableDungeonQueue>();

        private readonly List<DungeonItemID> _bosses = new();
        private readonly List<IRequirementNode> _entryNodes = new();
        private readonly List<DungeonNodeID> _nodes = new();
        private readonly List<KeyDoorID> _smallKeyDoors = new();
        private readonly List<KeyDoorID> _bigKeyDoors = new();
        private readonly List<DungeonItemID> _dungeonItems = new();
        private readonly List<DungeonItemID> _smallKeyDrops = new();
        private readonly List<DungeonItemID> _bigKeyDrops = new();
        private readonly List<IKeyLayout> _keyLayouts = new();
        
        private readonly IMutableDungeon _mutableDungeon = Substitute.For<IMutableDungeon>();
        private readonly IDungeonState _state = Substitute.For<IDungeonState>();

        public DungeonAccessibilityProviderTests()
        {
            _stateFactory(Arg.Any<List<KeyDoorID>>(), Arg.Any<int>(), Arg.Any<bool>(), Arg.Any<bool>()).Returns(_state);
            
            _dungeon.Bosses.Returns(_bosses);
            _dungeon.EntryNodes.Returns(_entryNodes);
            _dungeon.Nodes.Returns(_nodes);
            _dungeon.SmallKeyDoors.Returns(_smallKeyDoors);
            _dungeon.BigKeyDoors.Returns(_bigKeyDoors);
            _dungeon.DungeonItems.Returns(_dungeonItems);
            _dungeon.SmallKeyDrops.Returns(_smallKeyDrops);
            _dungeon.BigKeyDrops.Returns(_bigKeyDrops);
            _dungeon.KeyLayouts.Returns(_keyLayouts);
            
            _mutableDungeonQueue.GetNext().Returns(_mutableDungeon);
            _mutableDungeon.GetAvailableSmallKeys(Arg.Any<bool>()).Returns(0);
            _mutableDungeon.GetAccessibleKeyDoors(Arg.Any<bool>()).Returns(new List<KeyDoorID>());

            _state.UnlockedDoors.Returns(new List<KeyDoorID>());
        }
        
        [Fact]
        public void Ctor_Test()
        {
            var sut = new DungeonAccessibilityProvider(
                _taskScheduler, _mode,
                (unlockedDoors, keysCollected, bigKeyCollected, sequenceBreak) =>
                    _stateFactory(unlockedDoors, keysCollected, bigKeyCollected, sequenceBreak),
                () => _bossAccessibilityProvider, _ => _mutableDungeonQueue,
                _dungeon);
            
            Assert.NotNull(sut);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IDungeonAccessibilityProvider.Factory>();
            var sut = factory(_dungeon);
            
            Assert.NotNull(sut as DungeonAccessibilityProvider);
        }
    }
}