using OpenTracker.Models.Locations;

namespace OpenTracker.Models.SaveLoad
{
    public class ConnectionSaveData
    {
        public LocationID Location1 { get; set; }
        public LocationID Location2 { get; set; }
        public int Index1 { get; set; }
        public int Index2 { get; set; }
    }
}
