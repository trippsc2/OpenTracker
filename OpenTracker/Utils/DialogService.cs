using Avalonia.Controls;
using OpenTracker.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenTracker.Utils
{
    public class DialogService : IDialogService
    {
        public Window Owner { get; set; }
        public IDictionary<Type, Type> Mappings { get; }

        public DialogService()
        {
            Mappings = new Dictionary<Type, Type>();
        }

        public void Register<TViewModel, TView>()
            where TViewModel : IDialogRequestClose
            where TView : IDialog
        {
            if (Mappings.ContainsKey(typeof(TViewModel)))
                throw new ArgumentException($"Type {typeof(TViewModel)} is already mapped to type {typeof(TView)}");

            Mappings.Add(typeof(TViewModel), typeof(TView));
        }

        public async Task<bool?> ShowDialog<TViewModel>(TViewModel viewModel)
            where TViewModel : IDialogRequestClose
        {
            Type viewType = Mappings[typeof(TViewModel)];

            IDialog dialog = (IDialog)Activator.CreateInstance(viewType);

            void handler(object sender, DialogCloseRequestedEventArgs e)
            {
                viewModel.CloseRequested -= handler;

                if (e.DialogResult.HasValue)
                    dialog.Close(e.DialogResult);
                else
                    dialog.Close(null);
            }

            viewModel.CloseRequested += handler;

            dialog.DataContext = viewModel;
            dialog.Owner = Owner;

            bool? result = await dialog.ShowDialog<bool?>(Owner).ConfigureAwait(false);

            return result;
        }
    }
}
