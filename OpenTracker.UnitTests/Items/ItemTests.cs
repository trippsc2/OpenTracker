using OpenTracker.Models.Items;
using System;
using System.Collections.Generic;
using Xunit;

namespace OpenTracker.UnitTests.Items
{
    [Collection("Tests")]
    public class ItemTests
    {
        [Theory]
        [MemberData(nameof(Ctor_Data))]
        [InlineData(2, 1)]
        public void Ctor_CurrentTests(int starting, int maximum)
        {
            if (starting > maximum)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    new Item(ItemType.Sword, starting, maximum);
                });
            }
            else
            {
                var item = new Item(ItemType.Sword, starting, maximum);

                Assert.Equal(starting, item.Current);
            }
        }

        [Theory]
        [MemberData(nameof(Ctor_Data))]
        [InlineData(2, 1)]
        public void Ctor_MaximumTests(int starting, int maximum)
        {
            if (starting > maximum)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    new Item(ItemType.Sword, starting, maximum);
                });
            }
            else
            {
                var item = new Item(ItemType.Sword, starting, maximum);

                Assert.Equal(maximum, item.Maximum);
            }
        }

        [Theory]
        [MemberData(nameof(Ctor_Data))]
        public void CanAdd_Tests(int starting, int maximum)
        {
            var item = new Item(ItemType.Sword, starting, maximum);

            Assert.Equal(starting < maximum, item.CanAdd());
        }

        [Theory]
        [MemberData(nameof(Ctor_Data))]
        public void CanRemove_Tests(int starting, int maximum)
        {
            var item = new Item(ItemType.Sword, starting, maximum);

            Assert.Equal(starting > 0, item.CanRemove());
        }

        public static IEnumerable<object[]> Ctor_Data =>
            new List<object[]>
            {
                new object[] { 0, 1 },
                new object[] { 1, 1 },
                new object[] { 0, 2 },
                new object[] { 1, 2 },
                new object[] { 2, 2 }
            };

        [Theory]
        [MemberData(nameof(Item_Data))]
        public void Factory_CurrentTests(ItemType type, int starting, int maximum)
        {
            var item = ItemFactory.GetItem(type);

            Assert.Equal(starting, item.Current);
        }

        [Theory]
        [MemberData(nameof(Item_Data))]
        public void Factory_MaximumTests(ItemType type, int starting, int maximum)
        {
            var item = ItemFactory.GetItem(type);

            Assert.Equal(maximum, item.Maximum);
        }

        [Theory]
        [MemberData(nameof(Item_Data))]
        public void Dictionary_CurrentTests(ItemType type, int starting, int maximum)
        {
            var item = ItemDictionary.Instance[type];
            item.Reset();

            Assert.Equal(starting, item.Current);
        }

        [Theory]
        [MemberData(nameof(Item_Data))]
        public void Dictionary_MaximumTests(ItemType type, int starting, int maximum)
        {
            var item = ItemDictionary.Instance[type];
            item.Reset();

            Assert.Equal(maximum, item.Maximum);
        }

        public static IEnumerable<object[]> Item_Data()
        {
            var result = new List<object[]>();

            foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
            {
                switch (type)
                {
                    case ItemType.Sword:
                        {
                            result.Add(new object[] { type, 1, 5 });
                        }
                        break;
                    case ItemType.Shield:
                    case ItemType.BombosDungeons:
                    case ItemType.EtherDungeons:
                    case ItemType.QuakeDungeons:
                    case ItemType.SWSmallKey:
                    case ItemType.MMSmallKey:
                        {
                            result.Add(new object[] { type, 0, 3 });
                        }
                        break;
                    case ItemType.Mail:
                    case ItemType.Arrows:
                    case ItemType.Mushroom:
                    case ItemType.Gloves:
                    case ItemType.RedCrystal:
                    case ItemType.Pendant:
                    case ItemType.ATSmallKey:
                    case ItemType.IPSmallKey:
                        {
                            result.Add(new object[] { type, 0, 2 });
                        }
                        break;
                    case ItemType.Aga1:
                    case ItemType.Aga2:
                    case ItemType.Bow:
                    case ItemType.Boomerang:
                    case ItemType.RedBoomerang:
                    case ItemType.Hookshot:
                    case ItemType.Bomb:
                    case ItemType.BigBomb:
                    case ItemType.Powder:
                    case ItemType.MagicBat:
                    case ItemType.Boots:
                    case ItemType.FireRod:
                    case ItemType.IceRod:
                    case ItemType.Bombos:
                    case ItemType.Ether:
                    case ItemType.Quake:
                    case ItemType.Lamp:
                    case ItemType.Hammer:
                    case ItemType.Flute:
                    case ItemType.FluteActivated:
                    case ItemType.Net:
                    case ItemType.Book:
                    case ItemType.Shovel:
                    case ItemType.Flippers:
                    case ItemType.CaneOfSomaria:
                    case ItemType.CaneOfByrna:
                    case ItemType.Cape:
                    case ItemType.Mirror:
                    case ItemType.HalfMagic:
                    case ItemType.MoonPearl:
                    case ItemType.GreenPendant:
                    case ItemType.HCSmallKey:
                    case ItemType.DPSmallKey:
                    case ItemType.ToHSmallKey:
                    case ItemType.SPSmallKey:
                    case ItemType.TTSmallKey:
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
                            result.Add(new object[] { type, 0, 1 });
                        }
                        break;
                    case ItemType.TowerCrystals:
                    case ItemType.GanonCrystals:
                        {
                            result.Add(new object[] { type, 0, 7 });
                        }
                        break;
                    case ItemType.SmallKey:
                        {
                            result.Add(new object[] { type, 0, 29 });
                        }
                        break;
                    case ItemType.Bottle:
                    case ItemType.TRSmallKey:
                    case ItemType.GTSmallKey:
                        {
                            result.Add(new object[] { type, 0, 4 });
                        }
                        break;
                    case ItemType.Crystal:
                        {
                            result.Add(new object[] { type, 0, 5 });
                        }
                        break;
                    case ItemType.PoDSmallKey:
                        {
                            result.Add(new object[] { type, 0, 6 });
                        }
                        break;
                    case ItemType.BigKey:
                    case ItemType.LightWorldAccess:
                    case ItemType.DeathMountainEntryAccess:
                    case ItemType.DeathMountainExitAccess:
                    case ItemType.GrassHouseAccess:
                    case ItemType.BombHutAccess:
                    case ItemType.RaceGameLedgeAccess:
                    case ItemType.SouthOfGroveLedgeAccess:
                    case ItemType.DesertLedgeAccess:
                    case ItemType.DesertPalaceBackEntranceAccess:
                    case ItemType.CheckerboardLedgeAccess:
                    case ItemType.LWGraveyardLedgeAccess:
                    case ItemType.LWKingsTombAccess:
                    case ItemType.HyruleCastleTopAccess:
                    case ItemType.WaterfallFairyAccess:
                    case ItemType.LWWitchAreaAccess:
                    case ItemType.LakeHyliaFairyIslandAccess:
                    case ItemType.DeathMountainWestBottomAccess:
                    case ItemType.DeathMountainWestTopAccess:
                    case ItemType.DeathMountainEastBottomAccess:
                    case ItemType.DeathMountainEastBottomConnectorAccess:
                    case ItemType.DeathMountainEastTopConnectorAccess:
                    case ItemType.SpiralCaveAccess:
                    case ItemType.MimicCaveAccess:
                    case ItemType.DeathMountainEastTopAccess:
                    case ItemType.DarkWorldWestAccess:
                    case ItemType.BumperCaveAccess:
                    case ItemType.BumperCaveTopAccess:
                    case ItemType.HammerHouseAccess:
                    case ItemType.HammerPegsAreaAccess:
                    case ItemType.DarkWorldSouthAccess:
                    case ItemType.MireAreaAccess:
                    case ItemType.DWWitchAreaAccess:
                    case ItemType.DarkWorldEastAccess:
                    case ItemType.IcePalaceAccess:
                    case ItemType.DarkWorldSouthEastAccess:
                    case ItemType.DarkDeathMountainWestBottomAccess:
                    case ItemType.DarkDeathMountainTopAccess:
                    case ItemType.DWFloatingIslandAccess:
                    case ItemType.DarkDeathMountainEastBottomAccess:
                    case ItemType.TurtleRockTunnelAccess:
                    case ItemType.TurtleRockSafetyDoorAccess:
                        {
                            result.Add(new object[] { type, 0, 0 });
                        }
                        break;
                }
            }

            return result;
        }
    }
}
