using System.Collections.Concurrent;
using System.Collections.Generic;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Dungeons.Result;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.AccessibilityProviders
{
    public class DungeonAccessibilityProviderTests
    {
        private readonly IDungeon _dungeon = Substitute.For<IDungeon>();
        private readonly IMode _mode = Substitute.For<IMode>();
        private readonly IMutableDungeonQueue _mutableDungeonQueue = Substitute.For<IMutableDungeonQueue>();

        private readonly IBossAccessibilityProvider.Factory _bossProviderFactory = () =>
            Substitute.For<IBossAccessibilityProvider>();

        private readonly IKeyDoorIterator _keyDoorIterator = Substitute.For<IKeyDoorIterator>();
        private readonly IResultAggregator _resultAggregator = Substitute.For<IResultAggregator>();

        private readonly List<DungeonItemID> _bosses = new();
        private readonly List<DungeonNodeID> _nodes = new();
        private readonly List<IRequirementNode> _entryNodes = new()
        {
            Substitute.For<IRequirementNode>()
        };

        private readonly List<AccessibilityLevel> _bossAccessibility = new();
        private readonly IDungeonResult _result = Substitute.For<IDungeonResult>();

        public DungeonAccessibilityProviderTests()
        {
            _dungeon.Bosses.Returns(_bosses);
            _dungeon.Nodes.Returns(_nodes);
            _dungeon.EntryNodes.Returns(_entryNodes);

            _result.BossAccessibility.Returns(_bossAccessibility);

            _resultAggregator.AggregateResults(Arg.Any<BlockingCollection<IDungeonState>>()).Returns(_result);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 4)]
        public void Ctor_ShouldCreateBossProvidersEqualToBossCount(int expected, int bossCount)
        {
            for (var i = 0; i < bossCount; i++)
            {
                _bosses.Add(DungeonItemID.ATBoss);
                _bossAccessibility.Add(AccessibilityLevel.None);
            }
            
            var sut = new DungeonAccessibilityProvider(_mode, _bossProviderFactory,
                _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
                (_, _) => _resultAggregator);
            
            Assert.Equal(expected, sut.BossAccessibilityProviders.Count);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 4)]
        public void Accessible_ShouldEqualResultValue(int expected, int accessible)
        {
            _result.Accessible.Returns(accessible);
            var sut = new DungeonAccessibilityProvider(_mode, _bossProviderFactory,
                _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
                (_, _) => _resultAggregator);
            
            Assert.Equal(expected, sut.Accessible);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void SequenceBreak_ShouldEqualResultValue(bool expected, bool sequenceBreak)
        {
            _result.SequenceBreak.Returns(sequenceBreak);
            var sut = new DungeonAccessibilityProvider(_mode, _bossProviderFactory,
                _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
                (_, _) => _resultAggregator);
            
            Assert.Equal(expected, sut.SequenceBreak);
        }
    }
}