using OpenTracker.Models;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using ReactiveUI;

namespace OpenTracker.ViewModels
{
    public class SectionControlVM : ViewModelBase
    {
        private readonly Game _game;
        private readonly ISection _section;

        public string Name { get; }

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set => this.RaiseAndSetIfChanged(ref _imageSource, value);
        }

        public SectionControlVM(Game game, ISection section)
        {
            _game = game;
            _section = section;
            Name = section.Name;

            SetImage();
        }

        private void SetImage()
        {
            if (_section is ItemSection)
            {
                switch (_section.GetAccessibility(_game.Mode, _game.Items))
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
            }
        }
    }
}
