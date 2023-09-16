using Avalonia.Controls;
using System.Collections.Generic;
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

    /// <summary>
    /// Opens the OpenFileDialog window from the ViewModel.
    /// </summary>
    /// <returns>
    /// A task returning a nullable string representing the file path.
    /// </returns>
    public async Task<string?> ShowOpenDialogAsync()
    {
        var dialog = new OpenFileDialog()
        {
            AllowMultiple = false,
            Filters = new List<FileDialogFilter>
            {
                new() { Name = "JSON", Extensions = { "json" } }
            }
        };
        var result = await dialog.ShowAsync(_mainWindowProvider.GetMainWindow());

        if (result == null || result.Length == 0)
        {
            return null;
        }

        return result[0];
    }

    /// <summary>
    /// Opens the SaveFileDialog window from the ViewModel.
    /// </summary>
    /// <returns>
    /// A task returning a nullable string representing the file path.
    /// </returns>
    public async Task<string?> ShowSaveDialogAsync()
    {
        var dialog = new SaveFileDialog()
        {
            Filters = new List<FileDialogFilter>
            {
                new() { Name = "JSON", Extensions = { "json" } }
            }
        };

        return await dialog.ShowAsync(_mainWindowProvider.GetMainWindow());
    }
}