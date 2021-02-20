using ReactiveUI;
using System.Reactive;

namespace OpenTracker.ViewModels
{
    public interface ITopMenuVM
    {
        ReactiveCommand<Unit, Unit> ResetCommand { get; }
        ReactiveCommand<Unit, Unit> UndoCommand { get; }
        ReactiveCommand<Unit, Unit> RedoCommand { get; }
        ReactiveCommand<Unit, Unit> ToggleDisplayAllLocationsCommand { get; }
    }
}
