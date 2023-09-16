using System.ComponentModel;
using Avalonia.Layout;
using OpenTracker.Autofac;
using OpenTracker.Models.Settings;

namespace OpenTracker.Models.Requirements.ItemsPanelOrientation;

/// <summary>
///     This class contains items panel orientation requirement data.
/// </summary>
[DependencyInjection]
public sealed class ItemsPanelOrientationRequirement : BooleanRequirement, IItemsPanelOrientationRequirement
{
    private readonly ILayoutSettings _layoutSettings;
    private readonly Orientation _expectedValue;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="layoutSettings">
    ///     The layout settings.
    /// </param>
    /// <param name="expectedValue">
    ///     The expected orientation value.
    /// </param>
    public ItemsPanelOrientationRequirement(ILayoutSettings layoutSettings, Orientation expectedValue)
    {
        _layoutSettings = layoutSettings;
        _expectedValue = expectedValue;

        _layoutSettings.PropertyChanged += OnLayoutChanged;

        UpdateValue();
    }

    /// <summary>
    ///     Subscribes to the PropertyChanged event on the ILayoutSettings interface.
    /// </summary>
    /// <param name="sender">
    ///     The sending object of the event.
    /// </param>
    /// <param name="e">
    ///     The arguments of the PropertyChanged event.
    /// </param>
    private void OnLayoutChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ILayoutSettings.CurrentLayoutOrientation))
        {
            UpdateValue();
        }
    }

    protected override bool ConditionMet()
    {
        return _layoutSettings.CurrentLayoutOrientation == _expectedValue;
    }
}