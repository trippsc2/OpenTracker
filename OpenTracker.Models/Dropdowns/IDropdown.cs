using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using ReactiveUI;

namespace OpenTracker.Models.Dropdowns
{
    /// <summary>
    /// This interface contains dropdown data.
    /// </summary>
    public interface IDropdown : IReactiveObject, ISaveable<DropdownSaveData>
    {
        bool Checked { get; set; }
        bool RequirementMet { get; }

        void Reset();

        delegate IDropdown Factory(IRequirement requirement);
    }
}