using System.Collections.Generic;
using OpenTracker.Models.SaveLoad;

namespace OpenTracker.Models.Dropdowns
{
    /// <summary>
    /// This interface contains the dictionary container of dropdowns.
    /// </summary>
    public interface IDropdownDictionary : IDictionary<DropdownID, IDropdown>,
        ISaveable<Dictionary<DropdownID, DropdownSaveData>>
    {
        void Reset();
    }
}
