using OpenTracker.Models.Settings;

namespace OpenTracker.Models.Requirements.AlwaysDisplayDungeonItems;

/// <summary>
/// This interface contains the <see cref="ILayoutSettings.AlwaysDisplayDungeonItems"/> <see cref="IRequirement"/>
/// data.
/// </summary>
public interface IAlwaysDisplayDungeonItemsRequirement : IRequirement
{
    /// <summary>
    /// A factory for creating new <see cref="IAlwaysDisplayDungeonItemsRequirement"/> objects.
    /// </summary>
    /// <param name="expectedValue">
    ///     A <see cref="bool"/> representing the expected <see cref="ILayoutSettings.AlwaysDisplayDungeonItems"/>
    ///     value.
    /// </param>
    /// <returns>
    ///     A new <see cref="IAlwaysDisplayDungeonItemsRequirement"/> object.
    /// </returns>
    delegate IAlwaysDisplayDungeonItemsRequirement Factory(bool expectedValue);
}