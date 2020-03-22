using OpenTracker.Models.Enums;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Interfaces
{
    public interface ISection : INotifyPropertyChanged
    {
        string Name { get; }
        AccessibilityLevel Accessibility { get; }

        bool IsAvailable();
        void Clear();
    }
}
