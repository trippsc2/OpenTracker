using System.Collections.Generic;

namespace OpenTracker.Models.AutoTracking.SNESConnectors
{
    /// <summary>
    /// This class contains auto-tracking request data.
    /// </summary>
    public class Request : IRequest
    {
        public string Opcode { get; }
        public string Space { get; }
        public IList<string> Flags { get; }
        public IList<string> Operands { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="opcode">
        ///     A <see cref="string"/> representing opcode of the request.
        /// </param>
        /// <param name="space">
        ///     A <see cref="string"/> representing the device space against which the request is made.
        ///     This defaults to "SNES".
        /// </param>
        /// <param name="flags">
        ///     A <see cref="IList{T}"/> of <see cref="string"/> representing the flags to be added to the request.
        ///     This defaults to an empty list.
        /// </param>
        /// <param name="operands">
        ///     A <see cref="IList{T}"/> of <see cref="string"/> representing the operands of the request.
        ///     This defaults to an empty list.
        /// </param>
        public Request(
            string opcode, string space = "SNES", IList<string>? flags = null, IList<string>? operands = null)
        {
            Opcode = opcode;
            Space = space;
            Flags = flags ?? new List<string>(0);
            Operands = operands ?? new List<string>(0);
        }
    }
}
