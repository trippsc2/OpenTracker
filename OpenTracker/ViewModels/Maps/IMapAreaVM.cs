using Avalonia.Layout;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Maps
{
    public interface IMapAreaVM : INotifyPropertyChanged
    {
        Orientation Orientation { get; }
    }
}
