using System.Reactive;
using Avalonia.Input;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;

namespace OpenTracker.ViewModels.Items.Adapters;

/// <summary>
/// This interface contains the logic to adapt data for an item control.
/// </summary>
public interface IItemAdapter : IReactiveObject
{
    string ImageSource { get; }
    string? Label { get; }
    ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }
    string LabelColor { get; }
    IBossSelectPopupVM? BossSelect { get; }
}