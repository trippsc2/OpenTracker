using System.ComponentModel;
using System.Reactive;
using Avalonia.Input;
using ReactiveUI;

namespace OpenTracker.ViewModels.Items.Adapters
{
    /// <summary>
    /// This interface contains the logic to adapt data for an item control.
    /// </summary>
    public interface IItemAdapter : INotifyPropertyChanged
    {
        string ImageSource { get; }
        string? Label { get; }
        ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }
        string LabelColor { get; }
    }
}