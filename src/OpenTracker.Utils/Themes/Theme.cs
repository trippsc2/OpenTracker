using Avalonia.Styling;

namespace OpenTracker.Utils.Themes
{
    /// <summary>
    /// This class contains theme data.
    /// </summary>
    public class Theme : ITheme
    {
        public string Name { get; }
        public IStyle Style { get; }

        /// <summary>
        /// This class contains theme data.
        /// </summary>
        public Theme(string name, IStyle style)
        {
            Name = name;
            Style = style;
        }
    }
}