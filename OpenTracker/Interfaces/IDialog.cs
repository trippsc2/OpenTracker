using Avalonia.Controls;
using Avalonia.Styling;
using System.Threading.Tasks;

namespace OpenTracker.Interfaces
{
    /// <summary>
    /// This is the interface for dialog windows.
    /// </summary>
    public interface IDialog
    {
        object DataContext { get; set; }
        WindowBase Owner { get; }
        Styles Styles { get; }

        void Close(object dialogResult);
        Task<TResult> ShowDialog<TResult>(Window owner);
    }
}
