using RxVoid = ReactiveUI.Primitives.RxVoid;
using Avalonia.Input;
using ReactiveUI;

namespace OpenTracker.ViewModels.MapLocations;

public interface IShapedMapLocationVMBase : IReactiveObject
{
    double OffsetX { get; }
    double OffsetY { get; }
        
    ReactiveCommand<PointerEventArgs, RxVoid> HandlePointerEntered { get; }
    ReactiveCommand<PointerEventArgs, RxVoid> HandlePointerExited { get; }
}