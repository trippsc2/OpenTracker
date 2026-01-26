using System;

namespace OpenTracker.Models.Accessibility;

/// <summary>
/// This class contains methods for getting the maximum or minimum <see cref="AccessibilityLevel"/> value.
/// </summary>
public static class AccessibilityLevelMethods
{
    /// <summary>
    /// Returns the greatest <see cref="AccessibilityLevel"/> value provided.
    /// </summary>
    /// <param name="values">
    ///     A list of <see cref="AccessibilityLevel"/> values to be compared.    
    /// </param>
    /// <returns>
    ///     The greatest <see cref="AccessibilityLevel"/> value.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when no values are provided.
    /// </exception>
    public static AccessibilityLevel Max(params AccessibilityLevel[] values)
    {
        if (values.Length == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(values));
        }

        AccessibilityLevel maximum = default;

        foreach (var value in values)
        {
            if (value > maximum)
            {
                maximum = value;
            }
        }

        return maximum;
    }

    /// <summary>
    /// Returns the least <see cref="AccessibilityLevel"/> value provided.
    /// </summary>
    /// <param name="values">
    ///     A list of <see cref="AccessibilityLevel"/> values to be compared.    
    /// </param>
    /// <returns>
    ///     The least <see cref="AccessibilityLevel"/> value.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when no values are provided.
    /// </exception>
    public static AccessibilityLevel Min(params AccessibilityLevel[] values)
    {
        if (values.Length == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(values));
        }

        var minimum = (AccessibilityLevel) int.MaxValue;

        foreach (var value in values)
        {
            if (value < minimum)
            {
                minimum = value;
            }
        }

        return minimum;
    }
}