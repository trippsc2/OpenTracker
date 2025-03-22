using System.Collections.Generic;
using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.KeyLayouts;
using OpenTracker.Models.Dungeons.KeyLayouts.Factories;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Items;
using OpenTracker.Models.Items.Keys;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons
{
    public class DungeonTests
    {
        private readonly IMode _mode = Substitute.For<IMode>();
        private readonly IKeyLayoutFactory _keyLayoutFactory = Substitute.For<IKeyLayoutFactory>();

        private readonly IList<IKeyLayout> _keyLayout = Substitute.For<IList<IKeyLayout>>(); 

        private readonly ICappedItem _map = Substitute.For<ICappedItem>();
        private readonly ICappedItem _compass = Substitute.For<ICappedItem>();
        private readonly ISmallKeyItem _smallKey = Substitute.For<ISmallKeyItem>();
        private readonly IBigKeyItem _bigKey = Substitute.For<IBigKeyItem>();

        private readonly List<DungeonItemID> _dungeonItems = new();
        private readonly List<DungeonItemID> _bosses = new();
        private readonly List<DungeonItemID> _smallKeyDrops = new();
        private readonly List<DungeonItemID> _bigKeyDrops = new();

        private readonly List<KeyDoorID> _smallKeyDoors = new();
        private readonly List<KeyDoorID> _bigKeyDoors = new();

        private readonly List<DungeonNodeID> _nodes = new();
        private readonly List<IOverworldNode> _entryNodes = new();

        public DungeonTests()
        {
            _keyLayoutFactory.GetDungeonKeyLayouts(Arg.Any<IDungeon>()).Returns(_keyLayout);
        }
        
        [Theory]
        [InlineData(DungeonID.HyruleCastle, DungeonID.HyruleCastle)]
        [InlineData(DungeonID.AgahnimTower, DungeonID.AgahnimTower)]
        [InlineData(DungeonID.EasternPalace, DungeonID.EasternPalace)]
        [InlineData(DungeonID.DesertPalace, DungeonID.DesertPalace)]
        [InlineData(DungeonID.TowerOfHera, DungeonID.TowerOfHera)]
        [InlineData(DungeonID.PalaceOfDarkness, DungeonID.PalaceOfDarkness)]
        [InlineData(DungeonID.SwampPalace, DungeonID.SwampPalace)]
        [InlineData(DungeonID.SkullWoods, DungeonID.SkullWoods)]
        [InlineData(DungeonID.ThievesTown, DungeonID.ThievesTown)]
        [InlineData(DungeonID.IcePalace, DungeonID.IcePalace)]
        [InlineData(DungeonID.MiseryMire, DungeonID.MiseryMire)]
        [InlineData(DungeonID.TurtleRock, DungeonID.TurtleRock)]
        [InlineData(DungeonID.GanonsTower, DungeonID.GanonsTower)]
        public void Ctor_ShouldSetIDToProvided(DungeonID expected, DungeonID id)
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, id, _map, _compass, _smallKey, _bigKey, _dungeonItems, _bosses,
                _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            Assert.Equal(expected, sut.ID);
        }

        [Fact]
        public void Ctor_ShouldSetMap()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);

            Assert.Equal(_map, sut.Map);
        }

        [Fact]
        public void Ctor_ShouldSetCompass()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);

            Assert.Equal(_compass, sut.Compass);
        }

        [Fact]
        public void Ctor_ShouldSetSmallKey()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);

            Assert.Equal(_smallKey, sut.SmallKey);
        }

        [Fact]
        public void Ctor_ShouldSetBigKey()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);

            Assert.Equal(_bigKey, sut.BigKey);
        }
        
        [Fact]
        public void Ctor_ShouldSetDungeonItems()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);

            Assert.Equal(_dungeonItems, sut.DungeonItems);
        }
        
        [Fact]
        public void Ctor_ShouldSetBosses()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);

            Assert.Equal(_bosses, sut.Bosses);
        }
        
        [Fact]
        public void Ctor_ShouldSetSmallKeyDrops()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);

            Assert.Equal(_smallKeyDrops, sut.SmallKeyDrops);
        }
        
        [Fact]
        public void Ctor_ShouldSetBigKeyDrops()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);

            Assert.Equal(_bigKeyDrops, sut.BigKeyDrops);
        }
        
        [Fact]
        public void Ctor_ShouldSetSmallKeyDoors()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);

            Assert.Equal(_smallKeyDoors, sut.SmallKeyDoors);
        }
        
        [Fact]
        public void Ctor_ShouldSetBigKeyDoors()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);

            Assert.Equal(_bigKeyDoors, sut.BigKeyDoors);
        }

        [Fact]
        public void Ctor_ShouldCallGetDungeonKeyLayouts()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);

            _keyLayoutFactory.Received().GetDungeonKeyLayouts(sut);
        }

        [Fact]
        public void Ctor_ShouldSetKeyLayouts()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            Assert.Equal(_keyLayout, sut.KeyLayouts);
        }
        
        [Fact]
        public void Ctor_ShouldSetNodes()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);

            Assert.Equal(_nodes, sut.Nodes);
        }
        
        [Fact]
        public void Ctor_ShouldSetEntryNodes()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);

            Assert.Equal(_entryNodes, sut.EntryNodes);
        }

        [Fact]
        public void Total_ShouldRaisePropertyChanged()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            _dungeonItems.Add(DungeonItemID.ATBoss);
            
            Assert.PropertyChanged(sut, nameof(IDungeon.Total), () =>
                _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _mode, new PropertyChangedEventArgs(nameof(IMode.SmallKeyShuffle))));
        }

        [Fact]
        public void ModeChanged_ShouldNotUpdateTotal_WhenKeyDropShuffleChangesAndNoKeyDropsExist()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            _dungeonItems.Add(DungeonItemID.ATBoss);
            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.KeyDropShuffle)));
            
            Assert.Equal(0, sut.Total);
        }

        [Fact]
        public void ModeChanged_ShouldNotUpdateTotal_WhenMapShuffleChangesAndMapIsNull()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, null, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            _dungeonItems.Add(DungeonItemID.ATBoss);
            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.MapShuffle)));
            
            Assert.Equal(0, sut.Total);
        }

        [Fact]
        public void ModeChanged_ShouldNotUpdateTotal_WhenCompassShuffleChangesAndCompassIsNull()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, null, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            _dungeonItems.Add(DungeonItemID.ATBoss);
            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.CompassShuffle)));
            
            Assert.Equal(0, sut.Total);
        }

        [Fact]
        public void ModeChanged_ShouldNotUpdateTotal_WhenBigKeyShuffleChangesAndBigKeyIsNull()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, null, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            _dungeonItems.Add(DungeonItemID.ATBoss);
            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.BigKeyShuffle)));
            
            Assert.Equal(0, sut.Total);
        }

        [Fact]
        public void ModeChanged_ShouldUpdateTotal_WhenKeyDropShuffleChangesAndSmallKeyDropExists()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            _dungeonItems.Add(DungeonItemID.ATBoss);
            _smallKeyDrops.Add(DungeonItemID.ATBoss);
            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.KeyDropShuffle)));
            
            Assert.Equal(1, sut.Total);
        }

        [Fact]
        public void ModeChanged_ShouldUpdateTotal_WhenKeyDropShuffleChangesAndBigKeyDropExists()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            _dungeonItems.Add(DungeonItemID.ATBoss);
            _bigKeyDrops.Add(DungeonItemID.ATBoss);
            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.KeyDropShuffle)));
            
            Assert.Equal(1, sut.Total);
        }

        [Fact]
        public void ModeChanged_ShouldUpdateTotal_WhenMapShuffleChangesAndMapIsNotNull()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            _dungeonItems.Add(DungeonItemID.ATBoss);
            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.MapShuffle)));
            
            Assert.Equal(1, sut.Total);
        }

        [Fact]
        public void ModeChanged_ShouldUpdateTotal_WhenCompassShuffleChangesAndCompassIsNotNull()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            _dungeonItems.Add(DungeonItemID.ATBoss);
            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.CompassShuffle)));
            
            Assert.Equal(1, sut.Total);
        }

        [Fact]
        public void ModeChanged_ShouldUpdateTotal_WhenBigKeyShuffleChangesAndBigKeyIsNotNull()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            _dungeonItems.Add(DungeonItemID.ATBoss);
            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.BigKeyShuffle)));
            
            Assert.Equal(1, sut.Total);
        }

        [Fact]
        public void ModeChanged_ShouldUpdateTotal_WhenSmallKeyShuffleChanges()
        {
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            _dungeonItems.Add(DungeonItemID.ATBoss);
            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.SmallKeyShuffle)));
            
            Assert.Equal(1, sut.Total);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void Total_ShouldEqualTotalDungeonItems(int expected, int dungeonItems)
        {
            for (var i = 0; i < dungeonItems; i++)
            {
                _dungeonItems.Add(DungeonItemID.ATBoss);
            }
            
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            Assert.Equal(expected, sut.Total);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void TotalWithMapAndCompass_ShouldEqualTotalDungeonItems(int expected, int dungeonItems)
        {
            for (var i = 0; i < dungeonItems; i++)
            {
                _dungeonItems.Add(DungeonItemID.ATBoss);
            }
            
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            Assert.Equal(expected, sut.TotalWithMapAndCompass);
        }

        [Theory]
        [InlineData(0, 0, false, 0, 0)]
        [InlineData(0, 0, false, 0, 1)]
        [InlineData(0, 0, false, 1, 0)]
        [InlineData(0, 0, false, 1, 1)]
        [InlineData(0, 0, true, 0, 0)]
        [InlineData(1, 0, true, 0, 1)]
        [InlineData(1, 0, true, 1, 0)]
        [InlineData(2, 0, true, 1, 1)]
        [InlineData(1, 1, false, 0, 0)]
        [InlineData(1, 1, false, 0, 1)]
        [InlineData(1, 1, false, 1, 0)]
        [InlineData(1, 1, false, 1, 1)]
        [InlineData(1, 1, true, 0, 0)]
        [InlineData(2, 1, true, 0, 1)]
        [InlineData(2, 1, true, 1, 0)]
        [InlineData(3, 1, true, 1, 1)]
        public void Total_ShouldAccountForKeyDropShuffle(
            int expected, int dungeonItems, bool keyDropShuffle, int smallKeyDrops, int bigKeyDrops)
        {
            for (var i = 0; i < dungeonItems; i++)
            {
                _dungeonItems.Add(DungeonItemID.ATBoss);
            }

            _mode.KeyDropShuffle.Returns(keyDropShuffle);
            
            for (var i = 0; i < smallKeyDrops; i++)
            {
                _smallKeyDrops.Add(DungeonItemID.ATBoss);
            }
            
            for (var i = 0; i < bigKeyDrops; i++)
            {
                _bigKeyDrops.Add(DungeonItemID.ATBoss);
            }
            
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            Assert.Equal(expected, sut.Total);
        }
        
        [Theory]
        [InlineData(0, 0, false, 0, 0)]
        [InlineData(0, 0, false, 0, 1)]
        [InlineData(0, 0, false, 1, 0)]
        [InlineData(0, 0, false, 1, 1)]
        [InlineData(0, 0, true, 0, 0)]
        [InlineData(1, 0, true, 0, 1)]
        [InlineData(1, 0, true, 1, 0)]
        [InlineData(2, 0, true, 1, 1)]
        [InlineData(1, 1, false, 0, 0)]
        [InlineData(1, 1, false, 0, 1)]
        [InlineData(1, 1, false, 1, 0)]
        [InlineData(1, 1, false, 1, 1)]
        [InlineData(1, 1, true, 0, 0)]
        [InlineData(2, 1, true, 0, 1)]
        [InlineData(2, 1, true, 1, 0)]
        [InlineData(3, 1, true, 1, 1)]
        public void TotalWithMapAndCompass_ShouldAccountForKeyDropShuffle(
            int expected, int dungeonItems, bool keyDropShuffle, int smallKeyDrops, int bigKeyDrops)
        {
            for (var i = 0; i < dungeonItems; i++)
            {
                _dungeonItems.Add(DungeonItemID.ATBoss);
            }

            _mode.KeyDropShuffle.Returns(keyDropShuffle);
            
            for (var i = 0; i < smallKeyDrops; i++)
            {
                _smallKeyDrops.Add(DungeonItemID.ATBoss);
            }
            
            for (var i = 0; i < bigKeyDrops; i++)
            {
                _bigKeyDrops.Add(DungeonItemID.ATBoss);
            }
            
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            Assert.Equal(expected, sut.TotalWithMapAndCompass);
        }

        [Theory]
        [InlineData(1, 1, false, 0)]
        [InlineData(0, 1, false, 1)]
        [InlineData(0, 1, false, 2)]
        [InlineData(1, 1, true, 0)]
        [InlineData(1, 1, true, 1)]
        [InlineData(1, 1, true, 2)]
        [InlineData(2, 2, false, 0)]
        [InlineData(1, 2, false, 1)]
        [InlineData(0, 2, false, 2)]
        [InlineData(2, 2, true, 0)]
        [InlineData(2, 2, true, 1)]
        [InlineData(2, 2, true, 2)]
        public void Total_ShouldAccountForSmallKeyShuffle(
            int expected, int dungeonItems, bool smallKeyShuffle, int smallKeys)
        {
            for (var i = 0; i < dungeonItems; i++)
            {
                _dungeonItems.Add(DungeonItemID.ATBoss);
            }

            _mode.SmallKeyShuffle.Returns(smallKeyShuffle);

            _smallKey.Maximum.Returns(smallKeys);
            
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            Assert.Equal(expected, sut.Total);
        }

        [Theory]
        [InlineData(1, 1, false, 0)]
        [InlineData(0, 1, false, 1)]
        [InlineData(0, 1, false, 2)]
        [InlineData(1, 1, true, 0)]
        [InlineData(1, 1, true, 1)]
        [InlineData(1, 1, true, 2)]
        [InlineData(2, 2, false, 0)]
        [InlineData(1, 2, false, 1)]
        [InlineData(0, 2, false, 2)]
        [InlineData(2, 2, true, 0)]
        [InlineData(2, 2, true, 1)]
        [InlineData(2, 2, true, 2)]
        public void TotalWithMapAndCompass_ShouldAccountForSmallKeyShuffle(
            int expected, int dungeonItems, bool smallKeyShuffle, int smallKeys)
        {
            for (var i = 0; i < dungeonItems; i++)
            {
                _dungeonItems.Add(DungeonItemID.ATBoss);
            }

            _mode.SmallKeyShuffle.Returns(smallKeyShuffle);

            _smallKey.Maximum.Returns(smallKeys);
            
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            Assert.Equal(expected, sut.TotalWithMapAndCompass);
        }

        [Theory]
        [InlineData(1, 1, false, 0)]
        [InlineData(0, 1, false, 1)]
        [InlineData(1, 1, true, 0)]
        [InlineData(1, 1, true, 1)]
        [InlineData(2, 2, false, 0)]
        [InlineData(1, 2, false, 1)]
        [InlineData(2, 2, true, 0)]
        [InlineData(2, 2, true, 1)]
        public void Total_ShouldAccountForBigKeyShuffle(
            int expected, int dungeonItems, bool bigKeyShuffle, int bigKey)
        {
            for (var i = 0; i < dungeonItems; i++)
            {
                _dungeonItems.Add(DungeonItemID.ATBoss);
            }

            _mode.BigKeyShuffle.Returns(bigKeyShuffle);

            _bigKey.Maximum.Returns(bigKey);
            
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            Assert.Equal(expected, sut.Total);
        }
 
        [Theory]
        [InlineData(1, 1, false, 0)]
        [InlineData(0, 1, false, 1)]
        [InlineData(1, 1, true, 0)]
        [InlineData(1, 1, true, 1)]
        [InlineData(2, 2, false, 0)]
        [InlineData(1, 2, false, 1)]
        [InlineData(2, 2, true, 0)]
        [InlineData(2, 2, true, 1)]
        public void TotalWithMapAndCompass_ShouldAccountForBigKeyShuffle(
            int expected, int dungeonItems, bool bigKeyShuffle, int bigKey)
        {
            for (var i = 0; i < dungeonItems; i++)
            {
                _dungeonItems.Add(DungeonItemID.ATBoss);
            }

            _mode.BigKeyShuffle.Returns(bigKeyShuffle);

            _bigKey.Maximum.Returns(bigKey);
            
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            Assert.Equal(expected, sut.TotalWithMapAndCompass);
        }

        [Theory]
        [InlineData(1, 1, false, 0)]
        [InlineData(0, 1, false, 1)]
        [InlineData(1, 1, true, 0)]
        [InlineData(1, 1, true, 1)]
        [InlineData(2, 2, false, 0)]
        [InlineData(1, 2, false, 1)]
        [InlineData(2, 2, true, 0)]
        [InlineData(2, 2, true, 1)]
        public void Total_ShouldAccountForMapShuffle(
            int expected, int dungeonItems, bool mapShuffle, int map)
        {
            for (var i = 0; i < dungeonItems; i++)
            {
                _dungeonItems.Add(DungeonItemID.ATBoss);
            }

            _mode.MapShuffle.Returns(mapShuffle);

            _map.Maximum.Returns(map);
            
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            Assert.Equal(expected, sut.Total);
        }
 
        [Theory]
        [InlineData(1, 1, false, 0)]
        [InlineData(1, 1, false, 1)]
        [InlineData(1, 1, true, 0)]
        [InlineData(1, 1, true, 1)]
        [InlineData(2, 2, false, 0)]
        [InlineData(2, 2, false, 1)]
        [InlineData(2, 2, true, 0)]
        [InlineData(2, 2, true, 1)]
        public void TotalWithMapAndCompass_ShouldNotAccountForMapShuffle(
            int expected, int dungeonItems, bool mapShuffle, int map)
        {
            for (var i = 0; i < dungeonItems; i++)
            {
                _dungeonItems.Add(DungeonItemID.ATBoss);
            }

            _mode.MapShuffle.Returns(mapShuffle);

            _map.Maximum.Returns(map);
            
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            Assert.Equal(expected, sut.TotalWithMapAndCompass);
        }

        [Theory]
        [InlineData(1, 1, false, 0)]
        [InlineData(0, 1, false, 1)]
        [InlineData(1, 1, true, 0)]
        [InlineData(1, 1, true, 1)]
        [InlineData(2, 2, false, 0)]
        [InlineData(1, 2, false, 1)]
        [InlineData(2, 2, true, 0)]
        [InlineData(2, 2, true, 1)]
        public void Total_ShouldAccountForCompassShuffle(
            int expected, int dungeonItems, bool compassShuffle, int compass)
        {
            for (var i = 0; i < dungeonItems; i++)
            {
                _dungeonItems.Add(DungeonItemID.ATBoss);
            }

            _mode.CompassShuffle.Returns(compassShuffle);

            _compass.Maximum.Returns(compass);
            
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            Assert.Equal(expected, sut.Total);
        }
 
        [Theory]
        [InlineData(1, 1, false, 0)]
        [InlineData(1, 1, false, 1)]
        [InlineData(1, 1, true, 0)]
        [InlineData(1, 1, true, 1)]
        [InlineData(2, 2, false, 0)]
        [InlineData(2, 2, false, 1)]
        [InlineData(2, 2, true, 0)]
        [InlineData(2, 2, true, 1)]
        public void TotalWithMapAndCompass_ShouldNotAccountForCompassShuffle(
            int expected, int dungeonItems, bool compassShuffle, int compass)
        {
            for (var i = 0; i < dungeonItems; i++)
            {
                _dungeonItems.Add(DungeonItemID.ATBoss);
            }

            _mode.CompassShuffle.Returns(compassShuffle);

            _compass.Maximum.Returns(compass);
            
            var sut = new Dungeon(
                _mode, _keyLayoutFactory, DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems,
                _bosses, _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            Assert.Equal(expected, sut.TotalWithMapAndCompass);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IDungeon.Factory>();
            var sut = factory(
                DungeonID.AgahnimTower, _map, _compass, _smallKey, _bigKey, _dungeonItems, _bosses,
                _smallKeyDrops, _bigKeyDrops, _smallKeyDoors,
                _bigKeyDoors, _nodes, _entryNodes);
            
            Assert.NotNull(sut as Dungeon);
        }
    }
}