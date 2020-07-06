using OpenTracker.Models.Enums;
using OpenTracker.Models.Utils;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This is the dictionary container for items, both tracked and untracked
    /// </summary>
    public class ItemDictionary : Singleton<ItemDictionary>, IDictionary<ItemType, IItem>
    {
        private static readonly ConcurrentDictionary<ItemType, IItem> _dictionary =
            new ConcurrentDictionary<ItemType, IItem>();

        public ICollection<ItemType> Keys =>
            ((IDictionary<ItemType, IItem>)_dictionary).Keys;

        public ICollection<IItem> Values =>
            ((IDictionary<ItemType, IItem>)_dictionary).Values;

        public int Count =>
            ((ICollection<KeyValuePair<ItemType, IItem>>)_dictionary).Count;

        public bool IsReadOnly =>
            ((ICollection<KeyValuePair<ItemType, IItem>>)_dictionary).IsReadOnly;

        public IItem this[ItemType key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    Create(key);
                }

                return ((IDictionary<ItemType, IItem>)_dictionary)[key];
            }
            set => ((IDictionary<ItemType, IItem>)_dictionary)[key] = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemDictionary()
        {
        }

        private void Create(ItemType key)
        {
            Add(key, ItemFactory.GetItem(key));
        }

        /// <summary>
        /// Returns whether the current value of the specified item is greater than or
        /// equal than the specified minimum value
        /// </summary>
        /// <param name="type">
        /// The item type to be checked.
        /// </param>
        /// <param name="minimumValue">
        /// The minimum value to be checked against.
        /// </param>
        /// <returns>
        /// A boolean representing whether the current value of the item is greater than or equal to the specified value.
        /// </returns>
        public bool Has(ItemType type, int minimumValue = 1)
        {
            switch (type)
            {
                case ItemType.Mushroom:
                    {
                        return this[type].Current == 1;
                    }
                case ItemType.Sword:
                    {
                        return this[type].Current > minimumValue;
                    }
                case ItemType.TowerCrystals:
                case ItemType.GanonCrystals:
                    {
                        return this[type].Current + this[ItemType.Crystal].Current +
                            this[ItemType.RedCrystal].Current >= 7;
                    }
                case ItemType.HCSmallKey:
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
                    {
                        if (Mode.Instance.WorldState == WorldState.Retro)
                        {
                            return this[type].Current + this[ItemType.SmallKey].Current >= minimumValue;
                        }

                        return Mode.Instance.DungeonItemShuffle < DungeonItemShuffle.MapsCompassesSmallKeys ||
                            this[type].Current >= minimumValue;
                    }
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
                    {
                        return Mode.Instance.DungeonItemShuffle < DungeonItemShuffle.Keysanity ||
                            this[type].Current >= minimumValue;
                    }
                case ItemType.LightWorldAccess:
                case ItemType.DeathMountainExitAccess:
                case ItemType.WaterfallFairyAccess:
                case ItemType.LakeHyliaFairyIslandAccess:
                case ItemType.CheckerboardLedgeAccess:
                case ItemType.DesertLedgeAccess:
                case ItemType.DesertPalaceBackEntranceAccess:
                case ItemType.RaceGameLedgeAccess:
                case ItemType.HyruleCastleTopAccess:
                case ItemType.DeathMountainWestBottomAccess:
                case ItemType.DeathMountainWestTopAccess:
                case ItemType.DeathMountainEastTopAccess:
                case ItemType.DeathMountainEastBottomAccess:
                case ItemType.DeathMountainEastTopConnectorAccess:
                case ItemType.DarkWorldWestAccess:
                case ItemType.BumperCaveTopAccess:
                case ItemType.HammerHouseAccess:
                case ItemType.DWWitchAreaAccess:
                case ItemType.DarkWorldEastAccess:
                case ItemType.DarkWorldSouthEastAccess:
                case ItemType.IcePalaceAccess:
                case ItemType.DarkWorldSouthAccess:
                case ItemType.MireAreaAccess:
                case ItemType.DarkDeathMountainWestBottomAccess:
                case ItemType.DarkDeathMountainTopAccess:
                case ItemType.DWFloatingIslandAccess:
                case ItemType.DarkDeathMountainEastBottomAccess:
                case ItemType.TurtleRockTunnelAccess:
                case ItemType.TurtleRockSafetyDoorAccess:
                    {
                        return Mode.Instance.EntranceShuffle && this[type].Current >= minimumValue;
                    }
                default:
                    {
                        return this[type].Current >= minimumValue;
                    }
            }
        }

        /// <summary>
        /// Returns whether the current sword item represents Swordless mode.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the sword item represents Swordless mode.
        /// </returns>
        public bool Swordless()
        {
            return this[ItemType.Sword].Current == 0;
        }

        /// <summary>
        /// Returns whether the current item set can activate tablets.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the current item set can activate tablets.
        /// </returns>
        public bool CanActivateTablets()
        {
            return (Swordless() && Has(ItemType.Hammer)) ||
                Has(ItemType.Sword, 2);
        }

        /// <summary>
        /// Returns whether the current item set can use medallions.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the current item set can use medallions.
        /// </returns>
        public bool CanUseMedallions()
        {
            return Swordless() || Has(ItemType.Sword);
        }

        /// <summary>
        /// Returns whether the current item set provides the correct medallion for
        /// Misery Mire.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the current item set provides the correct
        /// medallion for Misery Mire.
        /// </returns>
        public bool HasMMMedallion()
        {
            return (Has(ItemType.Bombos) && Has(ItemType.Ether) &&
                Has(ItemType.Quake)) || (Has(ItemType.Bombos) &&
                (this[ItemType.BombosDungeons].Current == 1 ||
                this[ItemType.BombosDungeons].Current == 3)) ||
                (Has(ItemType.Ether) &&
                (this[ItemType.EtherDungeons].Current == 1 ||
                this[ItemType.EtherDungeons].Current == 3)) ||
                (Has(ItemType.Quake) &&
                (this[ItemType.QuakeDungeons].Current == 1 ||
                this[ItemType.QuakeDungeons].Current == 3));
        }

        /// <summary>
        /// Returns whether the current item set provides the correct medallion for
        /// Turtle Rock.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the current item set provides the correct
        /// medallion for Turtle Rock.
        /// </returns>
        public bool HasTRMedallion()
        {
            return (Has(ItemType.Bombos) && Has(ItemType.Ether) &&
                Has(ItemType.Quake)) || (Has(ItemType.Bombos) &&
                this[ItemType.BombosDungeons].Current > 1) ||
                (Has(ItemType.Ether) &&
                this[ItemType.EtherDungeons].Current > 1) ||
                (Has(ItemType.Quake) &&
                this[ItemType.QuakeDungeons].Current > 1);
        }

        /// <summary>
        /// Returns whether the current item set allows for curtains to be removed.
        /// </summary>
        /// <returns>
        /// A boolean that represents whether the current item set allows curtains to be removed.
        /// </returns>
        public bool CanRemoveCurtains()
        {
            return Swordless() || Has(ItemType.Sword);
        }

        /// <summary>
        /// Returns whether the current item set can clear the Agahnim's Tower entrance 
        /// barrier.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the current item set can clear the Agahnim's Tower
        /// entrance barrier.
        /// </returns>
        public bool CanClearAgaTowerBarrier()
        {
            return Has(ItemType.Cape) ||
                (Swordless() && Has(ItemType.Hammer)) ||
                Has(ItemType.Sword, 2);
        }

        /// <summary>
        /// Returns whether the current item set can logically extend the player's magic bar
        /// by the specified amount.
        /// </summary>
        /// <param name="bars">
        /// The number of full magic bars needed.
        /// </param>
        /// <returns>
        /// A boolean representing whether the current item set can logically extend the 
        /// player's magic bar enough.
        /// </returns>
        public bool CanExtendMagic(int bars = 2)
        {
            int magicCapacity = 1 * (Has(ItemType.Bottle) ? 2 : 1) *
                (Has(ItemType.HalfMagic) ? 2 : 1);

            return magicCapacity > bars;
        }

        /// <summary>
        /// Returns whether the current item set can melt Freezors or Kholdstare's ice.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the current item set can melt Freezors or
        /// Kholdstare's ice.
        /// </returns>
        public bool CanMeltThings()
        {
            return Has(ItemType.FireRod) || (Has(ItemType.Bombos) && CanUseMedallions());
        }

        /// <summary>
        /// Returns whether the current item set can shoot arrows.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the current item set can shoot arrows.
        /// </returns>
        public bool CanShootArrows()
        {
            return Has(ItemType.Bow);
        }

        /// <summary>
        /// Returns whether the current item set/game mode can logically pass red
        /// Eyegor/Goriya rooms
        /// </summary>
        /// <returns>
        /// A boolean representing whether the current item set/game mode can logically pass
        /// red Eyegor/Goriya rooms
        /// </returns>
        public bool CanPassRedEyegoreGoriyaRooms()
        {
            return CanShootArrows() || Mode.Instance.EnemyShuffle;
        }

        /// <summary>
        /// Returns whether the current item set/game mode allows the player to be in
        /// normal form in Light World.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the current item set/game mode allows the player
        /// to be in normal form in Light World.
        /// </returns>
        public bool NotBunnyInLightWorld()
        {
            return Mode.Instance.WorldState != WorldState.Inverted || Has(ItemType.MoonPearl);
        }

        /// <summary>
        /// Returns whether the current item set/game mode allows the player to be in
        /// normal form in Dark World.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the current item set/game mode allows the player
        /// to be in normal form in Dark World.
        /// </returns>
        public bool NotBunnyInDarkWorld()
        {
            return Mode.Instance.WorldState == WorldState.Inverted || Has(ItemType.MoonPearl);
        }

        /// <summary>
        /// Resets all contained items to their starting values.
        /// </summary>
        public void Reset()
        {
            foreach (IItem item in Values)
            {
                item.Reset();
            }
        }

        public void Add(ItemType key, IItem value)
        {
            ((IDictionary<ItemType, IItem>)_dictionary).Add(key, value);
        }

        public bool ContainsKey(ItemType key)
        {
            return ((IDictionary<ItemType, IItem>)_dictionary).ContainsKey(key);
        }

        public bool Remove(ItemType key)
        {
            return ((IDictionary<ItemType, IItem>)_dictionary).Remove(key);
        }

        public bool TryGetValue(ItemType key, out IItem value)
        {
            if (!ContainsKey(key))
            {
                Create(key);
            }

            return ((IDictionary<ItemType, IItem>)_dictionary).TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<ItemType, IItem> item)
        {
            ((ICollection<KeyValuePair<ItemType, IItem>>)_dictionary).Add(item);
        }

        public void Clear()
        {
            ((ICollection<KeyValuePair<ItemType, IItem>>)_dictionary).Clear();
        }

        public bool Contains(KeyValuePair<ItemType, IItem> item)
        {
            return ((ICollection<KeyValuePair<ItemType, IItem>>)_dictionary).Contains(item);
        }

        public void CopyTo(KeyValuePair<ItemType, IItem>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<ItemType, IItem>>)_dictionary).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<ItemType, IItem> item)
        {
            return ((ICollection<KeyValuePair<ItemType, IItem>>)_dictionary).Remove(item);
        }

        public IEnumerator<KeyValuePair<ItemType, IItem>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<ItemType, IItem>>)_dictionary).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }
    }
}
