using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System.Threading.Tasks;

namespace OpenTracker.Utils.Dialog;

/// <summary>
/// This class manages opening and closing file dialog windows from the ViewModel.
/// </summary>
public class FileDialogService : IFileDialogService
{
    private readonly IMainWindowProvider _mainWindowProvider;

    /// <summary>
    /// Constructor 
    /// </summary>
    /// <param name="mainWindowProvider">
    /// The main window provider service.
    /// </param>
    public FileDialogService(IMainWindowProvider mainWindowProvider)
    {
        _mainWindowProvider = mainWindowProvider;
    }

    private static readonly FilePickerFileType JsonFileType = new("JSON Files")
    {
        Patterns = new[] { "*.json" },
        MimeTypes = new[] { "application/json" }
    };

    /// <summary>
    /// Opens the OpenFileDialog window from the ViewModel.
    /// </summary>
    /// <returns>
    /// A task returning a nullable string representing the file path.
    /// </returns>
    public async Task<string?> ShowOpenDialogAsync()
    {
        var topLevel = TopLevel.GetTopLevel(_mainWindowProvider.GetMainWindow());
        if (topLevel?.StorageProvider is not IStorageProvider storageProvider)
        {
            return null;
        }

        var result = await storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            AllowMultiple = false,
            FileTypeFilter = new[] { JsonFileType }
        });

        return result.Count > 0 ? result[0].Path.LocalPath : null;
    }

    /// <summary>
    /// Opens the SaveFileDialog window from the ViewModel.
    /// </summary>
    /// <returns>
    /// A task returning a nullable string representing the file path.
    /// </returns>
    public async Task<string?> ShowSaveDialogAsync()
    {
        var topLevel = TopLevel.GetTopLevel(_mainWindowProvider.GetMainWindow());
        if (topLevel?.StorageProvider is not IStorageProvider storageProvider)
        {
            return null;
        }

        var result = await storageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            FileTypeChoices = new[] { JsonFileType }
        });

        return result?.Path.LocalPath;
    }
}