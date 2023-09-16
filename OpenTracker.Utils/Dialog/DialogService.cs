using Avalonia;
using System;
using System.Linq;
using System.Reactive;
using System.Reflection;
using System.Threading.Tasks;

namespace OpenTracker.Utils.Dialog;

/// <summary>
/// This is the class that manages opening and closing dialog windows from the ViewModel.
/// </summary>
public class DialogService : IDialogService
{
    private readonly IMainWindowProvider _mainWindowProvider;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mainWindowProvider">
    /// The main window provider service.
    /// </param>
    public DialogService(IMainWindowProvider mainWindowProvider)
    {
        _mainWindowProvider = mainWindowProvider;
    }

    /// <summary>
    /// Sets the data context of the View to the ViewModel.
    /// </summary>
    /// <param name="window">
    /// The View window.
    /// </param>
    /// <param name="viewModel">
    /// The ViewModel.
    /// </param>
    private static void Bind(IDataContextProvider window, object viewModel) =>
        window.DataContext = viewModel;

    /// <summary>
    /// Returns the type of the View class for the provided ViewModel.
    /// </summary>
    /// <param name="viewModel">
    /// The ViewModel to be matched with a View.
    /// </param>
    /// <returns>
    /// The type of View class.
    /// </returns>
    private static Type? GetViewType(object viewModel)
    {
        var assembly = Assembly.GetEntryAssembly() ??
                       throw new NullReferenceException();
        var viewTypes = assembly.GetTypes();
        var viewModelName = viewModel.GetType().FullName!;
        var viewName = viewModelName.Replace("ViewModel", "View").Replace("VM", "");

        return viewTypes.SingleOrDefault(t => t.FullName == viewName);
    }

    /// <summary>
    /// Returns a new instance of the View class for the provided ViewModel.
    /// </summary>
    /// <typeparam name="TViewModel">
    ///     The type of the ViewModel for the created View class.
    /// </typeparam>
    /// <typeparam name="TResult">
    ///     The type of the result of the dialog window.
    /// </typeparam>
    /// <param name="viewModel">
    /// The ViewModel for the created View class.
    /// </param>
    /// A new instance of the View class.
    /// <returns></returns>
    private static DialogWindowBase<TViewModel, TResult> CreateView<TViewModel, TResult>(TViewModel viewModel)
        where TViewModel : DialogViewModelBase<TResult>
    {
        var viewType = GetViewType(viewModel) ??
                       throw new InvalidOperationException(
                           $"View for {viewModel.GetType().FullName!} was not found!");

        var view = Activator.CreateInstance(viewType);

        return (DialogWindowBase<TViewModel, TResult>)view!;
    }

    /// <summary>
    /// Asynchronously create and show a dialog window for the specified ViewModel.
    /// </summary>
    /// <typeparam name="TViewModel">
    ///     The type of the ViewModel for the created View class.
    /// </typeparam>
    /// <typeparam name="TResult">
    ///     The type of the result of the dialog window.
    /// </typeparam>
    /// <param name="viewModel">
    ///     The ViewModel.
    /// </param>
    /// <param name="locking">
    ///     A boolean representing whether the dialog window should lock the parent window.
    /// </param>
    /// <returns>
    ///     A task returning the result of the dialog.
    /// </returns>
    public async Task<TResult> ShowDialogAsync<TViewModel, TResult>(TViewModel viewModel, bool locking = true)
        where TViewModel : DialogViewModelBase<TResult>
    {
        if (viewModel.IsOpen)
        {
            return default!;
        }

        var window = CreateView<TViewModel, TResult>(viewModel);
        Bind(window, viewModel);

        return await ShowDialogAsync(window, locking);
    }

    /// <summary>
    /// Asynchronously show the specified Window and return the result.
    /// </summary>
    /// <typeparam name="TViewModel">
    ///     The type of the ViewModel for the created View class.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The type of the result of the dialog.
    /// </typeparam>
    /// <param name="window">
    /// The window to be shown.
    /// </param>
    /// <param name="locking">
    /// A boolean representing whether the dialog window should lock the parent window.
    /// </param>
    /// <returns>
    /// A task returning the result of the dialog window.
    /// </returns>
    private async Task<TResult> ShowDialogAsync<TViewModel, TResult>(DialogWindowBase<TViewModel, TResult> window, bool locking)
        where TViewModel : DialogViewModelBase<TResult>
    {
        var mainWindow = _mainWindowProvider.GetMainWindow();

        TResult result = default!;

        if (locking)
        {
            result = await window.ShowDialog<TResult>(mainWindow);
        }
        else
        {
            window.Show(mainWindow);
        }

        return result!;
    }

    /// <summary>
    /// Asynchronously show a dialog window for the specified ViewModel without returning a result.
    /// </summary>
    /// <param name="viewModel">
    /// The ViewModel.
    /// </param>
    /// <param name="locking">
    /// A boolean representing whether the dialog window should lock the parent window.
    /// </param>
    /// <returns>
    /// A task for asynchronous operation.
    /// </returns>
    public Task ShowDialogAsync<TViewModel>(TViewModel viewModel, bool locking = true)
        where TViewModel : DialogViewModelBase
    {
        return ShowDialogAsync<TViewModel, Unit>(viewModel, locking);
    }
}