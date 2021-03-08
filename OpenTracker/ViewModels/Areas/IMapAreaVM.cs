using Avalonia.Layout;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Areas
{
    public interface IMapAreaVM : INotifyPropertyChanged
    {
        Orientation Orientation { get; }
    }
}
