using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenTracker.Models.AutoTracking.Memory;

/// <summary>
/// This class contains the memory address data provider.
/// </summary>
public class MemoryAddressProvider : IMemoryAddressProvider
{
    private readonly IMemoryAddress.Factory _addressFactory;
        
    public IDictionary<MemorySegmentType, IList<IMemoryAddress>> MemorySegments { get; } = 
        new Dictionary<MemorySegmentType, IList<IMemoryAddress>>();
    public IDictionary<ulong, IMemoryAddress> MemoryAddresses { get; } = new Dictionary<ulong, IMemoryAddress>();

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="addressFactory">
    ///     An Autofac factory for creating memory addresses.
    /// </param>
    public MemoryAddressProvider(IMemoryAddress.Factory addressFactory)
    {
        _addressFactory = addressFactory;

        var memorySegmentTypeCounts = new Dictionary<MemorySegmentType, int>();

        foreach (MemorySegmentType type in Enum.GetValues(typeof(MemorySegmentType)))
        {
            MemorySegments.Add(type, new List<IMemoryAddress>());
            memorySegmentTypeCounts.Add(type, GetMemorySegmentCount(type));
        }

        var maximumCount = memorySegmentTypeCounts.Values.Max();

        for (var i = 0; i < maximumCount; i++)
        {
            foreach (var (key, value) in memorySegmentTypeCounts)
            {
                if (i < value)
                {
                    CreateMemoryAddress(key, (ulong)i);
                }
            }
        }
            
        MemoryAddresses.Add(0x7e0010, _addressFactory());
    }

    public ulong GetMemorySegmentStart(MemorySegmentType type)
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

    public void Reset()
    {
        foreach (var memoryAddress in MemoryAddresses.Values)
        {
            memoryAddress.Reset();
        }
    }

    /// <summary>
    ///     Creates a memory address for the specified memory segment and offset.
    /// </summary>
    /// <param name="type">
    ///     The memory segment type.
    /// </param>
    /// <param name="offset">
    ///     The offset of the address.
    /// </param>
    private void CreateMemoryAddress(MemorySegmentType type, ulong offset)
    {
        var memoryAddress = _addressFactory();
        var memorySegment = MemorySegments[type];
        memorySegment.Add(memoryAddress);
        var address = GetMemorySegmentStart(type);
        address += offset;
        MemoryAddresses.Add(address, memoryAddress);
    }

    /// <summary>
    /// Returns the number of memory addresses within the specified memory segment.
    /// </summary>
    /// <param name="type">
    ///     The memory segment type.
    /// </param>
    /// <returns>
    ///     A 32-bit signed integer representing the number of memory addresses.
    /// </returns>
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
}