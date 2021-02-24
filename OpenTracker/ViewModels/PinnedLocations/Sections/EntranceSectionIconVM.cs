using OpenTracker.Interfaces;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels.PinnedLocations.Sections
{
    /// <summary>
    /// This is the ViewModel of the section icon control representing an entrance.
    /// </summary>
    public class EntranceSectionIconVM : ViewModelBase, ISectionIconVMBase, IClickHandler
    {
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly IEntranceSection _section;

        public string ImageSource =>
            _section.IsAvailable() ? "avares://OpenTracker/Assets/Images/door0.png" :
            "avares://OpenTracker/Assets/Images/door1.png";

        public delegate EntranceSectionIconVM Factory(IEntranceSection section);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="section">
        /// The entrance section to be represented.
        /// </param>
        public EntranceSectionIconVM(
            IUndoRedoManager undoRedoManager, IUndoableFactory undoableFactory,
            IEntranceSection section)
        {
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _section = section;

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
            _undoRedoManager.Execute(_undoableFactory.GetCollectSection(_section, force));
        }

        /// <summary>
        /// Handles right clicks and uncollects the section.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force)
        {
            _undoRedoManager.Execute(_undoableFactory.GetUncollectSection(_section));
        }
    }
}
