using System;
using ReactiveUI;

namespace OpenTracker.Utils;

/// <summary>
/// This is the base class for all ViewModel data.
/// </summary>
public abstract class ViewModel : ReactiveObject, IActivatableViewModel
{
    public ViewModelActivator Activator { get; } = new();

    public virtual Type GetViewType()
    {
        return typeof(IViewFor<>).MakeGenericType(GetType());
    }
}