using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Result;
using OpenTracker.Models.Dungeons.State;

namespace OpenTracker.Models.Dungeons.AccessibilityProvider
{
    /// <summary>
    ///     This class contains the logic for aggregating dungeon results into final values.
    /// </summary>
    public class ResultAggregator : IResultAggregator
    {
        private readonly IDungeon _dungeon;
        private readonly IMutableDungeonQueue _mutableDungeonQueue;
        
        private List<DungeonItemID> Bosses => _dungeon.Bosses;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="dungeon">
        ///     The dungeon data.
        /// </param>
        /// <param name="mutableDungeonQueue">
        ///     The mutable dungeon data instance queue.
        /// </param>
        public ResultAggregator(IDungeon dungeon, IMutableDungeonQueue mutableDungeonQueue)
        {
            _dungeon = dungeon;
            _mutableDungeonQueue = mutableDungeonQueue;
        }

        /// <summary>
        ///     Returns a tuple containing final boss and item accessibility values.
        /// </summary>
        /// <param name="finalQueue">
        ///     The blocking collection queue for final key door permutations.
        /// </param>
        /// <returns>
        ///     A tuple containing final boss and item accessibility values.
        /// </returns>
        public (List<AccessibilityLevel> bossAccessibility, bool visible, bool sequenceBreak, int accessible)
            AggregateResults(BlockingCollection<IDungeonState> finalQueue)
        {
            var inLogicResultQueue = new BlockingCollection<IDungeonResult>();
            var outOfLogicResultQueue = new BlockingCollection<IDungeonResult>();
            
            ProcessFinalKeyDoorPermutationQueue(finalQueue, inLogicResultQueue, outOfLogicResultQueue);

            return ProcessResults(inLogicResultQueue, outOfLogicResultQueue);
        }

        /// <summary>
        ///     Processes the final permutation queue.
        /// </summary>
        /// <param name="finalQueue">
        ///     The blocking collection queue to be processed.
        /// </param>
        /// <param name="inLogicResultQueue">
        ///     The blocking collection queue of in-logic results.
        /// </param>
        /// <param name="outOfLogicResultQueue">
        ///     The blocking collection queue of out-of-logic results.
        /// </param>
        private void ProcessFinalKeyDoorPermutationQueue(
            BlockingCollection<IDungeonState> finalQueue, BlockingCollection<IDungeonResult> inLogicResultQueue,
            BlockingCollection<IDungeonResult> outOfLogicResultQueue)
        {
            var dungeonData = _mutableDungeonQueue.GetNext();

            foreach (var item in finalQueue.GetConsumingEnumerable())
            {
                ProcessFinalDungeonState(dungeonData, item, inLogicResultQueue, outOfLogicResultQueue);
            }

            finalQueue.Dispose();
            inLogicResultQueue.CompleteAdding();
            outOfLogicResultQueue.CompleteAdding();
            _mutableDungeonQueue.Requeue(dungeonData);
        }

        /// <summary>
        ///     Process the final dungeon state permutation.
        /// </summary>
        /// <param name="dungeonData">
        ///     The mutable dungeon data.
        /// </param>
        /// <param name="state">
        ///     The permutation to be processed.
        /// </param>
        /// <param name="inLogicQueue">
        ///     The queue of results that were achieved without sequence breaks.
        /// </param>
        /// <param name="outOfLogicQueue">
        ///     The queue of results that were achieved with sequence breaks.
        /// </param>
        private static void ProcessFinalDungeonState(
            IMutableDungeon dungeonData, IDungeonState state, BlockingCollection<IDungeonResult> inLogicQueue,
            BlockingCollection<IDungeonResult> outOfLogicQueue)
        {
            dungeonData.ApplyState(state);

            if (!dungeonData.ValidateKeyLayout(state))
            {
                return;
            }

            var result = dungeonData.GetDungeonResult(state);

            if (state.SequenceBreak)
            {
                outOfLogicQueue.Add(result);
                return;
            }

            inLogicQueue.Add(result);
        }


        /// <summary>
        ///     Returns the final queue results.
        /// </summary>
        /// <param name="resultInLogicQueue">
        ///     A blocking collection queue for results in-logic.
        /// </param>
        /// <param name="resultOutOfLogicQueue">
        ///     A blocking collection queue for results out-of-logic.
        /// </param>
        /// <returns>
        ///     A tuple containing all of the final results.
        /// </returns>
        private (List<AccessibilityLevel> bossAccessibility, bool visible, bool sequenceBreak, int accessible)
            ProcessResults(
                BlockingCollection<IDungeonResult> resultInLogicQueue,
                BlockingCollection<IDungeonResult> resultOutOfLogicQueue)
        {
            List<AccessibilityLevel> lowestBossAccessibilities = new();
            List<AccessibilityLevel> highestBossAccessibilities = new();

            foreach (var _ in Bosses)
            {
                lowestBossAccessibilities.Add(AccessibilityLevel.Normal);
                highestBossAccessibilities.Add(AccessibilityLevel.None);
            }

            var lowestAccessible = int.MaxValue;
            var highestAccessible = 0;
            var sequenceBreak = false;
            var visible = false;

            foreach (var result in resultInLogicQueue.GetConsumingEnumerable())
            {
                ProcessBossAccessibilityResult(result, lowestBossAccessibilities, highestBossAccessibilities);
                ProcessItemAccessibilityResult(
                    result, ref lowestAccessible, ref highestAccessible, ref sequenceBreak, ref visible);
            }

            foreach (var result in resultOutOfLogicQueue.GetConsumingEnumerable())
            {
                ProcessBossAccessibilityResult(result, lowestBossAccessibilities, highestBossAccessibilities);
                ProcessItemAccessibilityResult(
                    result, ref lowestAccessible, ref highestAccessible, ref sequenceBreak, ref visible);
            }

            resultInLogicQueue.Dispose();
            resultOutOfLogicQueue.Dispose();

            var bossAccessibility = GetFinalBossAccessibility(
                highestBossAccessibilities, lowestBossAccessibilities);

            if (highestAccessible > lowestAccessible)
            {
                sequenceBreak = true;
            }
            
            return (bossAccessibility, visible, sequenceBreak, highestAccessible);
        }

        /// <summary>
        ///     Processes a result's boss accessibility values.
        /// </summary>
        /// <param name="result">
        ///     The result to be processed.
        /// </param>
        /// <param name="lowestBossAccessibilities">
        ///     A list of the lowest accessibilities for each boss so far.
        /// </param>
        /// <param name="highestBossAccessibilities">
        ///     A list of the highest accessibilities for each boss so far.
        /// </param>
        private static void ProcessBossAccessibilityResult(
            IDungeonResult result, IList<AccessibilityLevel> lowestBossAccessibilities,
            IList<AccessibilityLevel> highestBossAccessibilities)
        {
            for (var i = 0; i < result.BossAccessibility.Count; i++)
            {
                highestBossAccessibilities[i] = AccessibilityLevelMethods.Max(
                    highestBossAccessibilities[i], result.BossAccessibility[i]);

                if (result.SequenceBreak)
                {
                    continue;
                }

                lowestBossAccessibilities[i] = AccessibilityLevelMethods.Min(
                    lowestBossAccessibilities[i], result.BossAccessibility[i]);
            }
        }

        /// <summary>
        ///     Processes a result's item accessibility values.
        /// </summary>
        /// <param name="result">
        ///     The result to be processed.
        /// </param>
        /// <param name="highestAccessible">
        ///     A 32-bit signed integer representing the most accessible checks.
        /// </param>
        /// <param name="lowestAccessible">
        ///     A 32-bit signed integer representing the least accessible checks.
        /// </param>
        /// <param name="sequenceBreak">
        ///     A boolean representing whether the result required a sequence break.
        /// </param>
        /// <param name="visible">
        ///     A boolean representing whether the last inaccessible item is visible.
        /// </param>
        private static void ProcessItemAccessibilityResult(
            IDungeonResult result, ref int lowestAccessible, ref int highestAccessible, ref bool sequenceBreak,
            ref bool visible)
        {
            if (result.SequenceBreak && highestAccessible < result.Accessible)
            {
                sequenceBreak = true;
            }
            
            highestAccessible = Math.Max(highestAccessible, result.Accessible);
            visible |= result.Visible;

            if (result.SequenceBreak)
            {
                return;
            }

            lowestAccessible = Math.Min(lowestAccessible, result.Accessible);
        }
        
        /// <summary>
        ///     Returns the list of final boss accessibility values.
        /// </summary>
        /// <param name="highestBossAccessibilities">
        ///     A list of accessibility values for the most accessible.
        /// </param>
        /// <param name="lowestBossAccessibilities">
        ///     A list of accessibility values for the least accessible.
        /// </param>
        /// <returns>
        ///     A list of boss accessibility values.
        /// </returns>
        private static List<AccessibilityLevel> GetFinalBossAccessibility(
            List<AccessibilityLevel> highestBossAccessibilities, List<AccessibilityLevel> lowestBossAccessibilities)
        {
            var bossAccessibility = new List<AccessibilityLevel>();
            
            for (var i = 0; i < highestBossAccessibilities.Count; i++)
            {
                var highestAccessibility = highestBossAccessibilities[i];
                var lowestAccessibility = lowestBossAccessibilities[i];

                bossAccessibility.Add(highestAccessibility switch
                {
                    AccessibilityLevel.None => AccessibilityLevel.None,
                    AccessibilityLevel.SequenceBreak => AccessibilityLevel.SequenceBreak,
                    AccessibilityLevel.Normal when lowestAccessibility < AccessibilityLevel.Normal =>
                        AccessibilityLevel.SequenceBreak,
                    AccessibilityLevel.Normal => AccessibilityLevel.Normal,
                    _ => throw new Exception("Boss accessibility in unexpected state.")
                });
            }

            return bossAccessibility;
        }
    }
}