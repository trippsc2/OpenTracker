using System.ComponentModel;
using Avalonia.Layout;

namespace OpenTracker.ViewModels.Areas;

public interface IMapAreaVM : INotifyPropertyChanged
{
    Orientation Orientation { get; }
}