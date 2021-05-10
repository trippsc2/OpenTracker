using System.IO;

namespace OpenTracker.Models.Logging
{
    /// <summary>
    /// This class wraps the <see cref="StreamWriter"/> class to allow for unit testing.
    /// </summary>
    public class StreamWriterWrapper : StreamWriter, IStreamWriterWrapper
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path">
        ///     A <see cref="string"/> representing the file path.
        /// </param>
        /// <param name="append">
        ///     A <see cref="bool"/> representing whether the append the file.
        /// </param>
        public StreamWriterWrapper(string path, bool append) : base(path, append)
        {
        }
    }
}