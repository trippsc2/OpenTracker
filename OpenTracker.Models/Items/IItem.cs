using OpenTracker.Models.Enums;
using System.ComponentModel;

namespace OpenTracker.Models.Items
{
    public interface IItem : INotifyPropertyChanged
    {
        ItemType Type { get; }
        int Maximum { get; }
        int Current { get; }

        void Change(int delta, bool ignoreMaximum = false);
        void Reset();
        void SetCurrent(int current = 0);
    }
}