using Avalonia.Layout;
using System.ComponentModel;
using ReactiveUI;

namespace OpenTracker.ViewModels.Areas
{
    public interface IMapAreaVM : IReactiveObject
    {
        Orientation Orientation { get; }
    }
}
