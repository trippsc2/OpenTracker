using OpenTracker.Models.Requirements;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.Values
{
    /// <summary>
    /// This is the class for representing the autotracking result value that is conditional.
    /// </summary>
    public class AutoTrackConditionalValue : IAutoTrackValue
    {
        private readonly IRequirement _condition;
        private readonly IAutoTrackValue? _trueValue;
        private readonly IAutoTrackValue? _falseValue;

        public int? CurrentValue
        {
            get
            {
                if (_condition.Met)
                {
                    if (_trueValue == null)
                    {
                        return null;
                    }

                    return _trueValue.CurrentValue;
                }

                if (_falseValue == null)
                {
                    return null;
                }

                return _falseValue.CurrentValue;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public delegate AutoTrackConditionalValue Factory(
            IRequirement condition, IAutoTrackValue? trueValue, IAutoTrackValue? falseValue);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="condition">
        /// The condition for determining which value to use.
        /// </param>
        /// <param name="trueValue">
        /// The value to be presented, if the condition is met.
        /// </param>
        /// <param name="falseValue">
        /// The value to be presented, if the condition is not met.
        /// </param>
        public AutoTrackConditionalValue(
            IRequirement condition, IAutoTrackValue? trueValue, IAutoTrackValue? falseValue)
        {
            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
            _trueValue = trueValue;
            _falseValue = falseValue;

            _condition.PropertyChanged += OnRequirementChanged;

            if (_trueValue != null)
            {
                _trueValue.PropertyChanged += OnValueChanged;
            }

            if (_falseValue != null)
            {
                _falseValue.PropertyChanged += OnValueChanged;
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IRequirement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirement.Accessibility))
            {
                UpdateValue();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IAutoTrackValue interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnValueChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAutoTrackValue.CurrentValue))
            {
                UpdateValue();
            }
        }

        /// <summary>
        /// Updates the value presented.
        /// </summary>
        private void UpdateValue()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentValue)));
        }
    }
}
