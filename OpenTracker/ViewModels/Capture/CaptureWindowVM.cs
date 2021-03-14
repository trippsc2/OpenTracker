using System.Collections.ObjectModel;
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
        
        private bool _open;
        public bool Open
        {
            get => _open;
            set => this.RaiseAndSetIfChanged(ref _open, value);
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
            new ObservableCollection<ICaptureControlVM>();

        public CaptureWindowVM(string name)
        {
            _name = name;
        }
    }
}