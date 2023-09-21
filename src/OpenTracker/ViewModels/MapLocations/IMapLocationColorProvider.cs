using System.Reactive;
using Avalonia.Input;
using Avalonia.Media;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.MapLocations;

public interface IMapLocationColorProvider : IViewModel
{
    SolidColorBrush BorderColor { get; }
    SolidColorBrush Color { get; }
    ReactiveCommand<PointerEventArgs, Unit> HandlePointerEnterCommand { get; }
    ReactiveCommand<PointerEventArgs, Unit> HandlePointerLeaveCommand { get; }

    delegate IMapLocationColorProvider Factory(IMapLocation mapLocation);
}