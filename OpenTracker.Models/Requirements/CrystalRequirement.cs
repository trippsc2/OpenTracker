using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the class for GT crystal requirements.
    /// </summary>
    public class CrystalRequirement : IRequirement
    {
        private readonly IItem _gtCrystal;
        private readonly IItem _crystal;
        private readonly IItem _redCrystal;

        public bool Met =>
            Accessibility != AccessibilityLevel.None;

        public event PropertyChangedEventHandler PropertyChanged;

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set
            {
                if (_accessibility != value)
                {
                    _accessibility = value;
                    OnPropertyChanged(nameof(Accessibility));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public CrystalRequirement()
        {
            _gtCrystal = ItemDictionary.Instance[ItemType.TowerCrystals];
            _crystal = PrizeDictionary.Instance[PrizeType.Crystal];
            _redCrystal = PrizeDictionary.Instance[PrizeType.RedCrystal];

            _gtCrystal.PropertyChanged += OnItemChanged;
            _crystal.PropertyChanged += OnItemChanged;
            _redCrystal.PropertyChanged += OnItemChanged;

            UpdateAccessibility();
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IItem interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IItem.Current))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Updates the accessibility of this requirement.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility = _gtCrystal.Current + _crystal.Current + _redCrystal.Current >= 7 ?
                AccessibilityLevel.Normal : AccessibilityLevel.None;
        }
    }
}
