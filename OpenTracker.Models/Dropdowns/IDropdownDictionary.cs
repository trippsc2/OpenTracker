using System.Collections.Generic;
using OpenTracker.Models.Reset;
using OpenTracker.Models.SaveLoad;

namespace OpenTracker.Models.Dropdowns
{
    /// <summary>
    /// This interface contains the dictionary container of <see cref="IDropdown"/> objects.
    /// </summary>
    public interface IDropdownDictionary : IDictionary<DropdownID, IDropdown>, IResettable,
        ISaveable<IDictionary<DropdownID, DropdownSaveData>>
    {
    }
}
