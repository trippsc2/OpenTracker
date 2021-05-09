using System;
using System.IO;
using System.Threading.Tasks;

namespace OpenTracker.Models.Logging
{
    /// <summary>
    /// This interface wraps the <see cref="StreamWriter"/> class to allow for unit testing.
    /// </summary>
    public interface IStreamWriterWrapper : IAsyncDisposable, IDisposable
    {
        /// <inheritdoc cref="StreamWriter.WriteLine(string?)"/>
        void WriteLine(string? value);
        
        /// <inheritdoc cref="StreamWriter.WriteLineAsync(string?)"/>
        Task WriteLineAsync(string? value);

        /// <summary>
        /// A factory for creating new <see cref="IStreamWriterWrapper"/> objects.
        /// </summary>
        /// <param name="path">
        ///     A <see cref="string"/> representing the file path to which to write.
        /// </param>
        /// <param name="append">
        ///     A <see cref="bool"/> representing whether to append data to an existing file.
        /// </param>
        /// <returns>
        ///     A new <see cref="IStreamWriterWrapper"/> object.
        /// </returns>
        delegate IStreamWriterWrapper Factory(string path, bool append = false);
    }
}