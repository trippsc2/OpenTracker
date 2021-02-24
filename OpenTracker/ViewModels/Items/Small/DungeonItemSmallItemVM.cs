using OpenTracker.Interfaces;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Sections;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Globalization;

namespace OpenTracker.ViewModels.Items.Small
{
    /// <summary>
    /// This is the ViewModel for the small Items panel control representing dungeon items.
    /// </summary>
    public class DungeonItemSmallItemVM : ViewModelBase, ISmallItemVMBase, IClickHandler
    {
        private readonly IColorSettings _colorSettings;
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly ISection _section;

        public string FontColor =>
            _section.Available == 0 ? "#ffffffff" :
            _colorSettings.AccessibilityColors[_section.Accessibility];
        public string ImageSource
        {
            get
            {
                if (_section.IsAvailable())
                {
                    return _section.Accessibility switch
                    {
                        AccessibilityLevel.None => "avares://OpenTracker/Assets/Images/chest0.png",
                        AccessibilityLevel.Inspect => "avares://OpenTracker/Assets/Images/chest0.png",
                        _ => "avares://OpenTracker/Assets/Images/chest1.png"
                    };
                }
                
                return "avares://OpenTracker/Assets/Images/chest2.png";
            }
        }
        public string NumberString =>
            _section.Available.ToString(CultureInfo.InvariantCulture);

        public delegate DungeonItemSmallItemVM Factory(ISection section);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="section">
        /// The dungeon section to be represented.
        /// </param>
        public DungeonItemSmallItemVM(
            IColorSettings colorSettings, IUndoRedoManager undoRedoManager,
            IUndoableFactory undoableFactory, ISection section)
        {
            _colorSettings = colorSettings;
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _section = section;

            _colorSettings.AccessibilityColors.PropertyChanged += OnColorChanged;
            _section.PropertyChanged += OnSectionChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ObservableCollection class for the
        /// accessibility colors.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnColorChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateTextColor();
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
            if (e.PropertyName == nameof(ISection.Accessibility))
            {
                UpdateTextColor();
                UpdateImage();
            }

            if (e.PropertyName == nameof(ISection.Available))
            {
                this.RaisePropertyChanged(nameof(NumberString));
                UpdateTextColor();
                UpdateImage();
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the FontColor property.
        /// </summary>
        private void UpdateTextColor()
        {
            this.RaisePropertyChanged(nameof(FontColor));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the ImageSource property.
        /// </summary>
        private void UpdateImage()
        {
            this.RaisePropertyChanged(nameof(ImageSource));
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
