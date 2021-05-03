using System.Collections.Generic;

namespace OpenTracker.Models.AutoTracking.SNESConnectors
{
    /// <summary>
    /// This interface contains auto-tracker request data.
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// A <see cref="string"/> representing opcode of the request.
        /// </summary>
        string Opcode { get; }
        
        /// <summary>
        /// A <see cref="string"/> representing the device space against which the request is made.
        /// </summary>
        string Space { get; }
        
        /// <summary>
        /// A <see cref="IList{T}"/> of <see cref="string"/> objects representing the flags to be added to the request.
        /// This defaults to an empty list.
        /// </summary>
        IList<string> Flags { get; }

        /// <summary>
        /// A <see cref="IList{T}"/> of <see cref="string"/> representing the operands of the request.
        /// This defaults to an empty list.
        /// </summary>
        IList<string> Operands { get; }

        /// <summary>
        /// A factory for creating new <see cref="IRequest"/> objects.
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
        /// <returns>
        ///     A new <see cref="IRequest"/> object.
        /// </returns>
        delegate IRequest Factory(
            string opcode, string space = "SNES", IList<string>? flags = null, IList<string>? operands = null);
    }
}