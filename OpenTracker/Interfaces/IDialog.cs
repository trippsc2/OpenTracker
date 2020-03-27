using Avalonia.Controls;
using System.Threading.Tasks;

namespace OpenTracker.Interfaces
{
    public interface IDialog
    {
        object DataContext { get; set; }
        WindowBase Owner { get; set; }

        void Close(object dialogResult);
        Task<TResult> ShowDialog<TResult>(Window owner);
    }
}
