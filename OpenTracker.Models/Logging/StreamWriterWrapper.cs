using System.IO;

namespace OpenTracker.Models.Logging
{
    /// <summary>
    /// This class wraps the <see cref="StreamWriter"/> class to allow for unit testing.
    /// </summary>
    public class StreamWriterWrapper : StreamWriter, IStreamWriterWrapper
    {
        public StreamWriterWrapper(string path, bool append) : base(path, append)
        {
        }
    }
}