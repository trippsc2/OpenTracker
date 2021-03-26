using System;
using System.Collections.Generic;
using System.ComponentModel;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.RequirementNodes;
using Xunit;

namespace OpenTracker.UnitTests.Models.DungeonItems
{
    public class DungeonItemTests
    {
        private readonly IRequirementNode _node;
        private readonly DungeonItem _sut;

        public DungeonItemTests()
        {
            var dungeonData = Substitute.For<IMutableDungeon>();
            _node = Substitute.For<IRequirementNode>();
            _node.Accessibility.Returns(AccessibilityLevel.None);
            _sut = new DungeonItem(dungeonData, _node);
            dungeonData.DungeonItems.ItemCreated +=
                Raise.Event<EventHandler<KeyValuePair<DungeonItemID, IDungeonItem>>>(
                    dungeonData.DungeonItems,
                    new KeyValuePair<DungeonItemID, IDungeonItem>(DungeonItemID.HCSanctuary, _sut));
        }

        [Fact]
        public void Accessibility_ShouldRaisePropertyChanged()
        {
            _node.Accessibility.Returns(AccessibilityLevel.Normal);
            
            Assert.PropertyChanged(_sut, nameof(IDungeonItem.Accessibility),
                () => _node.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _node, new PropertyChangedEventArgs(nameof(IDungeonNode.Accessibility))));
        }
    }
}