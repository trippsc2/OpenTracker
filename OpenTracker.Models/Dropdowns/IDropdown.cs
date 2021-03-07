using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using System.ComponentModel;

namespace OpenTracker.Models.Dropdowns
{
    /// <summary>
    /// This is the interface for dropdown data.
    /// </summary>
    public interface IDropdown : INotifyPropertyChanged, ISaveable<DropdownSaveData>
    {
        bool Checked { get; set; }
        bool RequirementMet { get; }

        void Reset();

        delegate IDropdown Factory(IRequirement requirement);
    }
}