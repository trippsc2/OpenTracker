using OpenTracker.Models.Connections;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.Maps
{
    public class MapConnectionCollection : ViewModelCollection<IMapConnectionVM, IConnection>,
        IMapConnectionCollection
    {
        private readonly IMapConnectionVM.Factory _factory;

        public MapConnectionCollection(
            IMapConnectionVM.Factory factory, IConnectionCollection model) : base(model)
        {
            _factory = factory;
        }

        protected override IMapConnectionVM CreateViewModel(IConnection model)
        {
            return _factory(model);
        }
    }
}
