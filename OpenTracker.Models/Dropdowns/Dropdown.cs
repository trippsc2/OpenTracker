using System.ComponentModel;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Dropdowns;
using ReactiveUI;

namespace OpenTracker.Models.Dropdowns
{
    /// <summary>
    /// This class contains dropdown data.
    /// </summary>
    public class Dropdown : ReactiveObject, IDropdown
    {
        private readonly IRequirement _requirement;

        private readonly ICheckDropdown.Factory _checkDropdownFactory;
        private readonly IUncheckDropdown.Factory _uncheckDropdownFactory;

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
        /// <param name="checkDropdownFactory">
        /// An Autofac factory for creating undoable actions to check the dropdown.
        /// </param>
        /// <param name="uncheckDropdownFactory">
        /// An Autofac factory for creating undoable actions to uncheck the dropdown.
        /// </param>
        /// <param name="requirement">
        /// The requirement for the dropdown to be relevant.
        /// </param>
        public Dropdown(
            ICheckDropdown.Factory checkDropdownFactory, IUncheckDropdown.Factory uncheckDropdownFactory,
            IRequirement requirement)
        {
            _requirement = requirement;
            _checkDropdownFactory = checkDropdownFactory;
            _uncheckDropdownFactory = uncheckDropdownFactory;

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
        /// Returns a new undoable action to check the dropdown.
        /// </summary>
        /// <returns>
        /// A new undoable action to check the dropdown.
        /// </returns>
        public IUndoable CreateCheckDropdownAction()
        {
            return _checkDropdownFactory(this);
        }

        /// <summary>
        /// Returns a new undoable action to uncheck the dropdown.
        /// </summary>
        /// <returns>
        /// A new undoable action to uncheck the dropdown.
        /// </returns>
        public IUndoable CreateUncheckDropdownAction()
        {
            return _uncheckDropdownFactory(this);
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
