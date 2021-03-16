using OpenTracker.Models.Markings;
using System.Collections.Generic;

namespace OpenTracker.ViewModels.Markings.Images
{
    public interface IMarkingImageDictionary : IDictionary<MarkType, IMarkingImageVMBase>
    {
    }
}
