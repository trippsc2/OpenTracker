using OpenTracker.Models.Requirements;
using OpenTracker.ViewModels.Items;

namespace OpenTracker.ViewModels.Dungeons
{
    public interface IDungeonItemVM
    {
        delegate IDungeonItemVM Factory(IRequirement? requirement, IItemVM? item);
    }
}