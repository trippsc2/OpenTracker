using System.Collections.Generic;

namespace OpenTracker.Models.AutoTracking.SNESConnectors
{
    /// <summary>
    /// This class contains auto-tracking request data.
    /// </summary>
    public class RequestType : IRequestType
    {
        public string Opcode { get; }
        public string Space { get; }
        public List<string>? Flags { get; }
        public List<string>? Operands { get; }

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
