using OpenTracker.Models.SaveLoad;
using System.ComponentModel;

namespace OpenTracker.Models.Dropdowns
{
    public interface IDropdown : INotifyPropertyChanged, ISaveable<DropdownSaveData>
    {
        bool Checked { get; set; }
        bool RequirementMet { get; }
    }
}