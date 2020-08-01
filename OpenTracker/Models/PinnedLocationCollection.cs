using OpenTracker.ViewModels.PinnedLocations;
using System.Collections.ObjectModel;

namespace OpenTracker.Models
{
    /// <summary>
    /// This is the singleton collection of pinned locations.
    /// </summary>
    public static class PinnedLocationCollection
    {
        private static ObservableCollection<PinnedLocationVM> _instance =
            new ObservableCollection<PinnedLocationVM>();

        public static ObservableCollection<PinnedLocationVM> Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ObservableCollection<PinnedLocationVM>();
                }

                return _instance;
            }
        }
    }
}
