using System;
using System.Collections.Generic;
using Autofac;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dropdowns
{
    public class DropdownTests
    {
        [Theory]
        [MemberData(nameof(RequirementData))]
        public void Factory_Tests(
            ModeSaveData modeData, DropdownID id, bool expected)
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var mode = scope.Resolve<IMode>();
            var factory = scope.Resolve<IDropdownFactory>();
            var dropdown = factory.GetDropdown(id);
            
            mode.Load(modeData);
            
            Assert.Equal(expected, dropdown.RequirementMet);
        }

        [Theory]
        [MemberData(nameof(RequirementData))]
        public void Dictionary_Tests(
            ModeSaveData modeData, DropdownID id, bool expected)
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var mode = scope.Resolve<IMode>();
            var dropdowns = scope.Resolve<IDropdownDictionary>();
            var dropdown = dropdowns[id];
            
            mode.Load(modeData);
            
            Assert.Equal(expected, dropdown.RequirementMet);
        }

        public static IEnumerable<object[]> RequirementData()
        {
            var result = new List<object[]>();
            
            foreach (DropdownID dropdown in Enum.GetValues(typeof(DropdownID)))
            {
                switch (dropdown)
                {
                    case DropdownID.LumberjackCave:
                    case DropdownID.ForestHideout:
                    case DropdownID.CastleSecret:
                    case DropdownID.TheWell:
                    case DropdownID.MagicBat:
                    case DropdownID.SanctuaryGrave:
                    case DropdownID.HoulihanHole:
                    case DropdownID.GanonHole:
                    {
                        result.Add(new object[]
                        {
                            new ModeSaveData()
                            {
                                EntranceShuffle = EntranceShuffle.None
                            },
                            dropdown,
                            false
                        });
                        result.Add(new object[]
                        {
                            new ModeSaveData()
                            {
                                EntranceShuffle = EntranceShuffle.Dungeon
                            },
                            dropdown,
                            false
                        });
                        result.Add(new object[]
                        {
                            new ModeSaveData()
                            {
                                EntranceShuffle = EntranceShuffle.All
                            },
                            dropdown,
                            true
                        });
                        result.Add(new object[]
                        {
                            new ModeSaveData()
                            {
                                EntranceShuffle = EntranceShuffle.Insanity
                            },
                            dropdown,
                            true
                        });
                    }
                        break;
                    case DropdownID.SWNEHole:
                    case DropdownID.SWNWHole:
                    case DropdownID.SWSEHole:
                    case DropdownID.SWSWHole:
                    {
                        result.Add(new object[]
                        {
                            new ModeSaveData()
                            {
                                EntranceShuffle = EntranceShuffle.None
                            },
                            dropdown,
                            false
                        });
                        result.Add(new object[]
                        {
                            new ModeSaveData()
                            {
                                EntranceShuffle = EntranceShuffle.Dungeon
                            },
                            dropdown,
                            false
                        });
                        result.Add(new object[]
                        {
                            new ModeSaveData()
                            {
                                EntranceShuffle = EntranceShuffle.All
                            },
                            dropdown,
                            false
                        });
                        result.Add(new object[]
                        {
                            new ModeSaveData()
                            {
                                EntranceShuffle = EntranceShuffle.Insanity
                            },
                            dropdown,
                            true
                        });
                    }
                        break;
                }
            }

            return result;
        }

        [Fact]
        public void PropertyChanged_Tests()
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var requirements = scope.Resolve<IRequirementDictionary>();
            var factory = scope.Resolve<IDropdown.Factory>();
            var dropdown = factory(requirements[RequirementType.NoRequirement]);
            
            Assert.PropertyChanged(
                dropdown, nameof(IDropdown.Checked),
                () => { dropdown.Checked = true; });
        }

        [Fact]
        public void Reset_Tests()
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var requirements = scope.Resolve<IRequirementDictionary>();
            var factory = scope.Resolve<IDropdown.Factory>();
            var dropdown = factory(requirements[RequirementType.NoRequirement]);
            
            Assert.False(dropdown.Checked);

            dropdown.Checked = true;
            
            Assert.True(dropdown.Checked);
            
            dropdown.Reset();
            
            Assert.False(dropdown.Checked);
        }

        [Fact]
        public void Load_Tests()
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var requirements = scope.Resolve<IRequirementDictionary>();
            var factory = scope.Resolve<IDropdown.Factory>();
            var dropdown = factory(requirements[RequirementType.NoRequirement]);
            
            Assert.False(dropdown.Checked);
            
            dropdown.Load(null);
            
            Assert.False(dropdown.Checked);

            var saveData = new DropdownSaveData()
            {
                Checked = true
            };
            dropdown.Load(saveData);
            
            Assert.True(dropdown.Checked);
        }

        [Fact]
        public void Save_Tests()
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var requirements = scope.Resolve<IRequirementDictionary>();
            var factory = scope.Resolve<IDropdown.Factory>();
            var dropdown = factory(requirements[RequirementType.NoRequirement]);
            
            var saveData = dropdown.Save();
            
            Assert.Equal(dropdown.Checked, saveData.Checked);
            
            dropdown.Checked = true;
            saveData = dropdown.Save();
            
            Assert.Equal(dropdown.Checked, saveData.Checked);
        }
    }
}