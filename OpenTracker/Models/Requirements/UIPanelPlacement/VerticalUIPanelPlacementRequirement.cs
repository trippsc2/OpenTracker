using System.ComponentModel;
using Avalonia.Controls;
using OpenTracker.Models.Settings;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.UIPanelPlacement;

/// <summary>
///     This class contains vertical UI panel placement requirement data.
/// </summary>
[DependencyInjection]
public sealed class VerticalUIPanelPlacementRequirement : BooleanRequirement, IVerticalUIPanelPlacementRequirement
{
    private readonly ILayoutSettings _layoutSettings;
    private readonly Dock _expectedValue;
        
    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="layoutSettings">
    ///     The layout settings data.
    /// </param>
    /// <param name="expectedValue">
    ///     The expected dock value.
    /// </param>
    public VerticalUIPanelPlacementRequirement(ILayoutSettings layoutSettings, Dock expectedValue)
    {
        _layoutSettings = layoutSettings;
        _expectedValue = expectedValue;

        _layoutSettings.PropertyChanged += OnLayoutSettingsChanged;
            
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
    private void OnLayoutSettingsChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ILayoutSettings.VerticalUIPanelPlacement))
        {
            UpdateValue();
        }
    }
        
    protected override bool ConditionMet()
    {
        return _layoutSettings.VerticalUIPanelPlacement == _expectedValue;
    }
}