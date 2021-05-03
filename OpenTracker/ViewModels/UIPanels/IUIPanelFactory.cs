namespace OpenTracker.ViewModels.UIPanels
{
    public interface IUIPanelFactory
    {
        /// <summary>
        /// Returns a new UI panel control of the specified type.
        /// </summary>
        /// <param name="type">
        /// The UI panel type.
        /// </param>
        /// <returns>
        /// A new UI panel control.
        /// </returns>
        IUIPanelVM GetUIPanelVM(UIPanelType type);
    }
}