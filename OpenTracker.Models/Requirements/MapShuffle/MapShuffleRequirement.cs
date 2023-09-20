using System.ComponentModel;
using OpenTracker.Models.Modes;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.MapShuffle;

/// <summary>
/// This class contains the <see cref="IMode.MapShuffle"/> <see cref="IRequirement"/> data.
/// </summary>
[DependencyInjection]
public sealed class MapShuffleRequirement : BooleanRequirement, IMapShuffleRequirement
{
    private readonly IMode _mode;
    private readonly bool _expectedValue;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mode">
    ///     The <see cref="IMode"/> data.
    /// </param>
    /// <param name="expectedValue">
    ///     A <see cref="bool"/> representing the expected <see cref="IMode.MapShuffle"/> value.
    /// </param>
    public MapShuffleRequirement(IMode mode, bool expectedValue)
    {
        _mode = mode;
        _expectedValue = expectedValue;

        _mode.PropertyChanged += OnModeChanged;

        UpdateValue();
    }

    /// <summary>
    ///     Subscribes to the PropertyChanged event on the IMode interface.
    /// </summary>
    /// <param name="sender">
    ///     The sending object of the event.
    /// </param>
    /// <param name="e">
    ///     The arguments of the PropertyChanged event.
    /// </param>
    private void OnModeChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IMode.MapShuffle))
        {
            UpdateValue();
        }
    }

    protected override bool ConditionMet()
    {
        return _mode.MapShuffle == _expectedValue;
    }
}