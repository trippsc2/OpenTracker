using OpenTracker.Models.Enums;
using System;

namespace OpenTracker.Models.Interfaces
{
    public interface ISection
    {
        string Name { get; }
        event EventHandler ItemRequirementChanged;
        Func<Mode, ItemDictionary, Accessibility> GetAccessibility { get; }
        bool IsAvailable();
    }
}
