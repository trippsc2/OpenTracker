using OpenTracker.Models.Items;
using System.ComponentModel;

namespace OpenTracker.Models.PrizePlacements
{
    public interface IPrizePlacement : INotifyPropertyChanged
    {
        IItem Prize { get; set; }

        void Reset();
    }
}