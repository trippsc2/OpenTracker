using System.Collections.ObjectModel;
using Avalonia.Media;
using OpenTracker.Utils.Dialog;
using ReactiveUI;

namespace OpenTracker.ViewModels.Capture
{
    public class CaptureWindowVM : DialogViewModelBase, ICaptureWindowVM
    {
        public string Title => $"OpenTracker - {Name}";
        
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                this.RaiseAndSetIfChanged(ref _name, value);
                this.RaisePropertyChanged(nameof(Title));
            }
        }

        private SolidColorBrush? _backgroundColor;
        public SolidColorBrush? BackgroundColor
        {
            get => _backgroundColor;
            set => this.RaiseAndSetIfChanged(ref _backgroundColor, value);
        }

        private double? _height;
        public double? Height
        {
            get => _height;
            set => this.RaiseAndSetIfChanged(ref _height, value);
        }

        private double? _width;
        public double? Width
        {
            get => _width;
            set => this.RaiseAndSetIfChanged(ref _width, value);
        }

        public ObservableCollection<ICaptureControlVM> Contents { get; } =
            new();

        public CaptureWindowVM(string name)
        {
            _name = name;
        }
    }
}