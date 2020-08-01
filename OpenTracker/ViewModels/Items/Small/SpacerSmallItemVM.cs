using OpenTracker.Models.Requirements;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Items.Small
{
    /// <summary>
    /// This is the ViewModel for the small Items panel control for reserving space.
    /// </summary>
    public class SpacerSmallItemVM : SmallItemVMBase
    {
        private readonly IRequirement _requirement;

        public bool Visible =>
            _requirement.Met;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requirement">
        /// The requirement to be checked for whether the space is reserved.
        /// </param>
        public SpacerSmallItemVM(IRequirement requirement)
        {
            _requirement = requirement ?? throw new ArgumentNullException(nameof(requirement));

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
        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(Visible));
        }
    }
}
