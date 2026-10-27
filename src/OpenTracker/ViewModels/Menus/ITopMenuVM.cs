using RxVoid = ReactiveUI.Primitives.RxVoid;
using System;
using ReactiveUI;

namespace OpenTracker.ViewModels.Menus;

/// <summary>
/// This interface contains the top menu control ViewModel data.
/// </summary>
public interface ITopMenuVM
{
    ReactiveCommand<RxVoid, RxVoid> Reset { get; }
    ReactiveCommand<RxVoid, RxVoid> Undo { get; }
    ReactiveCommand<RxVoid, RxVoid> Redo { get; }
    ReactiveCommand<RxVoid, RxVoid> ToggleDisplayAllLocations { get; }
    ReactiveCommand<RxVoid, RxVoid> Open { get; }
    ReactiveCommand<RxVoid, RxVoid> Save { get; }
    ReactiveCommand<RxVoid, RxVoid> SaveAs { get; }

    delegate ITopMenuVM Factory(Action closeAction);
}