using System.Collections.Generic;

namespace OpenTracker.Models.AutotrackerConnectors
{
    /// <summary>
    /// This is the class containing data for a request sent to USB2SNES websocket.
    /// </summary>
    public class RequestType
    {
        public string Opcode { get; }

        public string Space { get; }

        public List<string> Flags { get; }

        public List<string> Operands { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="opcode">
        /// The opcode of the request.
        /// </param>
        /// <param name="space">
        /// The device space that is being requested against.
        /// </param>
        /// <param name="flags">
        /// The flags to be added to the request.
        /// </param>
        /// <param name="operands">
        /// The operands of the request.
        /// </param>
        public RequestType(string opcode, string space, List<string> flags, List<string> operands)
        {
            Opcode = opcode;
            Space = space;
            Flags = flags;
            Operands = operands;
        }
    }
}
