﻿using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils.Autofac;
using ReactiveUI;

namespace OpenTracker.Models.SequenceBreaks;

/// <summary>
/// This class contains sequence break data.
/// </summary>
[DependencyInjection]
public sealed class SequenceBreak : ReactiveObject, ISequenceBreak
{
    private bool _enabled;
    public bool Enabled
    {
        get => _enabled;
        set => this.RaiseAndSetIfChanged(ref _enabled, value);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="starting">
    ///     A <see cref="bool"/> representing the starting <see cref="Enabled"/> property value.
    /// </param>
    public SequenceBreak(bool starting = true)
    {
        Enabled = starting;
    }

    public SequenceBreakSaveData Save()
    {
        return new SequenceBreakSaveData {Enabled = Enabled};
    }

    public void Load(SequenceBreakSaveData? saveData)
    {
        if (saveData is null)
        {
            return;
        }
            
        Enabled = saveData.Enabled;
    }
}