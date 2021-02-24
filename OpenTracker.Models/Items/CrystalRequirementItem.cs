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

        public new delegate CrystalRequirementItem Factory();

        /// <summary>
        /// Constructor
        /// </summary>
        public CrystalRequirementItem() : base(0, 7, null)
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
