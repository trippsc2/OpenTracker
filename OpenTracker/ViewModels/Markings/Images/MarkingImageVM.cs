using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.ViewModels.Markings.Images;

/// <summary>
/// This class contains the static marking image control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class MarkingImageVM : ViewModel, IMarkingImageVMBase
{
    public string ImageSource { get; }

    public delegate MarkingImageVM Factory(string imageSource);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="imageSource">
    /// A string representing the image source.
    /// </param>
    public MarkingImageVM(string imageSource)
    {
        ImageSource = imageSource;
    }
}