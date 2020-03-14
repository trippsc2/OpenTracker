using OpenTracker.Models.Enums;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Interfaces
{
    public interface ISection : INotifyPropertyChanged
    {
        string Name { get; }
        event EventHandler ItemRequirementChanged;
        Func<Mode, ItemDictionary, Accessibility> GetAccessibility { get; }
        bool IsAvailable();
        void Clear();
    }
}
