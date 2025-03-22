using System.Collections.Generic;
using OpenTracker.Models.Reset;
using OpenTracker.Models.SaveLoad;

namespace OpenTracker.Models.Dropdowns
{
    /// <summary>
    /// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container of <see cref="IDropdown"/> objects
    /// index by <see cref="DropdownID"/>.
    /// </summary>
    public interface IDropdownDictionary : IDictionary<DropdownID, IDropdown>, IResettable,
        ISaveable<IDictionary<DropdownID, DropdownSaveData>>
    {
    }
}
