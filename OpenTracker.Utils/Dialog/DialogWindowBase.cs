using Avalonia;
using Avalonia.Controls;
using System;

namespace OpenTracker.Utils.Dialog;

/// <summary>
/// This is the base class for all dialog windows.
/// </summary>
/// <typeparam name="TResult">
/// The type of the result of the dialog window.
/// </typeparam>
public class DialogWindowBase<TResult> : Window
{
    private Window? ParentWindow =>
        (Window?)Owner;

    protected DialogViewModelBase<TResult>? ViewModel =>
        DataContext as DialogViewModelBase<TResult>;

    /// <summary>
    /// Constructor
    /// </summary>
    protected DialogWindowBase()
    {
        SubscribeToViewEvents();
    }

    /// <summary>
    /// Virtual method called when the dialog window is opened.
    /// </summary>
    protected virtual void OnOpened()
    {
        _ = ViewModel ?? throw new NullReferenceException();

        ViewModel.IsOpen = true;
    }

    /// <summary>
    /// Subscribes to the Opened event on this class.
    /// </summary>
    /// <param name="sender">
    /// The event sender.
    /// </param>
    /// <param name="e">
    /// The Opened event args.
    /// </param>
    private void OnOpened(object? sender, EventArgs e)
    {
        CenterDialog();
        OnOpened();
    }

    /// <summary>
    /// Virtual method called when the dialog window is closed.
    /// </summary>
    protected virtual void OnClosed()
    {
        _ = ViewModel ?? throw new NullReferenceException();

        ViewModel.IsOpen = false;
    }

    /// <summary>
    /// Subscribes to the Closed event on this class.
    /// </summary>
    /// <param name="sender">
    /// The event sender.
    /// </param>
    /// <param name="e">
    /// The Closed event args.
    /// </param>
    private void OnClosed(object? sender, EventArgs e)
    {
        OnClosed();
    }

    /// <summary>
    /// Centers the dialog window on the parent.
    /// </summary>
    private void CenterDialog()
    {
        if (ParentWindow is null)
        {
            return;
        }

        var x = ParentWindow.Position.X + (ParentWindow.Bounds.Width - Width) / 2;
        var y = ParentWindow.Position.Y + (ParentWindow.Bounds.Height - Height) / 2;

        Position = new PixelPoint((int)x, (int)y);
    }

    /// <summary>
    /// Subscribes to the ViewModel CloseRequested event.
    /// </summary>
    private void SubscribeToViewModelEvents()
    {
        _ = ViewModel ?? throw new NullReferenceException();

        ViewModel.CloseRequested += OnViewModelCloseRequested;
    }

    /// <summary>
    /// Unsubscribes from the ViewModel CloseRequested event.
    /// </summary>
    private void UnsubscribeFromViewModelEvents()
    {
        _ = ViewModel ?? throw new NullReferenceException();

        ViewModel.CloseRequested -= OnViewModelCloseRequested;
    }

    /// <summary>
    /// Subscribes to the View DataContextChanged and Opened events.
    /// </summary>
    private void SubscribeToViewEvents()
    {
        DataContextChanged += OnDataContextChanged;
        Opened += OnOpened;
        Closed += OnClosed;
    }

    /// <summary>
    /// Unsubscribes from the View DataContextChanged and Opened events.
    /// </summary>
    private void UnsubscribeFromViewEvents()
    {
        DataContextChanged -= OnDataContextChanged;
        Opened -= OnOpened;
        Closed -= OnClosed;
    }

    /// <summary>
    /// Subscribes to the DataContextChanged event on this class.
    /// </summary>
    /// <param name="sender">
    /// The event sender.
    /// </param>
    /// <param name="e">
    /// The DataContextChanged event args.
    /// </param>
    private void OnDataContextChanged(object? sender, EventArgs e) =>
        SubscribeToViewModelEvents();

    /// <summary>
    /// Subscribes to the ViewModel CloseRequested event.
    /// </summary>
    /// <param name="sender">
    /// The event sender.
    /// </param>
    /// <param name="args">
    /// The CloseRequested event args.
    /// </param>
    private void OnViewModelCloseRequested(object? sender, DialogResultEventArgs<TResult> args)
    {
        _ = ViewModel ?? throw new NullReferenceException();

        UnsubscribeFromViewModelEvents();
        UnsubscribeFromViewEvents();

        ViewModel.IsOpen = false;

        Close(args.Result);
    }
}

public class DialogWindowBase : DialogWindowBase<object>
{
}