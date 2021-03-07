using System.Collections.Generic;
using Autofac;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements
{
	public class SequenceBreakRequirementTests
    {
		[Theory]
		[MemberData(nameof(SequenceBreak_Data))]
		public void Enabled_AccessibilityTests(SequenceBreakType sequenceBreak, RequirementType type)
		{
			var container = ContainerConfig.Configure();

			using var scope = container.BeginLifetimeScope();
			var requirements = scope.Resolve<IRequirementDictionary>();
			var sequenceBreaks = scope.Resolve<ISequenceBreakDictionary>();
			sequenceBreaks[sequenceBreak].Enabled = true;
			
			Assert.Equal(
				AccessibilityLevel.SequenceBreak, requirements[type].Accessibility);
		}
 
		[Theory]
		[MemberData(nameof(SequenceBreak_Data))]
		public void Disabled_AccessibilityTests(SequenceBreakType sequenceBreak, RequirementType type)
		{
			var container = ContainerConfig.Configure();

			using var scope = container.BeginLifetimeScope();
			var requirements = scope.Resolve<IRequirementDictionary>();
			var sequenceBreaks = scope.Resolve<ISequenceBreakDictionary>();
			
			sequenceBreaks[sequenceBreak].Enabled = false;
			Assert.Equal(
				AccessibilityLevel.None, requirements[type].Accessibility);
		}
 
		public static IEnumerable<object[]> SequenceBreak_Data =>
            new List<object[]>
			{
				new object[]
				{
					SequenceBreakType.BlindPedestal,
					RequirementType.SBBlindPedestal
				},
				new object[]
				{
					SequenceBreakType.BonkOverLedge,
					RequirementType.SBBonkOverLedge
				},
				new object[]
				{
					SequenceBreakType.BumperCaveHookshot,
					RequirementType.SBBumperCaveHookshot
				},
				new object[]
				{
					SequenceBreakType.SpikeCave,
					RequirementType.SBSpikeCave
				},
				new object[]
				{
					SequenceBreakType.TRLaserSkip,
					RequirementType.SBTRLaserSkip
				},
				new object[]
				{
					SequenceBreakType.LanmolasBombs,
					RequirementType.SBLanmolasBombs
				},
				new object[]
				{
					SequenceBreakType.HelmasaurKingBasic,
					RequirementType.SBHelmasaurKingBasic
				},
				new object[]
				{
					SequenceBreakType.ArrghusBasic,
					RequirementType.SBArrghusBasic
				},
				new object[]
				{
					SequenceBreakType.MothulaBasic,
					RequirementType.SBMothulaBasic
				},
				new object[]
				{
					SequenceBreakType.BlindBasic,
					RequirementType.SBBlindBasic
				},
				new object[]
				{
					SequenceBreakType.KholdstareBasic,
					RequirementType.SBKholdstareBasic
				},
				new object[]
				{
					SequenceBreakType.VitreousBasic,
					RequirementType.SBVitreousBasic
				},
				new object[]
				{
					SequenceBreakType.TrinexxBasic,
					RequirementType.SBTrinexxBasic
				},
				new object[]
				{
					SequenceBreakType.BombDuplicationAncillaOverload,
					RequirementType.SBBombDuplicationAncillaOverload
				},
				new object[]
				{
					SequenceBreakType.BombDuplicationMirror,
					RequirementType.SBBombDuplicationMirror
				},
				new object[]
				{
					SequenceBreakType.BombJumpPoDHammerJump,
					RequirementType.SBBombJumpPoDHammerJump
				},
				new object[]
				{
					SequenceBreakType.BombJumpSWBigChest,
					RequirementType.SBBombJumpSWBigChest
				},
				new object[]
				{
					SequenceBreakType.BombJumpIPBJ,
					RequirementType.SBBombJumpIPBJ
				},
				new object[]
				{
					SequenceBreakType.BombJumpIPHookshotGap,
					RequirementType.SBBombJumpIPHookshotGap
				},
				new object[]
				{
					SequenceBreakType.BombJumpIPFreezorRoomGap,
					RequirementType.SBBombJumpIPFreezorRoomGap
				},
				new object[]
				{
					SequenceBreakType.DarkRoomDeathMountainEntry,
					RequirementType.SBDarkRoomDeathMountainEntry
				},
				new object[]
				{
					SequenceBreakType.DarkRoomDeathMountainExit,
					RequirementType.SBDarkRoomDeathMountainExit
				},
				new object[]
				{
					SequenceBreakType.DarkRoomHC,
					RequirementType.SBDarkRoomHC
				},
				new object[]
				{
					SequenceBreakType.DarkRoomAT,
					RequirementType.SBDarkRoomAT
				},
				new object[]
				{
					SequenceBreakType.DarkRoomEPRight,
					RequirementType.SBDarkRoomEPRight
				},
				new object[]
				{
					SequenceBreakType.DarkRoomEPBack,
					RequirementType.SBDarkRoomEPBack
				},
				new object[]
				{
					SequenceBreakType.DarkRoomPoDDarkBasement,
					RequirementType.SBDarkRoomPoDDarkBasement
				},
				new object[]
				{
					SequenceBreakType.DarkRoomPoDDarkMaze,
					RequirementType.SBDarkRoomPoDDarkMaze
				},
				new object[]
				{
					SequenceBreakType.DarkRoomPoDBossArea,
					RequirementType.SBDarkRoomPoDBossArea
				},
				new object[]
				{
					SequenceBreakType.DarkRoomMM,
					RequirementType.SBDarkRoomMM
				},
				new object[]
				{
					SequenceBreakType.DarkRoomTR,
					RequirementType.SBDarkRoomTR
				},
				new object[]
				{
					SequenceBreakType.FakeFlippersFairyRevival,
					RequirementType.SBFakeFlippersFairyRevival
				},
				new object[]
				{
					SequenceBreakType.FakeFlippersQirnJump,
					RequirementType.SBFakeFlippersQirnJump
				},
				new object[]
				{
					SequenceBreakType.FakeFlippersScreenTransition,
					RequirementType.SBFakeFlippersScreenTransition
				},
				new object[]
				{
					SequenceBreakType.FakeFlippersSplashDeletion,
					RequirementType.SBFakeFlippersSplashDeletion
				},
				new object[]
				{
					SequenceBreakType.WaterWalk,
					RequirementType.SBWaterWalk
				},
				new object[]
				{
					SequenceBreakType.WaterWalkFromWaterfallCave,
					RequirementType.SBWaterWalkFromWaterfallCave
				},
				new object[]
				{
					SequenceBreakType.SuperBunnyMirror,
					RequirementType.SBSuperBunnyMirror
				},
				new object[]
				{
					SequenceBreakType.SuperBunnyFallInHole,
					RequirementType.SBSuperBunnyFallInHole
				},
				new object[]
				{
					SequenceBreakType.CameraUnlock,
					RequirementType.SBCameraUnlock
				},
				new object[]
				{
					SequenceBreakType.DungeonRevive,
					RequirementType.SBDungeonRevive
				},
				new object[]
				{
					SequenceBreakType.FakePowder,
					RequirementType.SBFakePowder
				},
				new object[]
				{
					SequenceBreakType.Hover,
					RequirementType.SBHover
				},
				new object[]
				{
					SequenceBreakType.MimicClip,
					RequirementType.SBMimicClip
				},
				new object[]
				{
					SequenceBreakType.ToHHerapot,
					RequirementType.SBToHHerapot
				},
				new object[]
				{
					SequenceBreakType.IPIceBreaker,
					RequirementType.SBIPIceBreaker
				}
			};
    }
}
