using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels.SectionControls
{
    /// <summary>
    /// This is the ViewModel of the section icon control representing a take any section.
    /// </summary>
    public class TakeAnySectionIconControlVM : SectionIconControlVMBase, IClickHandler
    {
        private readonly ITakeAnySection _section;

        public string ImageSource
        {
            get
            {
                if (_section.IsAvailable())
                {
                    switch (_section.Accessibility)
                    {
                        case AccessibilityLevel.None:
                        case AccessibilityLevel.Inspect:
                            {
                                return "avares://OpenTracker/Assets/Images/chest0.png";
                            }
                        case AccessibilityLevel.Partial:
                        case AccessibilityLevel.SequenceBreak:
                        case AccessibilityLevel.Normal:
                            {
                                return "avares://OpenTracker/Assets/Images/chest1.png";
                            }
                    }
                }

                return "avares://OpenTracker/Assets/Images/chest2.png";
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="section">
        /// The take any section to be represented.
        /// </param>
        public TakeAnySectionIconControlVM(ITakeAnySection section)
        {
            _section = section ?? throw new ArgumentNullException(nameof(section));

            _section.PropertyChanged += OnSectionChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ISection interface.
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
