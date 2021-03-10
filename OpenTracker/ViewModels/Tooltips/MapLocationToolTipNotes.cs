using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.Tooltips
{
    public class MapLocationToolTipNotes :
        ViewModelCollection<IMapLocationToolTipMarkingVM, IMarking>, IMapLocationToolTipNotes
    {
        private readonly IMapLocationToolTipMarkingVM.Factory _factory;

        public delegate IMapLocationToolTipNotes Factory(ILocation location);

        public MapLocationToolTipNotes(
            IMapLocationToolTipMarkingVM.Factory factory, ILocation location)
            : base(location.Notes)
        {
            _factory = factory;
        }

        protected override IMapLocationToolTipMarkingVM CreateViewModel(IMarking model)
        {
            return _factory(model);
        }
    }
}
