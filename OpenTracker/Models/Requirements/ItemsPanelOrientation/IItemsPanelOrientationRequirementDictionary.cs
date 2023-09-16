using System.Collections.Generic;
using Avalonia.Layout;

namespace OpenTracker.Models.Requirements.ItemsPanelOrientation;

/// <summary>
///     This interface contains the dictionary container for items panel orientation requirements.
/// </summary>
public interface IItemsPanelOrientationRequirementDictionary : IDictionary<Orientation, IRequirement>
{
}