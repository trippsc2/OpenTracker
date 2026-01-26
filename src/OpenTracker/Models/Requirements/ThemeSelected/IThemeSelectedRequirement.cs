using OpenTracker.Utils.Themes;

namespace OpenTracker.Models.Requirements.ThemeSelected;

/// <summary>
///     This interface contains theme selected requirement data.
/// </summary>
public interface IThemeSelectedRequirement : IRequirement
{
    /// <summary>
    ///     A factory for creating new theme selected requirements.
    /// </summary>
    /// <param name="expectedValue">
    ///     The expected theme value.
    /// </param>
    delegate IThemeSelectedRequirement Factory(ITheme expectedValue);
}