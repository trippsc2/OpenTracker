using Avalonia.Controls;
using Avalonia.Controls.Templates;
using OpenTracker.ViewModels;
using System;

namespace OpenTracker
{
    /// <summary>
    /// This is the class for matching ViewModels with Views automatically.
    /// </summary>
    public class ViewLocator : IDataTemplate
    {
        public bool SupportsRecycling =>
            false;

        /// <summary>
        /// Returns a View class implementing IControl matching the supplied ViewModel class by name.
        /// </summary>
        /// <param name="data">
        /// The ViewModel class to be matched.
        /// </param>
        /// <returns>
        /// A View class implementing IControl.
        /// </returns>
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

        /// <summary>
        /// Returns whether the supplied ViewModel class inherits ViewModelBase.
        /// </summary>
        /// <param name="data">
        /// The ViewModel class to be tested.
        /// </param>
        /// <returns>
        /// A boolean representing whether the ViewModel class inherits ViewModelBase.
        /// </returns>
        public bool Match(object data)
        {
            return data is ViewModelBase;
        }
    }
}