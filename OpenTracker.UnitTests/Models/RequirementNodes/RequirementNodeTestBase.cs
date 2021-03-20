using Autofac;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.RequirementNodes
{
    public class RequirementNodeTestBase
    {
        public virtual void Tests(
            ModeSaveData modeData, (ItemType, int)[] items, (PrizeType, int)[] prizes,
            (SequenceBreakType, bool)[] sequenceBreaks, RequirementNodeID[] accessibleNodes,
            RequirementNodeID id, bool towerCrystalsKnown, AccessibilityLevel expected)
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var mode = scope.Resolve<IMode>();
            var itemDictionary = scope.Resolve<IItemDictionary>();
            var prizeDictionary = scope.Resolve<IPrizeDictionary>();
            var sequenceBreakDictionary = scope.Resolve<ISequenceBreakDictionary>();
            var requirementNodes = scope.Resolve<IRequirementNodeDictionary>();
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
    
            // foreach (var node in accessibleNodes)
            // {
            //     requirementNodes[node].AlwaysAccessible = true;
            // }

            var towerCrystals = (ICrystalRequirementItem)itemDictionary[ItemType.TowerCrystals];
            towerCrystals.Known = towerCrystalsKnown;
    
            Assert.Equal(expected, expected);
        }
    }
}