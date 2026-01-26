using System;
using OpenTracker.Models.Items.Keys;

namespace OpenTracker.Models.Items.Factories;

/// <summary>
/// This class contains creation logic for <see cref="IItem"/> objects.
/// </summary>
public class ItemFactory : IItemFactory
{
    private readonly Lazy<IItemAutoTrackValueFactory> _autoTrackValueFactory;

    private readonly ICappedItemFactory _cappedItemFactory;
    private readonly IKeyItemFactory _keyItemFactory;

    private readonly IItem.Factory _itemFactory;
    private readonly ICrystalRequirementItem.Factory _crystalRequirementFactory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="autoTrackValueFactory">
    ///     An Autofac factory for creating the <see cref="IItemAutoTrackValueFactory"/> object.
    /// </param>
    /// <param name="cappedItemFactory">
    ///     A factory for creating new <see cref="ICappedItem"/> objects.
    /// </param>
    /// <param name="keyItemFactory">
    ///     A factory for creating new <see cref="ISmallKeyItem"/> and <see cref="IBigKeyItem"/> objects.
    /// </param>
    /// <param name="itemFactory">
    ///     An Autofac factory for creating new <see cref="IItem"/> objects.
    /// </param>
    /// <param name="crystalRequirementFactory">
    ///     An Autofac factory for creating new <see cref="ICrystalRequirementItem"/> objects.
    /// </param>
    public ItemFactory(IItemAutoTrackValueFactory.Factory autoTrackValueFactory,
        ICappedItemFactory cappedItemFactory, IKeyItemFactory keyItemFactory, IItem.Factory itemFactory,
        ICrystalRequirementItem.Factory crystalRequirementFactory)
    {
        _autoTrackValueFactory = new Lazy<IItemAutoTrackValueFactory>(autoTrackValueFactory());

        _cappedItemFactory = cappedItemFactory;
        _keyItemFactory = keyItemFactory;

        _itemFactory = itemFactory;
        _crystalRequirementFactory = crystalRequirementFactory;
    }

    public IItem GetItem(ItemType type)
    {
        var starting = GetItemStarting(type);
        var autoTrackValue = _autoTrackValueFactory.Value.GetAutoTrackValue(type);
            
        switch (type)
        {
            case ItemType.Sword:
            case ItemType.Shield:
            case ItemType.Mail:
            case ItemType.Bow:
            case ItemType.Arrows:
            case ItemType.Boomerang:
            case ItemType.RedBoomerang:
            case ItemType.Hookshot:
            case ItemType.Bomb:
            case ItemType.BigBomb:
            case ItemType.Powder:
            case ItemType.MagicBat:
            case ItemType.Mushroom:
            case ItemType.Boots:
            case ItemType.FireRod:
            case ItemType.IceRod:
            case ItemType.Bombos:
            case ItemType.BombosDungeons:
            case ItemType.Ether:
            case ItemType.EtherDungeons:
            case ItemType.Quake:
            case ItemType.QuakeDungeons:
            case ItemType.SmallKey:
            case ItemType.Gloves:
            case ItemType.Lamp:
            case ItemType.Hammer:
            case ItemType.Flute:
            case ItemType.FluteActivated:
            case ItemType.Net:
            case ItemType.Book:
            case ItemType.Shovel:
            case ItemType.Flippers:
            case ItemType.Bottle:
            case ItemType.CaneOfSomaria:
            case ItemType.CaneOfByrna:
            case ItemType.Cape:
            case ItemType.Mirror:
            case ItemType.HalfMagic:
            case ItemType.MoonPearl:
            case ItemType.HCMap:
            case ItemType.EPMap:
            case ItemType.DPMap:
            case ItemType.ToHMap:
            case ItemType.PoDMap:
            case ItemType.SPMap:
            case ItemType.SWMap:
            case ItemType.TTMap:
            case ItemType.IPMap:
            case ItemType.MMMap:
            case ItemType.TRMap:
            case ItemType.GTMap:
            case ItemType.EPCompass:
            case ItemType.DPCompass:
            case ItemType.ToHCompass:
            case ItemType.PoDCompass:
            case ItemType.SPCompass:
            case ItemType.SWCompass:
            case ItemType.TTCompass:
            case ItemType.IPCompass:
            case ItemType.MMCompass:
            case ItemType.TRCompass:
            case ItemType.GTCompass:
                return _cappedItemFactory.GetItem(type, starting, autoTrackValue);
            case ItemType.TowerCrystals:
            case ItemType.GanonCrystals:
                return _crystalRequirementFactory();
            case ItemType.HCSmallKey:
            case ItemType.EPSmallKey:
            case ItemType.DPSmallKey:
            case ItemType.ToHSmallKey:
            case ItemType.ATSmallKey:
            case ItemType.PoDSmallKey:
            case ItemType.SPSmallKey:
            case ItemType.SWSmallKey:
            case ItemType.TTSmallKey:
            case ItemType.IPSmallKey:
            case ItemType.MMSmallKey:
            case ItemType.TRSmallKey:
            case ItemType.GTSmallKey:
            case ItemType.HCBigKey:
            case ItemType.EPBigKey:
            case ItemType.DPBigKey:
            case ItemType.ToHBigKey:
            case ItemType.PoDBigKey:
            case ItemType.SPBigKey:
            case ItemType.SWBigKey:
            case ItemType.TTBigKey:
            case ItemType.IPBigKey:
            case ItemType.MMBigKey:
            case ItemType.TRBigKey:
            case ItemType.GTBigKey:
                return _keyItemFactory.GetItem(type, autoTrackValue);
            case ItemType.HCFreeKey:
            case ItemType.ATFreeKey:
            case ItemType.EPFreeKey:
            case ItemType.DPFreeKey:
            case ItemType.SPFreeKey:
            case ItemType.SWFreeKey:
            case ItemType.TTFreeKey:
            case ItemType.IPFreeKey:
            case ItemType.MMFreeKey: 
            case ItemType.TRFreeKey:
            case ItemType.GTFreeKey:
            case ItemType.HCUnlockedDoor:
            case ItemType.ATUnlockedDoor:
            case ItemType.EPUnlockedDoor:
            case ItemType.DPUnlockedDoor:
            case ItemType.ToHUnlockedDoor:
            case ItemType.PoDUnlockedDoor:
            case ItemType.SPUnlockedDoor:
            case ItemType.SWUnlockedDoor:
            case ItemType.TTUnlockedDoor:
            case ItemType.IPUnlockedDoor:
            case ItemType.MMUnlockedDoor:
            case ItemType.TRUnlockedDoor:
            case ItemType.GTUnlockedDoor:
                return _itemFactory(starting, autoTrackValue);
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    /// <summary>
    /// Returns the item starting amount for the specified <see cref="ItemType"/>.
    /// </summary>
    /// <param name="type">
    ///     The <see cref="ItemType"/>.
    /// </param>
    /// <returns>
    ///     A <see cref="int"/> representing the starting amount of the item.
    /// </returns>
    private static int GetItemStarting(ItemType type)
    {
        return type == ItemType.Sword ? 1 : 0;
    }
}