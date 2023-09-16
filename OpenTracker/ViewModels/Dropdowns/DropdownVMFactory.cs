using System;
using System.Collections.Generic;
using OpenTracker.Models.Dropdowns;
using OpenTracker.ViewModels.Items;
using OpenTracker.ViewModels.Items.Adapters;

namespace OpenTracker.ViewModels.Dropdowns;

/// <summary>
/// This is the class containing the creation logic for the dropdown icon ViewModels.
/// </summary>
public class DropdownVMFactory : IDropdownVMFactory
{
    private readonly IDropdownDictionary _dropdowns;
    private readonly IDropdownVMDictionary _dropdownControls;

    private readonly ILargeItemVM.Factory _factory;
    private readonly IItemVM.Factory _itemFactory;
    private readonly DropdownAdapter.Factory _adapterFactory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dropdowns">
    /// The dropdown dictionary.
    /// </param>
    /// <param name="dropdownControls">
    /// The dropdown control dictionary.
    /// </param>
    /// <param name="factory">
    /// An Autofac factory for creating large item controls.
    /// </param>
    /// <param name="itemFactory">
    /// An Autofac factory for creating item controls.
    /// </param>
    /// <param name="adapterFactory">
    /// An Autofac factory for creating dropdown item adapters.
    /// </param>
    public DropdownVMFactory(
        IDropdownDictionary dropdowns, IDropdownVMDictionary dropdownControls, ILargeItemVM.Factory factory,
        IItemVM.Factory itemFactory, DropdownAdapter.Factory adapterFactory)
    {
        _dropdowns = dropdowns;
        _dropdownControls = dropdownControls;

        _factory = factory;
        _itemFactory = itemFactory;
        _adapterFactory = adapterFactory;
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
    public ILargeItemVM GetDropdownVM(DropdownID id)
    {
        return _factory(_itemFactory(_adapterFactory(_dropdowns[id], GetBaseImageSource(id))));
    }

    /// <summary>
    /// Returns an list of dropdown icon ViewModel instances.
    /// </summary>
    /// <returns>
    /// An list of dropdown icon ViewModel instances.
    /// </returns>
    public List<ILargeItemVM> GetDropdownVMs()
    {
        var result = new List<ILargeItemVM>();

        foreach (DropdownID id in Enum.GetValues(typeof(DropdownID)))
        {
            result.Add(_dropdownControls[id]);
        }


        return result;
    }
}