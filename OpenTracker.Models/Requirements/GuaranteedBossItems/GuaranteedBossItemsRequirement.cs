using System.ComponentModel;
using OpenTracker.Models.Modes;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.GuaranteedBossItems;

/// <summary>
/// This class contains <see cref="IMode.GuaranteedBossItems"/> requirement data.
/// </summary>
[DependencyInjection]
public sealed class GuaranteedBossItemsRequirement : BooleanRequirement, IGuaranteedBossItemsRequirement
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
    ///     A <see cref="bool"/> representing the expected <see cref="IMode.GuaranteedBossItems"/> value.
    /// </param>
    public GuaranteedBossItemsRequirement(IMode mode, bool expectedValue)
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
        if (e.PropertyName == nameof(IMode.GuaranteedBossItems))
        {
            UpdateValue();
        }
    }

    protected override bool ConditionMet()
    {
        return _mode.GuaranteedBossItems == _expectedValue;
    }
}