using OpenTracker.Models.Accessibility;
using ReactiveUI;

namespace OpenTracker.Models.Dungeons.AccessibilityProvider;

/// <summary>
/// This interface contains the boss accessibility provider data.
/// </summary>
public interface IBossAccessibilityProvider : IReactiveObject
{
    /// <summary>
    /// The <see cref="AccessibilityLevel"/> of the boss.
    /// </summary>
    AccessibilityLevel Accessibility { get; set; }

    /// <summary>
    /// A factory for creating new <see cref="IBossAccessibilityProvider"/> objects.
    /// </summary>
    /// <returns>
    ///     A new <see cref="IBossAccessibilityProvider"/> object.
    /// </returns>
    delegate IBossAccessibilityProvider Factory();
}