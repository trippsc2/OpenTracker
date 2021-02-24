using OpenTracker.Models.AutoTracking.Values;

namespace OpenTracker.Models.Items
{
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

        public CrystalRequirementItem(
            IItemAutoTrackValueFactory autoTrackValueFactory, ItemType type)
            : base(autoTrackValueFactory, type, 0, 7)
        {
        }

        public override void Reset()
        {
            Known = false;
            base.Reset();
        }
    }
}
