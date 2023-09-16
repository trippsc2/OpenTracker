using System.Collections.Generic;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;

namespace OpenTracker.Models.Requirements.Alternative;

/// <summary>
/// This class contains logic for a set of <see cref="IRequirement"/> alternatives.
/// </summary>
public class AlternativeRequirement : AccessibilityRequirement, IAlternativeRequirement
{
    private readonly IList<IRequirement> _requirements;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="requirements">
    ///     A <see cref="IList{T}"/> of <see cref="IRequirement"/> alternatives.
    /// </param>
    public AlternativeRequirement(IList<IRequirement> requirements)
    {
        _requirements = requirements;

        foreach (var requirement in requirements)
        {
            requirement.PropertyChanged += OnRequirementChanged;
        }

        UpdateValue();
    }

    /// <summary>
    /// Subscribes to the <see cref="IRequirement.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnRequirementChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IRequirement.Accessibility))
        {
            UpdateValue();
        }
    }

    protected override AccessibilityLevel GetAccessibility()
    {
        var accessibility = AccessibilityLevel.None;

        foreach (var requirement in _requirements)
        {
            accessibility = AccessibilityLevelMethods.Max(accessibility, requirement.Accessibility);

            if (accessibility == AccessibilityLevel.Normal)
            {
                break;
            }
        }

        return accessibility;
    }
}