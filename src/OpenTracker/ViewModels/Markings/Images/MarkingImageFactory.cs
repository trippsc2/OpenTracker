using System;
using OpenTracker.Models.Items;
using OpenTracker.Models.Markings;

namespace OpenTracker.ViewModels.Markings.Images;

/// <summary>
/// This is the class containing creation logic for marking image control ViewModel classes.
/// </summary>
public class MarkingImageFactory : IMarkingImageFactory
{
    private readonly IItemDictionary _items;

    private readonly MarkingImageVM.Factory _markingFactory;
    private readonly ItemMarkingImageVM.Factory _itemMarkingFactory;

    public MarkingImageFactory(
        IItemDictionary items, MarkingImageVM.Factory markingFactory,
        ItemMarkingImageVM.Factory itemMarkingFactory)
    {
        _items = items;

        _markingFactory = markingFactory;
        _itemMarkingFactory = itemMarkingFactory;
    }

    /// <summary>
    /// Returns a new marking image control ViewModel instance representing the specified mark
    /// type.
    /// </summary>
    /// <param name="type">
    /// The mark type to be represented.
    /// </param>
    /// <returns>
    /// A new marking image control ViewModel instance.
    /// </returns>
    public IMarkingImageVMBase GetMarkingImageVM(MarkType type)
    {
        switch (type)
        {
            case MarkType.Sword:
            case MarkType.Shield:
            case MarkType.Mail:
            case MarkType.Gloves:
            case MarkType.Bottle:
            {
                return _itemMarkingFactory(
                    _items[Enum.Parse<ItemType>(type.ToString())],
                    $"avares://OpenTracker/Assets/Images/Marks/{type.ToString().ToLowerInvariant()}");
            }
            default:
            {
                return _markingFactory(
                    $"avares://OpenTracker/Assets/Images/Marks/{type.ToString().ToLowerInvariant()}.png");
            }
        }
    }
}