using System;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.Items
{
    public class CrystalRequirementItemTests
    {
        private readonly ISaveLoadManager _saveLoadManager = Substitute.For<ISaveLoadManager>();
        
        private readonly IAddItem.Factory _addItemFactory = _ => Substitute.For<IAddItem>();
        private readonly IRemoveItem.Factory _removeItemFactory = _ => Substitute.For<IRemoveItem>();
        private readonly ICycleItem.Factory _cycleItemFactory = _ => Substitute.For<ICycleItem>();

        private readonly CrystalRequirementItem _sut;

        public CrystalRequirementItemTests()
        {
            _sut = new CrystalRequirementItem(_saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory);
        }

        [Fact]
        public void Ctor_ShouldSetCurrentTo0()
        {
            Assert.Equal(0, _sut.Current);
        }

        [Fact]
        public void Ctor_ShouldSetMaximumTo7()
        {
            Assert.Equal(7, _sut.Maximum);
        }

        [Fact]
        public void Ctor_ShouldSetKnownToFalse()
        {
            Assert.False(_sut.Known);
        }

        [Fact]
        public void PropertyChanged_Raise_WhenKnownChanged()
        {
            Assert.PropertyChanged(_sut, nameof(ICrystalRequirementItem.Known), () => _sut.Add());
        }

        [Fact]
        public void Add_ShouldSetKnownToTrue_WhenKnownReturnsFalse()
        {
            _sut.Add();
            
            Assert.True(_sut.Known);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        [InlineData(3, 4)]
        [InlineData(4, 5)]
        [InlineData(5, 6)]
        [InlineData(6, 7)]
        [InlineData(7, 8)]
        public void Add_ShouldAdd1ToCurrent_WhenKnownReturnsTrue(int expected, int adds)
        {
            for (var i = 0; i < adds; i++)
            {
                _sut.Add();
            }
            
            Assert.Equal(expected, _sut.Current);
        }

        [Fact]
        public void Add_ShouldThrowException_WhenCurrentIs7()
        {
            for (var i = 0; i < 8; i++)
            {
                _sut.Add();
            }

            Assert.Throws<Exception>(() => _sut.Add());
        }

        [Theory]
        [InlineData(false, 0)]
        [InlineData(true, 1)]
        [InlineData(true, 2)]
        [InlineData(true, 3)]
        [InlineData(true, 4)]
        [InlineData(true, 5)]
        [InlineData(true, 6)]
        [InlineData(true, 7)]
        [InlineData(true, 8)]
        public void CanRemove_ShouldReturnTrue_WhenKnownReturnsTrueOrCurrentIsGreaterThan0(bool expected, int adds)
        {
            for (var i = 0; i < adds; i++)
            {
                _sut.Add();
            }
            
            Assert.Equal(expected, _sut.CanRemove());
        }

        [Theory]
        [InlineData(0, 2)]
        [InlineData(1, 3)]
        [InlineData(2, 4)]
        [InlineData(3, 5)]
        [InlineData(4, 6)]
        [InlineData(5, 7)]
        [InlineData(6, 8)]
        public void Remove_ShouldSubtract1FromCurrent_WhenCurrentIsGreaterThan0(int expected, int adds)
        {
            for (var i = 0; i < adds; i++)
            {
                _sut.Add();
            }
            
            _sut.Remove();
            
            Assert.Equal(expected, _sut.Current);
        }

        [Fact]
        public void Remove_ShouldThrowException_WhenCurrentReturns0AndKnownReturnsFalse()
        {
            Assert.Throws<Exception>(() => _sut.Remove());
        }

        [Fact]
        public void Remove_ShouldSetKnownToFalse_WhenCurrentReturns0AndKnownReturnsTrue()
        {
            _sut.Add();
            _sut.Remove();
            
            Assert.False(_sut.Known);
        }

        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(0, 1, false)]
        [InlineData(1, 2, false)]
        [InlineData(2, 3, false)]
        [InlineData(3, 4, false)]
        [InlineData(4, 5, false)]
        [InlineData(5, 6, false)]
        [InlineData(6, 7, false)]
        [InlineData(7, 8, false)]
        [InlineData(0, 9, false)]
        [InlineData(0, 0, true)]
        [InlineData(7, 1, true)]
        [InlineData(6, 2, true)]
        [InlineData(5, 3, true)]
        [InlineData(4, 4, true)]
        [InlineData(3, 5, true)]
        [InlineData(2, 6, true)]
        [InlineData(1, 7, true)]
        [InlineData(0, 8, true)]
        [InlineData(0, 9, true)]
        public void Cycle_ShouldSetCurrent(int expected, int cycles, bool reverse)
        {
            for (var i = 0; i < cycles; i++)
            {
                _sut.Cycle(reverse);
            }
            
            Assert.Equal(expected, _sut.Current);
        }

        [Fact]
        public void Reset_ShouldSetKnownToFalse()
        {
            _sut.Add();
            _sut.Reset();
            
            Assert.False(_sut.Known);
        }
        
        [Fact]
        public void Save_ShouldReturnItemSaveData()
        {
            var sut = new CrystalRequirementItem(
                _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory);
            var saveData = sut.Save();
            
            Assert.Equal(typeof(ItemSaveData), saveData.GetType());
        }

        [Theory]
        [InlineData(false, 0)]
        [InlineData(true, 1)]
        [InlineData(true, 2)]
        public void Save_ShouldSetSaveDataKnownToValue(bool expected, int adds)
        {
            for (var i = 0; i < adds; i++)
            {
                _sut.Add();
            }
            
            var saveData = _sut.Save();
            
            Assert.Equal(expected, saveData.Known);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void Load_SetKnownToSaveDataValue(bool expected, bool known)
        {
            var saveData = new ItemSaveData
            {
                Known = known
            };
            _sut.Load(saveData);
            
            Assert.Equal(expected, _sut.Known);
        }

        [Fact]
        public void Load_ShouldDoNothing_WhenSaveDataIsNull()
        {
            ItemSaveData? saveData = null;
            _sut.Load(saveData);
            
            Assert.Equal(0, _sut.Current);
        }
        
        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<ICrystalRequirementItem.Factory>();
            var sut = factory();
            
            Assert.NotNull(sut as CrystalRequirementItem);
        }
    }
}