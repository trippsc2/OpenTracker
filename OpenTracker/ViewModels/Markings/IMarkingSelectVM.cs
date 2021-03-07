using OpenTracker.Models.Markings;
using System.Collections.Generic;

namespace OpenTracker.ViewModels.Markings
{
    public interface IMarkingSelectVM
    {
        bool PopupOpen { get; set; }

        delegate IMarkingSelectVM Factory(
            IMarking marking, List<IMarkingSelectItemVMBase> buttons, double width, double height);
    }
}