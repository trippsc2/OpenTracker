using System.ComponentModel;
using Avalonia.Controls;
using OpenTracker.Models.Settings;

namespace OpenTracker.Models.Requirements.ItemsPanelPlacement
{
    /// <summary>
    ///     This class contains horizontal items panel placement requirement data.
    /// </summary>
    public class HorizontalItemsPanelPlacementRequirement : BooleanRequirement, IHorizontalItemsPanelPlacementRequirement
    {
        private readonly ILayoutSettings _layoutSettings;
        private readonly Dock _expectedValue;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="layoutSettings">
        ///     The layout settings data.
        /// </param>
        /// <param name="expectedValue">
        ///     The expected dock value.
        /// </param>
        public HorizontalItemsPanelPlacementRequirement(ILayoutSettings layoutSettings, Dock expectedValue)
        {
            _layoutSettings = layoutSettings;
            _expectedValue = expectedValue;

            _layoutSettings.PropertyChanged += OnLayoutSettingsChanged;
                        
            UpdateValue();
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the ILayoutSettings interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnLayoutSettingsChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ILayoutSettings.HorizontalItemsPlacement))
            {
                UpdateValue();
            }
        }
        
        protected override bool ConditionMet()
        {
            return _layoutSettings.HorizontalItemsPlacement == _expectedValue;
        }
    }
}