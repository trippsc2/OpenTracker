using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Items;
using OpenTracker.Models.Prizes;

namespace OpenTracker.Models.Requirements.Item.Crystal
{
    /// <summary>
    /// This class contains GT crystal <see cref="IRequirement"/> data.
    /// </summary>
    public class CrystalRequirement : AccessibilityRequirement, ICrystalRequirement
    {
        private readonly ICrystalRequirementItem _gtCrystal;
        private readonly IItem _crystal;
        private readonly IItem _redCrystal;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="items">
        ///     The <see cref="IItemDictionary"/>.
        /// </param>
        /// <param name="prizes">
        ///     The <see cref="IPrizeDictionary"/>.
        /// </param>
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
        /// Subscribes to the <see cref="IItem.PropertyChanged"/> event.
        /// </summary>
        /// <param name="sender">
        ///     The <see cref="object"/> from which the event is sent.
        /// </param>
        /// <param name="e">
        ///     The <see cref="PropertyChangedEventArgs"/>.
        /// </param>
        private void OnItemChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IItem.Current) ||
                e.PropertyName == nameof(ICrystalRequirementItem.Known))
            {
                UpdateValue();
            }
        }

        protected override AccessibilityLevel GetAccessibility()
        {
            if (_crystal.Current + _redCrystal.Current >= 7)
            {
                return AccessibilityLevel.Normal;
            }

            if (_gtCrystal.Known)
            {
                return _gtCrystal.Current + _crystal.Current + _redCrystal.Current >= 7 ?
                    AccessibilityLevel.Normal : AccessibilityLevel.None;
            }

            return AccessibilityLevel.SequenceBreak;
        }
    }
}
