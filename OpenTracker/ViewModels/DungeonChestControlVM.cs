using Avalonia.Media;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Actions;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Globalization;

namespace OpenTracker.ViewModels
{
    public class DungeonChestControlVM : ViewModelBase, IClickHandler
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly AppSettings _appSettings;
        private readonly Game _game;
        private readonly ISection _section;

        public string FontColor =>
            _section.Available == 0 ? "#ffffffff" : _appSettings.AccessibilityColors[_section.Accessibility];

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
                            return "avares://OpenTracker/Assets/Images/chest0.png";
                        case AccessibilityLevel.Partial:
                        case AccessibilityLevel.SequenceBreak:
                        case AccessibilityLevel.Normal:
                            return "avares://OpenTracker/Assets/Images/chest1.png";
                    }
                }
                else
                    return "avares://OpenTracker/Assets/Images/chest2.png";

                return null;
            }
        }

        public string NumberString =>
            _section.Available.ToString(CultureInfo.InvariantCulture);

        public DungeonChestControlVM(UndoRedoManager undoRedoManager, AppSettings appSettings,
            Game game, ISection section)
        {
            _undoRedoManager = undoRedoManager;
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _section = section ?? throw new ArgumentNullException(nameof(section));

            _appSettings.AccessibilityColors.PropertyChanged += OnColorChanged;
            _section.PropertyChanged += OnSectionChanged;
        }

        private void OnColorChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateTextColor();
        }

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

        private void UpdateTextColor()
        {
            this.RaisePropertyChanged(nameof(FontColor));
        }

        private void UpdateImage()
        {
            this.RaisePropertyChanged(nameof(ImageSource));
        }

        private void CollectSection()
        {
            _undoRedoManager.Execute(new CollectSection(_game, _section));
        }

        private void UncollectSection()
        {
            _undoRedoManager.Execute(new UncollectSection(_section));
        }

        public void OnLeftClick(bool force)
        {
            if ((force || _section.Accessibility >= AccessibilityLevel.Partial) &&
                _section.IsAvailable())
                CollectSection();
        }

        public void OnRightClick(bool force)
        {
            if (_section is ItemSection itemSection)
            {
                if (_section.Available < itemSection.Total)
                    UncollectSection();
            }
        }
    }
}
