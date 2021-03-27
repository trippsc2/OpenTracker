using System.Collections.Generic;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.RequirementNodes;
using ReactiveUI;

namespace OpenTracker.Models.DungeonItems
{
    /// <summary>
    /// This class contains mutable dungeon item data.
    /// </summary>
    public class DungeonItem : ReactiveObject, IDungeonItem
    {
        private readonly IMutableDungeon _dungeonData;
        private readonly IRequirementNode _node;

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set => this.RaiseAndSetIfChanged(ref _accessibility, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dungeonData">
        /// The mutable dungeon data parent class.
        /// </param>
        /// <param name="node">
        /// The dungeon node to which this item belongs.
        /// </param>
        public DungeonItem(IMutableDungeon dungeonData, IRequirementNode node)
        {
            _dungeonData = dungeonData;
            _node = node;

            _dungeonData.DungeonItems.ItemCreated += OnDungeonItemCreated;
        }

        /// <summary>
        /// Subscribes to the DungeonItemCreated event on the IDungeonItemDictionary interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the DungeonItemCreated event.
        /// </param>
        private void OnDungeonItemCreated(object? sender, KeyValuePair<DungeonItemID, IDungeonItem> e)
        {
            if (e.Value != this)
            {
                return;
            }
            
            _dungeonData.DungeonItems.ItemCreated -= OnDungeonItemCreated;
            _node.PropertyChanged += OnNodeChanged;
            UpdateAccessibility();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IDungeonNode interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnNodeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IDungeonNode.Accessibility))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Updates the accessibility of the item.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility = _node.Accessibility;
        }
    }
}
