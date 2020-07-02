using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using OpenTracker.ViewModels.Bases;

namespace OpenTracker
{
    public class ViewLocator : IDataTemplate
    {
        public bool SupportsRecycling =>
            false;

        public IControl Build(object data)
        {
            if (data == null)
            {
                return new TextBlock { Text = "Not Found:" };
            }

            var name = data.GetType().FullName.Replace("VM", "").Replace("ViewModels","Views");
            var type = Type.GetType(name);

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type);
            }
            else
            {
                return new TextBlock { Text = "Not Found: " + name };
            }
        }

        public bool Match(object data)
        {
            return data is ViewModelBase;
        }
    }
}