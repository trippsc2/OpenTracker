using System.Collections.Generic;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Dungeons.Result;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.Items.Keys;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.KeyLayouts;
using OpenTracker.Models.Modes;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Mutable
{
    public class MutableDungeonTests
    {
        private readonly IMode _mode = Substitute.For<IMode>();

        private readonly IKeyDoorFactory _keyDoorFactory = Substitute.For<IKeyDoorFactory>();
        private readonly IDungeonItemFactory _dungeonItemFactory = Substitute.For<IDungeonItemFactory>();
        
        private readonly IKeyDoorDictionary.Factory _keyDoors;
        private readonly IDungeonNodeDictionary.Factory _nodes = 
            dungeonData => new DungeonNodeDictionary(_ => Substitute.For<IDungeonNode>(), dungeonData);
        private readonly IDungeonItemDictionary.Factory _dungeonItems;
        private readonly IDungeonResult.Factory _resultFactory =
            (bossAccessibility, accessible, sequenceBreak, visible, minimumInaccessible) => new DungeonResult(
                bossAccessibility, accessible, sequenceBreak, visible, minimumInaccessible);

        private readonly IDungeon _dungeon = Substitute.For<IDungeon>();

        public MutableDungeonTests()
        {
            _keyDoorFactory.GetKeyDoor(Arg.Any<IMutableDungeon>()).Returns(
                _ => Substitute.For<IKeyDoor>());
            _keyDoors = dungeonData => new KeyDoorDictionary(() => _keyDoorFactory, dungeonData);

            _dungeonItemFactory.GetDungeonItem(Arg.Any<IMutableDungeon>(), Arg.Any<DungeonItemID>()).Returns(
                _ => Substitute.For<IDungeonItem>());
            _dungeonItems = dungeonData => new DungeonItemDictionary(() => _dungeonItemFactory, dungeonData);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void Ctor_ShouldCreateNodes(int expected, int nodesCount)
        {
            var nodes = new List<DungeonNodeID>();
            _dungeon.Nodes.Returns(nodes);
            
            for (var i = 0; i < nodesCount; i++)
            {
                var id = (DungeonNodeID)i;
                nodes.Add(id);
            }

            var sut = new MutableDungeon(_mode, _keyDoors, _nodes, _dungeonItems, _resultFactory, _dungeon);
            sut.InitializeData();

            Assert.Equal(expected, sut.Nodes.Count);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void Ctor_ShouldCreateSmallKeyDoors(int expected, int smallKeyDoorCount)
        {
            var smallKeyDoors = new List<KeyDoorID>();
            _dungeon.SmallKeyDoors.Returns(smallKeyDoors);
            
            for (var i = 0; i < smallKeyDoorCount; i++)
            {
                var id = (KeyDoorID)i;
                smallKeyDoors.Add(id);
            }

            var sut = new MutableDungeon(_mode, _keyDoors, _nodes, _dungeonItems, _resultFactory, _dungeon);
            sut.InitializeData();

            Assert.Equal(expected, sut.KeyDoors.Count);
        }
        
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void Ctor_ShouldCreateBigKeyDoors(int expected, int bigKeyDoorCount)
        {
            var bigKeyDoors = new List<KeyDoorID>();
            _dungeon.BigKeyDoors.Returns(bigKeyDoors);
            
            for (var i = 0; i < bigKeyDoorCount; i++)
            {
                var id = (KeyDoorID)i;
                bigKeyDoors.Add(id);
            }

            var sut = new MutableDungeon(_mode, _keyDoors, _nodes, _dungeonItems, _resultFactory, _dungeon);
            sut.InitializeData();

            Assert.Equal(expected, sut.KeyDoors.Count);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void Ctor_ShouldCreateDungeonItems(int expected, int dungeonItemsCount)
        {
            var dungeonItems = new List<DungeonItemID>();
            _dungeon.DungeonItems.Returns(dungeonItems);
            
            for (var i = 0; i < dungeonItemsCount; i++)
            {
                var id = (DungeonItemID)i;
                dungeonItems.Add(id);
            }

            var sut = new MutableDungeon(_mode, _keyDoors, _nodes, _dungeonItems, _resultFactory, _dungeon);
            sut.InitializeData();

            Assert.Equal(expected, sut.DungeonItems.Count);
        }
 
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void Ctor_ShouldCreateBosses(int expected, int bossesCount)
        {
            var bosses = new List<DungeonItemID>();
            _dungeon.Bosses.Returns(bosses);
            
            for (var i = 0; i < bossesCount; i++)
            {
                var id = (DungeonItemID)i;
                bosses.Add(id);
            }

            var sut = new MutableDungeon(_mode, _keyDoors, _nodes, _dungeonItems, _resultFactory, _dungeon);
            sut.InitializeData();

            Assert.Equal(expected, sut.DungeonItems.Count);
        }
 
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void Ctor_ShouldCreateSmallKeyDrops(int expected, int smallKeyDropCount)
        {
            var smallKeyDrops = new List<DungeonItemID>();
            _dungeon.SmallKeyDrops.Returns(smallKeyDrops);
            
            for (var i = 0; i < smallKeyDropCount; i++)
            {
                var id = (DungeonItemID)i;
                smallKeyDrops.Add(id);
            }

            var sut = new MutableDungeon(_mode, _keyDoors, _nodes, _dungeonItems, _resultFactory, _dungeon);
            sut.InitializeData();

            Assert.Equal(expected, sut.DungeonItems.Count);
        }
 
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void Ctor_ShouldCreateBigKeyDrops(int expected, int bigKeyDropCount)
        {
            var bigKeyDrops = new List<DungeonItemID>();
            _dungeon.BigKeyDrops.Returns(bigKeyDrops);
            
            for (var i = 0; i < bigKeyDropCount; i++)
            {
                var id = (DungeonItemID)i;
                bigKeyDrops.Add(id);
            }

            var sut = new MutableDungeon(_mode, _keyDoors, _nodes, _dungeonItems, _resultFactory, _dungeon);
            sut.InitializeData();

            Assert.Equal(expected, sut.DungeonItems.Count);
        }

        [Fact]
        public void ApplyState_ShouldSetKeyDoorsToUnlocked()
        {
            var smallKeyDoors = new List<KeyDoorID>
            {
                KeyDoorID.HCEscapeFirstKeyDoor,
                KeyDoorID.HCEscapeSecondKeyDoor
            };
            
            _dungeon.SmallKeyDoors.Returns(smallKeyDoors);

            var sut = new MutableDungeon(_mode, _keyDoors, _nodes, _dungeonItems, _resultFactory, _dungeon);
            sut.InitializeData();

            var unlockedDoors = new List<KeyDoorID>
            {
                KeyDoorID.HCEscapeFirstKeyDoor
            };

            var state = new DungeonState(unlockedDoors, 0, false, false);
            sut.ApplyState(state);
            
            Assert.True(sut.KeyDoors[KeyDoorID.HCEscapeFirstKeyDoor].Unlocked);
        }

        [Theory]
        [InlineData(false, false, false, false, AccessibilityLevel.None)]
        [InlineData(false, false, false, false, AccessibilityLevel.SequenceBreak)]
        [InlineData(true, false, false, false, AccessibilityLevel.Normal)]
        [InlineData(false, false, false, true, AccessibilityLevel.None)]
        [InlineData(false, false, false, true, AccessibilityLevel.SequenceBreak)]
        [InlineData(false, false, false, true, AccessibilityLevel.Normal)]
        [InlineData(false, false, true, false, AccessibilityLevel.None)]
        [InlineData(true, false, true, false, AccessibilityLevel.SequenceBreak)]
        [InlineData(true, false, true, false, AccessibilityLevel.Normal)]
        [InlineData(false, false, true, true, AccessibilityLevel.None)]
        [InlineData(false, false, true, true, AccessibilityLevel.SequenceBreak)]
        [InlineData(false, false, true, true, AccessibilityLevel.Normal)]
        [InlineData(true, true, false, false, AccessibilityLevel.None)]
        [InlineData(true, true, false, false, AccessibilityLevel.SequenceBreak)]
        [InlineData(true, true, false, false, AccessibilityLevel.Normal)]
        [InlineData(true, true, false, true, AccessibilityLevel.None)]
        [InlineData(true, true, false, true, AccessibilityLevel.SequenceBreak)]
        [InlineData(true, true, false, true, AccessibilityLevel.Normal)]
        [InlineData(true, true, true, false, AccessibilityLevel.None)]
        [InlineData(true, true, true, false, AccessibilityLevel.SequenceBreak)]
        [InlineData(true, true, true, false, AccessibilityLevel.Normal)]
        [InlineData(true, true, true, true, AccessibilityLevel.None)]
        [InlineData(true, true, true, true, AccessibilityLevel.SequenceBreak)]
        [InlineData(true, true, true, true, AccessibilityLevel.Normal)]
        public void ApplyState_ShouldSetBigKeyDoorsToExpected(
            bool expected, bool bigKeyCollected, bool sequenceBreak, bool keyDropShuffle,
            AccessibilityLevel dropAccessibility)
        {
            var bigKeyDoors = new List<KeyDoorID>
            {
                KeyDoorID.HCEscapeFirstKeyDoor
            };
            
            var bigKeyDrops = new List<DungeonItemID>
            {
                DungeonItemID.HCSanctuary
            };

            _dungeon.BigKeyDoors.Returns(bigKeyDoors);
            _dungeon.BigKeyDrops.Returns(bigKeyDrops);
            _mode.KeyDropShuffle.Returns(keyDropShuffle);

            var sut = new MutableDungeon(_mode, _keyDoors, _nodes, _dungeonItems, _resultFactory, _dungeon);
            sut.InitializeData();

            sut.DungeonItems[DungeonItemID.HCSanctuary].Accessibility.Returns(dropAccessibility);

            var state = new DungeonState(
                new List<KeyDoorID>(), 0, bigKeyCollected, sequenceBreak);
            sut.ApplyState(state);
            
            Assert.Equal(expected, sut.KeyDoors[KeyDoorID.HCEscapeFirstKeyDoor].Unlocked);
        }

        [Theory]
        [InlineData(0, false, AccessibilityLevel.None, false)]
        [InlineData(0, false, AccessibilityLevel.None, true)]
        [InlineData(0, false, AccessibilityLevel.SequenceBreak, false)]
        [InlineData(1, false, AccessibilityLevel.SequenceBreak, true)]
        [InlineData(1, false, AccessibilityLevel.Normal, false)]
        [InlineData(1, false, AccessibilityLevel.Normal, true)]
        [InlineData(0, true, AccessibilityLevel.None, false)]
        [InlineData(0, true, AccessibilityLevel.None, true)]
        [InlineData(0, true, AccessibilityLevel.SequenceBreak, false)]
        [InlineData(0, true, AccessibilityLevel.SequenceBreak, true)]
        [InlineData(0, true, AccessibilityLevel.Normal, false)]
        [InlineData(0, true, AccessibilityLevel.Normal, true)]
        public void GetAvailableSmallKeys_ShouldReturnExpected(
            int expected, bool keyDropShuffle, AccessibilityLevel dropAccessibility, bool sequenceBreak)
        {
            var smallKeyDrops = new List<DungeonItemID>
            {
                DungeonItemID.HCSanctuary
            };

            _mode.KeyDropShuffle.Returns(keyDropShuffle);
            _dungeon.SmallKeyDrops.Returns(smallKeyDrops);

            var sut = new MutableDungeon(_mode, _keyDoors, _nodes, _dungeonItems, _resultFactory, _dungeon);
            sut.InitializeData();

            sut.DungeonItems[DungeonItemID.HCSanctuary].Accessibility.Returns(dropAccessibility);
            
            Assert.Equal(expected, sut.GetAvailableSmallKeys(sequenceBreak));
        }

        [Theory]
        [InlineData(0, false, AccessibilityLevel.None, false)]
        [InlineData(0, false, AccessibilityLevel.None, true)]
        [InlineData(0, false, AccessibilityLevel.SequenceBreak, false)]
        [InlineData(1, false, AccessibilityLevel.SequenceBreak, true)]
        [InlineData(1, false, AccessibilityLevel.Normal, false)]
        [InlineData(1, false, AccessibilityLevel.Normal, true)]
        [InlineData(0, true, AccessibilityLevel.None, false)]
        [InlineData(0, true, AccessibilityLevel.None, true)]
        [InlineData(0, true, AccessibilityLevel.SequenceBreak, false)]
        [InlineData(0, true, AccessibilityLevel.SequenceBreak, true)]
        [InlineData(0, true, AccessibilityLevel.Normal, false)]
        [InlineData(0, true, AccessibilityLevel.Normal, true)]
        public void GetAccessibleKeyDoors_ShouldReturnExpected(
            int expected, bool unlocked, AccessibilityLevel doorAccessibility, bool sequenceBreak)
        {
            const KeyDoorID id = KeyDoorID.HCEscapeFirstKeyDoor;
            var smallKeyDoors = new List<KeyDoorID> {id};

            _dungeon.SmallKeyDoors.Returns(smallKeyDoors);
            
            var sut = new MutableDungeon(_mode, _keyDoors, _nodes, _dungeonItems, _resultFactory, _dungeon);
            sut.InitializeData();

            sut.KeyDoors[id].Unlocked.Returns(unlocked);
            sut.KeyDoors[id].Accessibility.Returns(doorAccessibility);

            var accessibleDoors = sut.GetAccessibleKeyDoors(sequenceBreak);
            
            Assert.Equal(expected, accessibleDoors.Count);
        }

        [Theory]
        [InlineData(false, false, false)]
        [InlineData(true, false, true)]
        [InlineData(true, true, false)]
        [InlineData(true, true, true)]
        public void ValidateKeyLayout_ShouldReturnExpected(
            bool expected, bool firstKeyLayoutCanBeTrue, bool secondKeyLayoutCanBeTrue)
        {
            var keyLayouts = new List<IKeyLayout>
            {
                Substitute.For<IKeyLayout>(),
                Substitute.For<IKeyLayout>()
            };

            keyLayouts[0].CanBeTrue(Arg.Any<IMutableDungeon>(), Arg.Any<IDungeonState>())
                .Returns(firstKeyLayoutCanBeTrue);
            keyLayouts[1].CanBeTrue(Arg.Any<IMutableDungeon>(), Arg.Any<IDungeonState>())
                .Returns(secondKeyLayoutCanBeTrue);

            _dungeon.KeyLayouts.Returns(keyLayouts);
            
            var sut = new MutableDungeon(_mode, _keyDoors, _nodes, _dungeonItems, _resultFactory, _dungeon);
            sut.InitializeData();

            Assert.Equal(expected, sut.ValidateKeyLayout(Substitute.For<IDungeonState>()));
        }

        [Fact]
        public void GetDungeonResult_ShouldNotCheckKeyDrops_WhenKeyDropShuffleIsFalse()
        {
            const DungeonItemID id = DungeonItemID.HCSanctuary;
            var smallKeyDrops = new List<DungeonItemID> {id};
            _dungeon.SmallKeyDrops.Returns(smallKeyDrops);

            var sut = new MutableDungeon(_mode, _keyDoors, _nodes, _dungeonItems, _resultFactory, _dungeon);
            sut.InitializeData();

            _ = sut.GetDungeonResult(Substitute.For<IDungeonState>());

            _ = sut.DungeonItems[id].DidNotReceive().Accessibility;
        }

        [Fact]
        public void GetDungeonResult_ShouldCheckSmallKeyDrops_WhenKeyDropShuffleIsTrue()
        {
            const DungeonItemID id = DungeonItemID.HCSanctuary;
            var smallKeyDrops = new List<DungeonItemID> {id};
            _dungeon.SmallKeyDrops.Returns(smallKeyDrops);
            _mode.KeyDropShuffle.Returns(true);

            var sut = new MutableDungeon(_mode, _keyDoors, _nodes, _dungeonItems, _resultFactory, _dungeon);
            sut.InitializeData();

            _ = sut.GetDungeonResult(Substitute.For<IDungeonState>());

            _ = sut.DungeonItems[id].Received().Accessibility;
        }

        [Fact]
        public void GetDungeonResult_ShouldCheckBigKeyDrops_WhenKeyDropShuffleIsTrue()
        {
            const DungeonItemID id = DungeonItemID.HCSanctuary;
            var bigKeyDrops = new List<DungeonItemID> {id};
            _dungeon.BigKeyDrops.Returns(bigKeyDrops);
            _mode.KeyDropShuffle.Returns(true);

            var sut = new MutableDungeon(_mode, _keyDoors, _nodes, _dungeonItems, _resultFactory, _dungeon);
            sut.InitializeData();

            _ = sut.GetDungeonResult(Substitute.For<IDungeonState>());

            _ = sut.DungeonItems[id].Received().Accessibility;
        }

        [Fact]
        public void GetDungeonResult_ShouldAccountForBigKeyRemaining()
        {
            var dungeonItems = new List<DungeonItemID>
            {
                DungeonItemID.HCSanctuary,
                DungeonItemID.HCMapChest
            };
            
            _dungeon.DungeonItems.Returns(dungeonItems);
            _mode.BigKeyShuffle.Returns(false);

            var bigKey = Substitute.For<IBigKeyItem>();
            _dungeon.BigKey.Returns(bigKey);
            _dungeon.BigKey!.Maximum.Returns(1);
            _dungeon.Total.Returns(1);
            _dungeon.TotalWithMapAndCompass.Returns(1);

            var sut = new MutableDungeon(_mode, _keyDoors, _nodes, _dungeonItems, _resultFactory, _dungeon);
            sut.InitializeData();

            sut.DungeonItems[DungeonItemID.HCSanctuary].Accessibility.Returns(AccessibilityLevel.Normal);
            var state = Substitute.For<IDungeonState>();
            state.BigKeyCollected.Returns(false);
            
            var result = sut.GetDungeonResult(state);

            Assert.Equal(1, result.Accessible);
        }

        [Fact]
        public void GetDungeonResult_ShouldAccountForSmallKeysRemaining()
        {
            var dungeonItems = new List<DungeonItemID>
            {
                DungeonItemID.HCSanctuary,
                DungeonItemID.HCMapChest
            };
            
            _dungeon.DungeonItems.Returns(dungeonItems);
            _mode.SmallKeyShuffle.Returns(false);

            var smallKey = Substitute.For<ISmallKeyItem>();
            _dungeon.SmallKey.Returns(smallKey);
            _dungeon.SmallKey.Maximum.Returns(1);
            _dungeon.Total.Returns(1);
            _dungeon.TotalWithMapAndCompass.Returns(1);

            var sut = new MutableDungeon(_mode, _keyDoors, _nodes, _dungeonItems, _resultFactory, _dungeon);
            sut.InitializeData();

            sut.DungeonItems[DungeonItemID.HCSanctuary].Accessibility.Returns(AccessibilityLevel.Normal);
            var state = Substitute.For<IDungeonState>();
            state.BigKeyCollected.Returns(false);
            
            var result = sut.GetDungeonResult(state);

            Assert.Equal(1, result.Accessible);
        }
        
        [Theory]
        [InlineData(AccessibilityLevel.None, AccessibilityLevel.None)]
        [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
        [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
        public void GetDungeonResult_ShouldReturnExpectedBossAccessibility(
            AccessibilityLevel expected, AccessibilityLevel boss)
        {
            const DungeonItemID id = DungeonItemID.HCSanctuary;
            var bosses = new List<DungeonItemID> {id};
            _dungeon.Bosses.Returns(bosses);
            _dungeon.DungeonItems.Returns(bosses);

            var sut = new MutableDungeon(_mode, _keyDoors, _nodes, _dungeonItems, _resultFactory, _dungeon);
            sut.InitializeData();
            sut.DungeonItems[id].Accessibility.Returns(boss);
            var state = Substitute.For<IDungeonState>();

            var result = sut.GetDungeonResult(state);

            Assert.Equal(expected, result.BossAccessibility[0]);
        }

        [Theory]
        [InlineData(0, AccessibilityLevel.None, false)]
        [InlineData(0, AccessibilityLevel.None, true)]
        [InlineData(0, AccessibilityLevel.Inspect, false)]
        [InlineData(0, AccessibilityLevel.Inspect, true)]
        [InlineData(0, AccessibilityLevel.SequenceBreak, false)]
        [InlineData(1, AccessibilityLevel.SequenceBreak, true)]
        [InlineData(1, AccessibilityLevel.Normal, false)]
        [InlineData(1, AccessibilityLevel.Normal, true)]
        public void GetDungeonResult_ShouldReturnExpectedAccessible(
            int expected, AccessibilityLevel accessibility, bool sequenceBreak)
        {
            const DungeonItemID id = DungeonItemID.HCSanctuary;
            var items = new List<DungeonItemID> {id};
            _dungeon.DungeonItems.Returns(items);
            _dungeon.Total.Returns(1);
            _dungeon.TotalWithMapAndCompass.Returns(1);

            var sut = new MutableDungeon(_mode, _keyDoors, _nodes, _dungeonItems, _resultFactory, _dungeon);
            sut.InitializeData();
            sut.DungeonItems[id].Accessibility.Returns(accessibility);
            var state = Substitute.For<IDungeonState>();
            state.SequenceBreak.Returns(sequenceBreak);

            var result = sut.GetDungeonResult(state);

            Assert.Equal(expected, result.Accessible);
        }

        [Theory]
        [InlineData(0, false, AccessibilityLevel.None, false)]
        [InlineData(0, false, AccessibilityLevel.None, true)]
        [InlineData(0, false, AccessibilityLevel.SequenceBreak, false)]
        [InlineData(0, false, AccessibilityLevel.SequenceBreak, true)]
        [InlineData(0, false, AccessibilityLevel.Normal, false)]
        [InlineData(0, false, AccessibilityLevel.Normal, true)]
        [InlineData(1, true, AccessibilityLevel.None, false)]
        [InlineData(1, true, AccessibilityLevel.None, true)]
        [InlineData(1, true, AccessibilityLevel.SequenceBreak, false)]
        [InlineData(0, true, AccessibilityLevel.SequenceBreak, true)]
        [InlineData(0, true, AccessibilityLevel.Normal, false)]
        [InlineData(0, true, AccessibilityLevel.Normal, true)]
        public void GetDungeonResult_ShouldReturnExpectedMinimumInaccessible(
            int expected, bool guaranteedBossItems, AccessibilityLevel accessibility, bool sequenceBreak)
        {
            const DungeonItemID id = DungeonItemID.HCSanctuary;
            _mode.GuaranteedBossItems.Returns(guaranteedBossItems);
            var items = new List<DungeonItemID> {id};
            _dungeon.DungeonItems.Returns(items);
            _dungeon.Bosses.Returns(items);
            _dungeon.Total.Returns(1);
            _dungeon.TotalWithMapAndCompass.Returns(1);

            var sut = new MutableDungeon(_mode, _keyDoors, _nodes, _dungeonItems, _resultFactory, _dungeon);
            sut.InitializeData();
            sut.DungeonItems[id].Accessibility.Returns(accessibility);
            var state = Substitute.For<IDungeonState>();
            state.SequenceBreak.Returns(sequenceBreak);

            var result = sut.GetDungeonResult(state);

            Assert.Equal(expected, result.MinimumInaccessible);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IMutableDungeon.Factory>();
            var sut = factory(_dungeon);
            
            Assert.NotNull(sut as MutableDungeon);
        }
    }
}