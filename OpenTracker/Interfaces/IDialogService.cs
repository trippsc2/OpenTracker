using Avalonia.Controls;
using System.Threading.Tasks;

namespace OpenTracker.Interfaces
{
    public interface IDialogService
    {
        Window Owner { get; set; }

        void Register<TViewModel, TView>()
            where TViewModel : IDialogRequestClose
            where TView : IDialog;
        Task<bool?> ShowDialog<TViewModel>(TViewModel viewModel)
            where TViewModel : IDialogRequestClose;
    }
}
