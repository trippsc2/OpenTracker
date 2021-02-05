using OpenTracker.Models.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using WebSocketSharp;

namespace OpenTracker.Models.AutoTracking
{
    /// <summary>
    /// This is the class containing autotracking data and methods
    /// </summary>
    public class AutoTracker : Singleton<AutoTracker>, INotifyPropertyChanged
    {
        private byte? _inGameStatus;

        private bool InGame =>
            _inGameStatus.HasValue && _inGameStatus.Value > 0x05 && _inGameStatus.Value != 0x14 &&
            _inGameStatus.Value < 0x20;
        public ISNESConnector SNESConnector { get; }
        public Action<LogLevel, string> LogHandler { get; set; }

        public Dictionary<ulong, MemoryAddress> MemoryAddresses { get; } =
            new Dictionary<ulong, MemoryAddress>();

        public Dictionary<MemorySegmentType, List<MemoryAddress>> MemorySegments { get; } =
            new Dictionary<MemorySegmentType, List<MemoryAddress>>();

        public List<MemoryAddress> RoomMemory { get; } =
            new List<MemoryAddress>(592);
        public List<MemoryAddress> OverworldEventMemory { get; } =
            new List<MemoryAddress>(130);
        public List<MemoryAddress> ItemMemory { get; } =
            new List<MemoryAddress>(144);
        public List<MemoryAddress> NPCItemMemory { get; } =
            new List<MemoryAddress>(2);
        public List<MemoryAddress> DungeonItemMemory { get; } =
            new List<MemoryAddress>(6);
        public List<MemoryAddress> DungeonMemory { get; } =
            new List<MemoryAddress>(16);

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _raceIllegalTracking;
        public bool RaceIllegalTracking
        {
            get => _raceIllegalTracking;
            set
            {
                if (_raceIllegalTracking != value)
                {
                    _raceIllegalTracking = value;
                    PropertyChanged?.Invoke(
                        this, new PropertyChangedEventArgs(nameof(RaceIllegalTracking)));
                }
            }
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        public AutoTracker()
        {
            SNESConnector = SNESConnectorFactory.GetSNESConnector(HandleLog);

            foreach (MemorySegmentType type in Enum.GetValues(typeof(MemorySegmentType)))
            {
                MemorySegments.Add(type, new List<MemoryAddress>());
            }

            for (ulong i = 0; i < 592; i++)
            {
                CreateMemoryAddress(MemorySegmentType.Room, i);

                if (i < 130)
                {
                    CreateMemoryAddress(MemorySegmentType.OverworldEvent, i);
                }

                if (i < 144)
                {
                    CreateMemoryAddress(MemorySegmentType.Item, i);
                }

                if (i < 48)
                {
                    CreateMemoryAddress(MemorySegmentType.Dungeon, i);
                }

                if (i < 6)
                {
                    CreateMemoryAddress(MemorySegmentType.DungeonItem, i);
                }

                if (i < 2)
                {
                    CreateMemoryAddress(MemorySegmentType.NPCItem, i);
                }
            }
        }

        /// <summary>
        /// Returns the starting address of the specified memory segment.
        /// </summary>
        /// <param name="type">
        /// The memory segment type.
        /// </param>
        /// <returns>
        /// A 64-bit unsigned integer representing the starting memory address of the segment.
        /// </returns>
        private static ulong GetMemorySegmentStart(MemorySegmentType type)
        {
            return type switch
            {
                MemorySegmentType.Room => 0x7ef000,
                MemorySegmentType.OverworldEvent => 0x7ef280,
                MemorySegmentType.Item => 0x7ef340,
                MemorySegmentType.NPCItem => 0x7ef410,
                MemorySegmentType.DungeonItem => 0x7ef434,
                MemorySegmentType.Dungeon => 0x7ef4c0,
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Creates a memory address for the specified memory segment and offset.
        /// </summary>
        /// <param name="type">
        /// The memory segment type.
        /// </param>
        /// <param name="offset">
        /// The offset of the address.
        /// </param>
        private void CreateMemoryAddress(MemorySegmentType type, ulong offset)
        {
            var memoryAddress = new MemoryAddress();
            var memorySegment = MemorySegments[type];
            memorySegment.Add(memoryAddress);
            var address = GetMemorySegmentStart(type);
            address += (ulong)offset;
            MemoryAddresses.Add(address, memoryAddress);
        }

        /// <summary>
        /// Calls the LogHandler property and is passed to the connector to allow for log handling.
        /// </summary>
        /// <param name="logLevel">
        /// The log level of the event to be logged.
        /// </param>
        /// <param name="message">
        /// A string representing the log message.
        /// </param>
        private void HandleLog(LogLevel logLevel, string message)
        {
            LogHandler?.Invoke(logLevel, message);
        }

        /// <summary>
        /// Updates cached value of the SNES memory address that provides game status.
        /// </summary>
        public void InGameCheck()
        {
            if (SNESConnector != null && SNESConnector.Socket != null &&
                SNESConnector.Status != ConnectionStatus.Error &&
                SNESConnector.Read(0x7e0010, out byte inGameStatus))
            {
                _inGameStatus = inGameStatus;
            }
        }

        /// <summary>
        /// Updates cached values of a memory segment.
        /// </summary>
        /// <param name="type">
        /// The memory segment to updated.
        /// </param>
        public void MemoryCheck(MemorySegmentType type)
        {
            if (SNESConnector != null && SNESConnector.Socket != null &&
                SNESConnector.Status != ConnectionStatus.Error && InGame)
            {
                var memorySegment = MemorySegments[type];
                var startAddress = GetMemorySegmentStart(type);
                byte[] buffer = new byte[memorySegment.Count];

                if (SNESConnector.Read(startAddress, buffer))
                {
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        memorySegment[i].Value = buffer[i];
                    }
                }
            }
        }

        /// <summary>
        /// Returns an enumerator of devices to which can be connected.
        /// </summary>
        /// <returns>
        /// An enumerator of devices to which can be connected.
        /// </returns>
        public IEnumerable<string> GetDevices()
        {
            return SNESConnector.GetDevices();
        }

        /// <summary>
        /// Disconnects from the USB2SNES websocket, disposes of the connection, and
        ///  resets all memory addresses to null or 0.
        /// </summary>
        public void Stop()
        {
            SNESConnector.Disconnect();
            _inGameStatus = null;

            foreach (var address in RoomMemory)
            {
                address.Reset();
            }

            foreach (var address in OverworldEventMemory)
            {
                address.Reset();
            }

            foreach (var address in ItemMemory)
            {
                address.Reset();
            }

            foreach (var address in NPCItemMemory)
            {
                address.Reset();
            }

            foreach (var address in DungeonItemMemory)
            {
                address.Reset();
            }

            foreach (var address in DungeonMemory)
            {
                address.Reset();
            }
        }
    }
}
