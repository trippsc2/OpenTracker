using System.Collections.Generic;
using OpenTracker.Models.Reset;
using OpenTracker.Models.SaveLoad;

namespace OpenTracker.Models.Dropdowns;

/// <summary>
/// Represents a dictionary container of dropdown entrance hole objects indexed by ID.
/// </summary>
public interface IDropdownDictionary : IDictionary<DropdownID, IDropdown>, IResettable,
    ISaveable<IDictionary<DropdownID, DropdownSaveData>>
{
}