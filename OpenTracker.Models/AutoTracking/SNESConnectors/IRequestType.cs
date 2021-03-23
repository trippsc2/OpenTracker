using System.Collections.Generic;

namespace OpenTracker.Models.AutoTracking.SNESConnectors
{
    /// <summary>
    /// This interface contains auto-tracker request data.
    /// </summary>
    public interface IRequestType
    {
        List<string>? Flags { get; }
        string Opcode { get; }
        List<string>? Operands { get; }
        string Space { get; }

        delegate IRequestType Factory(
            string opcode, string space = "SNES", List<string>? flags = null, List<string>? operands = null);
    }
}