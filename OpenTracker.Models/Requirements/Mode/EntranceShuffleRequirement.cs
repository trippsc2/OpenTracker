using System.ComponentModel;
using OpenTracker.Models.Modes;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.Mode;

/// <summary>
/// This class contains <see cref="IMode.EntranceShuffle"/> <see cref="IRequirement"/> data.
/// </summary>
[DependencyInjection]
public sealed class EntranceShuffleRequirement : BooleanRequirement, IEntranceShuffleRequirement
{
    private readonly IMode _mode;
    private readonly EntranceShuffle _expectedValue;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mode">
    ///     The <see cref="IMode"/> data.
    /// </param>
    /// <param name="expectedValue">
    ///     A <see cref="EntranceShuffle"/> representing the expected <see cref="IMode.EntranceShuffle"/> value.
    /// </param>
    public EntranceShuffleRequirement(IMode mode, EntranceShuffle expectedValue)
    {
        _mode = mode;
        _expectedValue = expectedValue;

        _mode.PropertyChanged += OnModeChanged;

        UpdateValue();
    }

    /// <summary>
    /// Subscribes to the PropertyChanged event on the IMode interface.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnModeChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IMode.EntranceShuffle))
        {
            UpdateValue();
        }
    }

    protected override bool ConditionMet()
    {
        return _mode.EntranceShuffle == _expectedValue;
    }
}