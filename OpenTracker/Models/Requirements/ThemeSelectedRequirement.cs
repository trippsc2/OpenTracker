using System.ComponentModel;
using OpenTracker.Utils.Themes;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This class contains theme selected requirement data.
    /// </summary>
    public class ThemeSelectedRequirement : BooleanRequirement
    {
        private readonly IThemeManager _themeManager;
        private readonly ITheme _expectedValue;

        public delegate ThemeSelectedRequirement Factory(ITheme expectedValue);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="themeManager">
        /// The theme manager.
        /// </param>
        /// <param name="expectedValue">
        /// The expected theme value.
        /// </param>
        public ThemeSelectedRequirement(IThemeManager themeManager, ITheme expectedValue)
        {
            _themeManager = themeManager;
            _expectedValue = expectedValue;

            _themeManager.PropertyChanged += OnThemeManagerChanged;
            
            UpdateValue();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IThemeManager interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnThemeManagerChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IThemeManager.SelectedTheme))
            {
                UpdateValue();
            }
        }

        protected override bool ConditionMet()
        {
            return _themeManager.SelectedTheme == _expectedValue;
        }
    }
}