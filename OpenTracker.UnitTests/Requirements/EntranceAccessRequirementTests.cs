using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using Xunit;

namespace OpenTracker.UnitTests.Requirements
{
    [Collection("Tests")]
    public class EntranceAccessRequirementTests
    {
        [Theory]
        [InlineData(ItemType.LightWorldAccess, RequirementType.LightWorldAccess)]
        [InlineData(ItemType.DeathMountainEntryAccess, RequirementType.DeathMountainEntryAccess)]
        [InlineData(ItemType.DeathMountainExitAccess, RequirementType.DeathMountainExitAccess)]
        [InlineData(ItemType.GrassHouseAccess, RequirementType.GrassHouseAccess)]
        [InlineData(ItemType.BombHutAccess, RequirementType.BombHutAccess)]
        [InlineData(ItemType.RaceGameLedgeAccess, RequirementType.RaceGameLedgeAccess)]
        [InlineData(ItemType.SouthOfGroveLedgeAccess, RequirementType.SouthOfGroveLedgeAccess)]
        [InlineData(ItemType.DesertLedgeAccess, RequirementType.DesertLedgeAccess)]
        [InlineData(ItemType.DesertPalaceBackEntranceAccess, RequirementType.DesertPalaceBackEntranceAccess)]
        [InlineData(ItemType.CheckerboardLedgeAccess, RequirementType.CheckerboardLedgeAccess)]
        [InlineData(ItemType.LWGraveyardLedgeAccess, RequirementType.LWGraveyardLedgeAccess)]
        [InlineData(ItemType.LWKingsTombAccess, RequirementType.LWKingsTombAccess)]
        [InlineData(ItemType.HyruleCastleTopAccess, RequirementType.HyruleCastleTopAccess)]
        [InlineData(ItemType.WaterfallFairyAccess, RequirementType.WaterfallFairyAccess)]
        [InlineData(ItemType.LWWitchAreaAccess, RequirementType.LWWitchAreaAccess)]
        [InlineData(ItemType.LakeHyliaFairyIslandAccess, RequirementType.LakeHyliaFairyIslandAccess)]
        [InlineData(ItemType.DeathMountainWestBottomAccess, RequirementType.DeathMountainWestBottomAccess)]
        [InlineData(ItemType.DeathMountainWestTopAccess, RequirementType.DeathMountainWestTopAccess)]
        [InlineData(ItemType.DeathMountainEastBottomAccess, RequirementType.DeathMountainEastBottomAccess)]
        [InlineData(ItemType.DeathMountainEastBottomConnectorAccess, RequirementType.DeathMountainEastBottomConnectorAccess)]
        [InlineData(ItemType.DeathMountainEastTopConnectorAccess, RequirementType.DeathMountainEastTopConnectorAccess)]
        [InlineData(ItemType.SpiralCaveAccess, RequirementType.SpiralCaveAccess)]
        [InlineData(ItemType.MimicCaveAccess, RequirementType.MimicCaveAccess)]
        [InlineData(ItemType.DeathMountainEastTopAccess, RequirementType.DeathMountainEastTopAccess)]
        [InlineData(ItemType.DarkWorldWestAccess, RequirementType.DarkWorldWestAccess)]
        [InlineData(ItemType.BumperCaveAccess, RequirementType.BumperCaveAccess)]
        [InlineData(ItemType.BumperCaveTopAccess, RequirementType.BumperCaveTopAccess)]
        [InlineData(ItemType.HammerHouseAccess, RequirementType.HammerHouseAccess)]
        [InlineData(ItemType.HammerPegsAreaAccess, RequirementType.HammerPegsAreaAccess)]
        [InlineData(ItemType.DarkWorldSouthAccess, RequirementType.DarkWorldSouthAccess)]
        [InlineData(ItemType.MireAreaAccess, RequirementType.MireAreaAccess)]
        [InlineData(ItemType.DWWitchAreaAccess, RequirementType.DWWitchAreaAccess)]
        [InlineData(ItemType.DarkWorldEastAccess, RequirementType.DarkWorldEastAccess)]
        [InlineData(ItemType.IcePalaceEntranceAccess, RequirementType.IcePalaceEntranceAccess)]
        [InlineData(ItemType.DarkWorldSouthEastAccess, RequirementType.DarkWorldSouthEastAccess)]
        [InlineData(ItemType.DarkDeathMountainWestBottomAccess, RequirementType.DarkDeathMountainWestBottomAccess)]
        [InlineData(ItemType.DarkDeathMountainTopAccess, RequirementType.DarkDeathMountainTopAccess)]
        [InlineData(ItemType.DWFloatingIslandAccess, RequirementType.DWFloatingIslandAccess)]
        [InlineData(ItemType.DarkDeathMountainEastBottomAccess, RequirementType.DarkDeathMountainEastBottomAccess)]
        [InlineData(ItemType.TurtleRockTunnelAccess, RequirementType.TurtleRockTunnelAccess)]
        [InlineData(ItemType.TurtleRockSafetyDoorAccess, RequirementType.TurtleRockSafetyDoorAccess)]
        public void AccessibilityTests(ItemType item, RequirementType type)
        {
            Mode.Instance.EntranceShuffle = false;
            Assert.Equal(AccessibilityLevel.None, RequirementDictionary.Instance[type].Accessibility);
            Mode.Instance.EntranceShuffle = true;
            Assert.Equal(AccessibilityLevel.None, RequirementDictionary.Instance[type].Accessibility);
            ItemDictionary.Instance[item].Current = 0;
            Assert.Equal(AccessibilityLevel.None, RequirementDictionary.Instance[type].Accessibility);
            ItemDictionary.Instance[item].Current = 1;
            Assert.Equal(AccessibilityLevel.Normal, RequirementDictionary.Instance[type].Accessibility);
        }
    }
}
