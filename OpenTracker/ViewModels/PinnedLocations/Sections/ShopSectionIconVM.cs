using OpenTracker.Interfaces;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Reactive;
using Avalonia.Input;

namespace OpenTracker.ViewModels.PinnedLocations.Sections
{
    /// <summary>
    /// This class contains the shop section icon control ViewModel data.
    /// </summary>
    public class ShopSectionIconVM : ViewModelBase, ISectionIconVMBase
    {
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly IShopSection _section;

        public string ImageSource
        {
            get
            {
                if (!_section.IsAvailable())
                {
                    return "avares://OpenTracker/Assets/Images/chest2.png";
                }
                
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

                return "avares://OpenTracker/Assets/Images/chest2.png";
            }
        }
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

        public delegate ShopSectionIconVM Factory(IShopSection section);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="undoableFactory">
        /// A factory for creating undoable actions.
        /// </param>
        /// <param name="section">
        /// The take any section to be represented.
        /// </param>
        public ShopSectionIconVM(
            IUndoRedoManager undoRedoManager, IUndoableFactory undoableFactory, IShopSection section)
        {
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _section = section;
            
            HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);

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
        /// Creates an undoable action to collect the section and sends it to the undo/redo manager.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        private void CollectSection(bool force)
        {
            _undoRedoManager.Execute(_undoableFactory.GetCollectSection(_section, force));
        }

        /// <summary>
        /// Creates an undoable action to un-collect the section and send it to the undo/redo manager.
        /// </summary>
        private void UncollectSection()
        {
            _undoRedoManager.Execute(_undoableFactory.GetUncollectSection(_section));
        }

        /// <summary>
        /// Handles clicking the control.
        /// </summary>
        /// <param name="e">
        /// The PointerReleased event args.
        /// </param>
        private void HandleClick(PointerReleasedEventArgs e)
        {
            switch (e.InitialPressMouseButton)
            {
                case MouseButton.Left:
                {
                    CollectSection((e.KeyModifiers & KeyModifiers.Control) > 0);
                }
                    break;
                case MouseButton.Right:
                {
                    UncollectSection();
                }
                    break;
            }
        }
    }
}
