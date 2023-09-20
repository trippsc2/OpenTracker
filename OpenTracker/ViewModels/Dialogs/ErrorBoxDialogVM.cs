using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;

namespace OpenTracker.ViewModels.Dialogs;

/// <summary>
/// This is the ViewModel for the error box dialog window.
/// </summary>
[DependencyInjection]
public sealed class ErrorBoxDialogVM : ViewModel
{
    public string Title { get; }
    public string Text { get; }
    
    public Interaction<Unit, Unit> RequestCloseInteraction { get; } = new(RxApp.MainThreadScheduler);

    public ReactiveCommand<Unit, Unit> OkCommand { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="title">
    /// A string representing the window title.
    /// </param>
    /// <param name="text">
    /// A string representing the dialog text.
    /// </param>
    public ErrorBoxDialogVM(string title, string text)
    {
        Title = title;
        Text = text;
        
        OkCommand = ReactiveCommand.CreateFromTask(OkAsync);
    }

    private async Task OkAsync()
    {
        await RequestCloseInteraction.Handle(Unit.Default);
    }
}