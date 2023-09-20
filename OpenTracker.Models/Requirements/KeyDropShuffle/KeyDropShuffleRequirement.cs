using System.ComponentModel;
using OpenTracker.Models.Modes;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.KeyDropShuffle;

/// <summary>
/// This class contains the <see cref="IMode.KeyDropShuffle"/> <see cref="IRequirement"/> data.
/// </summary>
[DependencyInjection]
public sealed class KeyDropShuffleRequirement : BooleanRequirement, IKeyDropShuffleRequirement
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
    ///     A <see cref="bool"/> representing the expected <see cref="IMode.KeyDropShuffle"/> value.
    /// </param>
    public KeyDropShuffleRequirement(IMode mode, bool expectedValue)
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
        if (e.PropertyName == nameof(IMode.KeyDropShuffle))
        {
            UpdateValue();
        }
    }

    protected override bool ConditionMet()
    {
        return _mode.KeyDropShuffle == _expectedValue;
    }
}