using OpenTracker.Models.Items;
using OpenTracker.Models.Markings;
using System;

namespace OpenTracker.ViewModels.Markings.Images
{
    /// <summary>
    /// This is the class containing creation logic for marking image control ViewModel classes.
    /// </summary>
    public static class MarkingImageFactory
    {
        /// <summary>
        /// Returns a new marking image control ViewModel instance representing the specified mark
        /// type.
        /// </summary>
        /// <param name="type">
        /// The mark type to be represented.
        /// </param>
        /// <returns>
        /// A new marking image control ViewModel instance.
        /// </returns>
        public static MarkingImageVMBase GetMarkingImageVM(MarkType type)
        {
            switch (type)
            {
                case MarkType.Sword:
                case MarkType.Shield:
                case MarkType.Mail:
                case MarkType.Gloves:
                case MarkType.Bottle:
                    {
                        return new ItemMarkingImageVM(
                            ItemDictionary.Instance[Enum.Parse<ItemType>(type.ToString())],
                            $"avares://OpenTracker/Assets/Images/Marks/{type.ToString().ToLowerInvariant()}");
                    }
                default:
                    {
                        return new MarkingImageVM(
                            $"avares://OpenTracker/Assets/Images/Marks/{type.ToString().ToLowerInvariant()}.png");
                    }
            }
        }
    }
}
