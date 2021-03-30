using System;
using System.Collections.Generic;
using NSubstitute;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.KeyDoors;
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

        public DungeonAccessibilityProviderTests()
        {
            _dungeon.Bosses.Returns(_bosses);
            _dungeon.EntryNodes.Returns(_entryNodes);
            _dungeon.Nodes.Returns(_nodes);
            _dungeon.SmallKeyDoors.Returns(_smallKeyDoors);
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
    }
}