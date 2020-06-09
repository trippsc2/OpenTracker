using System.Collections.Generic;

namespace OpenTracker.Models.AutotrackerConnectors
{
    public class RequestType
    {
        public string Opcode { get; }

        public string Space { get; }

        public List<string> Flags { get; }

        public List<string> Operands { get; }

        public RequestType(string opcode, string space, List<string> flags, List<string> operands)
        {
            Opcode = opcode;
            Space = space;
            Flags = flags;
            Operands = operands;
        }

        public bool RequiresData()
        {
            return Opcode == "GetAddress" || Opcode == "GetFile" || Opcode == "List" ||
                Opcode == "Info" || this.Opcode == "Stream";
        }

        public bool HasData()
        {
            return Opcode == "PutAddress" || Opcode == "PutFile";
        }
    }
}
