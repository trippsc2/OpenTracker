using System.ComponentModel;
using OpenTracker.Models.Modes;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.Mode;

/// <summary>
/// This class contains <see cref="IMode.WorldState"/> <see cref="IRequirement"/> data.
/// </summary>
[DependencyInjection]
public sealed class WorldStateRequirement : BooleanRequirement, IWorldStateRequirement
{
    private readonly IMode _mode;
    private readonly WorldState _expectedValue;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mode">
    ///     The <see cref="IMode"/> data.
    /// </param>
    /// <param name="expectedValue">
    ///     A <see cref="WorldState"/> representing the expected <see cref="IMode.WorldState"/> value.
    /// </param>
    public WorldStateRequirement(IMode mode, WorldState expectedValue)
    {
        _mode = mode;
        _expectedValue = expectedValue;

        _mode.PropertyChanged += OnModeChanged;

        UpdateValue();
    }

    /// <summary>
    /// Subscribes to the <see cref="IMode.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnModeChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IMode.WorldState))
        {
            UpdateValue();
        }
    }

    protected override bool ConditionMet()
    {
        return _mode.WorldState == _expectedValue;
    }
}