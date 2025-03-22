using Avalonia.Layout;
using ReactiveUI;

namespace OpenTracker.ViewModels.Areas
{
    public interface IMapAreaVM : IReactiveObject
    {
        Orientation Orientation { get; }
    }
}
