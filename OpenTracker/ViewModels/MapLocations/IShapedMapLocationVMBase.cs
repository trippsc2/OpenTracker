using System.Reactive;
using Avalonia.Input;
using ReactiveUI;

namespace OpenTracker.ViewModels.MapLocations;

public interface IShapedMapLocationVMBase : IReactiveObject
{
    double OffsetX { get; }
    double OffsetY { get; }
        
    ReactiveCommand<PointerEventArgs, Unit> HandlePointerEnterCommand { get; }
    ReactiveCommand<PointerEventArgs, Unit> HandlePointerLeaveCommand { get; }
}