using Autofac;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements
{
    public class ComplexItemRequirementTestBase
    {
         public virtual void AccessibilityTests(
             ModeSaveData modeData, (ItemType, int)[] items, (PrizeType, int)[] prizes,
             (SequenceBreakType, bool)[] sequenceBreaks, (LocationID, int)[] smallKeys,
             (LocationID, int)[] bigKeys, RequirementNodeID[] accessibleNodes, RequirementType type,
             AccessibilityLevel expected)
         {
             var container = ContainerConfig.Configure();

             using var scope = container.BeginLifetimeScope();
             var mode = scope.Resolve<IMode>();
             var itemDictionary = scope.Resolve<IItemDictionary>();
             var locations = scope.Resolve<ILocationDictionary>();
             var prizeDictionary = scope.Resolve<IPrizeDictionary>();
             var sequenceBreakDictionary = scope.Resolve<ISequenceBreakDictionary>();
             var requirementNodes = scope.Resolve<IRequirementNodeDictionary>();
             var requirements = scope.Resolve<IRequirementDictionary>();
             
             mode.Load(modeData);

             foreach (var item in items)
             {
                 itemDictionary[item.Item1].Current = item.Item2;
             }

             foreach (var prize in prizes)
             {
                 prizeDictionary[prize.Item1].Current = prize.Item2;
             }

             foreach (var sequenceBreak in sequenceBreaks)
             {
                 sequenceBreakDictionary[sequenceBreak.Item1].Enabled =
                     sequenceBreak.Item2;
             }

             foreach (var smallKey in smallKeys)
             {
                 ((IDungeon)locations[smallKey.Item1]).SmallKeyItem.Current =
                     smallKey.Item2;
             }

             foreach (var bigKey in bigKeys)
             {
                 ((IDungeon)locations[bigKey.Item1]).BigKeyItem.Current =
                     bigKey.Item2;
             }

             // foreach (var accessibleNode in accessibleNodes)
             // {
             //     requirementNodes[accessibleNode].AlwaysAccessible = true;
             // }

             Assert.Equal(expected, requirements[type].Accessibility);
         }
    }
}