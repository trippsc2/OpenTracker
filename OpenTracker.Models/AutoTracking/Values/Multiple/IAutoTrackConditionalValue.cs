using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.AutoTracking.Values.Multiple
{
    /// <summary>
    /// This interface contains the auto-tracking result value data that is conditional.
    /// </summary>
    public interface IAutoTrackConditionalValue : IAutoTrackValue
    {
        /// <summary>
        /// A factory for creating new <see cref="IAutoTrackConditionalValue"/> objects.
        /// </summary>
        /// <param name="condition">
        ///     A <see cref="IRequirement"/> condition for determining which value to use.
        /// </param>
        /// <param name="trueValue">
        ///     The nullable <see cref="IAutoTrackValue"/> to be presented, if the condition is met.
        /// </param>
        /// <param name="falseValue">
        ///     The nullable <see cref="IAutoTrackValue"/> to be presented, if the condition is not met.
        /// </param>
        /// <returns>
        ///     A new <see cref="IAutoTrackConditionalValue"/> object.
        /// </returns>
        delegate IAutoTrackConditionalValue Factory(
            IRequirement condition, IAutoTrackValue? trueValue, IAutoTrackValue? falseValue);
    }
}