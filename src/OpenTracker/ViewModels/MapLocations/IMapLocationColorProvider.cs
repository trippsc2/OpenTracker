using System.Reactive;
using Avalonia.Input;
using OpenTracker.Models.Locations.Map;
using ReactiveUI;

namespace OpenTracker.ViewModels.MapLocations;

public interface IMapLocationColorProvider : IReactiveObject
{
    string BorderColor { get; }
    string Color { get; }
    ReactiveCommand<PointerEventArgs, Unit> HandlePointerEntered { get; }
    ReactiveCommand<PointerEventArgs, Unit> HandlePointerExited { get; }

    delegate IMapLocationColorProvider Factory(IMapLocation mapLocation);
}