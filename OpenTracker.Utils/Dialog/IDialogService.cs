using System.Threading.Tasks;

namespace OpenTracker.Utils.Dialog;

/// <summary>
/// This is the class that manages opening and closing dialog windows from the ViewModel.
/// </summary>
public interface IDialogService
{
    Task ShowDialogAsync<TViewModel>(TViewModel viewModel, bool locking = true)
        where TViewModel : DialogViewModelBase;
    Task<TResult> ShowDialogAsync<TViewModel, TResult>(TViewModel viewModel, bool locking = true)
        where TViewModel : DialogViewModelBase<TResult>;
}