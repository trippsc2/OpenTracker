using OpenTracker.Models.AutoTracking.AutotrackValues;

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

        public CrystalRequirementItem() : base(0, 7, null)
        {
        }

        public override void Reset()
        {
            Known = false;
            base.Reset();
        }
    }
}
