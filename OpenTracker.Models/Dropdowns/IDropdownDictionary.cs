using OpenTracker.Models.SaveLoad;
using System.Collections.Generic;

namespace OpenTracker.Models.Dropdowns
{
    /// <summary>
    /// This is the interface for the dropdown dictionary.
    /// </summary>
    public interface IDropdownDictionary : IDictionary<DropdownID, IDropdown>,
        ICollection<KeyValuePair<DropdownID, IDropdown>>,
        ISaveable<Dictionary<DropdownID, DropdownSaveData>>
    {
        void Reset();
    }
}
