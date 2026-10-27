using RxVoid = ReactiveUI.Primitives.RxVoid;
using Avalonia.Input;
using OpenTracker.Models.Locations.Map;
using ReactiveUI;

namespace OpenTracker.ViewModels.MapLocations;

public interface IMapLocationColorProvider : IReactiveObject
{
    string BorderColor { get; }
    string Color { get; }
    ReactiveCommand<PointerEventArgs, RxVoid> HandlePointerEntered { get; }
    ReactiveCommand<PointerEventArgs, RxVoid> HandlePointerExited { get; }

    delegate IMapLocationColorProvider Factory(IMapLocation mapLocation);
}