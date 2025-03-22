using Autofac;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.Modes
{
    public class ModeTests
    {
        private readonly Mode _sut = new Mode(
            _ => Substitute.For<IChangeItemPlacement>(),
            _ => Substitute.For<IChangeMapShuffle>(),
            _ => Substitute.For<IChangeCompassShuffle>(),
            _ => Substitute.For<IChangeSmallKeyShuffle>(),
            _ => Substitute.For<IChangeBigKeyShuffle>(),
            _ => Substitute.For<IChangeWorldState>(),
            _ => Substitute.For<IChangeEntranceShuffle>(),
            _ => Substitute.For<IChangeBossShuffle>(),
            _ => Substitute.For<IChangeEnemyShuffle>(),
            _ => Substitute.For<IChangeGuaranteedBossItems>(),
            _ => Substitute.For<IChangeGenericKeys>(),
            _ => Substitute.For<IChangeTakeAnyLocations>(),
            _ => Substitute.For<IChangeKeyDropShuffle>(),
            _ => Substitute.For<IChangeShopShuffle>());

        [Fact]
        public void ItemPlacement_ShouldDefaultToAdvanced()
        {
            Assert.Equal(ItemPlacement.Advanced, _sut.ItemPlacement);
        }

        [Fact]
        public void ItemPlacement_ShouldRaisePropertyChanged()
        {
            Assert.PropertyChanged(_sut, nameof(IMode.ItemPlacement), () =>
                _sut.ItemPlacement = ItemPlacement.Basic);
        }

        [Fact]
        public void MapShuffle_ShouldRaisePropertyChanged()
        {
            Assert.PropertyChanged(_sut, nameof(IMode.MapShuffle), () =>
                _sut.MapShuffle = true);
        }

        [Fact]
        public void CompassShuffle_ShouldRaisePropertyChanged()
        {
            Assert.PropertyChanged(_sut, nameof(IMode.CompassShuffle), () =>
                _sut.CompassShuffle = true);
        }

        [Fact]
        public void SmallKeyShuffle_ShouldRaisePropertyChanged()
        {
            Assert.PropertyChanged(_sut, nameof(IMode.SmallKeyShuffle), () =>
                _sut.SmallKeyShuffle = true);
        }

        [Fact]
        public void BigKeyShuffle_ShouldRaisePropertyChanged()
        {
            Assert.PropertyChanged(_sut, nameof(IMode.BigKeyShuffle), () =>
                _sut.BigKeyShuffle = true);
        }

        [Fact]
        public void WorldState_ShouldRaisePropertyChanged()
        {
            Assert.PropertyChanged(_sut, nameof(IMode.WorldState), () =>
                _sut.WorldState = WorldState.Inverted);
        }

        [Fact]
        public void WorldState_ShouldSetItemPlacementToAdvanced_WhenWorldStateIsSetToInverted()
        {
            _sut.ItemPlacement = ItemPlacement.Basic;
            _sut.WorldState = WorldState.Inverted;
            
            Assert.Equal(ItemPlacement.Advanced, _sut.ItemPlacement);
        }

        [Fact]
        public void EntranceShuffle_ShouldRaisePropertyChanged()
        {
            Assert.PropertyChanged(_sut, nameof(IMode.EntranceShuffle), () =>
                _sut.EntranceShuffle = EntranceShuffle.Dungeon);
        }

        [Fact]
        public void BossShuffle_ShouldRaisePropertyChanged()
        {
            Assert.PropertyChanged(_sut, nameof(IMode.BossShuffle), () =>
                _sut.BossShuffle = true);
        }

        [Fact]
        public void EnemyShuffle_ShouldRaisePropertyChanged()
        {
            Assert.PropertyChanged(_sut, nameof(IMode.EnemyShuffle), () =>
                _sut.EnemyShuffle = true);
        }

        [Fact]
        public void GuaranteedBossItems_ShouldRaisePropertyChanged()
        {
            Assert.PropertyChanged(_sut, nameof(IMode.GuaranteedBossItems), () =>
                _sut.GuaranteedBossItems = true);
        }

        [Fact]
        public void GenericKeys_ShouldRaisePropertyChanged()
        {
            Assert.PropertyChanged(_sut, nameof(IMode.GenericKeys), () =>
                _sut.GenericKeys = true);
        }

        [Fact]
        public void GenericKeys_ShouldSetSmallKeyShuffleToTrue_IfGenericKeysIsSetToTrue()
        {
            _sut.GenericKeys = true;
            
            Assert.True(_sut.SmallKeyShuffle);
        }

        [Fact]
        public void TakeAnyLocations_ShouldRaisePropertyChanged()
        {
            Assert.PropertyChanged(_sut, nameof(IMode.TakeAnyLocations), () =>
                _sut.TakeAnyLocations = true);
        }

        [Fact]
        public void KeyDropShuffle_ShouldRaisePropertyChanged()
        {
            Assert.PropertyChanged(_sut, nameof(IMode.KeyDropShuffle), () =>
                _sut.KeyDropShuffle = true);
        }

        [Fact]
        public void ShopShuffle_ShouldRaisePropertyChanged()
        {
            Assert.PropertyChanged(_sut, nameof(IMode.ShopShuffle), () =>
                _sut.ShopShuffle = true);
        }

        [Fact]
        public void CreateChangeItemPlacement_ShouldCreateNewAction()
        {
            var action = _sut.CreateChangeItemPlacementAction(ItemPlacement.Basic);
            
            Assert.NotNull(action as IChangeItemPlacement);
        }

        [Fact]
        public void CreateChangeMapShuffle_ShouldCreateNewAction()
        {
            var action = _sut.CreateChangeMapShuffleAction(true);
            
            Assert.NotNull(action as IChangeMapShuffle);
        }

        [Fact]
        public void CreateChangeCompassShuffle_ShouldCreateNewAction()
        {
            var action = _sut.CreateChangeCompassShuffleAction(true);
            
            Assert.NotNull(action as IChangeCompassShuffle);
        }

        [Fact]
        public void CreateChangeSmallKeyShuffle_ShouldCreateNewAction()
        {
            var action = _sut.CreateChangeSmallKeyShuffleAction(true);
            
            Assert.NotNull(action as IChangeSmallKeyShuffle);
        }

        [Fact]
        public void CreateChangeBigKeyShuffle_ShouldCreateNewAction()
        {
            var action = _sut.CreateChangeBigKeyShuffleAction(true);
            
            Assert.NotNull(action as IChangeBigKeyShuffle);
        }

        [Fact]
        public void CreateChangeWorldState_ShouldCreateNewAction()
        {
            var action = _sut.CreateChangeWorldStateAction(WorldState.Inverted);
            
            Assert.NotNull(action as IChangeWorldState);
        }

        [Fact]
        public void CreateChangeEntranceShuffle_ShouldCreateNewAction()
        {
            var action = _sut.CreateChangeEntranceShuffleAction(EntranceShuffle.Dungeon);
            
            Assert.NotNull(action as IChangeEntranceShuffle);
        }

        [Fact]
        public void CreateChangeBossShuffle_ShouldCreateNewAction()
        {
            var action = _sut.CreateChangeBossShuffleAction(true);
            
            Assert.NotNull(action as IChangeBossShuffle);
        }

        [Fact]
        public void CreateChangeEnemyShuffle_ShouldCreateNewAction()
        {
            var action = _sut.CreateChangeEnemyShuffleAction(true);
            
            Assert.NotNull(action as IChangeEnemyShuffle);
        }

        [Fact]
        public void CreateChangeGuaranteedBossItems_ShouldCreateNewAction()
        {
            var action = _sut.CreateChangeGuaranteedBossItemsAction(true);
            
            Assert.NotNull(action as IChangeGuaranteedBossItems);
        }

        [Fact]
        public void CreateChangeGenericKeys_ShouldCreateNewAction()
        {
            var action = _sut.CreateChangeGenericKeysAction(true);
            
            Assert.NotNull(action as IChangeGenericKeys);
        }

        [Fact]
        public void CreateChangeTakeAnyLocations_ShouldCreateNewAction()
        {
            var action = _sut.CreateChangeTakeAnyLocationsAction(true);
            
            Assert.NotNull(action as IChangeTakeAnyLocations);
        }

        [Fact]
        public void CreateChangeKeyDropShuffle_ShouldCreateNewAction()
        {
            var action = _sut.CreateChangeKeyDropShuffleAction(true);
            
            Assert.NotNull(action as IChangeKeyDropShuffle);
        }

        [Fact]
        public void CreateChangeShopShuffle_ShouldCreateNewAction()
        {
            var action = _sut.CreateChangeShopShuffleAction(true);
            
            Assert.NotNull(action as IChangeShopShuffle);
        }

        [Theory]
        [InlineData(ItemPlacement.Advanced, ItemPlacement.Advanced)]
        [InlineData(ItemPlacement.Basic, ItemPlacement.Basic)]
        public void Save_ShouldReturnExpectedItemPlacement(ItemPlacement expected, ItemPlacement actual)
        {
            _sut.ItemPlacement = actual;
            var saveData = _sut.Save();
            
            Assert.Equal(expected, saveData.ItemPlacement);
        }

        [Theory]
        [InlineData(WorldState.StandardOpen, WorldState.StandardOpen)]
        [InlineData(WorldState.Inverted, WorldState.Inverted)]
        public void Save_ShouldReturnExpectedWorldState(WorldState expected, WorldState actual)
        {
            _sut.WorldState = actual;
            var saveData = _sut.Save();
            
            Assert.Equal(expected, saveData.WorldState);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Save_ShouldReturnExpectedMapShuffle(bool expected, bool actual)
        {
            _sut.MapShuffle = actual;
            var saveData = _sut.Save();
            
            Assert.Equal(expected, saveData.MapShuffle);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Save_ShouldReturnExpectedCompassShuffle(bool expected, bool actual)
        {
            _sut.CompassShuffle = actual;
            var saveData = _sut.Save();
            
            Assert.Equal(expected, saveData.CompassShuffle);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Save_ShouldReturnExpectedSmallKeyShuffle(bool expected, bool actual)
        {
            _sut.SmallKeyShuffle = actual;
            var saveData = _sut.Save();
            
            Assert.Equal(expected, saveData.SmallKeyShuffle);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Save_ShouldReturnExpectedBigKeyShuffle(bool expected, bool actual)
        {
            _sut.BigKeyShuffle = actual;
            var saveData = _sut.Save();
            
            Assert.Equal(expected, saveData.BigKeyShuffle);
        }

        [Theory]
        [InlineData(EntranceShuffle.None, EntranceShuffle.None)]
        [InlineData(EntranceShuffle.Dungeon, EntranceShuffle.Dungeon)]
        [InlineData(EntranceShuffle.All, EntranceShuffle.All)]
        [InlineData(EntranceShuffle.Insanity, EntranceShuffle.Insanity)]
        public void Save_ShouldReturnExpectedEntranceShuffle(EntranceShuffle expected, EntranceShuffle actual)
        {
            _sut.EntranceShuffle = actual;
            var saveData = _sut.Save();
            
            Assert.Equal(expected, saveData.EntranceShuffle);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Save_ShouldReturnExpectedBossShuffle(bool expected, bool actual)
        {
            _sut.BossShuffle = actual;
            var saveData = _sut.Save();
            
            Assert.Equal(expected, saveData.BossShuffle);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Save_ShouldReturnExpectedEnemyShuffle(bool expected, bool actual)
        {
            _sut.EnemyShuffle = actual;
            var saveData = _sut.Save();
            
            Assert.Equal(expected, saveData.EnemyShuffle);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Save_ShouldReturnExpectedGuaranteedBossItems(bool expected, bool actual)
        {
            _sut.GuaranteedBossItems = actual;
            var saveData = _sut.Save();
            
            Assert.Equal(expected, saveData.GuaranteedBossItems);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Save_ShouldReturnExpectedGenericKeys(bool expected, bool actual)
        {
            _sut.GenericKeys = actual;
            var saveData = _sut.Save();
            
            Assert.Equal(expected, saveData.GenericKeys);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Save_ShouldReturnExpectedTakeAnyLocations(bool expected, bool actual)
        {
            _sut.TakeAnyLocations = actual;
            var saveData = _sut.Save();
            
            Assert.Equal(expected, saveData.TakeAnyLocations);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Save_ShouldReturnExpectedShopShuffle(bool expected, bool actual)
        {
            _sut.ShopShuffle = actual;
            var saveData = _sut.Save();
            
            Assert.Equal(expected, saveData.ShopShuffle);
        }

        [Fact]
        public void Load_ShouldDoNothing_WhenSaveDataIsNull()
        {
            var original = _sut.ToExpectedObject();
            _sut.Load(null);
            
            original.ShouldEqual(_sut);
        }
        
        [Theory]
        [InlineData(ItemPlacement.Advanced, ItemPlacement.Advanced)]
        [InlineData(ItemPlacement.Basic, ItemPlacement.Basic)]
        public void Load_ShouldSetExpectedItemPlacement(ItemPlacement expected, ItemPlacement saved)
        {
            var saveData = new ModeSaveData
            {
                ItemPlacement = saved
            };
            _sut.Load(saveData);
            
            Assert.Equal(expected, saveData.ItemPlacement);
        }

        [Theory]
        [InlineData(WorldState.StandardOpen, WorldState.StandardOpen)]
        [InlineData(WorldState.Inverted, WorldState.Inverted)]
        public void Load_ShouldSetExpectedWorldState(WorldState expected, WorldState saved)
        {
            var saveData = new ModeSaveData
            {
                WorldState = saved
            };
            _sut.Load(saveData);
            
            Assert.Equal(expected, saveData.WorldState);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Load_ShouldSetExpectedMapShuffle(bool expected, bool saved)
        {
            var saveData = new ModeSaveData
            {
                MapShuffle = saved
            };
            _sut.Load(saveData);
            
            Assert.Equal(expected, saveData.MapShuffle);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Load_ShouldSetExpectedCompassShuffle(bool expected, bool saved)
        {
            var saveData = new ModeSaveData
            {
                CompassShuffle = saved
            };
            _sut.Load(saveData);
            
            Assert.Equal(expected, saveData.CompassShuffle);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Load_ShouldSetExpectedSmallKeyShuffle(bool expected, bool saved)
        {
            var saveData = new ModeSaveData
            {
                SmallKeyShuffle = saved
            };
            _sut.Load(saveData);
            
            Assert.Equal(expected, saveData.SmallKeyShuffle);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Load_ShouldSetExpectedBigKeyShuffle(bool expected, bool saved)
        {
            var saveData = new ModeSaveData
            {
                BigKeyShuffle = saved
            };
            _sut.Load(saveData);
            
            Assert.Equal(expected, saveData.BigKeyShuffle);
        }

        [Theory]
        [InlineData(EntranceShuffle.None, EntranceShuffle.None)]
        [InlineData(EntranceShuffle.Dungeon, EntranceShuffle.Dungeon)]
        [InlineData(EntranceShuffle.All, EntranceShuffle.All)]
        [InlineData(EntranceShuffle.Insanity, EntranceShuffle.Insanity)]
        public void Load_ShouldSetExpectedEntranceShuffle(EntranceShuffle expected, EntranceShuffle saved)
        {
            var saveData = new ModeSaveData
            {
                EntranceShuffle = saved
            };
            _sut.Load(saveData);
            
            Assert.Equal(expected, saveData.EntranceShuffle);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Load_ShouldSetExpectedBossShuffle(bool expected, bool saved)
        {
            var saveData = new ModeSaveData
            {
                BossShuffle = saved
            };
            _sut.Load(saveData);
            
            Assert.Equal(expected, saveData.BossShuffle);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Load_ShouldSetExpectedEnemyShuffle(bool expected, bool saved)
        {
            var saveData = new ModeSaveData
            {
                EnemyShuffle = saved
            };
            _sut.Load(saveData);
            
            Assert.Equal(expected, saveData.EnemyShuffle);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Load_ShouldSetExpectedGuaranteedBossItems(bool expected, bool saved)
        {
            var saveData = new ModeSaveData
            {
                GuaranteedBossItems = saved
            };
            _sut.Load(saveData);
            
            Assert.Equal(expected, saveData.GuaranteedBossItems);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Load_ShouldSetExpectedGenericKeys(bool expected, bool saved)
        {
            var saveData = new ModeSaveData
            {
                GenericKeys = saved
            };
            _sut.Load(saveData);
            
            Assert.Equal(expected, saveData.GenericKeys);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Load_ShouldSetExpectedTakeAnyLocations(bool expected, bool saved)
        {
            var saveData = new ModeSaveData
            {
                TakeAnyLocations = saved
            };
            _sut.Load(saveData);
            
            Assert.Equal(expected, saveData.TakeAnyLocations);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Load_ShouldSetExpectedShopShuffle(bool expected, bool saved)
        {
            var saveData = new ModeSaveData
            {
                ShopShuffle = saved
            };
            _sut.Load(saveData);
            
            Assert.Equal(expected, saveData.ShopShuffle);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IMode>();
            
            Assert.NotNull(sut as Mode);
        }
    }
}