using System.ComponentModel;
using OpenTracker.Autofac;
using OpenTracker.Models.Settings;

namespace OpenTracker.Models.Requirements.AlwaysDisplayDungeonItems;

/// <summary>
/// This class contains the <see cref="ILayoutSettings.AlwaysDisplayDungeonItems"/> <see cref="IRequirement"/> data.
/// </summary>
[DependencyInjection]
public sealed class AlwaysDisplayDungeonItemsRequirement : BooleanRequirement, IAlwaysDisplayDungeonItemsRequirement
{
    private readonly ILayoutSettings _layoutSettings;
    private readonly bool _expectedValue;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="layoutSettings">
    ///     The <see cref="ILayoutSettings"/>.
    /// </param>
    /// <param name="expectedValue">
    ///     A <see cref="bool"/> representing the expected <see cref="ILayoutSettings.AlwaysDisplayDungeonItems"/>
    ///     value.
    /// </param>
    public AlwaysDisplayDungeonItemsRequirement(ILayoutSettings layoutSettings, bool expectedValue)
    {
        _layoutSettings = layoutSettings;
        _expectedValue = expectedValue;

        _layoutSettings.PropertyChanged += OnLayoutChanged;

        UpdateValue();
    }

    /// <summary>
    /// Subscribes to the <see cref="ILayoutSettings.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnLayoutChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ILayoutSettings.AlwaysDisplayDungeonItems))
        {
            UpdateValue();
        }
    }

    protected override bool ConditionMet()
    {
        return _layoutSettings.AlwaysDisplayDungeonItems == _expectedValue;
    }
}