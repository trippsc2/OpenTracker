using OpenTracker.Models.Requirements;
using OpenTracker.ViewModels.Bases;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the class for reserving space for a missing control in the dungeon area of the
    /// Items panel.
    /// </summary>
    public class SmallItemSpacerControlVM : SmallItemControlVMBase
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
        public SmallItemSpacerControlVM(IRequirement requirement)
        {
            _requirement = requirement ?? throw new ArgumentNullException(nameof(requirement));

            _requirement.PropertyChanged += OnRequirementChanged;
        }

        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(Visible));
        }
    }
}
