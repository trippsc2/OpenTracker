using OpenTracker.Models.AutoTracking.Values;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This class contains crystal requirement data.
    /// </summary>
    public class CrystalRequirementItem : CappedItem, ICrystalRequirementItem
    {
        private bool _known;
        public bool Known
        {
            get => _known;
            set
            {
                if (_known != value)
                {
                    _known = value;
                    OnPropertyChanged(nameof(Known));
                    OnPropertyChanged(nameof(Current));
                }
            }
        }

        public new delegate CrystalRequirementItem Factory(ItemType type);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="autoTrackValueFactory">
        /// The item auotrack value factory.
        /// </param>
        /// <param name="type">
        /// The item type.
        /// </param>
        public CrystalRequirementItem(
            IItemAutoTrackValueFactory autoTrackValueFactory, ItemType type)
            : base(autoTrackValueFactory, type, 0, 7)
        {
        }

        /// <summary>
        /// Resets the item to its starting value.
        /// </summary>
        public override void Reset()
        {
            Known = false;
            base.Reset();
        }
    }
}
