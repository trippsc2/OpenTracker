using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Requirements.KeyDoor;

/// <summary>
/// This class contains <see cref="IKeyDoor"/> <see cref="IRequirement"/> data.
/// </summary>
[DependencyInjection]
public sealed class KeyDoorRequirement : ReactiveObject, IRequirement
{
    private readonly CompositeDisposable _disposables = new();
    
    private IKeyDoor KeyDoor { get; }
    
    [ObservableAsProperty]
    public bool Met { get; }
    [ObservableAsProperty]
    public AccessibilityLevel Accessibility { get; }

    public event EventHandler? ChangePropagated;

    /// <summary>
    /// A factory method for creating new <see cref="KeyDoorRequirement"/> objects.
    /// </summary>
    public delegate KeyDoorRequirement Factory(IKeyDoor keyDoor);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="keyDoor">
    ///     The <see cref="IKeyDoor"/>.
    /// </param>
    public KeyDoorRequirement(IKeyDoor keyDoor)
    {
        KeyDoor = keyDoor;

        this.WhenAnyValue(x => x.KeyDoor.Unlocked)
            .ToPropertyEx(this, x => x.Met)
            .DisposeWith(_disposables);
        this.WhenAnyValue(x => x.Met)
            .Select(x => x ? AccessibilityLevel.Normal : AccessibilityLevel.None)
            .ToPropertyEx(this, x => x.Accessibility)
            .DisposeWith(_disposables);
        this.WhenAnyValue(x => x.Accessibility)
            .Subscribe(_ => ChangePropagated?.Invoke(this, EventArgs.Empty))
            .DisposeWith(_disposables);
    }
    
    public void Dispose()
    {
        _disposables.Dispose();
    }
}