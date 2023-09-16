using Avalonia.Controls;
using Avalonia.Controls.Templates;
using System;
using System.Linq;
using System.Reflection;

namespace OpenTracker.Utils;

public class ViewLocator : IDataTemplate
{
    public static bool SupportsRecycling =>
        false;

    public IControl Build(object data)
    {
        var assembly = Assembly.GetEntryAssembly() ??
                       throw new NullReferenceException();
        var viewTypes = assembly.GetTypes();
        var viewName = data.GetType().FullName!.Replace("ViewModel", "View").Replace("VM", "");
        var type = viewTypes.SingleOrDefault(t => t.FullName == viewName);

        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }
        else
        {
            return new TextBlock { Text = "Not Found: " + viewName };
        }
    }

    public bool Match(object data)
    {
        return data is ViewModelBase;
    }
}