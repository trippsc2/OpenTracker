using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.RequirementNodes;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.DungeonItems
{
    /// <summary>
    /// This is the class for mutable dungeon item data.
    /// </summary>
    public class DungeonItem : IDungeonItem
    {
        private readonly IMutableDungeon _dungeonData;
#if DEBUG
        private readonly DungeonItemID _id;
#endif
        private readonly IRequirementNode _node;

        public event PropertyChangedEventHandler? PropertyChanged;

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set
            {
                if (_accessibility != value)
                {
                    _accessibility = value;
                    OnPropertyChanged(nameof(Accessibility));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dungeonData">
        /// The mutable dungeon data parent class.
        /// </param>
        /// <param name="id">
        /// The item identity.
        /// </param>
        /// <param name="node">
        /// The dungeon node to which this item belongs.
        /// </param>
        public DungeonItem(
            IMutableDungeon dungeonData, DungeonItemID id, IRequirementNode node)
        {
            _dungeonData = dungeonData;
#if DEBUG
            _id = id;
#endif
            _node = node;

            _dungeonData.DungeonItems.ItemCreated += OnDungeonItemCreated;
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            if (e.Value == this)
            {
                _dungeonData.DungeonItems.ItemCreated -= OnDungeonItemCreated;
                _node.PropertyChanged += OnNodeChanged;
                UpdateAccessibility();
            }
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
