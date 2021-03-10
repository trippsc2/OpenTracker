using System.ComponentModel;

namespace OpenTracker.ViewModels.MapLocations
{
    public interface IShapedMapLocationVMBase : INotifyPropertyChanged
    {
        double OffsetX { get; }
        double OffsetY { get; }
    }
}