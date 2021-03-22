using System.Collections.Generic;
using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.SaveLoad;
using Xunit;

namespace OpenTracker.UnitTests.Models.Items
{
    public class ItemDictionaryTests
    {
        private readonly IItemFactory _itemFactory = Substitute.For<IItemFactory>();
        private readonly ItemDictionary _sut;

        public ItemDictionaryTests()
        {
            _itemFactory.GetItem(Arg.Any<ItemType>()).Returns(Substitute.For<IItem>());
            _sut = new ItemDictionary(() => _itemFactory);
        }

        [Fact]
        public void Reset_ShouldCallResetOnItems()
        {
            var item = _sut[ItemType.Sword];
            _sut.Reset();
            
            item.Received().Reset();
        }

        [Fact]
        public void Save_ShouldReturnSaveData()
        {
            _ = _sut[ItemType.Sword];
            var saveData = _sut.Save();

            Assert.Single(saveData);
        }

        [Fact]
        public void Load_ShouldDoNothing_WhenSaveDataIsNull()
        {
            var item = _sut[ItemType.Sword];
            _sut.Load(null);
            
            item.DidNotReceive().Load(Arg.Any<ItemSaveData>());
        }

        [Fact]
        public void Load_ShouldCallLoadOnItems_WhenSaveDataIsNotNull()
        {
            var item = _sut[ItemType.Sword];
            var saveData = new Dictionary<ItemType, ItemSaveData>
            {
                {ItemType.Sword, new ItemSaveData {Current = 0, Known = false}}
            };
            _sut.Load(saveData);
            
            item.Received().Load(Arg.Any<ItemSaveData>());
        }
    }
}