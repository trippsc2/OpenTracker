using OpenTracker.Models.Enums;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    public class ItemDictionary : Dictionary<ItemType, Item>
    {
        private readonly Mode _mode;

        public ItemDictionary(Mode mode, int size) : base(size)
        {
            _mode = mode;
        }

        public bool Has(ItemType type, int atLeast = 1)
        {
            switch (type)
            {
                case ItemType.Mushroom:
                    return this[type].Current == 1;
                case ItemType.Sword:
                    return this[type].Current > atLeast;
                case ItemType.TowerCrystals:
                case ItemType.GanonCrystals:
                    return this[type].Current + this[ItemType.Crystal].Current + this[ItemType.RedCrystal].Current >= 7;
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
                    return _mode.DungeonItemShuffle < DungeonItemShuffle.MapsCompassesSmallKeys || this[type].Current >= atLeast;
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
                    return _mode.DungeonItemShuffle < DungeonItemShuffle.Keysanity || this[type].Current >= atLeast;
                case ItemType.LightWorldAccess:
                case ItemType.DeathMountainExitAccess:
                case ItemType.WaterfallFairyAccess:
                case ItemType.LakeHyliaFairyIslandAccess:
                case ItemType.CheckerboardCaveAccess:
                case ItemType.DesertLeftAccess:
                case ItemType.DesertBackAccess:
                case ItemType.RaceGameAccess:
                case ItemType.HyruleCastleSecondFloorAccess:
                case ItemType.DeathMountainWestBottomAccess:
                case ItemType.DeathMountainWestTopAccess:
                case ItemType.DeathMountainEastTopAccess:
                case ItemType.DeathMountainEastBottomAccess:
                case ItemType.DeathMountainEastTopConnectorAccess:
                case ItemType.DarkWorldWestAccess:
                case ItemType.BumperCaveAccess:
                case ItemType.HammerHouseAccess:
                case ItemType.DarkWorldWitchAreaAccess:
                case ItemType.DarkWorldEastAccess:
                case ItemType.DarkWorldSouthEastAccess:
                case ItemType.IcePalaceAccess:
                case ItemType.DarkWorldSouthAccess:
                case ItemType.MireAreaAccess:
                case ItemType.DarkDeathMountainWestBottomAccess:
                case ItemType.DarkDeathMountainTopAccess:
                case ItemType.DarkDeathMountainFloatingIslandAccess:
                case ItemType.DarkDeathMountainEastBottomAccess:
                case ItemType.TurtleRockTunnelAccess:
                case ItemType.TurtleRockSafetyDoorAccess:
                    return _mode.EntranceShuffle.Value ? this[type].Current >= atLeast : false;
                default:
                    return this[type].Current >= atLeast;
            }
        }

        public bool Swordless()
        {
            return this[ItemType.Sword].Current == 0;
        }

        public bool CanActivateTablets()
        {
            if (Swordless())
                return Has(ItemType.Hammer);
            else
                return Has(ItemType.Sword, 2);
        }

        public bool CanUseMedallions()
        {
            if (Swordless())
                return true;
            else
                return Has(ItemType.Sword);
        }

        public bool CanRemoveCurtains()
        {
            if (Swordless())
                return true;
            else
                return Has(ItemType.Sword);
        }

        public bool CanClearAgaTowerBarrier()
        {
            if (Has(ItemType.Cape))
                return true;

            if (Swordless())
                return Has(ItemType.Hammer);
            else
                return Has(ItemType.Sword, 2);
        }

        public bool CanExtendMagic(int bars = 2)
        {
            if (bars > 4)
                return false;

            if (bars > 2)
                return Has(ItemType.Bottle) && Has(ItemType.HalfMagic);

            return Has(ItemType.Bottle) || Has(ItemType.HalfMagic);
        }

        public bool CanMeltThings()
        {
            return Has(ItemType.FireRod) || (Has(ItemType.Bombos) && CanUseMedallions());
        }

        public void Reset()
        {
            foreach (Item item in Values)
                item.Reset();
        }
    }
}
