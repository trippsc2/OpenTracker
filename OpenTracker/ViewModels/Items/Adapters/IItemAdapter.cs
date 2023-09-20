using System.Reactive;
using Avalonia.Input;
using Avalonia.Media;
using OpenTracker.Utils;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;

namespace OpenTracker.ViewModels.Items.Adapters;

/// <summary>
/// This interface contains the logic to adapt data for an item control.
/// </summary>
public interface IItemAdapter : IViewModel
{
    string ImageSource { get; }
    string? Label { get; }
    SolidColorBrush? LabelColor { get; }
    BossSelectPopupVM? BossSelect { get; }
    
    ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }
}