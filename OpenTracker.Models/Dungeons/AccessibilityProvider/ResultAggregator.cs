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
        private readonly IDungeonResult.Factory _resultFactory;

        private readonly IDungeon _dungeon;
        private readonly IMutableDungeonQueue _mutableDungeonQueue;

        private IEnumerable<DungeonItemID> Bosses => _dungeon.Bosses;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="dungeon">
        ///     The dungeon data.
        /// </param>
        /// <param name="mutableDungeonQueue">
        ///     The mutable dungeon data instance queue.
        /// </param>
        /// <param name="resultFactory">
        ///     An Autofac factory for creating dungeon results.
        /// </param>
        public ResultAggregator(
            IDungeonResult.Factory resultFactory, IDungeon dungeon, IMutableDungeonQueue mutableDungeonQueue)
        {
            _dungeon = dungeon;
            _mutableDungeonQueue = mutableDungeonQueue;
            _resultFactory = resultFactory;
        }

        public IDungeonResult AggregateResults(BlockingCollection<IDungeonState> finalQueue)
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
        ///     Returns the final results.
        /// </summary>
        /// <param name="inLogicResultQueue">
        ///     A blocking collection queue for results in-logic.
        /// </param>
        /// <param name="outOfLogicResultQueue">
        ///     A blocking collection queue for results out-of-logic.
        /// </param>
        /// <returns>
        ///     The final results.
        /// </returns>
        private IDungeonResult ProcessResults(
            BlockingCollection<IDungeonResult> inLogicResultQueue,
            BlockingCollection<IDungeonResult> outOfLogicResultQueue)
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

            foreach (var result in inLogicResultQueue.GetConsumingEnumerable())
            {
                ProcessBossAccessibilityResult(result, lowestBossAccessibilities, highestBossAccessibilities);
                ProcessItemAccessibilityResult(
                    result, ref lowestAccessible, ref highestAccessible, ref sequenceBreak, ref visible);
            }

            foreach (var result in outOfLogicResultQueue.GetConsumingEnumerable())
            {
                ProcessBossAccessibilityResult(result, lowestBossAccessibilities, highestBossAccessibilities);
                ProcessItemAccessibilityResult(
                    result, ref lowestAccessible, ref highestAccessible, ref sequenceBreak, ref visible);
            }

            inLogicResultQueue.Dispose();
            outOfLogicResultQueue.Dispose();

            var bossAccessibility = GetFinalBossAccessibility(
                highestBossAccessibilities, lowestBossAccessibilities);

            if (highestAccessible > lowestAccessible)
            {
                sequenceBreak = true;
            }

            var total = _dungeon.TotalWithMapAndCompass;

            if (highestAccessible < total)
            {
                sequenceBreak = true;
            }
            
            var accessible = Math.Min(highestAccessible, _dungeon.Total);

            return _resultFactory(bossAccessibility, accessible, sequenceBreak, visible);
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
            IDungeonResult result, List<AccessibilityLevel> lowestBossAccessibilities,
            List<AccessibilityLevel> highestBossAccessibilities)
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
        private static IList<AccessibilityLevel> GetFinalBossAccessibility(
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