using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Result;
using OpenTracker.Models.Dungeons.State;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.AccessibilityProviders
{
    public class ResultAggregatorTests
    {
        private readonly IDungeonResult.Factory _resultFactory =
            (bossAccessibility, accessible, sequenceBreak, visible) =>
                new DungeonResult(bossAccessibility, accessible, sequenceBreak, visible);

        private readonly IDungeon _dungeon = Substitute.For<IDungeon>();
        private readonly IMutableDungeonQueue _mutableDungeonQueue = Substitute.For<IMutableDungeonQueue>();

        private readonly IMutableDungeon _dungeonData = Substitute.For<IMutableDungeon>();

        private readonly List<DungeonItemID> _bosses = new();

        private readonly IResultAggregator _sut;

        public ResultAggregatorTests()
        {
            _dungeon.Bosses.Returns(_bosses);

            _mutableDungeonQueue.GetNext().Returns(_dungeonData);

            _sut = new ResultAggregator(_resultFactory, _dungeon, _mutableDungeonQueue);
        }

        [Fact]
        public void AggregateResults_ShouldExcludeStatesWithInvalidKeyLayouts()
        {
            var invalidState = Substitute.For<IDungeonState>();
            var validState = Substitute.For<IDungeonState>();

            _dungeon.Total.Returns(2);
            _dungeon.TotalWithMapAndCompass.Returns(2);

            _dungeonData.ValidateKeyLayout(invalidState).Returns(false);
            _dungeonData.ValidateKeyLayout(validState).Returns(true);
            _dungeonData.GetDungeonResult(invalidState).Returns(_resultFactory(
                new List<AccessibilityLevel>(), 2, false, false));
            _dungeonData.GetDungeonResult(validState).Returns(_resultFactory(
                new List<AccessibilityLevel>(), 1, false, false));

            var finalQueue = new BlockingCollection<IDungeonState> {validState, invalidState};
            finalQueue.CompleteAdding();

            var result = _sut.AggregateResults(finalQueue);
            
            Assert.Equal(1, result.Accessible);
        }
        
        [Fact]
        public void AggregateResults_ShouldReturnSequenceBreakAsTrue_WhenResultsAreVaried()
        {
            var state1 = Substitute.For<IDungeonState>();
            var state2 = Substitute.For<IDungeonState>();

            _dungeon.Total.Returns(2);
            _dungeon.TotalWithMapAndCompass.Returns(2);

            _dungeonData.ValidateKeyLayout(state1).Returns(true);
            _dungeonData.ValidateKeyLayout(state2).Returns(true);
            _dungeonData.GetDungeonResult(state1).Returns(_resultFactory(
                new List<AccessibilityLevel>(), 2, false, false));
            _dungeonData.GetDungeonResult(state2).Returns(_resultFactory(
                new List<AccessibilityLevel>(), 1, false, false));

            var finalQueue = new BlockingCollection<IDungeonState> {state2, state1};
            finalQueue.CompleteAdding();

            var result = _sut.AggregateResults(finalQueue);
            
            Assert.True(result.SequenceBreak);
        }

        [Fact]
        public void AggregateResults_ShouldReturnSequenceBreakAsTrue_WhenSequenceBreakIsMostAccessible()
        {
            var state1 = Substitute.For<IDungeonState>();
            var state2 = Substitute.For<IDungeonState>();

            _dungeon.Total.Returns(2);
            _dungeon.TotalWithMapAndCompass.Returns(2);

            _dungeonData.ValidateKeyLayout(state1).Returns(true);
            _dungeonData.ValidateKeyLayout(state2).Returns(true);
            _dungeonData.GetDungeonResult(state1).Returns(_resultFactory(
                new List<AccessibilityLevel>(), 2, true, false));
            _dungeonData.GetDungeonResult(state2).Returns(_resultFactory(
                new List<AccessibilityLevel>(), 1, false, false));

            var finalQueue = new BlockingCollection<IDungeonState> {state1, state2};
            finalQueue.CompleteAdding();

            var result = _sut.AggregateResults(finalQueue);
            
            Assert.True(result.SequenceBreak);
        }

        [Theory]
        [InlineData(AccessibilityLevel.None, AccessibilityLevel.None, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.None, AccessibilityLevel.SequenceBreak)]
        [InlineData(
            AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
        [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal, AccessibilityLevel.SequenceBreak)]
        [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.None, AccessibilityLevel.Normal)]
        [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak, AccessibilityLevel.Normal)]
        [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
        public void AggregateResults_ShouldReturnExpectedBossAccessibility(
            AccessibilityLevel expected, AccessibilityLevel state1Accessibility, AccessibilityLevel state2Accessibility)
        {
            var state1 = Substitute.For<IDungeonState>();
            var state2 = Substitute.For<IDungeonState>();

            _bosses.Add(DungeonItemID.ATBoss);
            _dungeon.Total.Returns(2);
            _dungeon.TotalWithMapAndCompass.Returns(2);

            _dungeonData.ValidateKeyLayout(state1).Returns(true);
            _dungeonData.ValidateKeyLayout(state2).Returns(true);
            _dungeonData.GetDungeonResult(state1).Returns(_resultFactory(
                new List<AccessibilityLevel> {state1Accessibility}, 2, false,
                false));
            _dungeonData.GetDungeonResult(state2).Returns(_resultFactory(
                new List<AccessibilityLevel> {state2Accessibility}, 1, false,
                false));

            var finalQueue = new BlockingCollection<IDungeonState> {state1, state2};
            finalQueue.CompleteAdding();

            var result = _sut.AggregateResults(finalQueue);
            
            Assert.Equal(expected, result.BossAccessibility[0]);
        }

        [Fact]
        public void AggregateResults_ShouldThrowException_WhenBossAccessibilityIsUnexpected()
        {
            var state = Substitute.For<IDungeonState>();

            _bosses.Add(DungeonItemID.ATBoss);
            _dungeon.Total.Returns(2);
            _dungeon.TotalWithMapAndCompass.Returns(2);

            _dungeonData.ValidateKeyLayout(state).Returns(true);
            _dungeonData.GetDungeonResult(state).Returns(_resultFactory(
                new List<AccessibilityLevel> {AccessibilityLevel.Inspect}, 2, false,
                false));

            var finalQueue = new BlockingCollection<IDungeonState> {state};
            finalQueue.CompleteAdding();

            Assert.Throws<Exception>(() => _ = _sut.AggregateResults(finalQueue));
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IResultAggregator.Factory>();
            var sut = factory(_dungeon, _mutableDungeonQueue);
            
            Assert.NotNull(sut as ResultAggregator);
        }
    }
}