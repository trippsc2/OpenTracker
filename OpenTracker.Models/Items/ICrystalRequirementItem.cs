namespace OpenTracker.Models.Items
{
    /// <summary>
    ///     This interface contains crystal requirement data.
    /// </summary>
    public interface ICrystalRequirementItem : IItem
    {
        /// <summary>
        ///     A boolean representing whether the crystal requirement is known.
        /// </summary>
        bool Known { get; set; }
        
        /// <summary>
        ///     A factory for creating new crystal requirements.
        /// </summary>
        /// <returns>
        ///     A new crystal requirement.
        /// </returns>
        delegate ICrystalRequirementItem Factory();
    }
}