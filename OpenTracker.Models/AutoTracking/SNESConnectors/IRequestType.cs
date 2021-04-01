using System.Collections.Generic;

namespace OpenTracker.Models.AutoTracking.SNESConnectors
{
    /// <summary>
    ///     This interface contains auto-tracker request data.
    /// </summary>
    public interface IRequestType
    {
        /// <summary>
        ///     A string representing opcode of the request.
        /// </summary>
        string Opcode { get; }
        
        /// <summary>
        ///     A string representing the device space against which the request is made.
        /// </summary>
        string Space { get; }
        
        /// <summary>
        ///     A list of strings representing the flags to be added to the request. This defaults to an empty list.
        /// </summary>
        IList<string> Flags { get; }

        /// <summary>
        ///     A list of strings representing the operands of the request. This defaults to an empty list.
        /// </summary>
        IList<string> Operands { get; }

        /// <summary>
        ///     A factory for creating requests.
        /// </summary>
        /// <param name="opcode">
        ///     A string representing opcode of the request.
        /// </param>
        /// <param name="space">
        ///     A string representing the device space against which the request is made. This defaults to "SNES".
        /// </param>
        /// <param name="flags">
        ///     A list of strings representing the flags to be added to the request. This defaults to an empty list.
        /// </param>
        /// <param name="operands">
        ///     A list of strings representing the operands of the request. This defaults to an empty list.
        /// </param>
        delegate IRequestType Factory(
            string opcode, string space = "SNES", IList<string>? flags = null, IList<string>? operands = null);
    }
}