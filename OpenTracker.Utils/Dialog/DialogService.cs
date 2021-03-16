using Avalonia;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OpenTracker.Utils.Dialog
{
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
        /// <typeparam name="TResult">
        /// The type of the result of the dialog window.
        /// </typeparam>
        /// <param name="viewModel">
        /// The ViewModel for the created View class.
        /// </param>
        /// A new instance of the View class.
        /// <returns></returns>
        private static DialogWindowBase<TResult> CreateView<TResult>(object viewModel)
        {
            var viewType = GetViewType(viewModel) ??
                throw new InvalidOperationException(
                    $"View for {viewModel.GetType().FullName!} was not found!");

            var result = Activator.CreateInstance(viewType) ??
                throw new NullReferenceException();

            return (DialogWindowBase<TResult>)result;
        }

        /// <summary>
        /// Asynchronously create and show a dialog window for the specified ViewModel.
        /// </summary>
        /// <typeparam name="TResult">
        /// The type of the result of the dialog window.
        /// </typeparam>
        /// <param name="viewModel">
        /// The ViewModel.
        /// </param>
        /// <param name="locking">
        /// A boolean representing whether the dialog window should lock the parent window.
        /// </param>
        /// <returns>
        /// A task returning the result of the dialog.
        /// </returns>
        public async Task<TResult> ShowDialogAsync<TResult>(object viewModel, bool locking = true)
        {
            if (((DialogViewModelBase<TResult>)viewModel).IsOpen)
            {
                return default!;
            }

            var window = CreateView<TResult>(viewModel);
            Bind(window, viewModel);

            return await ShowDialogAsync(window, locking);
        }

        /// <summary>
        /// Asynchronously show the specified Window and return the result.
        /// </summary>
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
        private async Task<TResult> ShowDialogAsync<TResult>(DialogWindowBase<TResult> window, bool locking)
        {
            var mainWindow = _mainWindowProvider.GetMainWindow();

            TResult result = default;

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
        public Task ShowDialogAsync(object viewModel, bool locking = true) =>
            ShowDialogAsync<object>(viewModel, locking);
    }
}
