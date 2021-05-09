using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using OpenTracker.Models.AutoTracking.SNESConnectors.Socket;
using OpenTracker.Models.Logging;

namespace OpenTracker.Models.AutoTracking.SNESConnectors.Requests
{
    /// <summary>
    /// This class contains the request to read a sequence of memory addresses. 
    /// </summary>
    public class ReadMemoryRequest : RequestBase<byte[]>, IReadMemoryRequest
    {
        private readonly int _bytesToRead;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">
        ///     The <see cref="IAutoTrackerLogger"/>.
        /// </param>
        /// <param name="address">
        ///     A <see cref="ulong"/> representing the starting memory address of the sequence.
        /// </param>
        /// <param name="bytesToRead">
        ///     A <see cref="int"/> representing the number of memory addresses to read.
        /// </param>
        public ReadMemoryRequest(IAutoTrackerLogger logger, ulong address, int bytesToRead)
            : base(logger, "GetAddress", "SNES", ConnectionStatus.Connected,
                $"Read {bytesToRead} byte(s) at {address:X}", operands: new List<string>
                {
                    AddressTranslator.TranslateAddress((uint)address).ToString("X", CultureInfo.InvariantCulture),
                    bytesToRead.ToString("X", CultureInfo.InvariantCulture)
                })
        {
            _bytesToRead = bytesToRead;
        }

        public override byte[] ProcessResponseAndReturnResults(IMessageEventArgsWrapper messageEventArgs,
            ManualResetEvent sendEvent)
        {
            if (!messageEventArgs.IsBinary || messageEventArgs.RawData is null)
            {
                throw new Exception($"Did not receive expected binary data from request \'{Description}\'.");
            }

            if (messageEventArgs.RawData.Length != _bytesToRead)
            {
                throw new Exception(
                    $"Expected to received {_bytesToRead} byte(s), but received " +
                    $"{messageEventArgs.RawData.Length} instead.");
            }

            sendEvent.Set();
            return messageEventArgs.RawData;
        }
    }
}