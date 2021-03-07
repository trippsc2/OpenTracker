using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.KeyDoors
{
    /// <summary>
    /// This class contains key door data.
    /// </summary>
    public class KeyDoor : IKeyDoor
    {
        private readonly IKeyDoorFactory _factory;
        private readonly IMutableDungeon _dungeonData;
        private IRequirementNode? _node;

        public IRequirement Requirement { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _unlocked;
        public bool Unlocked
        {
            get => _unlocked;
            set
            {
                if (_unlocked != value)
                {
                    _unlocked = value;
                    OnPropertyChanged(nameof(Unlocked));
                }
            }
        }

        public AccessibilityLevel Accessibility
        {
            get
            {
                if (_node == null)
                {
                    return AccessibilityLevel.None;
                }

                return _node.Accessibility;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// The key door factory.
        /// </param>
        /// <param name="requirementFactory">
        /// The key door requirement factory.
        /// </param>
        /// <param name="dungeonData">
        /// The mutable dungeon data parent class.
        /// </param>
        public KeyDoor(
            IKeyDoorFactory factory, KeyDoorRequirement.Factory requirementFactory, IMutableDungeon dungeonData)
        {
            _factory = factory;
            _dungeonData = dungeonData;

            Requirement = requirementFactory(this);

            _dungeonData.KeyDoors.ItemCreated += OnDoorCreated;
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
        /// Subscribes to the ItemCreated event on the IKeyDoorDictionary interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the ItemCreated event.
        /// </param>
        private void OnDoorCreated(object? sender, KeyValuePair<KeyDoorID, IKeyDoor> e)
        {
            if (e.Value == this)
            {
                _dungeonData.KeyDoors.ItemCreated -= OnDoorCreated;
                _node = _factory.GetKeyDoorNode(e.Key, _dungeonData);

                if (_node != null)
                {
                    _node.PropertyChanged += OnNodeChanged;
                }

                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IRequirement interface and
        /// RequirementNode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnNodeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirementNode.Accessibility))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Updates the accessibility of the key door.
        /// </summary>
        private void UpdateAccessibility()
        {
            OnPropertyChanged(nameof(Accessibility));
        }
    }
}
