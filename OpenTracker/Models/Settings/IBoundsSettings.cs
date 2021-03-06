﻿namespace OpenTracker.Models.Settings
{
    /// <summary>
    /// This interface contains window bounds settings.
    /// </summary>
    public interface IBoundsSettings
    {
        double? Height { get; set; }
        bool? Maximized { get; set; }
        double? Width { get; set; }
        double? X { get; set; }
        double? Y { get; set; }
    }
}