using OpenTracker.Interfaces;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels.UIPanels.LocationsPanel.PinnedLocations.Sections.SectionIcons
{
    /// <summary>
    /// This is the ViewModel of the section icon control representing an entrance.
    /// </summary>
    public class EntranceSectionIconVM : SectionIconVMBase, IClickHandler
    {
        private readonly IEntranceSection _section;

        public string ImageSource =>
            _section.IsAvailable() ? "avares://OpenTracker/Assets/Images/door0.png" :
            "avares://OpenTracker/Assets/Images/door1.png";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="section">
        /// The entrance section to be represented.
        /// </param>
        public EntranceSectionIconVM(IEntranceSection section)
        {
            _section = section ?? throw new ArgumentNullException(nameof(section));

            _section.PropertyChanged += OnSectionChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IBossSection interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISection.Accessibility) ||
                e.PropertyName == nameof(ISection.Available))
            {
                this.RaisePropertyChanged(nameof(ImageSource));
            }
        }

        /// <summary>
        /// Handles left clicks and collects the section.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force)
        {
            UndoRedoManager.Instance.Execute(new CollectSection(_section, force));
        }

        /// <summary>
        /// Handles right clicks and uncollects the section.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force)
        {
            UndoRedoManager.Instance.Execute(new UncollectSection(_section));
        }
    }
}
