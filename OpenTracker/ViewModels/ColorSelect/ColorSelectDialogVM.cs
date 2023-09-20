using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.ViewModels.ColorSelect;

/// <summary>
/// This class contains the color select dialog window ViewModel data.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class ColorSelectDialogVM : ViewModel
{
    private static readonly Dictionary<AccessibilityLevel, string> AccessibilityColorLabels = new()
    {
        {AccessibilityLevel.None, "None"},
        {AccessibilityLevel.Inspect, "Inspect"},
        {AccessibilityLevel.Partial, "Partial"},
        {AccessibilityLevel.SequenceBreak, "Sequence Break/Possibly Accessible"},
        {AccessibilityLevel.Normal, "Normal"}
    };
    
    public ObservableCollection<ColorSelectControlVM> FontColors { get; } = new();
    public ObservableCollection<ColorSelectControlVM> AccessibilityColors { get; } = new();
    public ObservableCollection<ColorSelectControlVM> ConnectorColors { get; } = new();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="colorSettings">
    ///     The <see cref="IColorSettings"/>
    /// </param>
    public ColorSelectDialogVM(IColorSettings colorSettings)
    {
        FontColors.Add(new ColorSelectControlVM(colorSettings.EmphasisFontColor, "Emphasis Font"));

        foreach (var accessibilityLevel in Enum.GetValues<AccessibilityLevel>())
        {
            if (accessibilityLevel == AccessibilityLevel.Cleared)
            {
                continue;
            }
            
            var color = colorSettings.AccessibilityColors[accessibilityLevel];
            var label = AccessibilityColorLabels[accessibilityLevel];
            AccessibilityColors.Add(new ColorSelectControlVM(color, label));
        }
        
        ConnectorColors.Add(new ColorSelectControlVM(colorSettings.ConnectorColor, "Connector"));
    }
}