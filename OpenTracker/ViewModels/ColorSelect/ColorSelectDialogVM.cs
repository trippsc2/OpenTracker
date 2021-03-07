using Avalonia;
using Avalonia.Media;
using Avalonia.Threading;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Settings;
using OpenTracker.Utils.Dialog;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace OpenTracker.ViewModels.ColorSelect
{
    /// <summary>
    /// This class contains the color select dialog window ViewModel data.
    /// </summary>
    public class ColorSelectDialogVM : DialogViewModelBase, IColorSelectDialogVM
    {
        public ObservableCollection<IColorSelectControlVM> FontColors { get; } =
            new ObservableCollection<IColorSelectControlVM>();
        public ObservableCollection<IColorSelectControlVM> AccessibilityColors { get; } =
            new ObservableCollection<IColorSelectControlVM>();
        public ObservableCollection<IColorSelectControlVM> ConnectorColors { get; } =
            new ObservableCollection<IColorSelectControlVM>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// An Autofac factory for creating color select controls.
        /// </param>
        public ColorSelectDialogVM(IColorSelectControlVM.Factory factory)
        {
            foreach (ColorType type in Enum.GetValues(typeof(ColorType)))
            {
                switch (type)
                {
                    case ColorType.EmphasisFont:
                        FontColors.Add(factory(type));
                        break;
                    case ColorType.AccessibilityNone:
                    case ColorType.AccessibilityPartial:
                    case ColorType.AccessibilityInspect:
                    case ColorType.AccessibilitySequenceBreak:
                    case ColorType.AccessibilityNormal:
                        AccessibilityColors.Add(factory(type));
                        break;
                    case ColorType.Connector:
                        ConnectorColors.Add(factory(type));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type));
                }
            }
        }
    }
}