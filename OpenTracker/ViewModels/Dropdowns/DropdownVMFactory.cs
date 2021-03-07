using OpenTracker.Models.Dropdowns;
using System;
using System.Collections.Generic;

namespace OpenTracker.ViewModels.Dropdowns
{
    /// <summary>
    /// This is the class containing the creation logic for the dropdown icon ViewModels.
    /// </summary>
    public class DropdownVMFactory : IDropdownVMFactory
    {
        private readonly IDropdownDictionary _dropdownDictionary;
        private readonly IDropdownVM.Factory _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dropdownDictionary">
        /// The dropdown dictionary.
        /// </param>
        /// <param name="factory">
        /// The factory for creating new dropdown controls.
        /// </param>
        public DropdownVMFactory(
            IDropdownDictionary dropdownDictionary, IDropdownVM.Factory factory)
        {
            _dropdownDictionary = dropdownDictionary;
            _factory = factory;
        }

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
        private IDropdownVM GetDropdownVM(DropdownID id)
        {
            return _factory(_dropdownDictionary[id], GetBaseImageSource(id));
        }

        /// <summary>
        /// Returns an list of dropdown icon ViewModel instances.
        /// </summary>
        /// <returns>
        /// An list of dropdown icon ViewModel instances.
        /// </returns>
        public List<IDropdownVM> GetDropdownVMs()
        {
            var result = new List<IDropdownVM>();

            foreach (DropdownID id in Enum.GetValues(typeof(DropdownID)))
            {
                result.Add(GetDropdownVM(id));
            }

            return result;
        }
    }
}
