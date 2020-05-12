using Avalonia.Controls;
using Avalonia.Styling;
using System.Threading.Tasks;

namespace OpenTracker.Interfaces
{
    public interface IDialog
    {
        object DataContext { get; set; }
        WindowBase Owner { get; set; }
        Styles Styles { get; }

        void Close(object dialogResult);
        Task<TResult> ShowDialog<TResult>(Window owner);
    }
}
