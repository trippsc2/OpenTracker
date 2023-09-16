using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Splat;

namespace OpenTracker.Utils;

public class ViewLocator : IDataTemplate
{
    public IControl Build(object data)
    {
        if (data is not ViewModel viewModel)
        {
            return new Panel();
        }

        var type = viewModel.GetType();

        try
        {
            var viewType = viewModel.GetViewType();
            
            var view = Locator.Current.GetService(viewType);

            if (view is not IControl)
            {
                throw new Exception();
            }

            return (IControl) Locator.Current.GetService(viewType)!;
        }
        catch
        {
            return new TextBlock { Text = "Cannot Create View for: " + type.Name };
        }
    }

    public bool Match(object data)
    {
        return data is ViewModel;
    }
}