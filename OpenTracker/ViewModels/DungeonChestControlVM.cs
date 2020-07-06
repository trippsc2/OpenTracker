using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Undoables;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Sections;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Globalization;
using OpenTracker.ViewModels.Bases;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the view-model for the dungeon chest control on the items panel.
    /// </summary>
    public class DungeonChestControlVM : SmallItemControlVMBase, IClickHandler
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly AppSettings _appSettings;
        private readonly ISection _section;

        public string FontColor =>
            _section.Available == 0 ? "#ffffffff" :
            _appSettings.AccessibilityColors[_section.Accessibility];

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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="appSettings">
        /// The app settings data.
        /// </param>
        /// <param name="section">
        /// The dungeon section represented by the control.
        /// </param>
        public DungeonChestControlVM(
            UndoRedoManager undoRedoManager, AppSettings appSettings, ISection section)
        {
            _undoRedoManager = undoRedoManager ?? throw new ArgumentNullException(nameof(undoRedoManager));
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _section = section ?? throw new ArgumentNullException(nameof(section));

            _appSettings.AccessibilityColors.PropertyChanged += OnColorChanged;
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
        /// Subscribes to the PropertyChanged event on the ISection-implementing class.
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
        /// Collects a single item from the section represented.
        /// </summary>
        private void CollectSection()
        {
            _undoRedoManager.Execute(new CollectSection(_section));
        }

        /// <summary>
        /// Uncollects a single item from the section represented.
        /// </summary>
        private void UncollectSection()
        {
            _undoRedoManager.Execute(new UncollectSection(_section));
        }

        /// <summary>
        /// Click handler for left click.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force)
        {
            if ((force || _section.Accessibility >= AccessibilityLevel.Partial) &&
                _section.IsAvailable())
            {
                CollectSection();
            }
        }

        /// <summary>
        /// Click handler for right click.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force)
        {
            if (_section is DungeonItemSection dungeonItemSection && 
                _section.Available < dungeonItemSection.Total)
            {
                UncollectSection();
            }
        }
    }
}
