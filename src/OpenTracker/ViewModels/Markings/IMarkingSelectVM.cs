using System.Collections.Generic;
using OpenTracker.Models.Markings;

namespace OpenTracker.ViewModels.Markings
{
    public interface IMarkingSelectVM
    {
        bool PopupOpen { get; set; }

        delegate IMarkingSelectVM Factory(
            IMarking marking, List<IMarkingSelectItemVMBase> buttons, double width, double height);
    }
}