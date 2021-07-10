namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This interface contains crystal requirement data.
    /// </summary>
    public interface ICrystalRequirementItem : IItem
    {
        /// <summary>
        /// A <see cref="bool"/> representing whether the crystal requirement is known.
        /// </summary>
        bool Known { get; set; }
        
        /// <summary>
        /// A factory for creating new <see cref="ICrystalRequirementItem"/> objects.
        /// </summary>
        /// <returns>
        ///     A new <see cref="ICrystalRequirementItem"/> object.
        /// </returns>
        new delegate ICrystalRequirementItem Factory();
    }
}