using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactions.DragAndDrop;
using OpenTracker.Models.Locations;

namespace OpenTracker.ViewModels.MapLocations
{
    /// <summary>
    /// This class contains the entrance drop handler logic.
    /// </summary>
    public class EntranceDropHandler : IDropHandler
    {
        public void Enter(object? sender, DragEventArgs e, object? sourceContext, object? targetContext)
        {
            if (Validate(sender, e, sourceContext, targetContext, null))
            {
                e.DragEffects |= DragDropEffects.Link;
                e.Handled = true;
                return;
            }

            e.DragEffects = DragDropEffects.None;
            e.Handled = true;
        }

        public void Over(object? sender, DragEventArgs e, object? sourceContext, object? targetContext)
        {
            if (Validate(sender, e, sourceContext, targetContext, null))
            {
                e.DragEffects |= DragDropEffects.Link;
                e.Handled = true;
                return;
            }

            e.DragEffects = DragDropEffects.None;
            e.Handled = true;
        }

        public void Drop(object? sender, DragEventArgs e, object? sourceContext, object? targetContext)
        {
            if (Execute(sender, e, sourceContext, targetContext, null))
            {
                e.DragEffects |= DragDropEffects.Link;
                e.Handled = true;
                return;
            }

            e.DragEffects = DragDropEffects.None;
            e.Handled = true;
        }

        public void Leave(object? sender, RoutedEventArgs e)
        {
            Cancel(sender, e);
        }

        public bool Validate(
            object? sender, DragEventArgs e, object? sourceContext, object? targetContext, object? state)
        {
            return sourceContext is IEntranceMapLocationVM && targetContext is IMapLocation;
        }

        public bool Execute(
            object? sender, DragEventArgs e, object? sourceContext, object? targetContext, object? state)
        {
            if (sourceContext is IEntranceMapLocationVM startEntrance && targetContext is IMapLocation finishLocation)
            {
                startEntrance.ConnectLocation(finishLocation);
                return true;
            }

            return false;
        }

        public void Cancel(object? sender, RoutedEventArgs e)
        {
        }
    }
}