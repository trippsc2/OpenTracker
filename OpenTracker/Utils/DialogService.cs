using Avalonia.Controls;
using OpenTracker.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenTracker.Utils
{
    /// <summary>
    /// This is the class for managing dialog windows.
    /// </summary>
    public class DialogService : IDialogService
    {
        public Window Owner { get; }
        public IDictionary<Type, Type> Mappings { get; } =
            new Dictionary<Type, Type>();

        /// <summary>
        /// Constructor
        /// </summary>
        public DialogService(Window owner)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }

        /// <summary>
        /// Registers the view-model and view class types.
        /// </summary>
        /// <typeparam name="TViewModel">
        /// The type of the view-model to be registered.
        /// </typeparam>
        /// <typeparam name="TView">
        /// The type of the view to be registered.
        /// </typeparam>
        public void Register<TViewModel, TView>()
            where TViewModel : IDialogRequestClose
            where TView : IDialog
        {
            if (Mappings.ContainsKey(typeof(TViewModel)))
            {
                throw new ArgumentException(
                    $"Type {typeof(TViewModel)} is already mapped to type {typeof(TView)}");
            }

            Mappings.Add(typeof(TViewModel), typeof(TView));
        }

        /// <summary>
        /// Creates and shows a dialog box and returns the dialog result.
        /// </summary>
        /// <typeparam name="TViewModel">
        /// The type of the view-model.
        /// </typeparam>
        /// <param name="viewModel">
        /// The view-model to be provided to the view.
        /// </param>
        /// <returns>
        /// A nullable boolean representing the dialog results.
        /// </returns>
        public async Task<bool?> ShowDialog<TViewModel>(TViewModel viewModel)
            where TViewModel : IDialogRequestClose
        {
            Type viewType = Mappings[typeof(TViewModel)];

            IDialog dialog = (IDialog)Activator.CreateInstance(viewType);

            void handler(object sender, DialogCloseRequestedEventArgs e)
            {
                viewModel.CloseRequested -= handler;

                if (e.DialogResult.HasValue)
                {
                    dialog.Close(e.DialogResult);
                }
                else
                {
                    dialog.Close(null);
                }
            }

            viewModel.CloseRequested += handler;

            dialog.DataContext = viewModel;
            bool? result = await dialog.ShowDialog<bool?>(Owner).ConfigureAwait(false);

            return result;
        }
    }
}
