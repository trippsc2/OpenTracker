using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.KeyDoor;
using ReactiveUI;

namespace OpenTracker.Models.Dungeons.KeyDoors
{
    /// <summary>
    ///     This class contains key door data.
    /// </summary>
    public class KeyDoor : ReactiveObject, IKeyDoor
    {
        private readonly INode _node;

        public IRequirement Requirement { get; }

        private bool _unlocked;
        public bool Unlocked
        {
            get => _unlocked;
            set => this.RaiseAndSetIfChanged(ref _unlocked, value);
        }

        public AccessibilityLevel Accessibility => _node.Accessibility; 

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="requirementFactory">
        ///     The key door requirement factory.
        /// </param>
        /// <param name="node">
        ///     The node to which the key door belongs.
        /// </param>
        public KeyDoor(IKeyDoorRequirement.Factory requirementFactory, INode node)
        {
            _node = node;
            
            Requirement = requirementFactory(this);
            
            UpdateAccessibility();

            _node.PropertyChanged += OnNodeChanged;
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the INode interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnNodeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IOverworldNode.Accessibility))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        ///     Updates the accessibility of the key door.
        /// </summary>
        private void UpdateAccessibility()
        {
            this.RaisePropertyChanged(nameof(Accessibility));
        }
    }
}
