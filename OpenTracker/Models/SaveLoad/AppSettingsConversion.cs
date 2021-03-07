using Avalonia.Controls;
using System;

namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This class contains conversion logic for the AppSettings save data between versions.
    /// </summary>
    public static class AppSettingsConversion
    {
        /// <summary>
        /// Converts the pre-1.3.2 save data to the accurate values.
        /// </summary>
        /// <param name="saveData">
        /// The save data to be converted.
        /// </param>
        public static void ConvertPre132(AppSettingsSaveData saveData)
        {
            _ = saveData ?? throw new NullReferenceException();

            saveData.HorizontalUIPanelPlacement = saveData.HorizontalUIPanelPlacement switch
            {
                Dock.Bottom => Dock.Top,
                _ => Dock.Bottom
            };

            saveData.VerticalUIPanelPlacement = saveData.VerticalUIPanelPlacement switch
            {
                Dock.Bottom => Dock.Left,
                _ => Dock.Right
            };

            saveData.HorizontalItemsPlacement = saveData.HorizontalItemsPlacement switch
            {
                Dock.Bottom => Dock.Left,
                _ => Dock.Right
            };

            saveData.VerticalItemsPlacement = saveData.VerticalItemsPlacement switch
            {
                Dock.Bottom => Dock.Top,
                _ => Dock.Bottom
            };
        }
    }
}
