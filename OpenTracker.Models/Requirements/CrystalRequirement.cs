using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the class for GT crystal requirements.
    /// </summary>
    public class CrystalRequirement : AccessibilityRequirement
    {
        private readonly ICrystalRequirementItem _gtCrystal;
        private readonly IItem _crystal;
        private readonly IItem _redCrystal;

        public delegate CrystalRequirement Factory();

        /// <summary>
        /// Constructor
        /// </summary>
        public CrystalRequirement(IItemDictionary items, IPrizeDictionary prizes)
        {
            _gtCrystal = (ICrystalRequirementItem)items[ItemType.TowerCrystals];
            _crystal = prizes[PrizeType.Crystal];
            _redCrystal = prizes[PrizeType.RedCrystal];

            _gtCrystal.PropertyChanged += OnItemChanged;
            _crystal.PropertyChanged += OnItemChanged;
            _redCrystal.PropertyChanged += OnItemChanged;

            UpdateValue();
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
            if (e.PropertyName == nameof(IItem.Current) ||
                e.PropertyName == nameof(ICrystalRequirementItem.Known))
            {
                UpdateValue();
            }
        }

        protected override AccessibilityLevel GetAccessibility()
        {
            if (_gtCrystal.Known)
            {
                return _gtCrystal.Current + _crystal.Current + _redCrystal.Current >= 7 ?
                    AccessibilityLevel.Normal : AccessibilityLevel.None;
            }

            return AccessibilityLevel.SequenceBreak;
        }
    }
}
