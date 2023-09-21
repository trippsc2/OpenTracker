using System.ComponentModel;
using System.Reactive;
using Avalonia.Input;
using ReactiveUI;

namespace OpenTracker.ViewModels.MapLocations;

public interface IShapedMapLocationVMBase : INotifyPropertyChanged
{
    double OffsetX { get; }
    double OffsetY { get; }
        
    ReactiveCommand<PointerEventArgs, Unit> HandlePointerEnterCommand { get; }
    ReactiveCommand<PointerEventArgs, Unit> HandlePointerLeaveCommand { get; }
}