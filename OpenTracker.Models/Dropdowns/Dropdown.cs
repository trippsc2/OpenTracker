using System.ComponentModel;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using ReactiveUI;

namespace OpenTracker.Models.Dropdowns
{
    /// <summary>
    /// This class contains dropdown data.
    /// </summary>
    public class Dropdown : ReactiveObject, IDropdown
    {
        private readonly IRequirement _requirement;

        public bool RequirementMet => _requirement.Met;

        private bool _checked;
        public bool Checked
        {
            get => _checked;
            set => this.RaiseAndSetIfChanged(ref _checked, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requirement">
        /// The requirement for the dropdown to be relevant.
        /// </param>
        public Dropdown(IRequirement requirement)
        {
            _requirement = requirement;

            _requirement.PropertyChanged += OnRequirementChanged;
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
        private void OnRequirementChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirement.Met))
            {
                this.RaisePropertyChanged(nameof(RequirementMet));
            }
        }

        /// <summary>
        /// Resets the dropdown.
        /// </summary>
        public void Reset()
        {
            Checked = false;
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
        public void Load(DropdownSaveData? saveData)
        {
            if (saveData == null)
            {
                return;
            }

            Checked = saveData.Checked;
        }
    }
}
