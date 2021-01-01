using OpenTracker.Models.Locations;
using OpenTracker.Models.Utils;
using OpenTracker.Utils;
using OpenTracker.ViewModels.PinnedLocations;

namespace OpenTracker.Models
{
    /// <summary>
    /// This is the singleton collection of pinned location ViewModels.
    /// </summary>
    public class PinnedLocationVMCollection : ViewModelCollection<PinnedLocationVM, ILocation>
    {
        private static PinnedLocationVMCollection _instance;

        public static PinnedLocationVMCollection Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PinnedLocationVMCollection(PinnedLocationCollection.Instance);
                }

                return _instance;
            }
        }

        public PinnedLocationVMCollection(IObservableCollection<ILocation> model) : base(model)
        {
        }

        protected override PinnedLocationVM CreateViewModel(ILocation model)
        {
            return PinnedLocationDictionary.Instance[model.ID];
        }
    }
}
