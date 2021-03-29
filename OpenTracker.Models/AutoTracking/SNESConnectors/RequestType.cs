using System.Collections.Generic;

namespace OpenTracker.Models.AutoTracking.SNESConnectors
{
    /// <summary>
    ///     This class contains auto-tracking request data.
    /// </summary>
    public class RequestType : IRequestType
    {
        public string Opcode { get; }
        public string Space { get; }
        public List<string> Flags { get; }
        public List<string> Operands { get; }

        /// <summary>
        ///     Constructor
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
        public RequestType(
            string opcode, string space = "SNES", List<string>? flags = null, List<string>? operands = null)
        {
            Opcode = opcode;
            Space = space;
            Flags = flags ?? new List<string>(0);
            Operands = operands ?? new List<string>(0);
        }
    }
}
