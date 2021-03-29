using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.AutoTracking.Values.Multiple
{
    /// <summary>
    ///     This interface contains the auto-tracking result value data that is conditional.
    /// </summary>
    public interface IAutoTrackConditionalValue : IAutoTrackValue
    {
        /// <summary>
        ///     A factory for creating auto-tracking result values that are conditional.
        /// </summary>
        /// <param name="condition">
        ///     A requirement condition for determining which value to use.
        /// </param>
        /// <param name="trueValue">
        ///     The value to be presented, if the condition is met.
        /// </param>
        /// <param name="falseValue">
        ///     The value to be presented, if the condition is not met.
        /// </param>
        /// <returns>
        ///     A new auto-tracking result value that is conditional.
        /// </returns>
        delegate IAutoTrackConditionalValue Factory(
            IRequirement condition, IAutoTrackValue? trueValue, IAutoTrackValue? falseValue);
    }
}