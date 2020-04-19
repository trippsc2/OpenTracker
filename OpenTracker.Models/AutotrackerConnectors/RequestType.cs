using System;
using System.Collections.Generic;
using System.Text;

namespace OpenTracker.Models.AutotrackerConnectors
{
    public class RequestType
    {
        public string Opcode { get; set; }

        public string Space { get; set; }

        public List<string> Flags { get; set; }

        public List<string> Operands { get; set; }

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
