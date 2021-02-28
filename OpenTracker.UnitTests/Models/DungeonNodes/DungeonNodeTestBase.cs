using Autofac;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Items;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.DungeonNodes
{
    public class DungeonNodeTestBase
    {
        public virtual void Tests(
            ModeSaveData modeData, RequirementNodeID[] accessibleEntryNodes,
            DungeonNodeID[] accessibleNodes, KeyDoorID[] unlockedDoors, (ItemType, int)[] items,
            (SequenceBreakType, bool)[] sequenceBreaks, LocationID dungeonID, DungeonNodeID id,
            AccessibilityLevel expected)
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var requirementNodes = scope.Resolve<IRequirementNodeDictionary>();
            var locations = scope.Resolve<ILocationDictionary>();
            var factory = scope.Resolve<IMutableDungeon.Factory>();
            var itemDictionary = scope.Resolve<IItemDictionary>();
            var sequenceBreakDictionary = scope.Resolve<ISequenceBreakDictionary>();
            var mode = scope.Resolve<IMode>();

            var dungeon = (IDungeon)locations[dungeonID];
            var dungeonData = factory(dungeon);
            dungeon.FinishMutableDungeonCreation(dungeonData);
            mode.Load(modeData);
            
            foreach (var node in accessibleEntryNodes)
            {
                requirementNodes[node].AlwaysAccessible = true;
            }
        
            foreach (var node in accessibleNodes)
            {
                dungeonData.Nodes[node].AlwaysAccessible = true;
            }
        
            foreach (var item in items)
            {
                itemDictionary[item.Item1].Current = item.Item2;
            }
        
            foreach (var sequenceBreak in sequenceBreaks)
            {
                sequenceBreakDictionary[sequenceBreak.Item1].Enabled =
                    sequenceBreak.Item2;
            }
        
            foreach (var door in unlockedDoors)
            {
                dungeonData.KeyDoors[door].Unlocked = true;
            }
        
            Assert.Equal(expected, dungeonData.Nodes[id].Accessibility);
        }
    }
}