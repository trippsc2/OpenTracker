using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Autofac;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Factories;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections.Entrance;
using OpenTracker.Models.Sections.Factories;
using OpenTracker.Models.UndoRedo.Sections;
using Xunit;

namespace OpenTracker.UnitTests.Models.Sections.Factories;

[ExcludeFromCodeCoverage]
public sealed class DropdownSectionFactoryTests
{
    private static readonly IAlternativeRequirementDictionary AlternativeRequirements =
        new AlternativeRequirementDictionary(_ => Substitute.For<IAlternativeRequirement>());
    private static readonly IEntranceShuffleRequirementDictionary EntranceShuffleRequirements =
        new EntranceShuffleRequirementDictionary(_ => Substitute.For<IEntranceShuffleRequirement>());

    private static readonly IOverworldNodeDictionary OverworldNodes = new OverworldNodeDictionary(
        () => Substitute.For<IOverworldNodeFactory>());

    private static readonly ISaveLoadManager SaveLoadManager = Substitute.For<ISaveLoadManager>();
    private static readonly IMode Mode = Substitute.For<IMode>();

    private static readonly ICollectSection.Factory CollectSectionFactory =
        (section, force) => new CollectSection(section, force);
    private static readonly IUncollectSection.Factory UncollectSectionFactory =
        section => new UncollectSection(section);

    private static readonly IMarking.Factory MarkingFactory = () => Substitute.For<IMarking>();

    private static readonly IDropdownSection.Factory Factory = (exitNode, holeNode, requirement) =>
        new DropdownSection(
            SaveLoadManager, Mode, CollectSectionFactory, UncollectSectionFactory, MarkingFactory, exitNode,
            holeNode, requirement);

    private static readonly Dictionary<LocationID, ExpectedObject> ExpectedValues = new();

    private readonly DropdownSectionFactory _sut = new(
        AlternativeRequirements, EntranceShuffleRequirements, OverworldNodes, Factory);

    private static void PopulateExpectedValues()
    {
        ExpectedValues.Clear();

        foreach (LocationID id in Enum.GetValues(typeof(LocationID)))
        {
            switch (id)
            {
                case LocationID.LumberjackCaveEntrance:
                    ExpectedValues.Add(id, Factory(
                        OverworldNodes[OverworldNodeID.LightWorld],
                        OverworldNodes[OverworldNodeID.LumberjackCaveHole],
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.All],
                            EntranceShuffleRequirements[EntranceShuffle.Insanity]
                        }]).ToExpectedObject());
                    break;
                case LocationID.TheWellEntrance:
                    ExpectedValues.Add(id, Factory(
                        OverworldNodes[OverworldNodeID.LightWorld],
                        OverworldNodes[OverworldNodeID.LightWorld],
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.All],
                            EntranceShuffleRequirements[EntranceShuffle.Insanity]
                        }]).ToExpectedObject());
                    break;
                case LocationID.MagicBatEntrance:
                    ExpectedValues.Add(id, Factory(
                        OverworldNodes[OverworldNodeID.LightWorld],
                        OverworldNodes[OverworldNodeID.MagicBatLedge],
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.All],
                            EntranceShuffleRequirements[EntranceShuffle.Insanity]
                        }]).ToExpectedObject());
                    break;
                case LocationID.ForestHideoutEntrance:
                case LocationID.HoulihanHole:
                    ExpectedValues.Add(id, Factory(
                        OverworldNodes[OverworldNodeID.LightWorld],
                        OverworldNodes[OverworldNodeID.LightWorldNotBunny],
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.All],
                            EntranceShuffleRequirements[EntranceShuffle.Insanity]
                        }]).ToExpectedObject());
                    break;
                case LocationID.CastleSecretEntrance:
                    ExpectedValues.Add(id, Factory(
                        OverworldNodes[OverworldNodeID.CastleSecretExitArea],
                        OverworldNodes[OverworldNodeID.LumberjackCaveHole],
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.All],
                            EntranceShuffleRequirements[EntranceShuffle.Insanity]
                        }]).ToExpectedObject());
                    break;
                case LocationID.SanctuaryGrave:
                    ExpectedValues.Add(id, Factory(
                        OverworldNodes[OverworldNodeID.LightWorld],
                        OverworldNodes[OverworldNodeID.EscapeGrave],
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.All],
                            EntranceShuffleRequirements[EntranceShuffle.Insanity]
                        }]).ToExpectedObject());
                    break;
                case LocationID.GanonHole:
                    ExpectedValues.Add(id, Factory(
                        OverworldNodes[OverworldNodeID.LightWorldInverted],
                        OverworldNodes[OverworldNodeID.GanonHole],
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            EntranceShuffleRequirements[EntranceShuffle.All],
                            EntranceShuffleRequirements[EntranceShuffle.Insanity]
                        }]).ToExpectedObject());
                    break;
            }
        }
    }

    [Fact]
    public void GetDropdownSection_ShouldThrowException_WhenIDIsUnexpected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _sut.GetDropdownSection((LocationID) int.MaxValue));
    }

    [Theory]
    [MemberData(nameof(GetDropdownSection_ShouldReturnExpectedData))]
    public void GetDropdownSection_ShouldReturnExpected(ExpectedObject expected, LocationID id)
    {
        var actual = _sut.GetDropdownSection(id);
            
        expected.ShouldEqual(actual);
    }

    public static IEnumerable<object[]> GetDropdownSection_ShouldReturnExpectedData()
    {
        PopulateExpectedValues();

        return ExpectedValues.Keys.Select(
            id => new object[] {ExpectedValues[id], id}).ToList();
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IDropdownSectionFactory>();
            
        Assert.NotNull(sut as DropdownSectionFactory);
    }
}