using System.Reactive;
using ReactiveUI;

namespace OpenTracker.ViewModels.Menus;

/// <summary>
/// This interface contains the top menu control ViewModel data.
/// </summary>
public interface ITopMenuVM
{
    ReactiveCommand<Unit, Unit> OpenCommand { get; }
    ReactiveCommand<Unit, Unit> SaveCommand { get; }
    ReactiveCommand<Unit, Unit> SaveAsCommand { get; }
    ReactiveCommand<Unit, Unit> ResetCommand { get; }
    ReactiveCommand<Unit, Unit> UndoCommand { get; }
    ReactiveCommand<Unit, Unit> RedoCommand { get; }
    ReactiveCommand<Unit, Unit> ToggleDisplayAllLocationsCommand { get; }
}