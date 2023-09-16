using System.Threading.Tasks;

namespace OpenTracker.Utils.Dialog;

/// <summary>
/// This interface manages opening and closing file dialog windows from the ViewModel.
/// </summary>
public interface IFileDialogService
{
    Task<string?> ShowOpenDialogAsync();
    Task<string?> ShowSaveDialogAsync();
}