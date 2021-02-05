using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.AutoTracking.AutotrackValues;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Requirements;
using System.Collections.Generic;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This is the class for creating section auto tracking.
    /// </summary>
    public static class SectionAutoTrackingFactory
    {
        /// <summary>
        /// Returns the autotracking value for the specified section.
        /// </summary>
        /// <param name="id">
        /// The location ID of the section.
        /// </param>
        /// <param name="sectionIndex">
        /// The index of the section.
        /// </param>
        /// <returns>
        /// The autotracking value for the specified section.
        /// </returns>
        public static IAutoTrackValue GetAutoTrackValue(LocationID id, int sectionIndex)
        {
            return id switch
            {
                LocationID.LinksHouse => new AutoTrackMultipleOverride(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef001], 0x04), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef208], 0x10), 1)
                    }),
                LocationID.Pedestal => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef300], 0x40), 1),
                LocationID.LumberjackCave => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef1c5], 0x02), 1),
                LocationID.BlindsHouse when sectionIndex == 0 => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef23a], 0x20), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef23a], 0x40), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef23a], 0x80), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef23b], 0x01), 1),
                    }),
                LocationID.BlindsHouse => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef23a], 0x10), 1),
                LocationID.TheWell when sectionIndex == 0 => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef05e], 0x20), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef05e], 0x40), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef05e], 0x80), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef05f], 0x01), 1),
                    }),
                LocationID.TheWell => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef05e], 0x10), 1),
                LocationID.BottleVendor => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef3c9], 0x02), 1),
                LocationID.ChickenHouse => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef210], 0x10), 1),
                LocationID.Tavern => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef206], 0x10), 1),
                LocationID.SickKid => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef410], 0x04), 1),
                LocationID.MagicBat => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef411], 0x80), 1),
                LocationID.RaceGame => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef2a8], 0x40), 1),
                LocationID.Library => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef410], 0x80), 1),
                LocationID.MushroomSpot => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef411], 0x10), 1),
                LocationID.ForestHideout => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef1c3], 0x02), 1),
                LocationID.CastleSecret when sectionIndex == 1 => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef3c6], 0x01), 1),
                LocationID.CastleSecret => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0aa], 0x10), 1),
                LocationID.WitchsHut => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef411], 0x20), 1),
                LocationID.SahasrahlasHut when sectionIndex == 0 => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef20a], 0x10), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef20a], 0x20), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef20a], 0x40), 1),
                    }),
                LocationID.SahasrahlasHut => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef410], 0x10), 1),
                LocationID.BonkRocks => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef248], 0x10), 1),
                LocationID.KingsTomb => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef226], 0x10), 1),
                LocationID.AginahsCave => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef214], 0x10), 1),
                LocationID.GroveDiggingSpot => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef2aa], 0x40), 1),
                LocationID.Dam when sectionIndex == 0 => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef216], 0x10), 1),
                LocationID.Dam => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef2bb], 0x40), 1),
                LocationID.MiniMoldormCave => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef246], 0x10), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef246], 0x20), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef246], 0x40), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef246], 0x80), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef247], 0x04), 1)
                    }),
                LocationID.IceRodCave => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef240], 0x10), 1),
                LocationID.Hobo => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef3c9], 0x01), 1),
                LocationID.PyramidLedge => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef2db], 0x40), 1),
                LocationID.FatFairy => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef22c], 0x10), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef22c], 0x20), 1)
                    }),
                LocationID.HauntedGrove => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef410], 0x08), 1),
                LocationID.HypeCave => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef23c], 0x10), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef23c], 0x20), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef23c], 0x40), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef23c], 0x80), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef23d], 0x04), 1)
                    }),
                LocationID.BombosTablet => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef411], 0x02), 1),
                LocationID.SouthOfGrove => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef237], 0x04), 1),
                LocationID.DiggingGame => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef2e8], 0x40), 1),
                LocationID.WaterfallFairy => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef228], 0x10), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef228], 0x20), 1)
                    }),
                LocationID.ZoraArea when sectionIndex == 0 => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef301], 0x40), 1),
                LocationID.ZoraArea => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef410], 0x02), 1),
                LocationID.Catfish => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef410], 0x20), 1),
                LocationID.GraveyardLedge => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef237], 0x02), 1),
                LocationID.DesertLedge => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef2b0], 0x40), 1),
                LocationID.CShapedHouse => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef238], 0x10), 1),
                LocationID.TreasureGame => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef20d], 0x04), 1),
                LocationID.BombableShack => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef20c], 0x10), 1),
                LocationID.Blacksmith => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef411], 0x04), 1),
                LocationID.PurpleChest => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef3c9], 0x10), 1),
                LocationID.HammerPegs => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef24f], 0x04), 1),
                LocationID.BumperCave => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef2ca], 0x40), 1),
                LocationID.LakeHyliaIsland => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef2b5], 0x40), 1),
                LocationID.MireShack => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef21a], 0x10), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef21a], 0x20), 1)
                    }),
                LocationID.CheckerboardCave => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef24d], 0x02), 1),
                LocationID.OldMan => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef410], 0x01), 1),
                LocationID.SpectacleRock when sectionIndex == 0 => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef283], 0x40), 1),
                LocationID.SpectacleRock => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef1d5], 0x04), 1),
                LocationID.EtherTablet => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef411], 0x01), 1),
                LocationID.SpikeCave => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef22e], 0x10), 1),
                LocationID.SpiralCave => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef1fc], 0x10), 1),
                LocationID.ParadoxCave when sectionIndex == 0 => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef1fe], 0x10), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef1fe], 0x20), 1)
                    }),
                LocationID.ParadoxCave => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef1de], 0x10), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef1de], 0x20), 1),
                         new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef1de], 0x40), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef1de], 0x80), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef1df], 0x01), 1)
                   }),
                LocationID.SuperBunnyCave => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef1f0], 0x10), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef1f0], 0x20), 1)
                    }),
                LocationID.HookshotCave when sectionIndex == 0 => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef078], 0x80), 1),
                LocationID.HookshotCave => new AutoTrackMultipleSum(
                    new List<IAutoTrackValue>
                    {
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef078], 0x10), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef078], 0x20), 1),
                        new AutoTrackFlagBool(
                            new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef078], 0x40), 1)
                    }),
                LocationID.FloatingIsland => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef285], 0x40), 1),
                LocationID.MimicCave => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef218], 0x10), 1),
                LocationID.HyruleCastle => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                    new AutoTrackMultipleDifference(
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef4c0], 255, 0),
                                new AutoTrackBitwiseIntegerValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef434], 0xF0, 4)
                            }),
                        new AutoTrackMultipleSum(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.HCMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.HCSmallKey]),
                                    new AutoTrackStaticValue(0))
                            })), null),
                LocationID.AgahnimTower when sectionIndex == 0 => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                    new AutoTrackMultipleDifference(
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackBitwiseIntegerValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef435], 0x3, 0),
                                new AutoTrackAddressValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef4c3], 255, 0)
                            }),
                        new AutoTrackMultipleSum(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.ATSmallKey]),
                                    new AutoTrackStaticValue(0))
                            })), null),
                LocationID.AgahnimTower => new AutoTrackAddressBool(
                    AutoTracker.Instance.MemoryAddresses[0x7ef3c5], 2, 1),
                LocationID.EasternPalace when sectionIndex == 0 => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                    new AutoTrackMultipleDifference(
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef4c1], 255, 0),
                                new AutoTrackBitwiseIntegerValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef436], 0x07, 0)
                            }),
                        new AutoTrackMultipleSum(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.EPMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.CompassShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.EPCompass]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.EPBigKey]),
                                    new AutoTrackStaticValue(0))
                            })), null),
                LocationID.EasternPalace => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef191], 0x08), 1),
                LocationID.DesertPalace when sectionIndex == 0 => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                    new AutoTrackMultipleDifference(
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef4c2], 255, 0),
                                new AutoTrackBitwiseIntegerValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef435], 0xE0, 5)
                            }),
                        new AutoTrackMultipleSum(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.DPMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.CompassShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.DPCompass]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.DPSmallKey]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.DPBigKey]),
                                    new AutoTrackStaticValue(0))
                            })), null),
                LocationID.DesertPalace => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef067], 0x08), 1),
                LocationID.TowerOfHera when sectionIndex == 0 => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                    new AutoTrackMultipleDifference(
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef4c9], 255, 0),
                                new AutoTrackBitwiseIntegerValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef435], 0x1C, 2)
                            }),
                        new AutoTrackMultipleSum(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.ToHMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.CompassShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.ToHCompass]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.ToHSmallKey]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.ToHBigKey]),
                                    new AutoTrackStaticValue(0))
                            })), null),
                LocationID.TowerOfHera => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef00f], 0x08), 1),
                LocationID.PalaceOfDarkness when sectionIndex == 0 => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                    new AutoTrackMultipleDifference(
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef4c5], 255, 0),
                                new AutoTrackBitwiseIntegerValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef434], 0x0F, 0)
                            }),
                        new AutoTrackMultipleSum(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.PoDMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.CompassShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.PoDCompass]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.PoDSmallKey]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.PoDBigKey]),
                                    new AutoTrackStaticValue(0))
                            })), null),
                LocationID.PalaceOfDarkness => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef0b5], 0x08), 1),
                LocationID.SwampPalace when sectionIndex == 0 => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                    new AutoTrackMultipleDifference(
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef4c4], 255, 0),
                                new AutoTrackBitwiseIntegerValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef439], 0xF, 0),
                            }),
                        new AutoTrackMultipleSum(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SPMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.CompassShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SPCompass]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SPSmallKey]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SPBigKey]),
                                    new AutoTrackStaticValue(0))
                        })), null),
                LocationID.SwampPalace => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef00d], 0x08), 1),
                LocationID.SkullWoods when sectionIndex == 0 => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                    new AutoTrackMultipleDifference(
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef4c7], 255, 0),
                                new AutoTrackBitwiseIntegerValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef437], 0xF0, 4)
                            }),
                        new AutoTrackMultipleSum(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SWMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.CompassShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SWCompass]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SWSmallKey]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.SWBigKey]),
                                    new AutoTrackStaticValue(0))
                            })), null),
                LocationID.SkullWoods => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef053], 0x08), 1),
                LocationID.ThievesTown when sectionIndex == 0 => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                    new AutoTrackMultipleDifference(
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef4ca], 255, 0),
                                new AutoTrackBitwiseIntegerValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef437], 0xF, 0)
                            }),
                        new AutoTrackMultipleSum(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TTMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.CompassShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TTCompass]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TTSmallKey]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TTBigKey]),
                                    new AutoTrackStaticValue(0))
                            })), null),
                LocationID.ThievesTown => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef159], 0x08), 1),
                LocationID.IcePalace when sectionIndex == 0 => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                    new AutoTrackMultipleDifference(
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef4c8], 255, 0),
                                new AutoTrackBitwiseIntegerValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef438], 0xF0, 4)
                            }),
                        new AutoTrackMultipleSum(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.IPMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.CompassShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.IPCompass]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.IPSmallKey]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.IPBigKey]),
                                    new AutoTrackStaticValue(0))
                            })), null),
                LocationID.IcePalace => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef1bd], 0x08), 1),
                LocationID.MiseryMire when sectionIndex == 0 => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                    new AutoTrackMultipleDifference(
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef4c6], 255, 0),
                                new AutoTrackBitwiseIntegerValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef438], 0xF, 0)
                            }),
                        new AutoTrackMultipleSum(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.MMMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.CompassShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.MMCompass]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.MMSmallKey]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.MMBigKey]),
                                    new AutoTrackStaticValue(0))
                            })), null),
                LocationID.MiseryMire => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef121], 0x08), 1),
                LocationID.TurtleRock when sectionIndex == 0 => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                    new AutoTrackMultipleDifference(
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef4cb], 255, 0),
                                new AutoTrackBitwiseIntegerValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef439], 0xF0, 4)
                            }),
                        new AutoTrackMultipleSum(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TRMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.CompassShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TRCompass]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TRSmallKey]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.TRBigKey]),
                                    new AutoTrackStaticValue(0))
                            })), null),
                LocationID.TurtleRock => new AutoTrackFlagBool(
                    new MemoryFlag(AutoTracker.Instance.MemoryAddresses[0x7ef149], 0x08), 1),
                LocationID.GanonsTower when sectionIndex == 0 => new AutoTrackConditionalValue(
                    RequirementDictionary.Instance[RequirementType.RaceIllegalTracking],
                    new AutoTrackMultipleDifference(
                        new AutoTrackMultipleOverride(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackAddressValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef4cc], 255, 0),
                                new AutoTrackBitwiseIntegerValue(
                                    AutoTracker.Instance.MemoryAddresses[0x7ef436], 0xF8, 3)
                            }),
                        new AutoTrackMultipleSum(
                            new List<IAutoTrackValue>
                            {
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.MapShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.GTMap]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.CompassShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.GTCompass]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.GTSmallKey]),
                                    new AutoTrackStaticValue(0)),
                                new AutoTrackConditionalValue(
                                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOff],
                                    new AutoTrackItemValue(ItemDictionary.Instance[ItemType.GTBigKey]),
                                    new AutoTrackStaticValue(0))
                            })), null),
                _ => null
            };
        }
    }
}
