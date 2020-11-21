using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Dropdowns
{
    /// <summary>
    /// This is the class for dropdown data.
    /// </summary>
    public class Dropdown : IDropdown
    {
        private readonly IRequirement _requirement;

        public bool RequirementMet =>
            _requirement.Met;

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _checked;
        public bool Checked
        {
            get => _checked;
            set
            {
                if (_checked != value)
                {
                    _checked = value;
                    OnPropertyChanged(nameof(Checked));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requirement">
        /// The requirement for the dropdown to be relevant.
        /// </param>
        public Dropdown(IRequirement requirement)
        {
            _requirement = requirement ?? throw new ArgumentNullException(nameof(requirement));

            _requirement.PropertyChanged += OnRequirementChanged;
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
        /// Subscribes to the PropertyChanged event on the IRequirement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirement.Accessibility))
            {
                OnPropertyChanged(nameof(RequirementMet));
            }
        }

        /// <summary>
        /// Returns a new dropdown save data instance for this dropdown.
        /// </summary>
        /// <returns>
        /// A new dropdown save data instance.
        /// </returns>
        public DropdownSaveData Save()
        {
            return new DropdownSaveData()
            {
                Checked = Checked
            };
        }

        /// <summary>
        /// Loads dropdown save data.
        /// </summary>
        /// <param name="saveData">
        /// The dropdown save data to load.
        /// </param>
        public void Load(DropdownSaveData saveData)
        {
            if (saveData == null)
            {
                throw new ArgumentNullException(nameof(saveData));
            }

            Checked = saveData.Checked;
        }
    }
}
