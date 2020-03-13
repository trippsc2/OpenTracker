using Avalonia.Media;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using ReactiveUI;
using System;

namespace OpenTracker.ViewModels
{
    public class SectionControlVM : ViewModelBase
    {
        private readonly AppSettingsVM _appSettings;
        private readonly Game _game;
        private readonly ISection _section;

        public string Name { get; }

        private IBrush _fontColor;
        public IBrush FontColor
        {
            get => _fontColor;
            set => this.RaiseAndSetIfChanged(ref _fontColor, value);
        }

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set => this.RaiseAndSetIfChanged(ref _imageSource, value);
        }

        private string _numberString;
        public string NumberString
        {
            get => _numberString;
            set => this.RaiseAndSetIfChanged(ref _numberString, value);
        }

        public SectionControlVM(AppSettingsVM appSettings, Game game, ISection section)
        {
            _appSettings = appSettings;
            _game = game;
            _section = section;
            Name = section.Name;

            _section.ItemRequirementChanged += OnItemRequirementChanged;

            Update();
        }

        private void OnItemRequirementChanged(object sender, EventArgs e)
        {
            Update();
        }

        private void Update()
        {
            Accessibility accessibility = _section.GetAccessibility(_game.Mode, _game.Items);

            if (accessibility == Accessibility.Normal)
                FontColor = new SolidColorBrush(new Color(255, 245, 245, 245));
            else
                FontColor = _appSettings.AccessibilityColors[accessibility];

            if (_section is ItemSection)
            {
                switch (accessibility)
                {
                    case Accessibility.None:
                    case Accessibility.Inspect:
                        ImageSource = "avares://OpenTracker/Assets/Images/chest0.png";
                        break;
                    case Accessibility.SequenceBreak:
                    case Accessibility.Normal:
                        ImageSource = "avares://OpenTracker/Assets/Images/chest1.png";
                        break;
                    case Accessibility.Cleared:
                        ImageSource = "avares://OpenTracker/Assets/Images/chest2.png";
                        break;
                }

                NumberString = ((ItemSection)_section).Available.ToString();
            }
        }
    }
}
