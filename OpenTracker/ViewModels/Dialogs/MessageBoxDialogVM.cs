using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;

namespace OpenTracker.ViewModels.Dialogs;

/// <summary>
/// This class contains the message box dialog window ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class MessageBoxDialogVM : ViewModel
{
    public string Title { get; }
    public string Text { get; }
    
    public Interaction<bool, Unit> RequestCloseInteraction { get; } = new(RxApp.MainThreadScheduler);

    public ReactiveCommand<Unit, Unit> YesCommand { get; }
    public ReactiveCommand<Unit, Unit> NoCommand { get; }
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="title">
    /// A string representing the window title.
    /// </param>
    /// <param name="text">
    /// A string representing the dialog text.
    /// </param>
    public MessageBoxDialogVM(string title, string text)
    {
        Title = title;
        Text = text;
        
        YesCommand = ReactiveCommand.CreateFromTask(YesAsync);
        NoCommand = ReactiveCommand.CreateFromTask(NoAsync);
    }

    private async Task YesAsync()
    {
        await RequestCloseInteraction.Handle(true);
    }

    private async Task NoAsync()
    {
        await RequestCloseInteraction.Handle(false);
    }
}