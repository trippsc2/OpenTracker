using System;

namespace OpenTracker.Models.AccessibilityLevels
{
    /// <summary>
    /// This class contains methods for getting the maximum or minimum AccessibilityLevel.
    /// </summary>
    public static class AccessibilityLevelMethods
    {
        /// <summary>
        /// Returns the highest of AccessibilityLevel value provided.
        /// </summary>
        /// <param name="a1">
        /// An AccessibilityLevel value to be compared.
        /// </param>
        /// <param name="a2">
        /// An AccessibilityLevel value to be compared.
        /// </param>
        /// <returns>
        /// Returns the highest of AccessibilityLevel value provided.
        /// </returns>
        public static AccessibilityLevel Max(AccessibilityLevel a1, AccessibilityLevel a2)
        {
            return (AccessibilityLevel)Math.Max((int)a1, (int)a2);
        }

        /// <summary>
        /// Returns the highest of AccessibilityLevel value provided.
        /// </summary>
        /// <param name="a1">
        /// An AccessibilityLevel value to be compared.
        /// </param>
        /// <param name="a2">
        /// An AccessibilityLevel value to be compared.
        /// </param>
        /// <param name="a3">
        /// An AccessibilityLevel value to be compared.
        /// </param>
        /// <returns>
        /// Returns the highest of AccessibilityLevel value provided.
        /// </returns>
        public static AccessibilityLevel Max(AccessibilityLevel a1, AccessibilityLevel a2, AccessibilityLevel a3)
        {
            return Max(Max(a1, a2), a3);
        }

        /// <summary>
        /// Returns the lowest of AccessibilityLevel value provided.
        /// </summary>
        /// <param name="a1">
        /// An AccessibilityLevel value to be compared.
        /// </param>
        /// <param name="a2">
        /// An AccessibilityLevel value to be compared.
        /// </param>
        /// <returns>
        /// Returns the lowest of AccessibilityLevel value provided.
        /// </returns>
        public static AccessibilityLevel Min(AccessibilityLevel a1, AccessibilityLevel a2)
        {
            return (AccessibilityLevel)Math.Min((int)a1, (int)a2);
        }

        /// <summary>
        /// Returns the lowest of AccessibilityLevel value provided.
        /// </summary>
        /// <param name="a1">
        /// An AccessibilityLevel value to be compared.
        /// </param>
        /// <param name="a2">
        /// An AccessibilityLevel value to be compared.
        /// </param>
        /// <param name="a3">
        /// An AccessibilityLevel value to be compared.
        /// </param>
        /// <returns>
        /// Returns the lowest of AccessibilityLevel value provided.
        /// </returns>
        public static AccessibilityLevel Min(AccessibilityLevel a1, AccessibilityLevel a2, AccessibilityLevel a3)
        {
            return Min(Min(a1, a2), a3);
        }
    }
}
