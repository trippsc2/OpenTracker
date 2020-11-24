using OpenTracker.Models.Dropdowns;
using System;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.Dropdowns
{
    /// <summary>
    /// This is the class containing the creation logic for the dropdown icon ViewModels.
    /// </summary>
    internal static class DropdownVMFactory
    {
        /// <summary>
        /// Returns the base image source string for the specified ID.
        /// </summary>
        /// <param name="id">
        /// The dropdown ID.
        /// </param>
        /// <returns>
        /// The base image source string.
        /// </returns>
        private static string GetBaseImageSource(DropdownID id)
        {
            return $"avares://OpenTracker/Assets/Images/Dropdowns/{id.ToString().ToLowerInvariant()}";
        }
        
        /// <summary>
        /// Returns a new dropdown icon ViewModel instance for the specified ID.
        /// </summary>
        /// <param name="id">
        /// The dropdown ID.
        /// </param>
        /// <returns>
        /// A new dropdown icon ViewModel instance.
        /// </returns>
        private static DropdownVM GetDropdownVM(DropdownID id)
        {
            return new DropdownVM(GetBaseImageSource(id), DropdownDictionary.Instance[id]);
        }

        /// <summary>
        /// Returns an observable collection of dropdown icon ViewModel instances.
        /// </summary>
        /// <returns>
        /// An observable collection of dropdown icon ViewModel instances.
        /// </returns>
        internal static ObservableCollection<DropdownVM> GetDropdownVMs()
        {
            var result = new ObservableCollection<DropdownVM>();

            foreach (DropdownID id in Enum.GetValues(typeof(DropdownID)))
            {
                result.Add(GetDropdownVM(id));
            }

            return result;
        }
    }
}
