using System.Threading.Tasks;

namespace OpenTracker.Utils.Dialog
{
    /// <summary>
    /// This is the class that manages opening and closing dialog windows from the ViewModel.
    /// </summary>
    public interface IDialogService
    {
        Task ShowDialogAsync(object viewModel, bool locking = true);
        Task<TResult> ShowDialogAsync<TResult>(object viewModel, bool locking = true);
    }
}