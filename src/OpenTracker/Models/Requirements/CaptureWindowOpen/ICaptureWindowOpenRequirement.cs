using OpenTracker.ViewModels.Capture;

namespace OpenTracker.Models.Requirements.CaptureWindowOpen
{
    /// <summary>
    ///     This interface contains capture window open requirement data.
    /// </summary>
    public interface ICaptureWindowOpenRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new capture window open requirements.
        /// </summary>
        /// <param name="captureWindow">
        ///     The capture window data.
        /// </param>
        /// <returns>
        ///     A new capture window open requirement.
        /// </returns>
        delegate ICaptureWindowOpenRequirement Factory(ICaptureWindowVM captureWindow);
    }
}