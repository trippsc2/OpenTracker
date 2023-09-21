using System;
using System.Collections.Generic;
using System.Linq;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.AutoTracking.Memory;

[DependencyInjection(SingleInstance = true)]
public sealed class MemoryAddressProvider : IMemoryAddressProvider
{
    public Dictionary<MemorySegmentType, List<MemoryAddress>> MemorySegments { get; }
    public Dictionary<ulong, MemoryAddress> MemoryAddresses { get; } = new Dictionary<ulong, MemoryAddress>();

    /// <summary>
    /// Initializes a new <see cref="MemoryAddressProvider"/> object.
    /// </summary>
    public MemoryAddressProvider()
    {
        MemorySegments = Enum
            .GetValues<MemorySegmentType>()
            .ToDictionary(type => type, _ => new List<MemoryAddress>());

        foreach (var segment in Enum.GetValues<MemorySegmentType>())
        {
            CreateAndStoreMemorySegmentAddresses(segment);
        }

        MemoryAddresses.Add(0x7e0010, new MemoryAddress());
    }
        
    private void CreateAndStoreMemorySegmentAddresses(MemorySegmentType type)
    {
        var count = GetMemorySegmentCount(type);
        var startAddress = GetMemorySegmentStartingAddress(type);
            
        var memorySegment = MemorySegments[type];

        for (var i = 0; i < count; i++)
        {
            var address = startAddress + (ulong)i;
            var memoryAddress = new MemoryAddress();
            memorySegment.Add(memoryAddress);
            MemoryAddresses.Add(address, memoryAddress);
        }
    }

    public static ulong GetMemorySegmentStartingAddress(MemorySegmentType type)
    {
        return type switch
        {
            MemorySegmentType.FirstRoom => 0x7ef000,
            MemorySegmentType.SecondRoom => 0x7ef080,
            MemorySegmentType.ThirdRoom => 0x7ef100,
            MemorySegmentType.FourthRoom => 0x7ef180,
            MemorySegmentType.FifthRoom => 0x7ef200,
            MemorySegmentType.FirstOverworldEvent => 0x7ef280,
            MemorySegmentType.SecondOverworldEvent => 0x7ef300,
            MemorySegmentType.FirstItem => 0x7ef340,
            MemorySegmentType.SecondItem => 0x7ef3c0,
            MemorySegmentType.NPCItem => 0x7ef410,
            MemorySegmentType.DungeonItem => 0x7ef434,
            MemorySegmentType.Dungeon => 0x7ef4c0,
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
    }
        
    private static int GetMemorySegmentCount(MemorySegmentType type)
    {
        return type switch
        {
            MemorySegmentType.FirstRoom => 128,
            MemorySegmentType.SecondRoom => 128,
            MemorySegmentType.ThirdRoom => 128,
            MemorySegmentType.FourthRoom => 128,
            MemorySegmentType.FifthRoom => 80,
            MemorySegmentType.FirstOverworldEvent => 128,
            MemorySegmentType.SecondOverworldEvent => 2,
            MemorySegmentType.FirstItem => 128,
            MemorySegmentType.SecondItem => 16,
            MemorySegmentType.NPCItem => 2,
            MemorySegmentType.DungeonItem => 6,
            MemorySegmentType.Dungeon => 48,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
    public void Reset()
    {
        foreach (var memoryAddress in MemoryAddresses.Values)
        {
            memoryAddress.Reset();
        }
    }
}