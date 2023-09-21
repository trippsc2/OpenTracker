using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Result;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Dungeons.AccessibilityProvider;

/// <summary>
/// This class contains the logic for aggregating dungeon results into final values.
/// </summary>
[DependencyInjection]
public sealed class ResultAggregator : IResultAggregator
{
    private readonly IDungeonResult.Factory _resultFactory;

    private readonly IDungeon _dungeon;
    private readonly IMutableDungeonQueue _mutableDungeonQueue;

    private IList<DungeonItemID> Bosses => _dungeon.Bosses;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="resultFactory">
    ///     An Autofac factory for creating new <see cref="IDungeonResult"/> objects.
    /// </param>
    /// <param name="dungeon">
    ///     The <see cref="IDungeon"/>.
    /// </param>
    /// <param name="mutableDungeonQueue">
    ///     The <see cref="IMutableDungeonQueue"/>.
    /// </param>
    public ResultAggregator(
        IDungeonResult.Factory resultFactory, IDungeon dungeon, IMutableDungeonQueue mutableDungeonQueue)
    {
        _resultFactory = resultFactory;

        _dungeon = dungeon;
        _mutableDungeonQueue = mutableDungeonQueue;
    }

    public IDungeonResult AggregateResults(BlockingCollection<IDungeonState> finalQueue)
    {
        var resultQueue = new BlockingCollection<IDungeonResult>();
            
        ProcessFinalKeyDoorPermutationQueue(finalQueue, resultQueue);

        return ProcessResults(resultQueue);
    }

    /// <summary>
    /// Processes the final permutation queue.
    /// </summary>
    /// <param name="finalQueue">
    ///     The <see cref="BlockingCollection{T}"/> queue of <see cref="IDungeonState"/> permutations to be
    ///     processed.
    /// </param>
    /// <param name="resultQueue">
    ///     The <see cref="BlockingCollection{T}"/> queue of <see cref="IDungeonResult"/>.
    /// </param>
    private void ProcessFinalKeyDoorPermutationQueue(
        BlockingCollection<IDungeonState> finalQueue, BlockingCollection<IDungeonResult> resultQueue)
    {
        var dungeonData = _mutableDungeonQueue.GetNext();

        foreach (var item in finalQueue.GetConsumingEnumerable())
        {
            ProcessFinalDungeonState(dungeonData, item, resultQueue);
        }

        finalQueue.Dispose();
        resultQueue.CompleteAdding();
        _mutableDungeonQueue.Requeue(dungeonData);
    }

    /// <summary>
    /// Process the final dungeon state permutation.
    /// </summary>
    /// <param name="dungeonData">
    ///     The <see cref="IMutableDungeon"/>.
    /// </param>
    /// <param name="state">
    ///     The <see cref="IDungeonState"/> permutation.
    /// </param>
    /// <param name="resultQueue">
    ///     The <see cref="BlockingCollection{T}"/> queue of <see cref="IDungeonResult"/>.
    /// </param>
    private static void ProcessFinalDungeonState(
        IMutableDungeon dungeonData, IDungeonState state, BlockingCollection<IDungeonResult> resultQueue)
    {
        dungeonData.ApplyState(state);

        if (!dungeonData.ValidateKeyLayout(state))
        {
            return;
        }

        var result = dungeonData.GetDungeonResult(state);

        resultQueue.Add(result);
    }
        
    /// <summary>
    /// Returns the final results.
    /// </summary>
    /// <param name="resultQueue">
    ///     A <see cref="BlockingCollection{T}"/> queue for <see cref="IDungeonResult"/>.
    /// </param>
    /// <returns>
    ///     The final <see cref="IDungeonResult"/>.
    /// </returns>
    private IDungeonResult ProcessResults(BlockingCollection<IDungeonResult> resultQueue)
    {
        List<AccessibilityLevel> lowestBossAccessibility = new();
        List<AccessibilityLevel> highestBossAccessibility = new();

        foreach (var _ in Bosses)
        {
            lowestBossAccessibility.Add(AccessibilityLevel.Normal);
            highestBossAccessibility.Add(AccessibilityLevel.None);
        }

        IDungeonResult? lowestAccessible = null;
        IDungeonResult? highestAccessible = null;

        ProcessResultQueue(
            resultQueue, lowestBossAccessibility, highestBossAccessibility, ref lowestAccessible,
            ref highestAccessible);

        resultQueue.Dispose();

        var bossAccessibility = GetFinalBossAccessibility(
            highestBossAccessibility, lowestBossAccessibility);
            
        if (highestAccessible is null)
        {
            return _resultFactory(bossAccessibility, 0, true, false);
        }

        var accessible = AdjustResultForMapCompassAndBosses(highestAccessible);

        if (lowestAccessible is null)
        {
            return _resultFactory(
                bossAccessibility, accessible, highestAccessible.SequenceBreak, highestAccessible.Visible);
        }

        var total = _dungeon.TotalWithMapAndCompass;

        var sequenceBreak = highestAccessible.SequenceBreak ||
                            highestAccessible.Accessible > lowestAccessible.Accessible || highestAccessible.Accessible < total;

        return _resultFactory(bossAccessibility, accessible, sequenceBreak, highestAccessible.Visible);
    }

    /// <summary>
    /// Processes a provided result queue. 
    /// </summary>
    /// <param name="resultQueue">
    ///     The <see cref="BlockingCollection{T}"/> queue of <see cref="IDungeonResult"/> to be processed.
    /// </param>
    /// <param name="lowestBossAccessibility">
    ///     A <see cref="List{T}"/> of the lowest <see cref="AccessibilityLevel"/> for each boss.
    /// </param>
    /// <param name="highestBossAccessibility">
    ///     A <see cref="List{T}"/> of the highest <see cref="AccessibilityLevel"/> for each boss.
    /// </param>
    /// <param name="lowestAccessible">
    ///     The nullable <see cref="IDungeonResult"/> with the fewest accessible items.
    /// </param>
    /// <param name="highestAccessible">
    ///     The nullable <see cref="IDungeonResult"/> with the most accessible items.
    /// </param>
    private void ProcessResultQueue(
        BlockingCollection<IDungeonResult> resultQueue, List<AccessibilityLevel> lowestBossAccessibility,
        List<AccessibilityLevel> highestBossAccessibility, ref IDungeonResult? lowestAccessible,
        ref IDungeonResult? highestAccessible)
    {
        foreach (var result in resultQueue.GetConsumingEnumerable())
        {
            ProcessBossAccessibilityResult(result, lowestBossAccessibility, highestBossAccessibility);
            ProcessItemAccessibilityResult(result, ref lowestAccessible, ref highestAccessible);
        }
    }

    /// <summary>
    /// Processes a result's boss accessibility values.
    /// </summary>
    /// <param name="result">
    ///     The <see cref="IDungeonResult"/> to be processed.
    /// </param>
    /// <param name="lowestBossAccessibility">
    ///     A <see cref="List{T}"/> of the lowest <see cref="AccessibilityLevel"/> for each boss.
    /// </param>
    /// <param name="highestBossAccessibility">
    ///     A <see cref="List{T}"/> of the highest <see cref="AccessibilityLevel"/> for each boss.
    /// </param>
    private static void ProcessBossAccessibilityResult(
        IDungeonResult result, List<AccessibilityLevel> lowestBossAccessibility,
        List<AccessibilityLevel> highestBossAccessibility)
    {
        for (var i = 0; i < result.BossAccessibility.Count; i++)
        {
            highestBossAccessibility[i] = AccessibilityLevelMethods.Max(
                highestBossAccessibility[i], result.BossAccessibility[i]);

            if (result.SequenceBreak)
            {
                continue;
            }

            lowestBossAccessibility[i] = AccessibilityLevelMethods.Min(
                lowestBossAccessibility[i], result.BossAccessibility[i]);
        }
    }

    /// <summary>
    /// Processes a result's item accessibility values.
    /// </summary>
    /// <param name="result">
    ///     The <see cref="IDungeonResult"/> to be processed.
    /// </param>
    /// <param name="lowestAccessible">
    ///     The nullable <see cref="IDungeonResult"/> with the fewest accessible items.
    /// </param>
    /// <param name="highestAccessible">
    ///     The nullable <see cref="IDungeonResult"/> with the most accessible items.
    /// </param>
    private void ProcessItemAccessibilityResult(
        IDungeonResult result, ref IDungeonResult? lowestAccessible, ref IDungeonResult? highestAccessible)
    {
        highestAccessible ??= result;
            
        var adjustedHighestAccessible = AdjustResultForMapCompassAndBosses(highestAccessible);
        var adjustedResultAccessible = AdjustResultForMapCompassAndBosses(result);
            
        if (adjustedHighestAccessible < adjustedResultAccessible || 
            adjustedHighestAccessible == adjustedResultAccessible && 
            highestAccessible.Accessible < result.Accessible)
        {
            highestAccessible = result;
        }
            
        if (result.SequenceBreak)
        {
            return;
        }

        if (lowestAccessible is null)
        {
            lowestAccessible = result;
            return;
        }
            
        var adjustedLowestAccessible = AdjustResultForMapCompassAndBosses(lowestAccessible);
            
        if (adjustedLowestAccessible > adjustedResultAccessible ||
            adjustedLowestAccessible == adjustedResultAccessible &&
            lowestAccessible.Accessible > result.Accessible)
        {
            lowestAccessible = result;
        }
    }

    /// <summary>
    /// Adjusts the accessible result to account for maps, compasses, and guaranteed boss items.
    /// </summary>
    /// <param name="result">
    ///     The <see cref="IDungeonResult"/> to be adjusted.
    /// </param>
    /// <returns>
    ///     A <see cref="int"/> representing the adjusted result.
    /// </returns>
    private int AdjustResultForMapCompassAndBosses(IDungeonResult result)
    {
        return Math.Min(result.Accessible, _dungeon.Total - result.MinimumInaccessible);
    }

    /// <summary>
    /// Returns the <see cref="IList{T}"/> of final boss <see cref="AccessibilityLevel"/> values.
    /// </summary>
    /// <param name="highestBossAccessibility">
    ///     A <see cref="List{T}"/> of <see cref="AccessibilityLevel"/> values for the most accessible.
    /// </param>
    /// <param name="lowestBossAccessibility">
    ///     A <see cref="List{T}"/> of <see cref="AccessibilityLevel"/> values for the least accessible.
    /// </param>
    /// <returns>
    ///     A <see cref="IList{T}"/> of boss <see cref="AccessibilityLevel"/> values.
    /// </returns>
    private static IList<AccessibilityLevel> GetFinalBossAccessibility(
        List<AccessibilityLevel> highestBossAccessibility, List<AccessibilityLevel> lowestBossAccessibility)
    {
        var bossAccessibility = new List<AccessibilityLevel>();
            
        for (var i = 0; i < highestBossAccessibility.Count; i++)
        {
            var highestAccessibility = highestBossAccessibility[i];
            var lowestAccessibility = lowestBossAccessibility[i];

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