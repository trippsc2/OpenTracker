using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This is the class containing take any sections.
    /// </summary>
    public class TakeAnySection : ITakeAnySection
    {
        private readonly List<RequirementNodeConnection> _connections;

        public string Name =>
            "Take Any";
        public IRequirement Requirement { get; }
        public bool UserManipulated { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

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

        private int _available;
        public int Available
        {
            get => _available;
            set
            {
                if (_available != value)
                {
                    _available = value;
                    OnPropertyChanged(nameof(Available));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connections">
        /// The list of connections to this section.
        /// </param>
        /// <param name="requirement">
        /// The requirement for this section to be visible.
        /// </param>
        public TakeAnySection(
            List<RequirementNodeConnection> connections, IRequirement requirement = null)
        {
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));

            if (requirement != null)
            {
                Requirement = requirement;
            }
            else
            {
                Requirement = RequirementDictionary.Instance[RequirementType.None];
            }

            Available = 1;

            SubscribeToConnections();
            UpdateAccessibility();
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
        /// Subscribes to the PropertyChanged event on the IRequirement interface and
        /// RequirementNode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        /// <summary>
        /// Creates subscription to the PropertyChanged event on the RequirementNode class and
        /// IRequirement interface.
        /// </summary>
        private void SubscribeToConnections()
        {
            foreach (RequirementNodeConnection connection in _connections)
            {
                List<RequirementNodeID> nodeSubscriptions = new List<RequirementNodeID>();
                List<IRequirement> requirementSubscriptions = new List<IRequirement>();

                if (!nodeSubscriptions.Contains(connection.FromNode))
                {
                    RequirementNodeDictionary.Instance[connection.FromNode].PropertyChanged +=
                        OnRequirementChanged;
                    nodeSubscriptions.Add(connection.FromNode);
                }

                if (!requirementSubscriptions.Contains(connection.Requirement))
                {
                    connection.Requirement.PropertyChanged += OnRequirementChanged;
                    requirementSubscriptions.Add(connection.Requirement);
                }
            }
        }

        /// <summary>
        /// Updates the accessibility and number of accessible items.
        /// </summary>
        private void UpdateAccessibility()
        {
            AccessibilityLevel finalAccessibility = AccessibilityLevel.None;

            foreach (RequirementNodeConnection connection in _connections)
            {
                AccessibilityLevel nodeAccessibility = AccessibilityLevel.Normal;
                
                nodeAccessibility = (AccessibilityLevel)Math.Min((byte)nodeAccessibility,
                    (byte)RequirementNodeDictionary.Instance[connection.FromNode].Accessibility);

                if (nodeAccessibility < AccessibilityLevel.SequenceBreak)
                {
                    continue;
                }

                AccessibilityLevel requirementAccessibility =
                    connection.Requirement.Accessibility;

                AccessibilityLevel finalConnectionAccessibility =
                    (AccessibilityLevel)Math.Min((byte)nodeAccessibility,
                    (byte)requirementAccessibility);

                if (finalConnectionAccessibility == AccessibilityLevel.Normal)
                {
                    finalAccessibility = AccessibilityLevel.Normal;
                    break;
                }

                if (finalConnectionAccessibility > finalAccessibility)
                {
                    finalAccessibility = finalConnectionAccessibility;
                }
            }

            Accessibility = finalAccessibility;
        }

        /// <summary>
        /// Returns whether the section can be cleared or collected.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the section can be cleared or collected.
        /// </returns>
        public bool CanBeCleared(bool force)
        {
            return IsAvailable() && (force || Accessibility > AccessibilityLevel.Inspect);
        }

        /// <summary>
        /// Returns whether the section can be uncollected.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the section can be uncollected.
        /// </returns>
        public bool CanBeUncleared()
        {
            return Available == 0;
        }

        /// <summary>
        /// Clears the section.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether to override the location logic.
        /// </param>
        public void Clear(bool force)
        {
            Available = 0;
        }

        /// <summary>
        /// Returns whether the location has not been fully collected.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the section has been fully collected.
        /// </returns>
        public bool IsAvailable()
        {
            return Available > 0;
        }

        /// <summary>
        /// Resets the section to its starting values.
        /// </summary>
        public void Reset()
        {
            Available = 1;
        }

        /// <summary>
        /// Returns a new section save data instance for this section.
        /// </summary>
        /// <returns>
        /// A new section save data instance.
        /// </returns>
        public SectionSaveData Save()
        {
            return new SectionSaveData()
            {
                Available = Available,
                UserManipulated = UserManipulated
            };
        }

        /// <summary>
        /// Loads section save data.
        /// </summary>
        public void Load(SectionSaveData saveData)
        {
            if (saveData == null)
            {
                throw new ArgumentNullException(nameof(saveData));
            }

            Available = saveData.Available;
            UserManipulated = saveData.UserManipulated;
        }
    }
}
