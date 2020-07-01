using OpenTracker.Models.Enums;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    public interface IRequirement : INotifyPropertyChanged
    {
        AccessibilityLevel Accessibility { get; }
    }
}
